using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using ArduinoUploader;
using ArduinoUploader.Hardware;

namespace STEP;

public class ArduinoCompiler
{
    /// <summary>
    /// The Upload method requires the avr-gcc compiler to be present at the host machine.
    /// It first compiles the STEP compiler's output to avr, then converts this output to Intel HEX format.
    /// The .hex file is then uploaded directly to the connected Arduino Uno board.
    /// </summary>
    public void Upload()
    {
        string directoryPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        // Check if avr-gcc files are present in correct folder
        if (!File.Exists($"{directoryPath}/avr-destination/bin/avr-gcc.exe") &&
            !File.Exists($"{directoryPath}/avr-destination/bin/avr-objcopy.exe"))
        {
            throw new ApplicationException(
                "Please download the avr8 toolchain and place the contents in the avr-destination folder");
        }

        try
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // The /C flag means that cmd should execute the following command and exit without waiting for further input.
                Process compiler = Process.Start("cmd.exe",
                    "/C " +
                    $"{directoryPath}/avr-destination/bin/avr-gcc.exe -O2 -Wall -mmcu=atmega328p " +
                    $"-I {directoryPath}/avr-destination/Arduino-Core/cores/arduino " +
                    $"-I {directoryPath}/avr-destination/Arduino-Core/variants/standard " +
                    $"{directoryPath}/compiled.c -o {directoryPath}/compiled.out");

                compiler?.WaitForExit();

                if (!File.Exists(directoryPath + "/compiled.out"))
                {
                    throw new ApplicationException("Arduino compiler error. compiled.out file was not generated.");
                }

                Process hexConverter = Process.Start("cmd.exe",
                    "/C " +
                    $"{directoryPath}/avr-destination/bin/avr-objcopy.exe -j .text -j .data -O ihex {directoryPath}/compiled.out {directoryPath}/compiled.hex");

                hexConverter?.WaitForExit();
            }
            else
            {
                Process compiler = Process.Start("/bin/bash",
                    $"{directoryPath}/avr-destination/bin/avr-gcc.exe -O2 -Wall -mmcu=atmega328p " +
                    $"-I {directoryPath}/avr-destination/Arduino-Core/cores/arduino " +
                    $"-I {directoryPath}/avr-destination/Arduino-Core/variants/standard " +
                    $"{directoryPath}/compiled.c -o {directoryPath}/compiled.out");

                compiler?.WaitForExit();

                if (!File.Exists(directoryPath + "/compiled.out"))
                {
                    throw new ApplicationException("Arduino compiler error. compiled.out file was not generated.");
                }

                Process hexConverter = Process.Start("/bin/bash",
                    $"{directoryPath}/avr-destination/bin/avr-objcopy.exe -O ihex {directoryPath}/compiled.out {directoryPath}/compiled.hex");

                hexConverter?.WaitForExit();
            }

            if (!File.Exists(directoryPath + "/compiled.hex"))
            {
                throw new ApplicationException("Arduino compiler error. compiled.hex file was not generated.");
            }

            ArduinoSketchUploader uploader = new ArduinoSketchUploader(new ArduinoSketchUploaderOptions()
            {
                FileName = directoryPath + "/compiled.hex",
                PortName = "COM3", // Can be omitted to try to auto-detect the COM port.
                ArduinoModel = ArduinoModel.UnoR3
            });
            uploader.UploadSketch();
        }
        catch (ApplicationException e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            // Clean up
            if (File.Exists(directoryPath + "/compiled.out"))
            {
                File.Delete(directoryPath + "/compiled.out");
            }

            if (File.Exists(directoryPath + "/compiled.hex"))
            {
                File.Delete(directoryPath + "/compiled.hex");
            }
        }
    }
}
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using ArduinoUploader;
using ArduinoUploader.Hardware;

namespace STEP;

public class ArduinoCompiler
{
    public void Upload()
    {
        string directoryPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            // The /C flag means that cmd should execute the following command and exit without waiting for further input.
            Process.Start("cmd.exe", "/C " + $"avr-gcc -O2 -Wall -mmcu=atmega328p {directoryPath}/compiled.c -o {directoryPath}/compiled.out");
            Process.Start("cmd.exe", "/C " + $"avr-objcopy -O ihex {directoryPath}/compiled.out {directoryPath}/.compiled.hex");
        }
        else
        {
            Process.Start("/bin/bash", $"avr-gcc -O2 -Wall -mmcu=atmega328p {directoryPath}/compiled.c -o {directoryPath}/compiled.out");
            Process.Start("/bin/bash", $"avr-objcopy -O ihex {directoryPath}/compiled.out {directoryPath}/.compiled.hex");
        }

        ArduinoSketchUploader uploader = new ArduinoSketchUploader(new ArduinoSketchUploaderOptions()
        {
            FileName = directoryPath + @"/.compiled.hex", 
            //PortName = "COM3", // Can be omitted to try to auto-detect the COM port.
            ArduinoModel = ArduinoModel.UnoR3
        });
        uploader.UploadSketch();
        
        // Clean up
        if (File.Exists(directoryPath + @"/compiled.out"))
        {
            File.Delete(directoryPath + @"/compiled.out");
        }
        if (File.Exists(directoryPath + @"/.compiled.hex"))
        {
            File.Delete(directoryPath + @"/.compiled.hex");
        }
    }
}
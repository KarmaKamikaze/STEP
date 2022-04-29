using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using ArduinoUploader;
using ArduinoUploader.Hardware;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace STEP;

public class ArduinoCompiler
{
    /// <summary>
    /// The Upload method requires the avr-gcc compiler to be present at the host machine.
    /// It first compiles the STEP compiler's output to avr, then converts this output to Intel HEX format.
    /// The .hex file is then uploaded directly to the connected Arduino Uno board.
    /// </summary>
    public void Upload(string filename)
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
                    $"{directoryPath}/{filename}.c -o {directoryPath}/{filename}.out");

                compiler?.WaitForExit();

                if (!File.Exists(directoryPath + $"/{filename}.out"))
                {
                    throw new ApplicationException($"Arduino compiler error. {filename}.out file was not generated.");
                }

                Process hexConverter = Process.Start("cmd.exe",
                    "/C " +
                    $"{directoryPath}/avr-destination/bin/avr-objcopy.exe -j .text -j .data -O ihex " +
                    $"{directoryPath}/{filename}.out {directoryPath}/{filename}.hex");

                hexConverter?.WaitForExit();
            }
            else
            {
                Process compiler = Process.Start("/bin/bash",
                    $"avr-gcc -O2 -Wall -mmcu=atmega328p " +
                    $"-I {directoryPath}/avr-destination/Arduino-Core/cores/arduino " +
                    $"-I {directoryPath}/avr-destination/Arduino-Core/variants/standard " +
                    $"{directoryPath}/{filename}.c -o {directoryPath}/{filename}.out");

                compiler?.WaitForExit();

                if (!File.Exists(directoryPath + $"/{filename}.out"))
                {
                    throw new ApplicationException($"Arduino compiler error. {filename}.out file was not generated.");
                }

                Process hexConverter = Process.Start("/bin/bash",
                    $"avr-objcopy -O ihex {directoryPath}/{filename}.out {directoryPath}/{filename}.hex");

                hexConverter?.WaitForExit();
            }

            if (!File.Exists(directoryPath + $"/{filename}.hex"))
            {
                throw new ApplicationException($"Arduino compiler error. {filename}.hex file was not generated.");
            }

            // Enable upload logging
            var nlogConfig = new LoggingConfiguration();

            nlogConfig.AddRule(minLevel: LogLevel.Trace, maxLevel: LogLevel.Fatal,
                target: new ConsoleTarget("consoleTarget")
                {
                    Layout = "${longdate} level=${level} message=${message}"
                });

            LogManager.Configuration = nlogConfig;

            // Update compiled hex file to Arduino board
            ArduinoSketchUploader uploader = new ArduinoSketchUploader(new ArduinoSketchUploaderOptions()
            {
                FileName = directoryPath + $"/{filename}.hex",
                PortName = "COM3", // Can be omitted to try to auto-detect the COM port.
                ArduinoModel = ArduinoModel.UnoR3
            }, new NLogArduinoUploaderLogger(error: true, warn: true, info: true));
            uploader.UploadSketch();
        }
        catch (ApplicationException e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            // Clean up
            if (File.Exists(directoryPath + $"/{filename}.out"))
            {
                File.Delete(directoryPath + $"/{filename}.out");
            }

            if (File.Exists(directoryPath + $"/{filename}.hex"))
            {
                File.Delete(directoryPath + $"/{filename}.hex");
            }
        }
    }

    private class NLogArduinoUploaderLogger : IArduinoUploaderLogger
    {
        private readonly bool _error;
        private readonly bool _warn;
        private readonly bool _info;
        private readonly bool _debug;
        private readonly bool _trace;

        public NLogArduinoUploaderLogger(bool error = false, bool warn = false, bool info = false, bool debug = false,
            bool trace = false)
        {
            _error = error;
            _warn = warn;
            _info = info;
            _debug = debug;
            _trace = trace;
        }

        private static readonly Logger Logger = LogManager.GetLogger("ArduinoSketchUploader");

        public void Error(string message, Exception exception)
        {
            if (_error) Logger.Error(exception, message);
        }

        public void Warn(string message)
        {
            if (_warn) Logger.Warn(message);
        }

        public void Info(string message)
        {
            if (_info) Logger.Info(message);
        }

        public void Debug(string message)
        {
            if (_debug) Logger.Debug(message);
        }

        public void Trace(string message)
        {
            if (_trace) Logger.Trace(message);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain
{
    public class HelperCmd
    {

        public static bool ExecuteCommand(string filename, string command, int? millisecondsWaitForExit = 5000)
        {
            var _command = string.Format("{0} {1}", filename, command);
            return ExecuteCommand(_command, millisecondsWaitForExit);
        }

        public static bool ExecuteCommand(string command, int? millisecondsWaitForExit = null)
        {
            var result = true;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Executing command: [ {0} ] ...", command);

            var processInfo = new ProcessStartInfo("cmd.exe", "/c " + command);

            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardError = true;
            processInfo.RedirectStandardOutput = true;

            var process = Process.Start(processInfo);
            if (millisecondsWaitForExit.IsNotNull())
                process.WaitForExit(millisecondsWaitForExit.Value);
            else
            {
                process.WaitForExit();
            }

            var output = process.StandardOutput.ReadToEnd();
            var error = process.StandardError.ReadToEnd();
            var exitCode = process.ExitCode;

            if (exitCode == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Command: [ {0} ] executed success!", command);
                result = true;
            }
            else
            {

                Console.WriteLine("ExitCode: {0} ", exitCode.ToString());

                if (!String.IsNullOrEmpty(output))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("output: {0}", output);
                }
                if (!String.IsNullOrEmpty(error))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("error: {0}", error);
                }
                result = true;
            }

            PrinstScn.WriteLine("");
            System.Threading.Thread.Sleep(3000);
            process.Close();
            return result;

        }

        public static void ExecuteApp(string fileName, string arguments, ProcessWindowStyle windowsStyle = ProcessWindowStyle.Hidden)
        {
            Console.ForegroundColor = ConsoleColor.Blue;

            var command = string.Format("{0} {1}", fileName, arguments);
            Console.WriteLine("Executing command: [ {0} ] ...", command);

            var startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = windowsStyle;
            startInfo.FileName = fileName;
            startInfo.Arguments = arguments;
            var process = Process.Start(startInfo);
            process.WaitForExit();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Command: [ {0} ] executed success!", command);

        }

    }
}

using System;
using System.Diagnostics;

namespace Common.Domain
{
    public static class HelperShell
    {
        public static bool Bash(this string command, int? millisecondsWaitForExit = null)
        {
            var result = true;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Executing command: [ {0} ] ...", command);

            var escapedArgs = command.Replace("\"", "\\\"");

            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{escapedArgs}\"",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true
                }
            };

            process.Start();

            if (millisecondsWaitForExit.IsNotNull())
                process.WaitForExit(millisecondsWaitForExit.Value);
            else
            {
                process.WaitForExit();
            }

            string output = process.StandardOutput.ReadToEnd();
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
    }
}

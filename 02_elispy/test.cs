using System;
using System.Diagnostics;
using System.Collections.Generic;

class Program {

    public static void Main() {
        bool t = true;
        bool nil = false;

        dynamic res = 1;
        dynamic n = 5;
        while (n > 0) { res = res * n;
n = n - 1; };
        Console.WriteLine(res);
        shell_exec("ls");
    }

    public static string shell_exec(string cmd) {
        var escaped_args = cmd.Replace("\"", "\\\"");
        var process = new Process() {
            StartInfo = new ProcessStartInfo {
                FileName = "/bin/bash",
                Arguments = $"-c \"{escaped_args}\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };

        process.Start();
        string result = process.StandardOutput.ReadToEnd();
        process.WaitForExit();

        if (process.ExitCode != 0)
            throw new InvalidOperationException("Process exited.");
        return result;
    }
}
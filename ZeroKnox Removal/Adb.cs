using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroKnox_Removal
{
    public class Adb
    {
        public static string AdbCommand(string cmd)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                UseShellExecute = false,
                CreateNoWindow = true,
                FileName = "ref//adb.exe",
                Arguments = cmd,
                RedirectStandardOutput = true
            };
            process.StartInfo = startInfo;
            Process process2 = process;
            process2.Start();
            return process2.StandardOutput.ReadToEnd();
        }
        public static string cmdCommand(string cmd)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                UseShellExecute = false,
                CreateNoWindow = true,
                FileName = "ref//cmd.exe",
                Arguments = cmd,
                RedirectStandardOutput = true
            };
            process.StartInfo = startInfo;
            Process process2 = process;
            process2.Start();
            return process2.StandardOutput.ReadToEnd();
        }

        public static bool Adbfound()
        {
            return Adb.AdbCommand("get-state").Contains("device");
        }


        public static string Miadb(string cmd)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                UseShellExecute = false,
                CreateNoWindow = true,
                FileName = "ref//miadb.exe",
                Arguments = cmd,
                RedirectStandardOutput = true
            };
            process.StartInfo = startInfo;
            Process process2 = process;
            process2.Start();
            return process2.StandardOutput.ReadToEnd();
        }
    }
}

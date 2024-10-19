using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZeroKnox_Removal
{
    internal partial class Processcommand : Form
    {
        public void SendCmd(string arguments)
        {
            Process process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.FileName = "ref//cmd.exe";
            process.StartInfo.Arguments = arguments;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardInput = true;
            process.Start();
            process.StandardOutput.ReadToEnd();
            process.StandardError.ReadToEnd();
        }

        public static Form form1;
        private void Processcommand_Load(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Form1.Properties;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Runtime.ConstrainedExecution;
using System.Web.UI.WebControls;
using static System.Windows.Forms.AxHost;
using Renci.SshNet;
using System.Data;
using System.Text;
using LibUsbDotNet.DeviceNotify;
using Newtonsoft.Json.Linq;
using System.Collections;
using static System.Net.Mime.MediaTypeNames;
using Application = System.Windows.Forms.Application;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Web.UI.WebControls.WebParts;
using System.Reflection.Emit;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using RegawMOD.Android;

namespace ZeroKnox_Removal
{

    public partial class zeroknox : Form
    {
        public zeroknox()
        {
            InitializeComponent();
            this.LoadPorts();
            comboBox1.Enabled = false;
        }

        private void R_Click(object sender, EventArgs e)
        {
            this.LoadPorts();
        }

        private void ADBENABLE_Click(object sender, EventArgs e)
        {
            this.LoadPorts();
            this.disableall();
            bool flag = MessageBox.Show("Go to emergency dialer enter *#0*#, click ok when done", "ZeroSec", MessageBoxButtons.YesNo) == DialogResult.Yes;
            if (flag)
            {
                zeroknox.ComPort comPort = this.cbDevices.SelectedItem as zeroknox.ComPort;
                bool flag2 = comPort != null;
                if (flag2)
                {

                    this.ResultTxt.AppendText("OPERATION Enabling ADB...");
                    zeroknox._serialPort = new SerialPort
                    {
                        PortName = comPort.Name
                    };
                    try
                    {
                        zeroknox._serialPort.Open();
                        bool isOpen = zeroknox._serialPort.IsOpen;
                        if (isOpen)
                        {
                            this.ResultTxt.AppendText("\r\nSending command data to Device...");
                            this.ResultTxt.AppendText("OK");
                            zeroknox._serialPort.Write("AT+KSTRINGB=0,3\r\n");
                            progressBar1.Value = 20;
                            Thread.Sleep(1000);

                            this.ResultTxt.AppendText("\r\nSending Request to Zero ...");
                            this.ResultTxt.AppendText("Accepted!");
                            progressBar1.Value = 40;
                            zeroknox._serialPort.Write("AT+DUMPCTRL=1,0\r\n");
                            Thread.Sleep(1000);
                            this.ResultTxt.AppendText("\r\nRequest Zero Unlock Key...");
                            this.ResultTxt.AppendText("Accepted!");
                            progressBar1.Value = 60;
                            zeroknox._serialPort.Write("AT+DEBUGLVC=0,5\r\n");
                            Thread.Sleep(1000);
                            this.ResultTxt.AppendText("\r\nEnabling Adb ...");
                            zeroknox._serialPort.Write("AT+SWATD=0\r\n");
                            Thread.Sleep(1000);
                            zeroknox._serialPort.Write("AT+ACTIVATE=0,0,0\r\n");
                            progressBar1.Value = 70;
                            Thread.Sleep(1000);
                            zeroknox._serialPort.Write("AT+SWATD=1\r\n");
                            progressBar1.Value = 80;
                            Thread.Sleep(1000);
                            zeroknox._serialPort.Write("AT+PARALLEL=2,0,00000;AT+DEBUGLVC=0,5\r\n");
                            progressBar1.Value = 90;
                            Thread.Sleep(1000);
                            zeroknox._serialPort.Write("AT+PARALLEL=2,0,00000\r\n");
                            progressBar1.Value = 100;
                        }
                        else
                        {
                            this.ResultTxt.AppendText("Failed to open port, check port try again");
                        }
                    }
                    catch
                    {
                    }
                    finally
                    {
                        bool isOpen2 = zeroknox._serialPort.IsOpen;
                        if (isOpen2)
                        {
                            zeroknox._serialPort.Close();
                        }
                    }
                }
            }
            else
            {
                this.ResultTxt.AppendText("First teach\n");
            }
            this.enableall();
            this.ResultTxt.AppendText("Done");
        }

        private void cbDevices_DropDown(object sender, EventArgs e)
        {
            this.LoadPorts();
        }
        private void getdeviceinfoadb()
        {
            string text = this.my_adb("adb.exe shell getprop ro.product.model");
            string text2 = this.my_adb("adb.exe shell getprop ro.build.version.release");
            string text3 = this.my_adb("adb.exe shell getprop ro.build.version.sdk");
            string text4 = this.my_adb("adb.exe shell getprop ro.boot.bootloader");
            string text5 = this.my_adb("adb.exe shell getprop gsm.version.baseband");
            string text6 = this.my_adb("adb.exe shell getprop ril.official_cscver");
            string text7 = this.my_adb("adb.exe shell getprop ro.build.display.id");
            string text8 = this.my_adb("adb.exe shell getprop ro.serialno");
            string text9 = this.my_adb("adb.exe shell getprop ro.build.version.security_patch");
            string text10 = this.my_adb("adb.exe shell getprop ro.vendor.boot.warranty_bit");
            string text11 = this.my_adb("adb.exe shell getprop ro.csc.sales_code");
            string text12 = this.my_adb("adb.exe shell getprop ro.csc.country_code");
            string text13 = this.my_adb("adb.exe shell getprop ro.carrier");
            string text14 = this.my_adb("adb.exe shell getprop knox.kg.state");
            this.ResultTxt.AppendText("\nCheck Device Connecting..." + this.vbNewLine);
            this.my_adb("adb.exe wait-for-device");
            this.ResultTxt.SelectionColor = Color.Green;
            this.ResultTxt.AppendText("OK" + this.vbNewLine);
            this.ResultTxt.SelectionColor = Color.Black;
            this.ResultTxt.AppendText("\r\nModel :  ");
            this.ResultTxt.SelectionColor = Color.Green;
            this.ResultTxt.AppendText(text);
            this.ResultTxt.SelectionColor = Color.Black;
            this.ResultTxt.AppendText("Android :  ");
            this.ResultTxt.SelectionColor = Color.Green;
            this.ResultTxt.AppendText(text2);
            this.ResultTxt.SelectionColor = Color.Black;
            this.ResultTxt.AppendText("Sdk Verison :  ");
            this.ResultTxt.SelectionColor = Color.Green;
            this.ResultTxt.AppendText(text3);
            this.ResultTxt.SelectionColor = Color.Black;
            this.ResultTxt.AppendText("BL :  ");
            this.ResultTxt.SelectionColor = Color.Green;
            this.ResultTxt.AppendText(text4);
            this.ResultTxt.SelectionColor = Color.Black;
            this.ResultTxt.AppendText("CP :  ");
            this.ResultTxt.SelectionColor = Color.Green;
            this.ResultTxt.AppendText(text5);
            this.ResultTxt.SelectionColor = Color.Black;
            this.ResultTxt.AppendText("CSC :  ");
            this.ResultTxt.SelectionColor = Color.Green;
            this.ResultTxt.AppendText(text6);
            this.ResultTxt.SelectionColor = Color.Black;
            this.ResultTxt.AppendText("Build number:  ");
            this.ResultTxt.SelectionColor = Color.Green;
            this.ResultTxt.AppendText(text7);
            this.ResultTxt.SelectionColor = Color.Black;
            this.ResultTxt.AppendText("Serial Number :  ");
            this.ResultTxt.SelectionColor = Color.Green;
            this.ResultTxt.AppendText(text8);
            this.ResultTxt.SelectionColor = Color.Black;
            this.ResultTxt.AppendText("Security Level :  ");
            this.ResultTxt.SelectionColor = Color.Green;
            this.ResultTxt.AppendText(text9);
            this.ResultTxt.SelectionColor = Color.Black;
            this.ResultTxt.AppendText("Warranty void :  ");
            this.ResultTxt.SelectionColor = Color.Green;
            this.ResultTxt.AppendText(text10);
            this.ResultTxt.SelectionColor = Color.Black;
            this.ResultTxt.AppendText("CSC :  ");
            this.ResultTxt.SelectionColor = Color.Green;
            this.ResultTxt.AppendText(text11);
            this.ResultTxt.SelectionColor = Color.Black;
            this.ResultTxt.AppendText("Country :  ");
            this.ResultTxt.SelectionColor = Color.Green;
            this.ResultTxt.AppendText(text12);
            this.ResultTxt.SelectionColor = Color.Black;
            this.ResultTxt.AppendText("Carrier :  ");
            this.ResultTxt.SelectionColor = Color.Green;
            this.ResultTxt.AppendText(text13);
            this.ResultTxt.SelectionColor = Color.Black;
            this.ResultTxt.AppendText("KG State :  ");
            this.ResultTxt.SelectionColor = Color.Red;
            this.ResultTxt.AppendText(text14);
            this.ResultTxt.SelectionColor = Color.Black;
        }
        private void LoadPorts()
        {
            this.cbDevices.DisplayMember = "DisplayName";
            List<zeroknox.ComPort> dataSource = this.GetSerialPorts().FindAll((zeroknox.ComPort c) => c.Vid.Equals("04E8") && c.Pid.Equals("6860"));
            this.cbDevices.DataSource = dataSource;
        }

        private List<zeroknox.ComPort> GetSerialPorts()
        {
            List<zeroknox.ComPort> result;
            using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM WIN32_SerialPort"))
            {
                List<ManagementBaseObject> source = managementObjectSearcher.Get().Cast<ManagementBaseObject>().ToList<ManagementBaseObject>();
                result = source.Select(delegate (ManagementBaseObject p)
                {
                    zeroknox.ComPort comPort = new zeroknox.ComPort
                    {
                        Name = p.GetPropertyValue("DeviceID").ToString(),
                        Vid = p.GetPropertyValue("PNPDeviceID").ToString(),
                        Description = p.GetPropertyValue("Caption").ToString()
                    };
                    Match match = Regex.Match(comPort.Vid, "VID_([0-9A-F]{4})", RegexOptions.IgnoreCase);
                    Match match2 = Regex.Match(comPort.Vid, "PID_([0-9A-F]{4})", RegexOptions.IgnoreCase);
                    bool success = match.Success;
                    if (success)
                    {
                        comPort.Vid = match.Groups[1].Value;
                    }
                    bool success2 = match2.Success;
                    if (success2)
                    {
                        comPort.Pid = match2.Groups[1].Value;
                    }
                    return comPort;
                }).ToList<zeroknox.ComPort>();
            }
            return result;
        }

        private static SerialPort _serialPort;


        private const string vidPattern = "VID_([0-9A-F]{4})";


        private const string pidPattern = "PID_([0-9A-F]{4})";

        public static string Path { get; private set; }

        public class ComPort
        {

            public string Description { get; set; }


            public string DisplayName
            {
                get
                {
                    return this.Description + " (" + this.Name + ")";
                }
            }


            public string Name { get; set; }


            public string Pid { get; set; }


            public string Vid { get; set; }
        }

        private void frpresetadb_Click(object sender, EventArgs e)
        {
            ResultTxt.Text = ("Unlocking Frp...\n");
            this.disableall();
            this.getdeviceinfoadb();
            unlocksamsungfrp.RunWorkerAsync();

        }
        private void disableall()
        {
            ADBENABLE.Enabled = false;
            samsungverizonfixtestmode.Enabled = false;
            factoryresetmtp.Enabled = false;
            rebootdownloadmodemtp.Enabled = false;
            frpresetadb.Enabled = false;
            knoxbypass.Enabled = false;
            ADBENABLEoldmethod.Enabled = false;
            ADBENABLEsecondmethod.Enabled = false;
            btn_kgoneclick.Enabled = false;
            kgremove4.Enabled = false;
            button1.Enabled = false;
            kgremove.Enabled = false;
            kgremove2.Enabled = false;
            bypasskjlockedwoutlock.Enabled = false;
            disableupdatesam.Enabled = false;
            hideiconmicrophone.Enabled = false;
            Debloatandroid.Enabled = false;
            optimisephone.Enabled = false;
            ActivateMDMButton.Enabled = false;
            //btnreadinfosamsungadb.Enabled = false;
        }
        private void enableall()
        {
            samsungverizonfixtestmode.Enabled = true;
            ADBENABLE.Enabled = true;
            factoryresetmtp.Enabled = true;
            rebootdownloadmodemtp.Enabled = true;
            frpresetadb.Enabled = true;
            knoxbypass.Enabled = true;
            ADBENABLEoldmethod.Enabled = true;
            ADBENABLEsecondmethod.Enabled = true;
            btn_kgoneclick.Enabled = true;
            kgremove4.Enabled = true;
            button1.Enabled = true;
            kgremove.Enabled = true;
            kgremove2.Enabled = true;
            bypasskjlockedwoutlock.Enabled = true;
            disableupdatesam.Enabled = true;
            hideiconmicrophone.Enabled = true;
            Debloatandroid.Enabled = true;
            optimisephone.Enabled = true;
            ActivateMDMButton.Enabled = true;
            //btnreadinfosamsungadb.Enabled = true;
        }

        private void factoryresetmtp_Click(object sender, EventArgs e)
        {
            this.LoadPorts();

            bool flag = MessageBox.Show("Are you want do Factory Reset ???", "ZeroSec", MessageBoxButtons.OKCancel) == DialogResult.OK;
            if (flag)
            {
                zeroknox.ComPort comPort = this.cbDevices.SelectedItem as zeroknox.ComPort;
                bool flag2 = comPort != null;
                if (flag2)
                {

                    ResultTxt.Text = ("\t\nOPERATION\t:Factory Reset...\n");
                    zeroknox._serialPort = new SerialPort
                    {
                        PortName = comPort.Name
                    };
                    try
                    {
                        zeroknox._serialPort.Open();
                        bool isOpen = zeroknox._serialPort.IsOpen;
                        if (isOpen)
                        {
                            ResultTxt.Text = ("Factory Reset...\r\n");
                            ResultTxt.Text = ("OK \r\n");
                            zeroknox._serialPort.Write("AT+FACTORST=0,0\r\n");
                            Thread.Sleep(1000);
                            zeroknox._serialPort.Write("AT+FACTORST=0,0\r\r\n");
                            Thread.Sleep(1000);
                            zeroknox._serialPort.Write("AT+CRST=FS\r\n");
                            Thread.Sleep(1000);
                            zeroknox._serialPort.Write("AT%FRST\r\n");
                            Thread.Sleep(1000);
                            zeroknox._serialPort.Write("AT^RESET\r\n");
                            Thread.Sleep(1000);
                            Thread.Sleep(1000);
                            ResultTxt.Text = ("Done!\r\n");
                        }
                        else
                        {
                            ResultTxt.Text = ("Failed to open port, check port try again\n");
                        }
                    }
                    catch
                    {
                    }
                    finally
                    {
                        bool isOpen2 = zeroknox._serialPort.IsOpen;
                        if (isOpen2)
                        {
                            zeroknox._serialPort.Close();
                        }
                    }
                }
            }
            else
            {
                ResultTxt.Text = ("Good by\n");
            }
        }

        private void rebootdownloadmodemtp_Click(object sender, EventArgs e)
        {
            this.LoadPorts();

            bool flag = MessageBox.Show("Are you want do Reboot to Download mode ???", "ZeroSec", MessageBoxButtons.OKCancel) == DialogResult.OK;
            if (flag)
            {
                zeroknox.ComPort comPort = this.cbDevices.SelectedItem as zeroknox.ComPort;
                bool flag2 = comPort != null;
                if (flag2)
                {

                    ResultTxt.Text = ("\t\nReboot\t:Download mode...\n");
                    zeroknox._serialPort = new SerialPort
                    {
                        PortName = comPort.Name
                    };
                    try
                    {
                        zeroknox._serialPort.Open();
                        bool isOpen = zeroknox._serialPort.IsOpen;
                        if (isOpen)
                        {
                            ResultTxt.Text = ("...\t");
                            zeroknox._serialPort.Write("AT+SUDDLMOD=0,0\r\n");
                        }
                        else
                        {
                            ResultTxt.Text = ("Failed to open port, check port try again\n");
                        }
                    }
                    catch
                    {
                    }
                    finally
                    {
                        bool isOpen2 = zeroknox._serialPort.IsOpen;
                        if (isOpen2)
                        {
                            zeroknox._serialPort.Close();
                        }
                    }
                }
            }
            else
            {
                ResultTxt.Text = ("Good by\n");
            }
        }

        private void unlocksamsungfrp_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                //Check if there is a request to cancel the process
                if (unlocksamsungfrp.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

            }
            bool flag = !Adb.Adbfound();
            if (flag)
            {

            }
            else
            {

                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 10;
                }));
                Adb.AdbCommand("shell input keyevent 4");
                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 20;
                }));
                Adb.AdbCommand("shell input keyevent 4");
                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 40;
                }));
                Adb.AdbCommand("shell content insert --uri content://settings/secure --bind name:s:user_setup_complete --bind value:s:1");
                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 60;
                }));
                Adb.AdbCommand("shell pm clear --user 0 com.sec.android.app.SecSetupWizard");
                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 70;
                }));
                Adb.AdbCommand("shell pm clear --user 0 com.sec.android.app.setupwizardlegalprovidershell");
                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 80;
                }));
                Adb.AdbCommand("shell pm clear --user 0 com.google.android.setupwizard");
                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 90;
                }));
                Adb.AdbCommand("reboot");
                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 100;
                }));
            }
            base.Invoke(new MethodInvoker(delegate ()
            {
                this.enableall();
                this.ResultTxt.SelectionColor = Color.Black;
                this.ResultTxt.AppendText("\r\nReset FRP---------->: " + this.vbNewLine);
                this.ResultTxt.SelectionColor = Color.Green;
                this.ResultTxt.AppendText("DONE" + this.vbNewLine);
                this.elapsedTime();
            }));
        }

        private void samsungknox_DoWork(object sender, DoWorkEventArgs e)
        {
            bool flag = !Adb.Adbfound();
            if (flag)
            {
            }
            else
            {
                Adb.AdbCommand("shell pm uninstall -k --user 0 com.sec.knox.bridge");
                Thread.Sleep(250);
                Adb.AdbCommand("shell pm uninstall -k --user 0 com.sec.knox.switchknoxll");
                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 10;

                }));
                Thread.Sleep(250);
                Adb.AdbCommand("shell pm uninstall -k --user 0 com.sec.enterprise.knox.cloudmdm.smdms");
                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 20;

                }));
                Thread.Sleep(250);
                Adb.AdbCommand("shell pm uninstall --user 0 com.samsung.android.knox.pushmanager");
                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 30;

                }));
                Thread.Sleep(250);
                Adb.AdbCommand("shell pm uninstall -k --user 0 com.sec.knox.switcher");
                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 40;

                }));
                Thread.Sleep(250);
                Adb.AdbCommand("shell pm uninstall -k --user 0 com.samsung.android.mdm");
                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 50;

                }));
                Thread.Sleep(250);
                Adb.AdbCommand("shell pm uninstall -k --user 0 com.samsung.android.securityResultTxtagent");
                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 60;

                }));
                Thread.Sleep(250);
                Adb.AdbCommand("shell am force-stop com.sec.knox.knoxsetupwizardclient");
                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 70;

                }));
                Thread.Sleep(250);
                Adb.AdbCommand("shell pm uninstall --user 0 com.sec.enterprise.mdm.services.simpin");
                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 80;

                }));
                Thread.Sleep(250);
                Adb.AdbCommand("shell pm uninstall --user 0 com.samsung.android.mdm");
                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 90;

                }));
                Thread.Sleep(250);
                Adb.AdbCommand("shell pm uninstall --user 0 com.samsung.knox.securefolder");
                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 100;

                }));
                Thread.Sleep(250);
                Adb.AdbCommand("reboot");
            }
            Thread.Sleep(1000);
            base.Invoke(new MethodInvoker(delegate ()
            {
                this.enableall();
                ResultTxt.Text = "Done";
            }));
        }

        private void knoxbypass_Click(object sender, EventArgs e)
        {
            MessageBox.Show("REMEMBER!!! After factory reset, bypass will deleted!");
            this.getdeviceinfoadb();
            this.disableall();
            Thread.Sleep(1000);
            ResultTxt.Text = "In Progress...";
            samsungknox.RunWorkerAsync();

        }








        public void status(string message)
        {

        }


        private void pictureBox3_Click(object sender, EventArgs e)
        {

            System.Diagnostics.Process.Start("https://t.me/zerosecurity0");
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.youtube.com/channel/UCqL181dQd-qiBD3ULmSblzQ");
        }

        private void kg2method_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }



        private void ADBENABLEoldmethod_Click(object sender, EventArgs e)
        {
            this.LoadPorts();
            this.disableall();
            bool flag = MessageBox.Show("Is phone connected to Wifi???", "ZeroSec", MessageBoxButtons.YesNo) == DialogResult.Yes;
            if (flag)
            {
                zeroknox.ComPort comPort = this.cbDevices.SelectedItem as zeroknox.ComPort;
                bool flag2 = comPort != null;
                if (flag2)
                {

                    ResultTxt.Text = ("OPERATION Enabling ADB...");
                    zeroknox._serialPort = new SerialPort
                    {
                        PortName = comPort.Name
                    };
                    try
                    {
                        zeroknox._serialPort.Open();
                        bool isOpen = zeroknox._serialPort.IsOpen;
                        if (isOpen)
                        {
                            ResultTxt.Text = ("Sending command data to Device...");
                            ResultTxt.Text = ("OK \r\n");
                            zeroknox._serialPort.Write("AT+DUMPCTRL=1,0\r\n\r\n");
                            Thread.Sleep(1000);
                            MessageBox.Show("Go to emergency dialer enter *#0*#, click ok when done");
                            ResultTxt.Text = ("Sending Request to Zero ...\t");
                            ResultTxt.Text = ("Accepted!\r\n");
                            zeroknox._serialPort.Write("AT+DUMPCTRL=1,0\r\n");
                            Thread.Sleep(1000);
                            ResultTxt.Text = ("Request Zero Unlock Key...\t");
                            ResultTxt.Text = ("Accepted!\r\n");
                            zeroknox._serialPort.Write("AT+DEBUGLVC=0,5\r\n");
                            Thread.Sleep(1000);
                            ResultTxt.Text = ("Enabling Adb ...\t");
                            zeroknox._serialPort.Write("AT+SWATD=0\r\n");
                            Thread.Sleep(1000);
                            zeroknox._serialPort.Write("AT+ACTIVATE=0,0,0\r\n");
                            Thread.Sleep(1000);
                            zeroknox._serialPort.Write("AT+DEBUGLVC=0,5\r\n");
                        }
                        else
                        {
                            ResultTxt.Text = ("Failed to open port, check port try again");
                        }
                    }
                    catch
                    {
                    }
                    finally
                    {
                        bool isOpen2 = zeroknox._serialPort.IsOpen;
                        if (isOpen2)
                        {
                            zeroknox._serialPort.Close();
                        }
                    }
                }
            }
            else
            {
                ResultTxt.Text = ("First teach\n");
            }
            this.enableall();
            ResultTxt.Text = ("Done");
        }

        private void ADBENABLEsecondmethod_Click(object sender, EventArgs e)
        {
            this.LoadPorts();
            this.disableall();
            bool flag = MessageBox.Show("Is phone connected to Wifi???", "ZeroSec", MessageBoxButtons.YesNo) == DialogResult.Yes;
            if (flag)
            {
                zeroknox.ComPort comPort = this.cbDevices.SelectedItem as zeroknox.ComPort;
                bool flag2 = comPort != null;
                if (flag2)
                {

                    ResultTxt.Text = ("OPERATION Enabling ADB...");
                    zeroknox._serialPort = new SerialPort
                    {
                        PortName = comPort.Name
                    };
                    try
                    {
                        zeroknox._serialPort.Open();
                        bool isOpen = zeroknox._serialPort.IsOpen;
                        if (isOpen)
                        {
                            ResultTxt.Text = ("Sending command data to Device...");
                            ResultTxt.Text = ("OK \r\n");
                            zeroknox._serialPort.Write("AT+SYSSCOPE=1,0\r\n\r\n");
                            Thread.Sleep(1000);
                            MessageBox.Show("Go to emergency dialer enter *#0*#, click ok when done");
                            ResultTxt.Text = ("Sending Request to Zero ...\t");
                            ResultTxt.Text = ("Accepted!\r\n");
                            zeroknox._serialPort.Write("AT+KSTRINGB=0,3\r\n");
                            Thread.Sleep(1000);
                            ResultTxt.Text = ("Request Zero Unlock Key...\t");
                            ResultTxt.Text = ("Accepted!\r\n");
                            zeroknox._serialPort.Write("AT+DUMPCTRL=1,0\r\n");
                            Thread.Sleep(1000);
                            ResultTxt.Text = ("Enabling Adb ...\t");
                            zeroknox._serialPort.Write("AT+SWATD=0\r\n");
                            Thread.Sleep(1000);
                            zeroknox._serialPort.Write("AT+ACTIVATE=0,0,0\r\n");
                            Thread.Sleep(1000);
                            zeroknox._serialPort.Write("AT+SWATD=1\r\n");
                            Thread.Sleep(1000);
                            zeroknox._serialPort.Write("AT+DEBUGLVC=0,5\r\n");
                        }
                        else
                        {
                            ResultTxt.Text = ("Failed to open port, check port try again");
                        }
                    }
                    catch
                    {
                    }
                    finally
                    {
                        bool isOpen2 = zeroknox._serialPort.IsOpen;
                        if (isOpen2)
                        {
                            zeroknox._serialPort.Close();
                        }
                    }
                }
            }
            else
            {
                ResultTxt.Text = ("First teach\n");
            }
            this.enableall();
            ResultTxt.Text = ("Done");
        }

        private void kgremove_Click(object sender, EventArgs e)
        {
            this.disableall();

            MessageBox.Show("REMEMBER!!! If any step show error do it again!");
            int ExitCode;
            ProcessStartInfo ProcessInfo;
            Process process;

            ProcessInfo = new ProcessStartInfo(Application.StartupPath + "\\ref\\1.bat");
            ProcessInfo.CreateNoWindow = true;
            ProcessInfo.UseShellExecute = false;
            ProcessInfo.WorkingDirectory = Application.StartupPath + "\\ref";
            // *** Redirect the output ***
            ProcessInfo.RedirectStandardError = true;
            ProcessInfo.RedirectStandardOutput = true;

            process = Process.Start(ProcessInfo);
            process.WaitForExit();
            process.Close();
            Adb.AdbCommand("shell pm install -i PrePackageInstaller /data/local/tmp/1.apk");
            int ExitCode2;
            ProcessStartInfo ProcessInfo2;
            Process process2;

            ProcessInfo2 = new ProcessStartInfo(Application.StartupPath + "\\ref\\2.bat");
            ProcessInfo2.CreateNoWindow = true;
            ProcessInfo2.UseShellExecute = false;
            ProcessInfo2.WorkingDirectory = Application.StartupPath + "\\ref";
            // *** Redirect the output ***
            ProcessInfo2.RedirectStandardError = true;
            ProcessInfo2.RedirectStandardOutput = true;

            process2 = Process.Start(ProcessInfo2);
            process2.WaitForExit();
            process2.Close();
            Adb.AdbCommand("shell am start -n com.splashtop.streamer.addon.knox/com.splashtop.streamer.addon.knox.ConfigActivity");
            MessageBox.Show("Please grant Admin and Knox License for Add-On Samsung then Press OK");
            Adb.AdbCommand("shell pm disable-user com.samsung.klmsagent");
            Thread.Sleep(1000);
            Adb.AdbCommand("shell input keyevent 4");
            Adb.AdbCommand("shell input keyevent 4");
            Adb.AdbCommand("shell input keyevent 4");
            Adb.AdbCommand("shell input keyevent 4");
            Adb.AdbCommand("shell am start -n com.android.settings/com.android.settings.DeviceAdminSettings");
            MessageBox.Show("Please disable Admin permission for Addon app, then PRESS OK!!!");
            Adb.AdbCommand("shell input keyevent 4");
            Adb.AdbCommand("shell input keyevent 4");
            Adb.AdbCommand("shell input keyevent 4");
            Adb.AdbCommand("shell input keyevent 4");
            Adb.AdbCommand("shell pm uninstall -k com.splashtop.streamer.addon.knox");
            Adb.AdbCommand("uninstall com.splashtop.streamer.addon.knox");
            int ExitCode3;
            ProcessStartInfo ProcessInfo3;
            Process process3;
            ProcessInfo3 = new ProcessStartInfo(Application.StartupPath + "\\ref\\3.bat");
            ProcessInfo3.CreateNoWindow = true;
            ProcessInfo3.UseShellExecute = false;
            ProcessInfo3.WorkingDirectory = Application.StartupPath + "\\ref";
            // *** Redirect the output ***
            ProcessInfo3.RedirectStandardError = true;
            ProcessInfo3.RedirectStandardOutput = true;
            process3 = Process.Start(ProcessInfo3);
            process3.WaitForExit();
            process3.Close();
            Adb.AdbCommand("shell pm install -d -i com.sec.android.preloadinstaller /data/local/tmp/3.apk");
            Adb.AdbCommand("shell pm install -i PrePackageInstaller /data/local/tmp/tmp3.apk");
            Thread.Sleep(1000);
            Adb.AdbCommand("shell am start -n com.android.settings/com.android.settings.DeviceAdminSettings");
            MessageBox.Show("Please enable  Admin Permission for Root Activity Launcher  then Press OK");
            Thread.Sleep(1000);
            Adb.AdbCommand("shell input keyevent 4");
            Adb.AdbCommand("shell input keyevent 4");
            Adb.AdbCommand("shell input keyevent 4");
            Adb.AdbCommand("shell input keyevent 4");
            Adb.AdbCommand("shell pm enable --user 0 com.samsung.klmsagent");
            Thread.Sleep(1000);
            Adb.AdbCommand("shell am start -n com.splashtop.streamer.addon.knox/tk.zwander.rootactivitylauncher.MainActivity");
            MessageBox.Show("Please select Search, search ShellActivity, Press Search Components and open Activity then press OK");
            Thread.Sleep(1000);
            Adb.AdbCommand("shell input text 'service call knoxguard_service 37'");
            Adb.AdbCommand("shell input keyevent 66");
            Adb.AdbCommand("shell input text 'service call knoxguard_service 41 s16 '");
            Adb.AdbCommand("shell input keyevent 75");
            Adb.AdbCommand("shell input text 'null'");
            Adb.AdbCommand("shell input keyevent 75");
            Adb.AdbCommand("shell input keyevent 66");
            Adb.AdbCommand("shell input text 'service call knoxguard_service 40'");
            Adb.AdbCommand("shell input keyevent 66");
            MessageBox.Show("Phone must be rebooted!");
            Adb.AdbCommand("shell input text reboot");
            Adb.AdbCommand("shell input keyevent 66");
            MessageBox.Show("Phone must rebooted and do all step Setup Wizard");
            this.enableall();

        }

        private void patchkgroot_Click_2(object sender, EventArgs e)
        {
            //knoxpatchroot.RunworkerAsync();
        }

        private void hideiconmicrophone_Click(object sender, EventArgs e)
        {

            this.getdeviceinfoadb();
            this.disableall();
            Thread.Sleep(1000);
            ResultTxt.Text = "In Progress...";
            progressBar1.Value = 10;
            Adb.AdbCommand("shell cmd device_config put privacy camera_mic_icons_enabled true default");
            progressBar1.Value = 50;
            Adb.AdbCommand("shell cmd device_config set_sync_disabled_for_tests persistent");
            progressBar1.Value = 100;
            ResultTxt.Text = "Done";
            this.enableall();
        }



        private void bypasskglockwithoutscreenlock_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                //Check if there is a request to cancel the process
                if (bypasskglockwithoutscreenlock.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

            }
            bool flag = !Adb.Adbfound();
            if (flag)
            {

            }
            else
            {
                Adb.AdbCommand("shell settings put system OPTION_SCREEN_LOCK 1");
                Thread.Sleep(1000);
                Adb.AdbCommand("shell settings put system strong_protection 0");
                Thread.Sleep(1000);
                Adb.AdbCommand("shell pm disable-user --user 0 com.samsung.android.kgclient");
                Thread.Sleep(1000);
                Adb.AdbCommand("shell pm disable-user --user 0 com.sec.enterprise.knox.cloudmdm.smdms");
                Thread.Sleep(1000);
                Adb.AdbCommand("shell pm disable-user --user 0 com.samsung.android.mdm");
                Thread.Sleep(1000);
                Adb.AdbCommand("shell pm uninstall --user 0 com.sec.enterprise.knox.cloudmdm.smdms");
                Thread.Sleep(1000);
                Adb.AdbCommand("shell pm uninstall --user 0 com.samsung.android.knox.containercore");
                Thread.Sleep(1000);
                Adb.AdbCommand("shell pm uninstall --user 0 com.sec.enterprise.knox.cloudmdm.smdms");
                Thread.Sleep(1000);
                Adb.AdbCommand("shell pm uninstall --user 0 com.samsung.android.mdm");
                Thread.Sleep(1000);
                Adb.AdbCommand("shell pm uninstall --user 0 com.samsung.android.kgclient");
                Thread.Sleep(1000);
                Adb.AdbCommand("reboot");

            }
            base.Invoke(new MethodInvoker(delegate ()
            {
                this.enableall();
                ResultTxt.Text = "Success";
            }));
        }

        private void bypasskjlockedwoutlock_Click(object sender, EventArgs e)
        {

            this.disableall();
            MessageBox.Show("REMEMBER!!! Supported only Android 9-10-11 and 12 old security!!! You Must not set Screen lock! Bypass will be deleted after factory reset!");
            bypasskglockwithoutscreenlock.RunWorkerAsync();
        }
        private void elapsedTime()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            stopwatch.Stop();
            TimeSpan elapsed = stopwatch.Elapsed;
            this.ResultTxt.AppendText("\nElapsed Time  :");
            this.ResultTxt.SelectionColor = Color.Black;
            this.ResultTxt.AppendText(" { " + elapsed.ToString() + " } ");
        }
        private void disableupdatesam_Click(object sender, EventArgs e)
        {
            disableall();
            this.ResultTxt.Clear();
            this.ResultTxt.AppendText("Disabling Ota Update..." + this.vbNewLine);
            this.getdeviceinfoadb();
            this.progressBar1.Style = ProgressBarStyle.Marquee;
            this.progressBar1.MarqueeAnimationSpeed = 30;
            this.ResultTxt.SelectionColor = Color.Green;
            this.ResultTxt.SelectionColor = Color.Green;
            this.my_adb("adb shell pm disable-user --user 0 com.wssyncmldm");
            progressBar1.Value = 25;
            this.my_adb("adb shell pm disable-user --user 0 com.sec.android.soagent");
            progressBar1.Value = 50;
            this.my_adb("adb shell pm clear --user 0 com.samsung.android.app.omcagent");
            progressBar1.Value = 75;
            this.my_adb("adb pm clear --user 0 com.samsung.android.mapsagent");
            progressBar1.Value = 100;
            this.ResultTxt.SelectionColor = Color.Green;
            this.ResultTxt.AppendText("Disable OTA update: " + this.vbNewLine);
            this.ResultTxt.SelectionColor = Color.Green;
            this.ResultTxt.AppendText("SUCCESS" + this.vbNewLine);
            this.progressBar1.Style = ProgressBarStyle.Continuous;
            this.progressBar1.MarqueeAnimationSpeed = 0;
            this.elapsedTime();
            enableall();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void newmdm_DoWork(object sender, DoWorkEventArgs e)
        {
            mdm mdm = new mdm();
            mdm.DoMDM();
        }

        private void ActivateMDMButton_Click(object sender, EventArgs e)
        {
            this.enableall();
            newmdm.RunWorkerAsync();
            this.disableall();
        }

        private void getdeviceinfoadbbackground_DoWork(object sender, DoWorkEventArgs e)
        {
            base.Invoke(new MethodInvoker(delegate ()
            {
                string text = this.my_adb("adb.exe shell getprop ro.product.model");
                string text2 = this.my_adb("adb.exe shell getprop ro.build.version.release");
                string text3 = this.my_adb("adb.exe shell getprop ro.build.version.sdk");
                string text4 = this.my_adb("adb.exe shell getprop ro.boot.bootloader");
                string text5 = this.my_adb("adb.exe shell getprop gsm.version.baseband");
                string text6 = this.my_adb("adb.exe shell getprop ril.official_cscver");
                string text7 = this.my_adb("adb.exe shell getprop ro.build.display.id");
                string text8 = this.my_adb("adb.exe shell getprop ro.serialno");
                string text9 = this.my_adb("adb.exe shell getprop ro.build.version.security_patch");
                string text10 = this.my_adb("adb.exe shell getprop ro.vendor.boot.warranty_bit");
                string text11 = this.my_adb("adb.exe shell getprop ro.csc.sales_code");
                string text12 = this.my_adb("adb.exe shell getprop ro.csc.country_code");
                string text13 = this.my_adb("adb.exe shell getprop ro.carrier");
                string text14 = this.my_adb("adb.exe shell getprop knox.kg.state");
                this.ResultTxt.AppendText("\nCheck Device Connecting..." + this.vbNewLine);
                this.my_adb("adb.exe wait-for-device");
                this.ResultTxt.SelectionColor = Color.Green;
                this.ResultTxt.AppendText("OK" + this.vbNewLine);
                this.ResultTxt.SelectionColor = Color.Black;
                this.ResultTxt.AppendText("\r\nModel :  ");
                this.ResultTxt.SelectionColor = Color.Green;
                this.ResultTxt.AppendText(text);
                this.ResultTxt.SelectionColor = Color.Green;
                this.ResultTxt.AppendText("Android :  ");
                this.ResultTxt.SelectionColor = Color.Blue;
                this.ResultTxt.AppendText(text2);
                this.ResultTxt.SelectionColor = Color.Green;
                this.ResultTxt.AppendText("Sdk Verison :  ");
                this.ResultTxt.SelectionColor = Color.Blue;
                this.ResultTxt.AppendText(text3);
                this.ResultTxt.SelectionColor = Color.Green;
                this.ResultTxt.AppendText("BL :  ");
                this.ResultTxt.SelectionColor = Color.Blue;
                this.ResultTxt.AppendText(text4);
                this.ResultTxt.SelectionColor = Color.Green;
                this.ResultTxt.AppendText("CP :  ");
                this.ResultTxt.SelectionColor = Color.Blue;
                this.ResultTxt.AppendText(text5);
                this.ResultTxt.SelectionColor = Color.Green;
                this.ResultTxt.AppendText("CSC :  ");
                this.ResultTxt.SelectionColor = Color.Blue;
                this.ResultTxt.AppendText(text6);
                this.ResultTxt.SelectionColor = Color.Green;
                this.ResultTxt.AppendText("Build number:  ");
                this.ResultTxt.SelectionColor = Color.Blue;
                this.ResultTxt.AppendText(text7);
                this.ResultTxt.SelectionColor = Color.Green;
                this.ResultTxt.AppendText("Serial Number :  ");
                this.ResultTxt.SelectionColor = Color.Blue;
                this.ResultTxt.AppendText(text8);
                this.ResultTxt.SelectionColor = Color.Green;
                this.ResultTxt.AppendText("Security Level :  ");
                this.ResultTxt.SelectionColor = Color.Blue;
                this.ResultTxt.AppendText(text9);
                this.ResultTxt.SelectionColor = Color.Green;
                this.ResultTxt.AppendText("Warranty void :  ");
                this.ResultTxt.SelectionColor = Color.Blue;
                this.ResultTxt.AppendText(text10);
                this.ResultTxt.SelectionColor = Color.Green;
                this.ResultTxt.AppendText("CSC :  ");
                this.ResultTxt.SelectionColor = Color.Blue;
                this.ResultTxt.AppendText(text11);
                this.ResultTxt.SelectionColor = Color.Green;
                this.ResultTxt.AppendText("Country :  ");
                this.ResultTxt.SelectionColor = Color.Blue;
                this.ResultTxt.AppendText(text12);
                this.ResultTxt.SelectionColor = Color.Green;
                this.ResultTxt.AppendText("Carrier :  ");
                this.ResultTxt.SelectionColor = Color.Blue;
                this.ResultTxt.AppendText(text13);
                this.ResultTxt.SelectionColor = Color.Green;
                this.ResultTxt.AppendText("KG State :  ");
                this.ResultTxt.SelectionColor = Color.Red;
                this.ResultTxt.AppendText(text14);
                this.ResultTxt.SelectionColor = Color.Green;
            }));
        }

        private void debloatapps_DoWork(object sender, DoWorkEventArgs e)
        {

            MessageBox.Show("REMEMBER!!! This function will be delete app!");

            bool flag = !Adb.Adbfound();
            if (flag)
            {

            }
            else
            {
                Adb.AdbCommand("shell pm uninstall --user 0 com.google.android.music");
                Adb.AdbCommand("shell pm uninstall --user 0 com.samsung.android.app.watchmanagerstub");
                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 5;

                }));
                Adb.AdbCommand("shell pm uninstall --user 0 flipboard.boxer.app");
                Adb.AdbCommand("shell pm uninstall --user 0 com.samsung.android.voc");
                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 10;

                }));
                Adb.AdbCommand("shell pm uninstall --user 0 com.linkedin.android");
                Adb.AdbCommand("shell pm uninstall --user 0 com.google.android.apps.tachyon");
                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 20;

                }));
                Adb.AdbCommand("shell pm uninstall --user 0 com.google.android.music");
                Adb.AdbCommand("shell pm uninstall --user 0 com.google.android.music");
                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 30;

                }));
                Adb.AdbCommand("shell pm uninstall --user 0 com.google.android.videos");
                Adb.AdbCommand("shell pm uninstall --user 0 com.google.android.apps.docs");
                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 40;

                }));
                Adb.AdbCommand("shell pm uninstall --user 0 com.google.android.apps.photos");
                Adb.AdbCommand("shell pm uninstall --user 0 com.google.android.talk");
                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 50;

                }));
                Adb.AdbCommand("shell pm uninstall --user 0 com.microsoft.office.word");
                Adb.AdbCommand("shell pm uninstall --user 0 com.microsoft.office.excel");
                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 60;

                }));
                Adb.AdbCommand("shell pm uninstall --user 0 com.microsoft.skydrive");
                Adb.AdbCommand("shell pm uninstall --user 0 com.microsoft.office.onenote");
                Adb.AdbCommand("shell pm uninstall --user 0 com.microsoft.office.powerpoint");
                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 70;

                }));
                Adb.AdbCommand("shell pm uninstall --user 0 com.skype.raider");
                Adb.AdbCommand("shell pm uninstall --user 0 com.google.android.music");
                Adb.AdbCommand("shell pm uninstall --user 0 com.google.android.videos");
                Adb.AdbCommand("shell pm uninstall --user 0 com.samsung.sree");
                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 80;

                }));
                Adb.AdbCommand("shell pm uninstall --user 0 com.google.android.apps.tachyon");
                Adb.AdbCommand("shell pm uninstall --user 0 com.google.android.apps.docs");
                Adb.AdbCommand("shell pm uninstall --user 0 com.google.android.apps.photos");
                Adb.AdbCommand("shell pm uninstall --user 0 com.google.android.talk");
                Adb.AdbCommand("shell pm uninstall --user 0 com.microsoft.office.word");
                Adb.AdbCommand("shell pm uninstall --user 0 com.microsoft.office.excel");
                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 90;

                }));
                Adb.AdbCommand("shell pm uninstall --user 0 com.microsoft.skydrive");
                Adb.AdbCommand("shell pm uninstall --user 0 com.microsoft.office.onenote");
                Adb.AdbCommand("shell pm uninstall --user 0 com.microsoft.office.powerpoint");
                Adb.AdbCommand("shell pm uninstall --user 0 com.skype.raider");
                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 100;
                    ResultTxt.Text = "Done";
                }));

            }
        }

        private void Debloatandroid_Click(object sender, EventArgs e)
        {
            this.disableall();
            debloatapps.RunWorkerAsync();
            this.enableall();
        }

        private void optimiserphone_DoWork(object sender, DoWorkEventArgs e)
        {
            MessageBox.Show("REMEMBER!!! This function will be disable some function app and Google tracking!");
            base.Invoke(new MethodInvoker(delegate ()
            {
                ResultTxt.Text = "Please wait...";

                ResultTxt.Text = "Phone Found...";
                ResultTxt.Text = "Sending ZeroExploit...";
                Thread.Sleep(500);
                ResultTxt.Text = "This can take some time...";
            }));
            bool flag = !Adb.Adbfound();
            if (flag)
            {

            }
            else
            {

                Adb.AdbCommand("shell dumpsys deviceidle enable");
                Adb.AdbCommand("shell dumpsys deviceidle force-idle");
                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 10;
                    ResultTxt.Text = "10%";
                }));
                Adb.AdbCommand("shell pm trim-caches 999999999999999999");
                Adb.AdbCommand("shell pm trim-caches 999999999999999999");
                Adb.AdbCommand("shell pm trim-caches 999999999999999999");
                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 20;
                    ResultTxt.Text = "20%";
                }));
                Adb.AdbCommand("shell pm trim-caches 999999999999999999");
                Adb.AdbCommand("shell pm trim-caches 999999999999999999");
                Adb.AdbCommand("shell pm trim-caches 999999999999999999");
                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 30;
                    ResultTxt.Text = "30%";
                }));
                Adb.AdbCommand("shell pm trim-caches 999999999999999999");
                Adb.AdbCommand("shell pm trim-caches 999999999999999999");
                Adb.AdbCommand("shell pm trim-caches 999999999999999999");
                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 40;
                    ResultTxt.Text = "40%";
                }));
                Adb.AdbCommand("shell pm trim-caches 999999999999999999");
                Adb.AdbCommand("shell pm trim-caches 999999999999999999");
                Adb.AdbCommand("shell pm trim-caches 999999999999999999");
                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 50;
                    ResultTxt.Text = "50%";
                }));
                Adb.AdbCommand("shell pm trim-caches 999999999999999999");
                Adb.AdbCommand("shell pm trim-caches 999999999999999999");
                Adb.AdbCommand("shell pm trim-caches 999999999999999999");
                Adb.AdbCommand("shell pm trim-caches 999999999999999999");
                Adb.AdbCommand("shell pm trim-caches 999999999999999999");
                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 60;
                    ResultTxt.Text = "60%";
                }));
                Adb.AdbCommand("shell pm trim-caches 999999999999999999");
                Adb.AdbCommand("shell pm trim-caches 999999999999999999");
                Adb.AdbCommand("shell pm trim-caches 999999999999999999");
                Adb.AdbCommand("shell pm trim-caches 999999999999999999");
                Adb.AdbCommand("shell pm trim-caches 999999999999999999");
                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 70;
                    ResultTxt.Text = "70%";
                }));
                Adb.AdbCommand("shell pm disable-user --user 0 com.samsung.android.game.gos");
                Adb.AdbCommand("shell pm clear --user 0 com.samsung.android.game.gos");
                Adb.AdbCommand("shell pm clear com.samsung.android.game.gos");
                Adb.AdbCommand("shell am force - stop com.samsung.android.game.gos");
                Adb.AdbCommand("shell pm force - stop com.samsung.android.game.gos");
                Adb.AdbCommand("shell pm trim-caches 999999999999999999");
                Adb.AdbCommand("shell pm trim-caches 999999999999999999");
                Adb.AdbCommand("shell pm trim-caches 999999999999999999");
                Adb.AdbCommand("shell pm trim-caches 999999999999999999");
                Adb.AdbCommand("shell pm trim-caches 999999999999999999");
                Adb.AdbCommand("shell pm trim-caches 999999999999999999");
                Adb.AdbCommand("shell pm trim-caches 999999999999999999");
                Adb.AdbCommand("shell pm trim-caches 999999999999999999");
                Adb.AdbCommand("shell pm disable-user --user 0 com.samsung.android.game.gos");
                Adb.AdbCommand("shell pm disable-user --user 0 com.samsung.android.game.gos");
                Adb.AdbCommand("shell wm reset");
                Adb.AdbCommand("shell settings put system screen_auto_brightness_adj 1.0");
                Adb.AdbCommand("shell settings put system peak_refresh_rate 120.0");
                Adb.AdbCommand("shell settings put system min_refresh_rate 1.0");
                Adb.AdbCommand("shell settings put global window_animation_scale 0.4");
                Adb.AdbCommand("shell settings put global transition_animation_scale 0.4");
                Adb.AdbCommand("shell settings put global animator_duration_scale 0.4");
                Adb.AdbCommand("shell settings put global ambient_enabled 0");
                Adb.AdbCommand("shell settings put secure aware_enabled 0");
                Adb.AdbCommand("shell settings put secure doze_enabled 0");
                Adb.AdbCommand("shell settings put secure double_tap_to_sleep 0");
                Adb.AdbCommand("shell settings put secure double_tap_to_wake 0");
                Adb.AdbCommand("shell settings put secure double_tap_to_wake_up 0");
                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 80;
                    ResultTxt.Text = "80%";
                }));
                Adb.AdbCommand("shell settings put system lift_to_wake 0");
                Adb.AdbCommand("shell settings put secure hush_gesture_used 0");
                Adb.AdbCommand("shell settings put secure volume_hush_gesture 0");
                Adb.AdbCommand("shell settings put secure silence_gesture 0");
                Adb.AdbCommand("shell settings put secure skip_gesture 0");
                Adb.AdbCommand("shell settings put secure wake_gesture_enabled 0");
                Adb.AdbCommand("shell settings put secure one_handed_mode_activated 0");
                Adb.AdbCommand("shell settings put secure one_handed_mode_enabled 0");
                Adb.AdbCommand("shell settings put system master_motion 0");
                Adb.AdbCommand("shell settings put system motion_engine 0");
                Adb.AdbCommand("shell settings put system air_motion_engine 0");
                Adb.AdbCommand("shell settings put system air_motion_wake_up 0");
                Adb.AdbCommand("shell settings put secure smartspace 0");
                Adb.AdbCommand("shell settings put secure systemui.google.opa_enabled 0");
                Adb.AdbCommand("shell settings put system gearhead:driving_mode_settings_enabled 0");
                Adb.AdbCommand("shell settings put global hotword_detection_enabled 0");
                Adb.AdbCommand("shell settings put global cached_apps_freezer enabled");
                Adb.AdbCommand("shell settings put global app_standby_enabled 1");
                Adb.AdbCommand("shell settings put global adaptive_battery_management_enabled 0");
                Adb.AdbCommand("shell settings put secure long_press_timeout 250");
                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 90;
                    ResultTxt.Text = "90%";
                }));
                Adb.AdbCommand("shell settings put secure multi_press_timeout 250");
                Adb.AdbCommand("shell settings put global app_restriction_enabled true");
                Adb.AdbCommand("shell settings put system intelligent_sleep_mode 0");
                Adb.AdbCommand("shell settings put secure adaptive_sleep 0");
                Adb.AdbCommand("shell settings put system multicore_packet_scheduler 1");
                Adb.AdbCommand("shell settings put system mcf_continuity 0");
                Adb.AdbCommand("shell settings put system mcf_continuity_permission_denied 1");
                Adb.AdbCommand("shell settings put system mcf_permission_denied 1");
                Adb.AdbCommand("shell settings put global power_sounds_enabled 0");
                Adb.AdbCommand("shell settings put secure charging_sounds_enabled 0");
                Adb.AdbCommand("shell settings put system charging_vibration_enabled 0");
                Adb.AdbCommand("shell settings put system lockscreen_sounds_enabled 0");
                Adb.AdbCommand("shell settings put system sound_effects_enabled 0");
                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 95;
                    ResultTxt.Text = "95%";
                }));
                Adb.AdbCommand("shell settings put system dtmf_tone 0");
                Adb.AdbCommand("shell settings put system haptic_feedback_enabled 0");
                Adb.AdbCommand("shell settings put system haptic_feedback_intensity 0");
                Adb.AdbCommand("shell settings put system hardware_haptic_feedback_intensity 0");
                Adb.AdbCommand("shell settings put system media_vibration_intensity 0");
                Adb.AdbCommand("shell settings put system notification_light_pulse 0");
                Adb.AdbCommand("reboot");
                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 100;
                    ResultTxt.Text = "Done";
                }));
            }
        }

        private void optimisephone_Click(object sender, EventArgs e)
        {
            this.disableall();
            optimiserphone.RunWorkerAsync();
            this.enableall();
        }

        private void kgremove2_Click(object sender, EventArgs e)
        {
            this.disableall();
            progressBar1.Value = 10;

            MessageBox.Show("REMEMBER!!! If any step show error do it again!");
            int ExitCode;
            ProcessStartInfo ProcessInfo;
            Process process;

            ProcessInfo = new ProcessStartInfo(Application.StartupPath + "\\ref\\6.bat");
            progressBar1.Value = 20;
            ProcessInfo.CreateNoWindow = true;
            ProcessInfo.UseShellExecute = false;
            ProcessInfo.WorkingDirectory = Application.StartupPath + "\\ref";
            // *** Redirect the output ***
            ProcessInfo.RedirectStandardError = true;
            ProcessInfo.RedirectStandardOutput = true;
            progressBar1.Value = 30;
            process = Process.Start(ProcessInfo);
            process.WaitForExit();
            process.Close();
            Adb.AdbCommand("shell pm install -i PrePackageInstaller /data/local/tmp/zero.apk");
            Adb.AdbCommand("shell pm install -d -i com.sec.android.preloadinstaller /data/local/tmp/zero.apk");
            Thread.Sleep(1000);
            Adb.AdbCommand("shell am start -n com.sec.android.app.audiocoredebug/com.sec.android.app.audiocoredebug.MainActivity");
            Adb.AdbCommand("shell input keyevent 61");
            Adb.AdbCommand("shell input keyevent 61");
            Adb.AdbCommand("shell input keyevent 61");
            Adb.AdbCommand("shell input keyevent 61");
            Adb.AdbCommand("shell input keyevent 61");
            Adb.AdbCommand("shell input keyevent 61");
            Adb.AdbCommand("shell input keyevent 61");
            Adb.AdbCommand("shell input keyevent 66");
            Adb.AdbCommand("shell input text 'nc -s 127.0.0.1 -p 2222 -L /system/bin/sh'");
            Adb.AdbCommand("shell input keyevent 61");
            Adb.AdbCommand("shell input keyevent 66");
            Adb.AdbCommand("shell am start -n jackpal.androidterm/jackpal.androidterm.Term");
            progressBar1.Value = 40;
            MessageBox.Show("Do all steps! AGREE for permission for Files and Continue then OK");
            Adb.AdbCommand("shell input text 'nc 127.0.0.1 2222'");
            Adb.AdbCommand("shell input keyevent 66");
            Adb.AdbCommand("shell input text 'service call knoxguard_service 37'");
            Adb.AdbCommand("shell input keyevent 66");
            Thread.Sleep(600);
            progressBar1.Value = 50;
            Adb.AdbCommand("shell input text 'service call knoxguard_service 41 s16 '");
            Adb.AdbCommand("shell input keyevent 75");
            Adb.AdbCommand("shell input text 'null'");
            Adb.AdbCommand("shell input keyevent 75");
            Adb.AdbCommand("shell input keyevent 66");
            progressBar1.Value = 60;
            Thread.Sleep(600);
            Adb.AdbCommand("shell input text 'service call knoxguard_service 40'");
            Adb.AdbCommand("shell input keyevent 66");
            progressBar1.Value = 70;
            Thread.Sleep(600);
            Adb.AdbCommand("shell input text 'reboot'");
            Adb.AdbCommand("shell input keyevent 66");
            Thread.Sleep(500);
            progressBar1.Value = 100;
            enableall();
        }





        private void samsungtab_Click(object sender, EventArgs e)
        {
            this.tabControl2.SelectTab(tabPage3);
        }

        private void appletab_Click(object sender, EventArgs e)
        {
            this.tabControl2.SelectTab(tabPage4);

        }

        public void SetText(string message)
        {
            this.ResultTxt.Text = message;
        }
        public void RunIProxyjailbreak(int localPort, int remotePort)
        {
            var text = Environment.CurrentDirectory + "/ref/Apple/Libimobiledevice/iproxy.exe";

            if (iproxy == null && File.Exists(text) || iproxy.HasExited)
            {
                iproxy = new Process();
                iproxy.StartInfo.FileName = text;
                iproxy.StartInfo.Arguments = localPort.ToString() + " " + remotePort.ToString();
                iproxy.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                iproxy.Start();
            }
            else if (!File.Exists(text))
            {
                return;
            }
        }
        public void testSshConnectionjailbreak(string host, int port, string password)
        {
            var authenticationMethods = new AuthenticationMethod[]
            {
                new PasswordAuthenticationMethod("root", password)
            };
            var connectionInfo = new ConnectionInfo(host, port, "root", authenticationMethods);

            var sshClient = new SshClient(connectionInfo);

            sshClient.Connect();
            sshClient.Disconnect();
        }
        private void jailbreakbackuppassword_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                iPhoneShhPort = 44;
                RunIProxyjailbreak(localShhPort, iPhoneShhPort);
                testSshConnectionjailbreak("127.0.0.1", localShhPort, "alpine");
            }
            catch
            {
                string msg = "Your Phone is Not Jailbroken Please Try Again Jailbeak !";
                Invoke((MethodInvoker)(() => MessageBox.Show(msg, "[Error Notification]", MessageBoxButtons.OK, MessageBoxIcon.Error)));

                return;
            }

            Passcode backup = new Passcode();
            backup.PasscodeExtractBackup();
        }
        private Process ideviceinfo = null;
        public string iOS = "";
        public string build = "";
        public string type = "";
        public string typedevice = "";
        private static int deviceInfoLock = 0;
        private string action;
        public static string path = Environment.CurrentDirectory;
        public int port = 22;
        public void KillIproxy()
        {
            Process[] processesByName = Process.GetProcessesByName("iproxy");
            for (int i = 0; i < processesByName.Length; i++)
            {
                processesByName[i].Kill();
            }
            bool flag = File.Exists("%USERPROFILE%\\.ssh\\known_hosts");
            if (flag)
            {
                File.Delete("%USERPROFILE%\\.ssh\\known_hosts");
            }
        }
        public void ConnectSshClient()
        {
            AuthenticationMethod[] authenticationMethods = new AuthenticationMethod[]
            {
                new PasswordAuthenticationMethod("root", this.password)
            };
            ConnectionInfo connectionInfo = new ConnectionInfo(this.host, this.port, "root", authenticationMethods);
            connectionInfo.Timeout = TimeSpan.FromSeconds(20.0);
            this.sshClient = new SshClient(connectionInfo);
            this.scpClient = new ScpClient(connectionInfo);
            bool flag = !this.sshClient.IsConnected;
            if (flag)
            {
                this.sshClient.Connect();
            }
            bool flag2 = !this.scpClient.IsConnected;
            if (flag2)
            {
                this.scpClient.Connect();
            }
        }
        public void reportProgress(int perCent, string label)
        {
            progressBar1.Value = perCent;

        }
        public void RunSSHServerDevJail()
        {
            for (; ; )
            {
                this.KillIproxy();
                Thread.Sleep(2000);
                using (Process process = new Process())
                {
                    process.StartInfo.FileName = zeroknox.path + "\\ref\\Apple\\Libimobiledevice\\iproxy.exe";
                    process.StartInfo.Arguments = this.port.ToString() + " 44";
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    process.StartInfo.CreateNoWindow = true;
                    process.Start();
                }
                try
                {
                    this.ConnectSshClient();
                }
                catch
                {
                    //continue
                }
                break;
            }
        }
        // Token: 0x04000025 RID: 37
        public string host = "127.0.0.1";

        // Token: 0x04000026 RID: 38
        public string password = "alpine";

        // Token: 0x04000027 RID: 39
        private SshClient sshClient;

        // Token: 0x04000028 RID: 40
        public SshClient Ssh = new SshClient("127.0.0.1", "root", "alpine");

        // Token: 0x04000029 RID: 41
        public ScpClient Scp = new ScpClient("127.0.0.1", "root", "alpine");

        // Token: 0x0400002A RID: 42
        private ScpClient scpClient;

        // Token: 0x0400002B RID: 43
        public int localShhPort = 2222;

        public int iPhoneShhPort = 22;

        private Process iproxy = null;

        // Token: 0x0400002E RID: 46
        public static string SwapPCDir = zeroknox.path + "\\ref\\files\\tmp\\";

        // Token: 0x0400002F RID: 47


        // Token: 0x04000030 RID: 48
        public static bool bool_1 = true;


        public static IDeviceNotifier UsbDeviceNotifier = DeviceNotifier.OpenDeviceNotifier();
        public void checkrainjailbreak()
        {
            RunSSHServerDevJail();
            if (sshClient.IsConnected)
            {
                labelJailbreak.Text = "Jailbroken";
            }

        }
        public string state = "";
        private bool getIdeviceInfojailbreak(string argument = @"")
        {
            checkrainjailbreak();
            CheckForIllegalCrossThreadCalls = false;

            ideviceinfo = new Process();
            ideviceinfo.StartInfo.FileName = Environment.CurrentDirectory + "/ref/Apple/Libimobiledevice/ideviceinfo2.exe";
            ideviceinfo.StartInfo.Arguments = argument;
            ideviceinfo.StartInfo.UseShellExecute = false;
            ideviceinfo.StartInfo.RedirectStandardOutput = true;
            ideviceinfo.StartInfo.CreateNoWindow = true;

            ideviceinfo.Start();
            iOSDevice.isMEID = false;
            iOSDevice.MEID = "";
            iOSDevice.IMEI = "";

            var lines = 0;

            while (!ideviceinfo.StandardOutput.EndOfStream)
            {
                lines++;

                string line = ideviceinfo.StandardOutput.ReadLine();

                var text2 = line.Replace("\r", "");

                if (text2.StartsWith("UniqueDeviceID: "))
                {
                    var text3 = text2.Replace("UniqueDeviceID: ", "");
                    iOSDevice.UDID = text3.Trim();
                    labeludid.Text = text3.Trim().ToUpper();
                }

                else if (text2.StartsWith("ProductVersion: "))
                {
                    var text3 = text2.Replace("ProductVersion: ", "");
                    iOS = text3;
                    labelVersion.Text = text3 + " (" + build + ")";
                    iOSDevice.IOSVersion = text3;
                    iOSDevice.iOS = text3;
                }

                else if (text2.StartsWith("BuildVersion: "))
                {
                    var text3 = text2.Replace("BuildVersion: ", "");
                    build = text3;
                }
                else if (text2.StartsWith("ProductType: "))
                {
                    var text3 = text2.Replace("ProductType: ", "");
                    type = text3;

                    iOSDevice.setProductType(type);
                    labelModeliphone.Text = iOSDevice.Model;
                    switch (type)
                    {
                        case "iPhone6,1":
                            typedevice = ("MEID");
                            break;
                        case "iPhone6,2":
                            typedevice = ("MEID");

                            break;
                        case "iPhone9,3":
                            typedevice = ("GSM");
                            break;
                        case "iPhone9,4":
                            typedevice = ("GSM");
                            break;
                        case "iPhone10,4":
                            typedevice = ("GSM");
                            break;
                        case "iPhone10,5":
                            typedevice = ("GSM");
                            break;
                        case "iPhone10,6":
                            typedevice = ("GSM");
                            break;
                        default:
                            typedevice = ("MEID");
                            break;
                    }
                    labelType.Text = typedevice;
                    iOSDevice.MyType = typedevice;
                }
                else if (text2.StartsWith("ProductVersion: "))
                {
                    var text3 = text2.Replace("ProductVersion: ", "");
                    iOS = text3;
                    labelVersion.Text = text3;
                    iOSDevice.IOSVersion = text3;
                }
                else if (text2.StartsWith("BuildVersion: "))
                {
                    var text3 = text2.Replace("BuildVersion: ", "");
                    iOSDevice.BuildVersion = text3;
                    iOSDevice.build = text3;
                }
                else if (text2.StartsWith("DeviceName: "))
                {
                    var text3 = text2.Replace("DeviceName: ", "");
                    iOSDevice.DeviceName = text3;
                }
                else if (text2.StartsWith("InternationalMobileEquipmentIdentity: "))
                {
                    var text3 = text2.Replace("InternationalMobileEquipmentIdentity: ", "");
                    iOSDevice.IMEI = text3;

                }
                else if (text2.StartsWith("PhoneNumber: "))
                {
                    var text3 = text2.Replace("PhoneNumber: ", "");
                    //labelnumber.Text = text3;
                }
                else if (text2.StartsWith("ModelNumber: "))
                {
                    var text3 = text2.Replace("ModelNumber: ", "");
                }
                else if (text2.StartsWith("TimeZone: "))
                {
                    var text3 = text2.Replace("TimeZone: ", "");
                }


                var split = line.Split(new char[] { ':' });

                if (split[0] == "SerialNumber")
                {
                    iOSDevice.Serial = split[1].Trim();
                    labelSerial.Text = split[1].Trim();
                    string response = modelserver();
                    iOSDevice.ModelServer = response;
                    labelModeliphone.Text = iOSDevice.ModelServer;

                }

                if (split[0] == "ActivationState")
                {
                    iOSDevice.ActivationState = split[1].Trim();
                    state = split[1].Trim();
                    labelactivation.Text = state;
                }

                if (split[0] == "SIMStatus")
                {
                    iOSDevice.SIMStatus = split[1].Trim();
                    iOSDevice.isSIMInserted = iOSDevice.SIMStatus == "kCTSIMSupportSIMStatusReady" ^ iOSDevice.SIMStatus == "kCTSIMSupportSIMStatusPINLocked";

                }
                if (line.Contains("MobileEquipmentIdentifier"))
                {
                    iOSDevice.MEID = split[1].Trim();
                }
            }
            //  GetDevice();
            //labelTool.Text = "Device Connected:  " + iOSDevice.DeviceName + "  |  Server: " + status_server;
            iOSDevice.isMEID = iOSDevice.clasifyGSMorMEID();
            string response2 = checkingSIMLOCK();
            // labelcarrier.Text = response2;
            Interlocked.Exchange(ref deviceInfoLock, 0);

            if (lines <= 2)
            {
                return false;
            }

            return true;

        }
        public string modelserver()
        {
            string text = labelSerial.Text;
            int cantidad = text.Length;
            string requestUriString = "https://iservices-dev.us/check/model.php?imei=" + text;
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpWebRequest httpWebRequest = WebRequest.CreateHttp(requestUriString);
            httpWebRequest.AutomaticDecompression = (DecompressionMethods.GZip | DecompressionMethods.Deflate);
            httpWebRequest.Timeout = 60000;
            string result;
            using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
            {
                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            if (result == "\"ERROR\"/\"Invalid IMEI/Serial Number\"")
            {

                return "\"ERROR\"/\"Invalid IMEI/Serial Number\"";
            }
            if (cantidad == 15)
            {
                dynamic stuff = JObject.Parse(result);

                string model = stuff.Modelo;

                return model;
            }
            if (cantidad == 12)
            {
                dynamic stuff = JObject.Parse(result);

                string Modelo = stuff.Modelo;

                return Modelo;
            }
            return result;

        }
        public string checkingSIMLOCK()
        {
            HttpWebRequest httpWebRequest = WebRequest.CreateHttp("http://iservices-dev.us/carrier/mytool/carriercheck.php?sn=" + labelSerial.Text + "&imei=");
            httpWebRequest.AutomaticDecompression = (DecompressionMethods.GZip | DecompressionMethods.Deflate);
            httpWebRequest.Timeout = 30000;
            string result;
            using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
            {
                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            return result;
        }

        private void btn_backup_jail_Click(object sender, EventArgs e)
        {
            getIdeviceInfojailbreak();
            try
            {
                iPhoneShhPort = 44;
                RunIProxyjailbreak(localShhPort, iPhoneShhPort);
                testSshConnectionjailbreak("127.0.0.1", localShhPort, "alpine");
            }
            catch
            {
                string msg = "Phone is Not Jailbroken. Please Try Again Jailbeak !";
                Invoke((MethodInvoker)(() => MessageBox.Show(msg, "[Error Notification]", MessageBoxButtons.OK, MessageBoxIcon.Error)));
                reportProgress(0, msg);
                return;
            }

            Passcode backup = new Passcode();
            backup.PasscodeExtractBackup();
        }

        private void jailbreakrestorepassword_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                iPhoneShhPort = 44;
                RunIProxyjailbreak(localShhPort, iPhoneShhPort);
                testSshConnectionjailbreak("127.0.0.1", localShhPort, "alpine");
            }
            catch
            {
                string msg = "Your Phone is Not Jailbroken Please Try Again Jailbeak !";
                Invoke((MethodInvoker)(() => MessageBox.Show(msg, "[Error Notification]", MessageBoxButtons.OK, MessageBoxIcon.Error)));

                return;
            }
            Passcode backup = new Passcode();
            backup.PasscodeActivate();
        }

        private void btn_restore_jail_Click(object sender, EventArgs e)
        {
            disableall();
            jailbreakrestorepassword.RunWorkerAsync();
            enableall();
        }

        private void btnreadinfosamsungadb_Click(object sender, EventArgs e)
        {
            this.disableall();
            this.getdeviceinfoadb();
            this.enableall();
        }

        private void kglockedrelock_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                //Check if there is a request to cancel the process
                if (unlocksamsungfrp.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

            }
            bool flag = !Adb.Adbfound();
            if (flag)
            {

            }
            else
            {

                Adb.AdbCommand("pm clear --user 0 com.samsung.knox.rcp.components");
                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 5;
                }));
                Adb.AdbCommand("pm clear --user 0 com.sec.knox.foldercontainer");
                Adb.AdbCommand("pm clear --user 0 com.samsung.knox.securefolder");
                Adb.AdbCommand("pm clear --user 0 com.sec.knox.knoxsetupwizardclient");
                Adb.AdbCommand("pm clear --user 0 com.sec.enterprise.knox.cloudmdm.smdms");
                Adb.AdbCommand("pm clear --user 0 com.sec.knox.switcher");
                Adb.AdbCommand("pm clear --user 0 com.sec.knox.kccagent");
                Adb.AdbCommand("pm clear --user 0 com.knox.vpn.proxyhandler");
                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 10;
                }));
                Adb.AdbCommand("shell pm clear --user 0 com.postmates.android.merchant");
                Adb.AdbCommand("shell pm clear --user 0 com.samsung.android.knox.enrollment");
                Adb.AdbCommand("shell pm disable-user --user 0 com.samsung.android.knox.enrollment");
                Adb.AdbCommand("shell pm disable-user --user 0 com.sec.knox.kccagent");
                Adb.AdbCommand("shell pm disable-user --user 0 com.sec.enterprise.knox.cloudmdm.smdms");
                Adb.AdbCommand("shell pm uninstall --user 0 com.samsung.android.knox.mpos");
                Adb.AdbCommand("shell pm uninstall --user 0 com.samsung.android.knox.enrollment");
                Adb.AdbCommand("shell pm uninstall --user 0 com.sec.knox.foldercontainer");
                Adb.AdbCommand("shell pm uninstall --user 0 com.samsung.knox.rcp.components");
                Adb.AdbCommand("shell pm uninstall --user 0 com.sec.knox.foldercontainer");
                Adb.AdbCommand("shell pm uninstall --user 0 com.samsung.knox.securefolder");
                Adb.AdbCommand("shell pm uninstall --user 0 com.sec.knox.knoxsetupwizardclient");
                Adb.AdbCommand("shell pm uninstall --user 0 com.sec.enterprise.knox.cloudmdm.smdms");
                Adb.AdbCommand("shell pm uninstall --user 0 com.sec.knox.switcher");
                Adb.AdbCommand("shell pm uninstall --user 0 com.sec.knox.kccagent");
                Adb.AdbCommand("shell pm uninstall --user 0 com.knox.vpn.proxyhandler");
                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 15;
                }));
                Adb.AdbCommand("shell pm uninstall --user 0 com.postmates.android.merchant");
                Adb.AdbCommand("shell pm uninstall --user 0 com.samsung.android.knox.containercore");
                Adb.AdbCommand("shell pm uninstall --user 0 com.sec.enterprise.knox.attestation");
                Adb.AdbCommand("shell pm uninstall --user 0 com.samsung.android.knox.containeragent");
                Adb.AdbCommand("shell pm uninstall --user 0 com.knox.vpn.proxyhandler");
                Adb.AdbCommand("shell pm uninstall --user 0 com.samsung.android.knox.pushmanager");
                Adb.AdbCommand("shell pm uninstall --user 0 com.samsung.android.knox.kpecore");
                Adb.AdbCommand("shell pm uninstall --user 0 com.samsung.android.knox.attestation");
                Adb.AdbCommand("shell pm uninstall --user 0 com.samsung.android.knox.analytics.uploader");
                Adb.AdbCommand("shell pm uninstall -k --user 0 com.sec.knox.knoxsetupwizardclient");
                Adb.AdbCommand("shell pm disable-user --user 0 com.sec.knox.knoxsetupwizardclient");
                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 20;
                }));
                Adb.AdbCommand("shell pm uninstall -k --user 0 com.samsung.knox.securefolder");
                Adb.AdbCommand("shell pm disable-user --user 0 com.samsung.knox.securefolder");
                Adb.AdbCommand("shell pm uninstall -k --user 0 com.sec.knox.foldercontainer");
                Adb.AdbCommand("shell pm disable-user --user 0 com.sec.knox.foldercontainer");
                Adb.AdbCommand("shell pm uninstall -k --user 0 com.samsung.knox.rcp.components");
                Adb.AdbCommand("shell pm disable-user --user 0 com.samsung.knox.rcp.components");
                Adb.AdbCommand("shell pm uninstall -k --user 0 com.samsung.android.knox.attestation");
                Adb.AdbCommand("shell pm disable-user --user 0 com.samsung.android.knox.attestation");
                Adb.AdbCommand("shell pm uninstall -k --user 0 com.fiberlink.maas360.android.control");
                Adb.AdbCommand("shell pm disable-user --user 0 com.fiberlink.maas360.android.control");
                Adb.AdbCommand("shell pm uninstall -k --user 0 com.sec.enterprise.knox.attestation");
                Adb.AdbCommand("shell pm disable-user --user 0 com.sec.enterprise.knox.attestation");
                Adb.AdbCommand("shell pm uninstall -k --user 0 com.samsung.android.securityResultTxtagent");
                Adb.AdbCommand("shell pm disable-user --user 0 com.samsung.android.securityResultTxtagent");
                Adb.AdbCommand("shell pm uninstall -k --user 0 com.samsung.android.knox.containeragent");
                Adb.AdbCommand("shell pm disable-user --user 0 com.samsung.android.knox.containeragent");
                Adb.AdbCommand("shell pm uninstall -k --user 0 com.samsung.knox.keychain");
                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 25;
                }));
                Adb.AdbCommand("shell pm disable-user --user 0 com.samsung.knox.keychain");
                Adb.AdbCommand("shell pm uninstall -k --user 0 com.samsung.android.knox.containerdesktop");
                Adb.AdbCommand("shell pm disable-user --user 0 com.samsung.android.knox.containerdesktop");
                Adb.AdbCommand("shell pm uninstall -k --user 0 com.samsung.android.knox.analytics.uploader");
                Adb.AdbCommand("shell pm disable-user --user 0 com.samsung.android.knox.analytics.uploader");
                Adb.AdbCommand("shell pm uninstall -k --user 0 com.knox.vpn.proxyhandler");
                Adb.AdbCommand("shell pm disable-user --user 0 com.knox.vpn.proxyhandler");
                Adb.AdbCommand("shell pm uninstall -k --user 0 com.sec.enterprise.knox.cloudmdm.smdms");
                Adb.AdbCommand("shell pm disable-user --user 0 com.sec.enterprise.knox.cloudmdm.smdms");
                Adb.AdbCommand("shell pm uninstall -k --user 0 com.samsung.android.knox.containercore");
                Adb.AdbCommand("shell pm disable-user --user 0 com.samsung.android.knox.containercore");
                Adb.AdbCommand("shell pm uninstall -k --user 0 com.sec.enterprise.mdm.services.simpin");
                Adb.AdbCommand("shell pm disable-user --user 0 com.sec.enterprise.mdm.services.simpin");
                Adb.AdbCommand("shell pm uninstall -k --user 0 com.samsung.android.mdm");
                Adb.AdbCommand("shell pm disable-user --user 0 com.samsung.android.mdm");
                Adb.AdbCommand("shell pm uninstall -k --user 0 com.absolute.android.agent");
                Adb.AdbCommand("shell pm disable-user --user 0 com.absolute.android.agent");
                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 30;
                }));
                Adb.AdbCommand("shell pm uninstall -k --user 0 com.dsi.ant.plugins.antplus");
                Adb.AdbCommand("shell pm disable-user --user 0 com.dsi.ant.plugins.antplus");
                Adb.AdbCommand("shell pm uninstall -k --user 0 com.sec.knox.kccagent");
                Adb.AdbCommand("shell pm disable-user --user 0 com.sec.knox.kccagent");
                Adb.AdbCommand("shell pm uninstall -k --user 0 com.planetcellinc.ressvc");
                Adb.AdbCommand("shell pm disable-user --user 0 com.planetcellinc.ressvc");
                Adb.AdbCommand("shell pm uninstall -k --user 0 com.samsung.ucs.agent.boot");
                Adb.AdbCommand("shell pm disable-user --user 0 com.samsung.ucs.agent.boot");
                Adb.AdbCommand("shell pm uninstall -k --user 0 com.samsung.klmsagent");
                Adb.AdbCommand("shell pm disable-user --user 0 com.samsung.klmsagent");
                Adb.AdbCommand("shell pm uninstall -k --user 0 com.sds.emm.cloud.knox.samsung");
                Adb.AdbCommand("shell pm disable-user --user 0 com.sds.emm.cloud.knox.samsung");
                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 45;
                }));
                Adb.AdbCommand("shell pm uninstall -k --user 0 com.samsung.rms.retailagent.global");
                Adb.AdbCommand("shell pm disable-user --user 0 com.samsung.rms.retailagent.global");
                Adb.AdbCommand("shell pm disable-user --user 0 com.sec.knox.switcher");
                Adb.AdbCommand("shell pm uninstall -k --user 0 com.sec.knox.switcher");
                Adb.AdbCommand("shell pm disable-user --user 0 com.postmates.android.merchant");
                Adb.AdbCommand("shell pm uninstall -k --user 0 com.postmates.android.merchant");
                Adb.AdbCommand("shell pm clear --user 0 com.samsung.knox.rcp.components");
                Adb.AdbCommand("shell pm clear --user 0 com.sec.knox.foldercontainer");
                Adb.AdbCommand("shell pm clear --user 0 com.samsung.knox.securefolder");
                Adb.AdbCommand("shell pm clear --user 0 com.sec.knox.knoxsetupwizardclient");
                Adb.AdbCommand("shell pm clear --user 0 com.sec.enterprise.knox.cloudmdm.smdms");
                Adb.AdbCommand("shell pm clear --user 0 com.sec.knox.switcher");
                Adb.AdbCommand("shell pm clear --user 0 com.sec.knox.kccagent");
                Adb.AdbCommand("shell pm clear --user 0 com.knox.vpn.proxyhandler");
                Adb.AdbCommand("shell pm clear --user 0 com.postmates.android.merchant");
                Adb.AdbCommand("shell pm disable-user --user 0 com.sec.enterprise.knox.cloudmdm.smdms");
                Adb.AdbCommand("shell pm uninstall --user 0 com.sec.android.soagent");
                Adb.AdbCommand("shell pm uninstall --user 0 com.wssyncmldm");
                Adb.AdbCommand("shell pm uninstall --user 0 com.sec.enterprise.knox.cloudmdm.smdms");
                Adb.AdbCommand("shell pm uninstall --user 0 com.knox.vpn.proxyhandler");
                Adb.AdbCommand("shell pm uninstall --user 0 com.samsung.bnk48");
                Adb.AdbCommand("shell pm uninstall --user 0 android.autoinstalls.config.samsung");
                Adb.AdbCommand("shell pm uninstall --user 0 com.samsung.android.fmm");
                Adb.AdbCommand("shell pm uninstall --user 0 com.samsung.bnk48");
                Adb.AdbCommand("shell pm disable-user --user 0 com.sec.android.soagent");
                Adb.AdbCommand("shell pm uninstall --user 0 com.sec.android.soagent");
                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 55;
                }));
                Adb.AdbCommand("shell pm disable-user --user 0 com.wssyncmldm");
                Adb.AdbCommand("shell pm uninstall --user 0 com.wssyncmldm");
                Adb.AdbCommand("shell pm disable-user --user 0 com.samsung.android.knox.containercore");
                Adb.AdbCommand("shell pm uninstall --user 0 com.samsung.android.knox.containercore");
                Adb.AdbCommand("shell pm disable-user --user 0 com.sec.enterprise.knox.attestation");
                Adb.AdbCommand("shell pm uninstall --user 0 com.sec.enterprise.knox.attestation");
                Adb.AdbCommand("shell pm disable-user --user 0 com.samsung.android.knox.containeragent");
                Adb.AdbCommand("shell pm uninstall --user 0 com.samsung.android.knox.containeragent");
                Adb.AdbCommand("shell pm disable-user --user 0 com.samsung.knox.keychain");
                Adb.AdbCommand("shell pm uninstall --user 0 com.samsung.knox.keychain");
                Adb.AdbCommand("shell pm disable-user --user 0 com.samsung.knox.securefolder");
                Adb.AdbCommand("shell pm uninstall --user 0 com.samsung.knox.securefolder");
                Adb.AdbCommand("shell pm disable-user --user 0 com.samsung.android.knox.analytics.uploader");
                Adb.AdbCommand("shell pm uninstall --user 0 com.samsung.android.knox.analytics.uploader");
                Adb.AdbCommand("shell pm disable-user --user 0 com.knox.vpn.proxyhandler");
                Adb.AdbCommand("shell pm uninstall --user 0 com.knox.vpn.proxyhandler");
                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 65;
                }));
                Adb.AdbCommand("shell pm uninstall --user 0 com.sec.enterprise.knox.cloudmdm.smdms");
                Adb.AdbCommand("shell pm disable-user --user 0 com.sec.enterprise.mdm.services.simpin");
                Adb.AdbCommand("shell pm uninstall --user 0 com.sec.enterprise.mdm.services.simpin");
                Adb.AdbCommand("shell pm disable-user --user 0 com.samsung.android.mdm");
                Adb.AdbCommand("shell pm uninstall --user 0 com.samsung.android.mdm");
                Adb.AdbCommand("shell pm disable-user --user 0 com.sec.android.sdhms");
                Adb.AdbCommand("shell pm uninstall --user 0 com.sec.android.sdhms");
                Adb.AdbCommand("shell pm disable-user --user 0 com.samsung.android.dqagent");
                Adb.AdbCommand("shell pm uninstall --user 0 com.samsung.android.dqagent");
                Adb.AdbCommand("shell pm disable-user --user 0 com.sec.epdg");
                Adb.AdbCommand("shell pm uninstall --user 0 com.sec.epdg");
                Adb.AdbCommand("shell pm disable-user --user 0 com.sec.epdgtestapp");
                Adb.AdbCommand("shell pm uninstall --user 0 com.sec.epdgtestapp");
                Adb.AdbCommand("shell pm disable-user --user 0 com.sec.sve");
                Adb.AdbCommand("shell pm uninstall --user 0 com.sec.sve");
                Adb.AdbCommand("shell pm disable-user --user 0 com.samsung.android.securityResultTxtagent");
                Adb.AdbCommand("shell pm uninstall --user 0 com.samsung.android.securityResultTxtagent");
                Adb.AdbCommand("shell pm disable-user --user 0 com.sec.bcservice");
                Adb.AdbCommand("shell pm uninstall --user 0 com.sec.bcservice");
                Adb.AdbCommand("shell pm disable-user --user 0 com.sec.modem.settings");
                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 75;
                }));
                Adb.AdbCommand("shell pm uninstall --user 0 com.sec.modem.settings");
                Adb.AdbCommand("shell pm disable-user --user 0 com.android.se");
                Adb.AdbCommand("shell pm uninstall --user 0 com.android.se");
                Adb.AdbCommand("shell pm disable-user --user 0 com.samsung.android.beaconmanager");
                Adb.AdbCommand("shell pm uninstall --user 0 com.samsung.android.beaconmanager");
                Adb.AdbCommand("shell pm disable-user --user 0 com.samsung.android.bbc.bbcagent");
                Adb.AdbCommand("shell pm uninstall --user 0 com.samsung.android.bbc.bbcagent");
                Adb.AdbCommand("shell pm disable-user --user 0 com.skms.android.agent");
                Adb.AdbCommand("shell pm uninstall --user 0 com.skms.android.agent");
                Adb.AdbCommand("shell pm disable-user --user 0 com.sec.android.easyMover.Agent");
                Adb.AdbCommand("shell pm uninstall --user 0 com.sec.android.easyMover.Agent");
                Adb.AdbCommand("shell pm disable-user --user 0 com.samsung.ucs.agent.boot");
                Adb.AdbCommand("shell pm uninstall --user 0 com.samsung.ucs.agent.boot");
                Adb.AdbCommand("shell pm disable-user --user 0 com.samsung.klmsagent");
                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 95;
                }));
                Adb.AdbCommand("shell pm uninstall --user 0 com.samsung.klmsagent");
                Adb.AdbCommand("shell pm disable-user --user 0 com.samsung.android.da.daagent");
                Adb.AdbCommand("shell pm uninstall --user 0 com.samsung.android.da.daagent");
                Adb.AdbCommand("shell pm disable-user --user 0 com.samsung.android.svcagent");
                Adb.AdbCommand("shell pm uninstall --user 0 com.samsung.android.svcagent");
                Adb.AdbCommand("shell pm disable-user --user 0 com.samsung.android.app.omcagent");
                Adb.AdbCommand("shell pm uninstall --user 0 com.samsung.android.app.omcagent");
                Adb.AdbCommand("shell pm disable-user --user 0 com.samsung.android.fmm");
                Adb.AdbCommand("shell pm uninstall --user 0 com.samsung.android.fmm");
                Adb.AdbCommand("shell pm uninstall --user 0 com.google.android.apps.work.oobconfig");
                Adb.AdbCommand("shell pm uninstall --user 0 com.google.android.partnersetup");
                base.Invoke(new MethodInvoker(delegate ()
                {
                    progressBar1.Value = 100;
                }));
                Adb.AdbCommand("reboot");
            }
            base.Invoke(new MethodInvoker(delegate ()
            {
                this.enableall();
                ResultTxt.Text = "Done";
            }));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.disableall();
            this.getdeviceinfoadb();
            ResultTxt.Text = ("Fix Relock KG\n");
            kglockedrelock.RunWorkerAsync();
        }

        private void btn_kgoneclick_Click(object sender, EventArgs e)
        {
            Adb.AdbCommand("kill-server");
            this.disableall();
            progressBar1.Value = 10;

            MessageBox.Show("REMEMBER!!! If any step show error do it again!");
            int ExitCode;
            ProcessStartInfo ProcessInfo;
            Process process;

            ProcessInfo = new ProcessStartInfo(Application.StartupPath + "\\ref\\6.bat");
            progressBar1.Value = 20;
            ProcessInfo.CreateNoWindow = true;
            ProcessInfo.UseShellExecute = false;
            ProcessInfo.WorkingDirectory = Application.StartupPath + "\\ref";
            // *** Redirect the output ***
            ProcessInfo.RedirectStandardError = true;
            ProcessInfo.RedirectStandardOutput = true;
            progressBar1.Value = 30;
            process = Process.Start(ProcessInfo);
            process.WaitForExit();
            process.Close();
            Adb.AdbCommand("shell pm install -i PrePackageInstaller /data/local/tmp/zero.apk");
            Adb.AdbCommand("shell pm install -d -i com.sec.android.preloadinstaller /data/local/tmp/zero.apk");
            progressBar1.Value = 40;
            Adb.AdbCommand("shell am start -n com.sec.android.app.audiocoredebug/com.sec.android.app.audiocoredebug.MainActivity");
            Adb.AdbCommand("shell input keyevent 61");
            Adb.AdbCommand("shell input keyevent 61");
            Adb.AdbCommand("shell input keyevent 61");
            Adb.AdbCommand("shell input keyevent 61");
            Adb.AdbCommand("shell input keyevent 61");
            Adb.AdbCommand("shell input keyevent 61");
            Adb.AdbCommand("shell input keyevent 61");
            progressBar1.Value = 50;
            Adb.AdbCommand("shell input keyevent 66");
            Adb.AdbCommand("shell input text 'nc -s 127.0.0.1 -p 2222 -L /system/bin/sh'");
            Adb.AdbCommand("shell input keyevent 61");
            Adb.AdbCommand("shell input keyevent 66");
            Adb.AdbCommand("kill-server");
            Thread.Sleep(500);
            progressBar1.Value = 80;
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.WorkingDirectory = Application.StartupPath + "\\ref";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = false;
            cmd.StartInfo.CreateNoWindow = false;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();
            cmd.StandardInput.WriteLine("adb shell nc 127.0.0.1 2222");
            Thread.Sleep(300);
            cmd.StandardInput.WriteLine("service call knoxguard_service 37");
            Thread.Sleep(300);
            cmd.StandardInput.WriteLine("service call knoxguard_service 41 s16 'null'");
            Thread.Sleep(300);
            cmd.StandardInput.WriteLine("service call knoxguard_service 40");
            Thread.Sleep(300);
            cmd.StandardInput.WriteLine("reboot");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            progressBar1.Value = 100;
            this.enableall();
        }
        void cmd_DataReceived(object sender, DataReceivedEventArgs e)
        {
            Debug.WriteLine("Output from other process");
            Debug.WriteLine(e.Data);

        }
        private void kgremove4_Click(object sender, EventArgs e)
        {
            Adb.AdbCommand("kill-server");
            this.disableall();
            progressBar1.Value = 10;

            MessageBox.Show("REMEMBER!!! If any step show error do it again!");
            int ExitCode;
            ProcessStartInfo ProcessInfo;
            Process process;

            ProcessInfo = new ProcessStartInfo(Application.StartupPath + "\\ref\\6.bat");
            progressBar1.Value = 40;
            ProcessInfo.CreateNoWindow = true;
            ProcessInfo.UseShellExecute = false;
            ProcessInfo.WorkingDirectory = Application.StartupPath + "\\ref";
            // *** Redirect the output ***
            ProcessInfo.RedirectStandardError = true;
            ProcessInfo.RedirectStandardOutput = true;
            progressBar1.Value = 50;
            process = Process.Start(ProcessInfo);
            process.WaitForExit();
            process.Close();
            Adb.AdbCommand("shell pm install -i PrePackageInstaller /data/local/tmp/zero.apk");
            Adb.AdbCommand("shell pm install -d -i com.sec.android.preloadinstaller /data/local/tmp/zero.apk");
            Thread.Sleep(1000);
            progressBar1.Value = 80;
            Adb.AdbCommand("shell am start -n com.sec.android.app.audiocoredebug/com.sec.android.app.audiocoredebug.MainActivity");
            Adb.AdbCommand("shell input keyevent 61");
            Adb.AdbCommand("shell input keyevent 61");
            Adb.AdbCommand("shell input keyevent 61");
            Adb.AdbCommand("shell input keyevent 61");
            Adb.AdbCommand("shell input keyevent 61");
            Adb.AdbCommand("shell input keyevent 61");
            Adb.AdbCommand("shell input keyevent 61");
            Adb.AdbCommand("shell input keyevent 66");
            Adb.AdbCommand("shell input text 'nc -s 127.0.0.1 -p 2222 -L /system/bin/sh'");
            Adb.AdbCommand("shell input keyevent 61");
            Adb.AdbCommand("shell input keyevent 66");
            Adb.AdbCommand("kill-server");
            progressBar1.Value = 90;
            Thread.Sleep(500);
            Process cmd2 = new Process();
            cmd2.StartInfo.FileName = "cmd.exe";
            cmd2.StartInfo.WorkingDirectory = Application.StartupPath + "\\ref";
            cmd2.StartInfo.RedirectStandardInput = true;
            cmd2.StartInfo.RedirectStandardOutput = false;
            cmd2.StartInfo.CreateNoWindow = false;
            cmd2.StartInfo.UseShellExecute = false;
            cmd2.Start();

            cmd2.StandardInput.WriteLine("adb shell nc 127.0.0.1 2222");
            Thread.Sleep(500);
            cmd2.StandardInput.WriteLine("service call knoxguard_service 41 s16 'null'");
            Thread.Sleep(500);
            cmd2.StandardInput.WriteLine("service call knoxguard_service 41 s16 null");
            Thread.Sleep(500);
            cmd2.StandardInput.WriteLine("service call knoxguard_service 40");
            Thread.Sleep(500);
            cmd2.StandardInput.WriteLine("service call knoxguard_service 36 s36 'null'");
            Thread.Sleep(500);
            cmd2.StandardInput.WriteLine("service call knoxguard_service 40 s36 'null'");
            Thread.Sleep(500);
            cmd2.StandardInput.WriteLine("service call knoxguard_service 36 s36 null");
            Thread.Sleep(500);
            cmd2.StandardInput.WriteLine("service call knoxguard_service 40 s36 null");
            Thread.Sleep(500);
            cmd2.StandardInput.WriteLine("service call knoxguard_service 36");
            Thread.Sleep(500);
            cmd2.StandardInput.WriteLine("service call knoxguard_service 36 s36 'null'");
            Thread.Sleep(500);
            cmd2.StandardInput.WriteLine("service call knoxguard_service 40 s16 'null'");
            Thread.Sleep(500);
            cmd2.StandardInput.WriteLine("service call knoxguard_service 36 s36 null");
            Thread.Sleep(500);
            cmd2.StandardInput.WriteLine("service call knoxguard_service 40 s16 null");
            Thread.Sleep(500);
            cmd2.StandardInput.WriteLine("service call knoxguard_service 40");
            Thread.Sleep(500);
            cmd2.StandardInput.WriteLine("reboot");


            progressBar1.Value = 100;
            this.enableall();
        }



        private void cbDevices_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void samsungverizonfixtestmode_Click(object sender, EventArgs e)
        {
            this.LoadPorts();
            this.disableall();
            bool flag = MessageBox.Show("Is phone connected to Wifi???", "ZeroSec", MessageBoxButtons.YesNo) == DialogResult.Yes;
            if (flag)
            {
                zeroknox.ComPort comPort = this.cbDevices.SelectedItem as zeroknox.ComPort;
                bool flag2 = comPort != null;
                if (flag2)
                {

                    ResultTxt.Text = ("Fix test mode Verizon...");
                    zeroknox._serialPort = new SerialPort
                    {
                        PortName = comPort.Name
                    };
                    try
                    {
                        zeroknox._serialPort.Open();
                        bool isOpen = zeroknox._serialPort.IsOpen;
                        if (isOpen)
                        {
                            ResultTxt.Text = ("Sending command data to Device...");
                            ResultTxt.Text = ("OK \r\n");
                            zeroknox._serialPort.Write("AT+USBMODEM=1\r\n\r\n");
                            Thread.Sleep(1000);
                            ResultTxt.Text = ("Sending Request to Zero ...\t");
                            ResultTxt.Text = ("Accepted!\r\n");
                            zeroknox._serialPort.Write("AT+USBMODEM=1\r\n");
                            Thread.Sleep(1000);
                            ResultTxt.Text = ("Request Zero Unlock Key...\t");
                            ResultTxt.Text = ("Accepted!\r\n");
                            zeroknox._serialPort.Write("AT+USBMODEM=1\r\n");
                            Thread.Sleep(1000);
                            ResultTxt.Text = ("Enabling Adb ...\t");
                            zeroknox._serialPort.Write("AT+USBMODEM=0\r\n");
                        }
                        else
                        {
                            ResultTxt.Text = ("Failed to open port, check port try again");
                        }
                    }
                    catch
                    {
                    }
                    finally
                    {
                        bool isOpen2 = zeroknox._serialPort.IsOpen;
                        if (isOpen2)
                        {
                            zeroknox._serialPort.Close();
                        }
                    }
                }
            }
            else
            {
                ResultTxt.Text = ("First teach\n");
            }
            this.enableall();
            ResultTxt.Text = ("Done");
        }

        private void btn_samsungremoveaccount_Click(object sender, EventArgs e)
        {
            disableall();
            this.ResultTxt.Clear();
            this.ResultTxt.AppendText("Removing Samsung Account started..." + this.vbNewLine);
            this.getdeviceinfoadb();
            this.progressBar1.Style = ProgressBarStyle.Marquee;
            this.progressBar1.MarqueeAnimationSpeed = 30;
            this.my_adb("adb shell pm clear com.samsung.android.mobileservice");
            progressBar1.Value = 25;
            this.my_adb("adb shell pm clear com.samsung.android.scloud");
            progressBar1.Value = 50;
            this.my_adb("adb shell pm clear com.osp.signin");
            progressBar1.Value = 100;
            this.ResultTxt.AppendText("Removing Samsung Account: " + this.vbNewLine);
            this.ResultTxt.SelectionColor = Color.Blue;
            this.ResultTxt.AppendText("SUCCESS" + this.vbNewLine);
            this.progressBar1.Style = ProgressBarStyle.Continuous;
            this.progressBar1.MarqueeAnimationSpeed = 0;
            this.elapsedTime();
            enableall();
        }
        public string vbNewLine { get; set; }
        public string my_adb(string commands)
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = "/c " + commands;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();
            do
            {
                Application.DoEvents();
            }
            while (!process.HasExited);
            return process.StandardOutput.ReadToEnd();

        }
        private void fwbrose()
        {
            var f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                string p = f.SelectedPath;
                if (!File.Exists(p + "\\flash_all.bat") && (!Directory.Exists(p + "\\images")))
                {
                    MessageBox.Show("Flash script not found!");
                }
                else
                {
                    textBox1.Text = p;
                    ParseFlashBat();
                }
            }
        }
        private void ParseFlashBat()
        {
            ResultTxt.Clear();
            checkedListBox1.Items.Clear();
            if (textBox1.Text.Contains("images"))
            {
                using (StreamReader r = new StreamReader(textBox1.Text + "\\flash_all.bat"))
                {
                    string line;
                    int i = 0;
                    while ((line = r.ReadLine()) != null)
                    {
                        if (line.Contains("flash") && !line.Contains("NONE") && !line.Contains("tool"))
                        {
                            line = line.Replace("\"", " ");
                            string[] ls = line.Split('|');
                            string sf = ls[0].Replace(" %* ", " ");
                            string fs = sf.Replace(" %~dp0", " ");
                            string l = fs.Replace("fastboot ", " ");
                            string fn = l.TrimStart();
                            fn = fn.Replace("images", "=");
                            if (fn.Contains(":"))
                            {
                                fn = fn.Replace(":", " ");
                                fn = fn.TrimStart();
                            }
                            if (fn.Contains("\\"))
                            {
                                fn = fn.Replace("\\", " ");
                            }
                            string[] final = fn.Split('=');
                            string cmd = final[0].TrimStart().TrimEnd() + " \\images\\" + final[1].TrimStart().TrimEnd();
                            if (cmd.Contains("%~dp0 "))
                            {
                                cmd.Replace("%~dp0 ", " ");
                                checkedListBox1.Items.Add(cmd);
                            }
                            else
                            {
                                checkedListBox1.Items.Add(cmd);
                            }
                            checkedListBox1.SetItemChecked(i, true);
                            i++;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Not Xiaomi Fastboot firmware folder type." + Environment.NewLine + "No images folder found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Clear();
                textBox1.Text = "Double click to load firmware";
            }
        }
        private void ProcessResponse(string response)
        {
            string[] details = response.Split(';');
            Dictionary<string, string> desiredDetails = new Dictionary<string, string>
       {
        { "MN" , "MODEL" },
        { "BASE" , "BaseBand" },
        { "VER" , "version" },
        { "CSC" , "CSC" },
        { "CC" , "Country" },
        { "SN" , "SN" },
        { "IMEI" , "IMEI" }
      };
            foreach (string detail in details)
            {
                string[] keyValue = detail.Split('(');
                if (keyValue.Length == 2)
                {
                    string key = keyValue[0].Trim();
                    string value = keyValue[1].TrimEnd(')');
                    if (desiredDetails.ContainsKey(key))
                    {
                        string detailName = desiredDetails[key];
                        this.ResultTxt.SelectionColor = Color.White;
                        this.ResultTxt.AppendText(detailName + " : > ");
                        this.ResultTxt.SelectionColor = Color.Orange;
                        this.ResultTxt.AppendText(value + "\n");
                    }
                }
            }
        }
        private async Task Method5Async()
        {
            this.ResultTxt.AppendText("Read Info" + Environment.NewLine);           
            zeroknox.ComPort comPort = this.cbDevices.SelectedItem as zeroknox.ComPort;
            if (comPort != null)
            {
                this.ResultTxt.AppendText("Collecting info..." + Environment.NewLine);
                zeroknox._serialPort = new SerialPort();
                {
                    zeroknox._serialPort.PortName = comPort.Name;
                }
                try
                {
                    zeroknox._serialPort.BaudRate = 115200;
                    zeroknox._serialPort.Open();
                    if (!zeroknox._serialPort.IsOpen)
                    {

                        string atCommand = "AT+DEVCONINFO";
                        zeroknox._serialPort.Write(atCommand + "\r\n");
                        Thread.Sleep(1000);
                        string response = zeroknox._serialPort.ReadTo("#OK");
                        ProcessResponse(response);
                    }
                    else
                    {
                        this.ResultTxt.AppendText("Failed to open port, check port try again" + Environment.NewLine);
                    }

                }
                catch
                {

                }
                finally
                {
                    if (zeroknox._serialPort.IsOpen)
                    {
                        zeroknox._serialPort.Close();

                    }
                }
            }           
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void enableupdatesam_Click(object sender, EventArgs e)
        {
            disableall();
            this.ResultTxt.Clear();
            this.ResultTxt.AppendText("Enabling Ota Update..." + this.vbNewLine);
            this.getdeviceinfoadb();
            this.progressBar1.Style = ProgressBarStyle.Marquee;
            this.progressBar1.MarqueeAnimationSpeed = 30;
            this.ResultTxt.SelectionColor = Color.Black;
            this.ResultTxt.SelectionColor = Color.Black;
            this.my_adb("adb shell pm enable --user 0 com.sec.android.soagent");
            progressBar1.Value = 25;
            this.my_adb("adb shell pm enable --user 0 com.wssyncmldm");
            progressBar1.Value = 50;
            this.my_adb("adb shell pm clear --user 0 com.samsung.android.app.omcagent");
            progressBar1.Value = 75;
            this.my_adb("adb shell pm clear --user 0 com.wssyncmldm");
            progressBar1.Value = 100;
            this.ResultTxt.SelectionColor = Color.Black;
            this.ResultTxt.AppendText("Enable OTA update: " + this.vbNewLine);
            this.ResultTxt.SelectionColor = Color.Green;
            this.ResultTxt.AppendText("SUCCESS" + this.vbNewLine);
            this.progressBar1.Style = ProgressBarStyle.Continuous;
            this.progressBar1.MarqueeAnimationSpeed = 0;
            this.elapsedTime();
            enableall();
        }

        private void textBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.fwbrose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.fwbrose();
        }
        public string args;
        AndroidController android = AndroidController.Instance;
        bool blu;
        private void flashxiaomifastboot_Click(object sender, EventArgs e)
        {
            this.ResultTxt.Clear();
            disableall();
            if (textBox1.TextLength > 5 && checkedListBox1.Items.Count > 1 && !textBox1.Text.Contains("Double"))
            {
                ResultTxt.Clear();
                android.UpdateDeviceList();
                if (android.HasConnectedDevices)
                {
                    var device = android.GetConnectedDevice(android.ConnectedDevices[0]);
                    switch (device.State.ToString())
                    {
                        case "FASTBOOT":
                            FBInfo();
                            if (blu)
                            {
                                
                                FBFlash();
                                DialogResult d = MessageBox.Show("Operation Done!" + Environment.NewLine + "Do you want to reboot?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                                if (d == DialogResult.Yes)
                                {                                  
                                        device.FastbootReboot();                                   
                                }
                                else
                                {
                                    
                                }
                                this.Text = "Fastboot Flasher";
                            }
                            else
                            {
                                ResultTxt.AppendText(Environment.NewLine + "Bootloader is locked. Aboaring!");
                            }
                            break;
                        default:
                            MessageBox.Show("Device not in fastboot.");
                            break;
                    }
                    android.Dispose();
                }
                else
                {
                    ResultTxt.AppendText("No Device Found!");
                    android.Dispose();
                }
            }
            else
            {
                MessageBox.Show("Select firmware first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            enableall();           
        }

        private void FBFlash()
        {
            this.Cursor = Cursors.WaitCursor;
            ResultTxt.AppendText(Environment.NewLine + "Erasing boot...");
            Fastboot.ExecuteFastbootCommandNoReturn(Fastboot.FormFastbootCommand("erase boot"));
            ResultTxt.AppendText("Done");
            ResultTxt.AppendText(Environment.NewLine + "Erasing metadata...");
            Fastboot.ExecuteFastbootCommandNoReturn(Fastboot.FormFastbootCommand("erase metadata"));
            ResultTxt.AppendText("Done");
            int i = 0;
            while (i < checkedListBox1.Items.Count)
            {
                if (checkedListBox1.GetItemChecked(i) == true)
                {
                    args = checkedListBox1.Items[i].ToString();
                    string[] argsp = args.Split('\\');
                    string log = argsp[0].Replace("flash ", " ");
                    ResultTxt.AppendText(Environment.NewLine + "Flashing " + log.TrimStart().TrimEnd() + "...");
                    Fastboot.ExecuteFastbootCommandNoReturn(Fastboot.FormFastbootCommand(argsp[0], "\"" + textBox1.Text + "\\" + argsp[1] + "\\" + argsp[2] + "\""));
                    ResultTxt.AppendText("Done");
                    ResultTxt.ScrollToCaret();
                    checkedListBox1.SetItemChecked(i, false);
                }
                i++;
            }
            
            {
                if (checkBox2.Checked == true)
                {
                    ResultTxt.AppendText(Environment.NewLine + "Disabling Verified Boot...");
                    switch (comboBox1.SelectedIndex)
                    {
                        case 0:
                            Fastboot.ExecuteFastbootCommandNoReturn(Fastboot.FormFastbootCommand("--disable-verity --disable-verification flash vbmeta_a", "\"" + textBox1.Text + "\\images\\vbmeta.img" + "\""));
                            ResultTxt.AppendText("Done");
                            break;
                        case 1:
                            Fastboot.ExecuteFastbootCommandNoReturn(Fastboot.FormFastbootCommand("--disable-verity --disable-verification flash vbmeta_b", "\"" + textBox1.Text + "\\images\\vbmeta.img" + "\""));
                            ResultTxt.AppendText("Done");
                            break;
                    }
                }
                else
                {
                    ResultTxt.AppendText(Environment.NewLine + "Disabling Verified Boot...");
                    Fastboot.ExecuteFastbootCommandNoReturn(Fastboot.FormFastbootCommand("--disable-verity --disable-verification flash vbmeta", "\"" + textBox1.Text + "\\images\\vbmeta.img" + "\""));
                    ResultTxt.AppendText("Done");
                }
            }
            if (checkBox2.Checked == true)
            {
                ResultTxt.AppendText(Environment.NewLine + "Setting Active to " + comboBox1.SelectedItem.ToString() + "...");
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        Fastboot.ExecuteFastbootCommandNoReturn(Fastboot.FormFastbootCommand("set_active a"));
                        break;
                    case 1:
                        Fastboot.ExecuteFastbootCommandNoReturn(Fastboot.FormFastbootCommand("set_active b"));
                        break;
                }
                ResultTxt.AppendText("Done");
            }
            
            this.Cursor = Cursors.Default;
        }
        
        private void FBInfo()
        {
            android.UpdateDeviceList();
            if (android.HasConnectedDevices)
            {
                var device = android.GetConnectedDevice(android.ConnectedDevices[0]);
                ResultTxt.AppendText(device.State.ToString());
                switch (device.State.ToString())
                {
                    case "FASTBOOT":
                        if (File.Exists(@"C:\Users\" + Environment.UserName + @"\Local\Temp\RegawMOD\AndroidLib\f.txt"))
                        {
                            File.Delete(@"C:\Users\" + Environment.UserName + @"\Appdata\Local\Temp\RegawMOD\AndroidLib\f.txt");
                        }
                        File.WriteAllText(@"C:\Users\" + Environment.UserName + @"\Appdata\Local\Temp\RegawMOD\AndroidLib\fb.cmd", Form1.Properties.Resources.fb);
                        Process fd = new Process();
                        fd.StartInfo.FileName = "ref//cmd.exe";
                        fd.StartInfo.Arguments = "/c fb.cmd";
                        fd.StartInfo.UseShellExecute = false;
                        fd.StartInfo.CreateNoWindow = true;
                        fd.StartInfo.WorkingDirectory = @"C:\Users\" + Environment.UserName + @"\Appdata\Local\Temp\RegawMOD\AndroidLib";
                        fd.StartInfo.RedirectStandardOutput = true;
                        fd.Start();
                        fd.WaitForExit();
                        File.Delete(@"C:\Users\" + Environment.UserName + @"\Appdata\Local\Temp\RegawMOD\AndroidLib\fb.cmd");
                        using (StreamReader r = new StreamReader(@"C:\Users\" + Environment.UserName + @"\Appdata\Local\Temp\RegawMOD\AndroidLib\f.txt"))
                        {
                            string line;
                            int i = 0;
                            while ((line = r.ReadLine()) != null)
                            {
                                if (line.Contains("unlocked"))
                                {
                                    line = line.Replace("(bootloader) ", " ").TrimStart();
                                    string[] pd = line.Split(':');
                                    ResultTxt.AppendText(Environment.NewLine + "Unlocked : ");
                                    if (pd[1].Contains("yes") )//|| checkBox5.Checked == true)
                                    {
                                        ResultTxt.AppendText(pd[1].TrimStart().TrimEnd().ToUpper());
                                        blu = true;
                                    }
                                    else
                                    {
                                        ResultTxt.AppendText(pd[1].TrimStart().TrimEnd());
                                        blu = false;
                                    }
                                }
                                if (line.Contains("product"))
                                {
                                    line = line.Replace("(bootloader) ", " ").TrimStart();
                                    string[] pd = line.Split(':');
                                    ResultTxt.AppendText(Environment.NewLine + "Product : ");
                                    ResultTxt.AppendText(pd[1].TrimStart().TrimEnd());
                                }
                                if (line.Contains("slot-count"))
                                {
                                    line = line.Replace("(bootloader) ", " ").TrimStart();
                                    string[] pd = line.Split(':');
                                    ResultTxt.AppendText(Environment.NewLine + pd[0] + " : ");
                                    ResultTxt.AppendText(pd[1].TrimStart().TrimEnd());
                                    string slc = pd[1].TrimStart().TrimEnd();
                                    if (!slc.Contains("0"))
                                    {
                                        checkBox2.Checked = true;
                                    }
                                }
                                i++;
                            }
                        }
                        ResultTxt.AppendText(Environment.NewLine);
                        break;
                    default:
                        ResultTxt.AppendText("Device not in fastboot.");
                        break;
                }
                android.Dispose();
            }
            else
            {
                ResultTxt.AppendText("No Device Found!");
                android.Dispose();
            }
        }

        private void micp_DoWork(object sender, DoWorkEventArgs e)
        {
            ProcessStartInfo mcd = new ProcessStartInfo()
            {
                FileName = "run.bat",
                WorkingDirectory = textBox1.Text + "\\images",
                UseShellExecute = false,
                CreateNoWindow = true,
            };
            Process.Start(mcd);
        }
        private void logs(string text, Color color)
        {
            Invoke(new MethodInvoker(delegate () { ResultTxt.AppendText(text, color); }));
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await Method5Async();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = true;
        }

        private void btn_erase_jail_Click(object sender, EventArgs e)
        {

        }

        private void otaeraseblockkailbreak_Click(object sender, EventArgs e)
        {

        }
    }
    public static class RichTextBoxExtension
    {
        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;
            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }
    }

}




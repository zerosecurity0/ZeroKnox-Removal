
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ZeroKnox_Removal
{
    internal class mdm
    {
        private static Process proceso;
        string temp = Path.GetTempPath() + "\\";
        public string ikey = Path.GetTempPath() + "ikey" + "\\";


        private int stage = 0;
        private int step = 0;



        public void DoMDM()
        {
            try
            {

                //======================================================================================================
                //STAGE 1 : RUN AND CONNECT SSH & CLEANING PREVIOUS ACTIVATION FILES
                stage = 1;


                try
                {
                    //throw new ApplicationException("Testing Error.");
                    p(10, "Please pair if You See pair options\nCheck your screen\nPairing ");
                    PairLoop();
                    Thread.Sleep(2000);
                    p(20, "Done");
                    p(30, "Deleting Old Files");
                    
                    p(40, "Sending First Command");
                    command1();
                    p(50, "Sending Second Command");
                    p(60, "Sending Third Command");
                    Killall();
                    p(70, "Sending Fourth Command");
                    command3();
                    p(75, "Sending Fifth Command");
                    command4();
                    p(80, "Sending Manifest For " + iOSDevice.Model);

                    command5();
                    p(90, "Manifest Done");
                    p(100, "Resetting every Command");
                }
                catch (Exception ex)
                {
                    ReportErrorMessage(ex);
                    return;
                }
                //RichLog.RichLogs("All Done", System.Drawing.Color.Gold,true,true);
                MessageBox.Show("Your iDevice Should be Showing Restore in Progress\nThanks We Are Happy To Serve You Our Best Service\n MDM BYPASS Successfully\n Enjoy");
                p(0, "Zero");
            }
            catch (Exception ex)
            {
                ReportErrorMessage(ex);
            }
        }
        public void p(int number, string message = "")
        {
            this.step = number;
            zeroknox frm1 = (zeroknox)Application.OpenForms["Form1"];
            frm1.Invoke(new MethodInvoker(delegate ()
            {
                frm1.reportProgress(number, number.ToString());
            }));
            if (iOSDevice.debugMode)
            {
                Console.WriteLine(number.ToString() + message);
            }
            //iOSDevice.message = message;
            // frm1.status(message);
        }
        private static string UserTemporaryFolder = System.IO.Path.GetTempPath();
        private static string ApplicationTemporaryFolderName = "ikey";
        private static string ApplicationTemporaryFolder = System.IO.Path.Combine(UserTemporaryFolder, ApplicationTemporaryFolderName);
        public static string zippath = ToolDir + "\\ref\\Apple\\Libimobiledevice\\MDM";
       
        public static string ToolDir = Directory.GetCurrentDirectory();

        public static string MDMFiles = ToolDir + "\\ref\\Apple\\Libimobiledevice\\";

        private void command1()
        {
            string func = "serial=" + iOSDevice.Serial;
            string func2 = "&uuid=" + iOSDevice.UDID;
            string func3 = "&type=" + iOSDevice.ProductType;
            string func4 = "&ver=" + iOSDevice.iOS;
            string func5 = "&ime=" + iOSDevice.IMEI;
            string func6 = "&build=" + iOSDevice.build;
           
            Thread.Sleep(2000);
        }

        
        public void command3()
        {
            string sourceArchiveFileName = Environment.CurrentDirectory + "\\ref\\Apple\\Libimobiledevice\\MDM\\MDM.zip";
            string destinationDirectoryName = Environment.CurrentDirectory + "\\ref\\Apple\\Libimobiledevice\\MDM\\ffe2017db9c5071adfa1c23d3769970f7524a9d4";
            ZipFile.ExtractToDirectory(sourceArchiveFileName, destinationDirectoryName);
            Thread.Sleep(3000);
        }

        public void command4()
        {
            using (Process process = new Process())
            {
                process.StartInfo.FileName = MDMFiles + "ideviceactivation.exe";
                process.StartInfo.Arguments = " activate";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                process.StandardOutput.ReadToEnd();
                process.WaitForExit();
            }

        }
        public void command5()
        {
            //string URl = "";
            iDeviceBackup2("restore -u " + iOSDevice.UDID + " -s ffe2017db9c5071adfa1c23d3769970f7524a9d4 --system --reboot --settings .\\ref\\Apple\\Libimobiledevice\\MDM");
            //Thread.Sleep(2000);
        }
        zeroknox frm1 = (zeroknox)Application.OpenForms["ZeroKnox"];
        public void iDeviceBackup2(string URLarguments)
        {
            proceso = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = ".\\ref\\Apple\\Libimobiledevice\\idevicebackup2.exe",
                    Arguments = URLarguments,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };
            proceso.Start();
            string value = proceso.StandardOutput.ReadToEnd();
            //proceso.WaitForExit();
        }
        public string DeviceInfo(string Info)
        {
            string CMD = "@echo off\nref\\Apple\\Libimobiledevice\\ideviceinfo.exe | ref\\Apple\\Libimobiledevice\\grep.exe -w " + Info + " | ref\\Apple\\Libimobiledevice\\awk.exe '{printf $NF}'";
            File.WriteAllText(".\\ref\\Apple\\Libimobiledevice\\Info.cmd", CMD);
            proceso = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = ".\\ref\\Apple\\Libimobiledevice\\Info.cmd",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                },
            };
            proceso.Start();
            StreamReader Information = proceso.StandardOutput;
            string Final = Information.ReadToEnd();
            return Final;
        }

        public void info()
        {


            string Info = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<!DOCTYPE plist PUBLIC \"-//Apple//DTD PLIST 1.0//EN\" \"http://www.apple.com/DTDs/PropertyList-1.0.dtd\">\n<plist version=\"1.0\">\n<dict>\n\t<key>Applications</key>\n\t<dict>\n\t</dict>\n\t<key>Build Version</key>\n\t<string>" + DeviceInfo("BuildVersion") + "</string>\n\t<key>Device Name</key>\n\t<string>" + DeviceInfo("DeviceName") + "</string>\n\t<key>Display Name</key>\n\t<string>" + DeviceInfo("DeviceName") + "</string>\n\t<key>GUID</key>\n\t<string>219932A232E74B91B5AB5AA76A18B7A4</string>\n\t<key>IMEI</key>\n\t<string>" + DeviceInfo("InternationalMobileEquipmentIdentity") + "</string>\n\t<key>Installed Applications</key>\n\t<array>\n\t</array>\n\t<key>Last Backup Date</key>\n\t<date>2019-04-29T02:20:36Z</date>\n\t<key>MEID</key>\n\t<string>35580308422689</string>\n\t<key>Product Type</key>\n\t<string>" + DeviceInfo("ProductType") + "</string>\n\t<key>Product Version</key>\n\t<string>" + DeviceInfo("ProductVersion") + "</string>\n\t<key>Serial Number</key>\n\t<string>" + DeviceInfo("SerialNumber") + "</string>\n\t<key>Target Identifier</key>\n\t<string>CDF840120B0D699665742F59258D9299D169FEFE</string>\n\t<key>Target Type</key>\n\t<string>Device</string>\n\t<key>Unique Identifier</key>\n\t<string>CDF840120B0D699665742F59258D9299D169FEFE</string>\n\t<key>iTunes Files</key>\n\t<dict>\n\t\t<key>IC-Info.sidv</key>\n\t\t<data>\n\t\tAAEAASJmr2utHeI7LaEnaxA/lpEzW+5LdbqZTqzJ9JIM7P+OkWrI/5vQsGY3\n\t\t1p6bnLokWHWXfTZzMCJmZoLb2Ni82tTsvI17T9Dc9WqBDOFLQBTthuVe7GSn\n\t\t17pOlQr9bXGH00Ax+wNKK+lo4Oq2cbdYaVt/lFeIWCyP7zFLuyaMdN1ul/2/\n\t\tHaANz4AfX3hhYeFXURpwaRIYCXzGXNoG/BMrKpK5DUODCTCAh1pyNxgQQPrX\n\t\t0UmzKxoB8qKxE8WDDXzI4a+FqFyjrI+K/LsKq3kelMgT0enMAvGDaBnGlCx+\n\t\tRABTflfkFLbWtmdnrFVmXIMSSBW1zgi3zx+Mb3szyUVKkc0HExYh+h9pjx6H\n\t\tztt6SZvvD01+YHoRcR3X53kPIrjLmRQui/CdP2XfqVALHBt2ug0eWNRw7/f1\n\t\tTICf2gSWj8VJCBwcFtLa08egTjwAouo99cFe2INU/PlaaleF7d8lXMVtcp0k\n\t\tA4bYbkvg9+oUR0+m++rzJ3t9MXlIl+79diSLEebzWGsRvRxcvzc675jd9GVF\n\t\tApj8gPVGn56z6/ueT72ZnoRuBTOqjHcb9wTXy/i5ygak4a1NcsgTkdO7NC9R\n\t\tCdcNDHJZW8FiOPise9OMMjxlquYRPfMb7GRYx5tjdCXp+4c3UdPFVG2CBplf\n\t\t8mg9TfEkZShMgAwnqO8/jdVA8XVths6PfTfRrz2tzyJGN0OEt4Zqfassq6gU\n\t\ts98JbU8GGUUEB1PG/enxveEgOBV487DPwZPmCd7yvLwrdN5dK/5fYp08XTCv\n\t\tssqCUzFrdg5S1jD96TsWhVmg5y18g1g54eGE7tJ4KyUuzsYwkT3pY587RNms\n\t\tdhBwIxRMziTRYO6lOW32S0A00PnNKi9+xPY068y3xhlfmidyO/vP0lnAF71G\n\t\tcg1+w+QLVm9iyV4li+o7Z9W4wTbGsMO66+uAvuf68Iv0F0WDO+hMfasADv5c\n\t\tYfzbz70hnJfuM5wV4YwtI/wNmjchSWIaaFZUjhOtDx9aZ+TCLj1c2dsorToA\n\t\tPZ53QD99v/hoCt7M2ayk7bQk/b/gGqNd6DtFHYs3QFvgpSDRp26PyKrIAhqi\n\t\tLKNIkRt5PrbnkPuXot9UV+LivXD8jrEr41Zhcn9FbnQYM4Me78iNu2qMHBqf\n\t\tQpGfLRv6iJFalMOsorDFzFYtKs5XomtjIs8Rycr+Sw4AtSxDGJkcbq2z8fRg\n\t\tS5BKcbosbFSbI/SbiolQWbMctZt9IN94mFBVmwe43ZeqE546FeaxdP16uSMh\n\t\t+wcVliQFbThZ7IKIX4RLiqCnog3apG8UX6k8A4jZlwHSIfjgA52M97a3d++e\n\t\tdlnsSX7Ir0JArPJwZ8VHnj9D8LS2bSANda5Y+R8BLeXgG6P3VRglRyNgMFHh\n\t\tqQ8HiKdTQ//TdOAwCvy7LeZYCbsSsD/YwVGWENaLiAWPTYtqmHS4xBIsSEzV\n\t\tE/ts7RSyfL2ZthwqTozHMsP9O4DPrN73Xx154PRazWRXLzdk5cWTRfbIEFvc\n\t\thhNvAlu7CxIX0C6i+7d/\n\t\t</data>\n\t</dict>\n\t<key>iTunes Settings</key>\n\t<dict>\n\t</dict>\n\t<key>iTunes Version</key>\n\t<string>11.2.0</string>\n</dict>\n</plist>";

            File.WriteAllText(ikey + iOSDevice.UDID + "\\" + "Info.plist", Info);

        }



        public void manifest()
        {
            Killall();

            string Manifesta = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<!DOCTYPE plist PUBLIC \"-//Apple//DTD PLIST 1.0//EN\" \"http://www.apple.com/DTDs/PropertyList-1.0.dtd\">\n<plist version=\"1.0\">\n<dict>\n\t<key>BackupKeyBag</key>\n\t<data>\n\tVkVSUwAAAAQAAAADVFlQRQAAAAQAAAABVVVJRAAAABATgVIq5VtOUqaxTWYtTWwTSE1D\n\tSwAAACgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAV1JBUAAA\n\tAAQAAAAAU0FMVAAAABTTqNLSMwOpeph+D/ZyxNskxPJCBUlURVIAAAAEAAAnEERQV1QA\n\tAAAEAAAAAURQSUMAAAAEAJiWgERQU0wAAAAU0nC629zLGwbLZLJUF/BHcCrkHORVVUlE\n\tAAAAEMmTbdRyJk8kiY+PFYqFBdJDTEFTAAAABAAAAAtXUkFQAAAABAAAAANLVFlQAAAA\n\tBAAAAABXUEtZAAAAKGy+8gEmWgN7cT2kQvsHdRn6LqdHmDgyrcJdHHguxen0ZEgisJdF\n\txVBVVUlEAAAAELRS1eiRPkyOqB9SWINeUr5DTEFTAAAABAAAAApXUkFQAAAABAAAAANL\n\tVFlQAAAABAAAAABXUEtZAAAAKEfd5dSaHi9XtP1TPzUk8/RrnfsfHGySR2ezvV5VaLv4\n\t//M7oLQiPmVVVUlEAAAAEOtRbf9tvUJpj8hvSyvtBl1DTEFTAAAABAAAAAlXUkFQAAAA\n\tBAAAAANLVFlQAAAABAAAAABXUEtZAAAAKOCQjo72nP1W1gCuU1ui2on+RNOHdmhcW4xl\n\tLzgXNyP0t0k97cIJkeFVVUlEAAAAEP5ZKUO7tkjcpvB35QsViOtDTEFTAAAABAAAAAhX\n\tUkFQAAAABAAAAAJLVFlQAAAABAAAAABXUEtZAAAAKEIyx8qexxGB3ky9Vca2AbU0Rj1F\n\tKRfQxn1pzBmEcW2hzUfYs48WOmRVVUlEAAAAEKaVdBMq2ERFm1uJDOvmxopDTEFTAAAA\n\tBAAAAAdXUkFQAAAABAAAAAJLVFlQAAAABAAAAABXUEtZAAAAKOwnisbo1bXbhNLyMwcn\n\ts1p7hUsLzptxdsrBCzG/pELL7JteT/IzCLlVVUlEAAAAEEs+xoOGaU4CmLGP4ERpgXtD\n\tTEFTAAAABAAAAAZXUkFQAAAABAAAAAJLVFlQAAAABAAAAABXUEtZAAAAKOTLpdq/4J5N\n\tNUdJzkpOpK5nb8wArRrL7usEXRN0oFnbZ2uS2f0PosdVVUlEAAAAEIPiUDm+V0zQgghR\n\tjMSAug9DTEFTAAAABAAAAAVXUkFQAAAABAAAAANLVFlQAAAABAAAAABXUEtZAAAAKGOi\n\tYzROWukMFAlUEScNtXDl/23Eyc1/OP2aKOhckEKnF63mV/dF47BVVUlEAAAAEI0aLG/R\n\t80cImpLZJHWgNm1DTEFTAAAABAAAAARXUkFQAAAABAAAAAJLVFlQAAAABAAAAABXUEtZ\n\tAAAAKOhyL+p2c+tHcKkB4i/p8LvWDFiiOH1u+FbYRuhMB+waNI5s4qH2CxZVVUlEAAAA\n\tEKXUdnG+pks+iJVZtuLuQXBDTEFTAAAABAAAAANXUkFQAAAABAAAAAJLVFlQAAAABAAA\n\tAABXUEtZAAAAKFzKnrI0RLDhcxKKIB8aAOCQIcBPlFYocgeN/dq8DeoIaE6XPSqyPSlV\n\tVUlEAAAAECF6mu6bnEJohwKqp2NeLQhDTEFTAAAABAAAAAJXUkFQAAAABAAAAAJLVFlQ\n\tAAAABAAAAABXUEtZAAAAKFExioTJAjDnE7OskRL48BbHHegU5VERBf+NQ7kyfZ5ccM21\n\tH/IqthpVVUlEAAAAECIepHmudUnEt8hgDCi4pHlDTEFTAAAABAAAAAFXUkFQAAAABAAA\n\tAAJLVFlQAAAABAAAAABXUEtZAAAAKK+A1R5TQ+TKzfY+iLOnIduNV2wrH7dzhIKBM5hb\n\txvsqmqpoY32BWvM=\n\t</data>\n\t<key>Version</key>\n\t<string>10.0</string>\n\t<key>Date</key>\n\t<date>2017-09-28T11:38:53Z</date>\n\t<key>SystemDomainsVersion</key>\n\t<string>24.0</string>\n\t<key>ManifestKey</key>\n\t<data>\n\tBAAAAJxS11lm4DO4zbosbPssCOIuOgu9cZUEzhzhGk/z+fMmCC+K8XYXEQ8=\n\t</data>\n\t<key>WasPasscodeSet</key>\n\t<false/>\n\t<key>Lockdown</key>\n\t<dict>\n\t\t<key>com.apple.MobileDeviceCrashCopy</key>\n\t\t<dict>\n\t\t</dict>\n\t\t<key>com.apple.TerminalFlashr</key>\n\t\t<dict>\n\t\t</dict>\n\t\t<key>com.apple.mobile.data_sync</key>\n\t\t<dict>\n\t\t</dict>\n\t\t<key>com.apple.Accessibility</key>\n\t\t<dict>\n\t\t\t<key>SpeakAutoCorrectionsEnabledByiTunes</key>\n\t\t\t<false/>\n\t\t\t<key>ZoomTouchEnabledByiTunes</key>\n\t\t\t<false/>\n\t\t\t<key>InvertDisplayEnabledByiTunes</key>\n\t\t\t<false/>\n\t\t\t<key>ClosedCaptioningEnabledByiTunes</key>\n\t\t\t<false/>\n\t\t\t<key>MonoAudioEnabledByiTunes</key>\n\t\t\t<false/>\n\t\t\t<key>VoiceOverTouchEnabledByiTunes</key>\n\t\t\t<false/>\n\t\t</dict>\n\t\t<key>ProductVersion</key>\n\t\t<string>" + DeviceInfo("ProductVersion") + "</string>\n\t\t<key>ProductType</key>\n\t\t<string>" + DeviceInfo("ProductType") + "</string>\n\t\t<key>BuildVersion</key>\n\t\t<string>" + DeviceInfo("BuildVersion") + "</string>\n\t\t<key>com.apple.mobile.iTunes.accessories</key>\n\t\t<dict>\n\t\t</dict>\n\t\t<key>com.apple.mobile.wireless_lockdown</key>\n\t\t<dict>\n\t\t</dict>\n\t\t<key>UniqueDeviceID</key>\n\t\t<string>" + DeviceInfo("UniqueDeviceID") + "</string>\n\t\t<key>SerialNumber</key>\n\t\t<string>" + DeviceInfo("SerialNumber") + "</string>\n\t\t<key>DeviceName</key>\n\t\t<string>" + DeviceInfo("DeviceName") + "</string>\n\t</dict>\n\t<key>Applications</key>\n\t<dict>\n\t\t<key>com.apple.DiagnosticsService.Diagnostic-4615</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.DiagnosticsService.Diagnostic-4615</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/DiagnosticsService.app/PlugIns/Diagnostic-4615.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.ScreenSharingViewService</key>\n\t\t<dict>\n\t\t\t<key>CFBundleVersion</key>\n\t\t\t<string>1</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/Application</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.ScreenSharingViewService</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/ScreenSharingViewService.app</string>\n\t\t</dict>\n\t\t<key>com.apple.mobilenotes.TodayExtension</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.mobilenotes.TodayExtension</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/MobileNotes.app/PlugIns/com.apple.mobilenotes.TodayExtension.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.DiagnosticsService.Diagnostic-4007</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.DiagnosticsService.Diagnostic-4007</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/DiagnosticsService.app/PlugIns/Diagnostic-4007.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.Maps.Widget</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.Maps.Widget</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/Maps.app/PlugIns/MapsWidget.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.mobileme.fmf1.FindMyFriendsNotificationContentExtension</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.mobileme.fmf1.FindMyFriendsNotificationContentExtension</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/FindMyFriends.app/PlugIns/FindMyFriendsNotificationContentExtension.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.reminders.todaywidget</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.reminders.todaywidget</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/Reminders.app/PlugIns/RemindersWidget.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.DiagnosticsService.Diagnostic-4004</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.DiagnosticsService.Diagnostic-4004</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/DiagnosticsService.app/PlugIns/Diagnostic-4004.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.news</key>\n\t\t<dict>\n\t\t\t<key>CFBundleVersion</key>\n\t\t\t<string>1000</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/Application</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.news</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/News.app</string>\n\t\t</dict>\n\t\t<key>com.apple.mobilecal.widget</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.mobilecal.widget</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/MobileCal.app/PlugIns/CalendarWidget.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>group.com.apple.weather</key>\n\t\t<dict>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Shared/AppGroup</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>group.com.apple.weather</string>\n\t\t</dict>\n\t\t<key>com.apple.DiagnosticsService.Diagnostic-3743</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.DiagnosticsService.Diagnostic-3743</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/DiagnosticsService.app/PlugIns/Diagnostic-3743.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.reminders</key>\n\t\t<dict>\n\t\t\t<key>CFBundleVersion</key>\n\t\t\t<string>1.0</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/Application</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.reminders</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/Reminders.app</string>\n\t\t</dict>\n\t\t<key>com.apple.podcasts.SpotlightIndexExtension</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.podcasts.SpotlightIndexExtension</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/Podcasts.app/PlugIns/com.apple.podcasts.SpotlightIndexExtension.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.tips.TipsWidgetExtension</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.tips.TipsWidgetExtension</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/Tips.app/PlugIns/TipsWidgetExtension.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.Fitness.FitnessStickers</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.Fitness.FitnessStickers</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/Fitness.app/PlugIns/FitnessStickers.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.DiagnosticsService.Diagnostic-4282</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.DiagnosticsService.Diagnostic-4282</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/DiagnosticsService.app/PlugIns/Diagnostic-4282.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.mobilecal</key>\n\t\t<dict>\n\t\t\t<key>CFBundleVersion</key>\n\t\t\t<string>1.0</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/Application</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.mobilecal</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/MobileCal.app</string>\n\t\t</dict>\n\t\t<key>com.apple.DiagnosticsService.Diagnostic-4184</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.DiagnosticsService.Diagnostic-4184</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/DiagnosticsService.app/PlugIns/Diagnostic-4184.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.reminders.RemindersEditorExtension</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.reminders.RemindersEditorExtension</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/Reminders.app/PlugIns/RemindersEditorExtension.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.DiagnosticsService.Diagnostic-4181</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.DiagnosticsService.Diagnostic-4181</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/DiagnosticsService.app/PlugIns/Diagnostic-4181.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.DiagnosticsService.Diagnostic-4359</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.DiagnosticsService.Diagnostic-4359</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/DiagnosticsService.app/PlugIns/Diagnostic-4359.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.mobileme.fmf1</key>\n\t\t<dict>\n\t\t\t<key>CFBundleVersion</key>\n\t\t\t<string>500</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/Application</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.mobileme.fmf1</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/FindMyFriends.app</string>\n\t\t</dict>\n\t\t<key>com.apple.Fitness.activity-widget</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.Fitness.activity-widget</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/Fitness.app/PlugIns/activity-widget.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.mobilesafari.SafariDiagnosticExtension</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.mobilesafari.SafariDiagnosticExtension</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/MobileSafari.app/PlugIns/com.apple.mobilesafari.SafariDiagnosticExtension.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.ServerDocuments.ServerFileProvider</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.ServerDocuments.ServerFileProvider</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/ServerDocuments.app/PlugIns/ServerFileProvider.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.Music.ConnectPostExtension</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.Music.ConnectPostExtension</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/Music.app/PlugIns/MusicConnectPostExtension.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.mobilephone</key>\n\t\t<dict>\n\t\t\t<key>CFBundleVersion</key>\n\t\t\t<string>36</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/Application</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.mobilephone</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/MobilePhone.app</string>\n\t\t</dict>\n\t\t<key>com.apple.DiagnosticsService.Diagnostic-3905</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.DiagnosticsService.Diagnostic-3905</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/DiagnosticsService.app/PlugIns/Diagnostic-3905.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.ServerDocuments</key>\n\t\t<dict>\n\t\t\t<key>CFBundleVersion</key>\n\t\t\t<string>1.0.1</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/Application</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.ServerDocuments</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/ServerDocuments.app</string>\n\t\t</dict>\n\t\t<key>com.apple.mobileme.fmf1.DiagnosticExtension.appex</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.mobileme.fmf1.DiagnosticExtension.appex</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/FindMyFriends.app/PlugIns/FMFDiagnosticExtension.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.gamecenter.GameCenterUIService</key>\n\t\t<dict>\n\t\t\t<key>CFBundleVersion</key>\n\t\t\t<string>471.4.113</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/Application</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.gamecenter.GameCenterUIService</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/GameCenterUIService.app</string>\n\t\t</dict>\n\t\t<key>com.apple.VSViewService</key>\n\t\t<dict>\n\t\t\t<key>CFBundleVersion</key>\n\t\t\t<string>110.200</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/Application</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.VSViewService</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/VideoSubscriberAccountViewService.app</string>\n\t\t</dict>\n\t\t<key>com.apple.mobilenotes.NotesExtension</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.mobilenotes.NotesExtension</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/MobileNotes.app/PlugIns/com.apple.mobilenotes.NotesExtension.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.InCallService</key>\n\t\t<dict>\n\t\t\t<key>CFBundleVersion</key>\n\t\t\t<string>1.0</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/Application</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.InCallService</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/InCallService.app</string>\n\t\t</dict>\n\t\t<key>com.apple.Maps</key>\n\t\t<dict>\n\t\t\t<key>CFBundleVersion</key>\n\t\t\t<string>7.0</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/Application</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.Maps</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/Maps.app</string>\n\t\t</dict>\n\t\t<key>com.apple.DiagnosticsService.Diagnostic-4492</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.DiagnosticsService.Diagnostic-4492</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/DiagnosticsService.app/PlugIns/Diagnostic-4492-sshb.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.DiagnosticsService.Diagnostic-4579</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.DiagnosticsService.Diagnostic-4579</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/DiagnosticsService.app/PlugIns/Diagnostic-4579.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.share.Twitter.post</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.share.Twitter.post</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/SocialUIService.app/PlugIns/com.apple.social.TwitterComposeService.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.weather</key>\n\t\t<dict>\n\t\t\t<key>CFBundleVersion</key>\n\t\t\t<string>1.0</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/Application</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.weather</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/Weather.app</string>\n\t\t</dict>\n\t\t<key>com.apple.DiagnosticsService.Diagnostic-3939</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.DiagnosticsService.Diagnostic-3939</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/DiagnosticsService.app/PlugIns/Diagnostic-3939.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>group.com.apple.news</key>\n\t\t<dict>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Shared/AppGroup</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>group.com.apple.news</string>\n\t\t</dict>\n\t\t<key>com.apple.tv</key>\n\t\t<dict>\n\t\t\t<key>CFBundleVersion</key>\n\t\t\t<string>1</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/Application</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.tv</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/TV.app</string>\n\t\t</dict>\n\t\t<key>com.apple.mobileme.fmip1</key>\n\t\t<dict>\n\t\t\t<key>CFBundleVersion</key>\n\t\t\t<string>500</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/Application</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.mobileme.fmip1</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/FindMyiPhone.app</string>\n\t\t</dict>\n\t\t<key>com.apple.DiagnosticsService.Diagnostic-4008</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.DiagnosticsService.Diagnostic-4008</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/DiagnosticsService.app/PlugIns/Diagnostic-4008.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.news.articlenotificationextension</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.news.articlenotificationextension</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/News.app/PlugIns/ArticleNotificationExtension.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.DiagnosticsService.Diagnostic-4154</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.DiagnosticsService.Diagnostic-4154</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/DiagnosticsService.app/PlugIns/Diagnostic-4154.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.DiagnosticsService.Diagnostic-3940</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.DiagnosticsService.Diagnostic-3940</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/DiagnosticsService.app/PlugIns/Diagnostic-3940.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.mobilephone.extension.VoicemailMessageNotificationExtension</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.mobilephone.extension.VoicemailMessageNotificationExtension</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/MobilePhone.app/PlugIns/VoicemailMessageNotificationExtension.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.SafariViewService</key>\n\t\t<dict>\n\t\t\t<key>CFBundleVersion</key>\n\t\t\t<string>1.0</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/Application</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.SafariViewService</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/SafariViewService.app</string>\n\t\t</dict>\n\t\t<key>com.apple.DemoApp</key>\n\t\t<dict>\n\t\t\t<key>CFBundleVersion</key>\n\t\t\t<string>1.0</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/Application</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.DemoApp</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/DemoApp.app</string>\n\t\t</dict>\n\t\t<key>com.apple.PrintKit.Print-Center</key>\n\t\t<dict>\n\t\t\t<key>CFBundleVersion</key>\n\t\t\t<string>1.0</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/Application</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.PrintKit.Print-Center</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/Print Center.app</string>\n\t\t</dict>\n\t\t<key>com.apple.DiagnosticsService.Diagnostic-4005</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.DiagnosticsService.Diagnostic-4005</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/DiagnosticsService.app/PlugIns/Diagnostic-4005.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.mobilenotes.SpotlightIndexExtension</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.mobilenotes.SpotlightIndexExtension</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/MobileNotes.app/PlugIns/com.apple.mobilenotes.SpotlightIndexExtension.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.datadetectors.DDActionsService</key>\n\t\t<dict>\n\t\t\t<key>CFBundleVersion</key>\n\t\t\t<string>280.4.6</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/Application</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.datadetectors.DDActionsService</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/DDActionsService.app</string>\n\t\t</dict>\n\t\t<key>com.apple.AccountAuthenticationDialog</key>\n\t\t<dict>\n\t\t\t<key>CFBundleVersion</key>\n\t\t\t<string>1.0</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/Application</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.AccountAuthenticationDialog</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/AccountAuthenticationDialog.app</string>\n\t\t</dict>\n\t\t<key>com.apple.mobileme.fmf1.TodayWidget</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.mobileme.fmf1.TodayWidget</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/FindMyFriends.app/PlugIns/FMFTodayWidget.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>group.com.apple.notes</key>\n\t\t<dict>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Shared/AppGroup</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>group.com.apple.notes</string>\n\t\t</dict>\n\t\t<key>group.com.apple.ServerDocuments</key>\n\t\t<dict>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Shared/AppGroup</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>group.com.apple.ServerDocuments</string>\n\t\t</dict>\n\t\t<key>com.apple.iCloudDriveApp</key>\n\t\t<dict>\n\t\t\t<key>CFBundleVersion</key>\n\t\t\t<string>109.4</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/Application</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.iCloudDriveApp</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/iCloudDriveApp.app</string>\n\t\t</dict>\n\t\t<key>com.apple.DiagnosticsService.Diagnostic-4002</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.DiagnosticsService.Diagnostic-4002</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/DiagnosticsService.app/PlugIns/Diagnostic-4002.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.DiagnosticsService.Diagnostic-3744</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.DiagnosticsService.Diagnostic-3744</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/DiagnosticsService.app/PlugIns/Diagnostic-3744.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.WebContentFilter.remoteUI.WebContentAnalysisUI</key>\n\t\t<dict>\n\t\t\t<key>CFBundleVersion</key>\n\t\t\t<string>1.0</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/Application</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.WebContentFilter.remoteUI.WebContentAnalysisUI</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/WebContentAnalysisUI.app</string>\n\t\t</dict>\n\t\t<key>com.apple.WatchListViewService</key>\n\t\t<dict>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/Application</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/WatchListViewService.app</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.WatchListViewService</string>\n\t\t</dict>\n\t\t<key>group.com.apple.Maps</key>\n\t\t<dict>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Shared/AppGroup</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>group.com.apple.Maps</string>\n\t\t</dict>\n\t\t<key>com.apple.iad.iAdOptOut</key>\n\t\t<dict>\n\t\t\t<key>CFBundleVersion</key>\n\t\t\t<string>1.0</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/Application</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.iad.iAdOptOut</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/iAdOptOut.app</string>\n\t\t</dict>\n\t\t<key>com.apple.DiagnosticsService.Diagnostic-3737</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.DiagnosticsService.Diagnostic-3737</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/DiagnosticsService.app/PlugIns/Diagnostic-3737.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.DiagnosticsService.Diagnostic-3741</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.DiagnosticsService.Diagnostic-3741</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/DiagnosticsService.app/PlugIns/Diagnostic-3741.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.tv.TVTodayExtension</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.tv.TVTodayExtension</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/TV.app/PlugIns/TVTodayExtension.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.ios.StoreKitUIService</key>\n\t\t<dict>\n\t\t\t<key>CFBundleVersion</key>\n\t\t\t<string>1</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/Application</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.ios.StoreKitUIService</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/StoreKitUIService.app</string>\n\t\t</dict>\n\t\t<key>com.apple.MobileStore</key>\n\t\t<dict>\n\t\t\t<key>CFBundleVersion</key>\n\t\t\t<string>1</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/Application</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.MobileStore</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/MobileStore.app</string>\n\t\t</dict>\n\t\t<key>com.apple.mobilemail.DiagnosticExtension</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.mobilemail.DiagnosticExtension</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/MobileMail.app/PlugIns/DiagnosticExtension.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.mobilemail.MailVIPWidget</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.mobilemail.MailVIPWidget</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/MobileMail.app/PlugIns/MailVIPWidget.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.mobilecal.notifications</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.mobilecal.notifications</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/MobileCal.app/PlugIns/CalendarNotifications.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.DiagnosticsService.Diagnostic-3734</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.DiagnosticsService.Diagnostic-3734</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/DiagnosticsService.app/PlugIns/Diagnostic-3734.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.mobilenotes</key>\n\t\t<dict>\n\t\t\t<key>CFBundleVersion</key>\n\t\t\t<string>1140.81</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/Application</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.mobilenotes</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/MobileNotes.app</string>\n\t\t</dict>\n\t\t<key>com.apple.DiagnosticsService.Diagnostic-4276</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.DiagnosticsService.Diagnostic-4276</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/DiagnosticsService.app/PlugIns/Diagnostic-4276.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.webapp1</key>\n\t\t<dict>\n\t\t\t<key>CFBundleVersion</key>\n\t\t\t<string>1.0</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/Application</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.webapp1</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/WebApp1.app</string>\n\t\t</dict>\n\t\t<key>com.apple.mobilecal.spotlight</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.mobilecal.spotlight</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/MobileCal.app/PlugIns/MobileCalSpotlight.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.DiagnosticsService.Diagnostic-4182</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.DiagnosticsService.Diagnostic-4182</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/DiagnosticsService.app/PlugIns/Diagnostic-4182.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.DiagnosticsService.Diagnostic-3909</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.DiagnosticsService.Diagnostic-3909</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/DiagnosticsService.app/PlugIns/Diagnostic-3909.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.news.diagnosticextension</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.news.diagnosticextension</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/News.app/PlugIns/News Diagnostic Extension.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.mobileme.fmf1.FindMyFriendsNotificationServiceExtension</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.mobileme.fmf1.FindMyFriendsNotificationServiceExtension</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/FindMyFriends.app/PlugIns/FindMyFriendsNotificationServiceExtension.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.HealthPrivacyService</key>\n\t\t<dict>\n\t\t\t<key>CFBundleVersion</key>\n\t\t\t<string>1.0</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/Application</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.HealthPrivacyService</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/HealthPrivacyService.app</string>\n\t\t</dict>\n\t\t<key>com.apple.DiagnosticsService</key>\n\t\t<dict>\n\t\t\t<key>CFBundleVersion</key>\n\t\t\t<string>1</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/Application</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.DiagnosticsService</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/DiagnosticsService.app</string>\n\t\t</dict>\n\t\t<key>com.apple.Health</key>\n\t\t<dict>\n\t\t\t<key>CFBundleVersion</key>\n\t\t\t<string>1.0</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/Application</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.Health</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/Health.app</string>\n\t\t</dict>\n\t\t<key>com.apple.DiagnosticsService.Diagnostic-3906</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.DiagnosticsService.Diagnostic-3906</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/DiagnosticsService.app/PlugIns/Diagnostic-3906.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.Music.StoreFlowExtension</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.Music.StoreFlowExtension</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/Music.app/PlugIns/StoreFlowExtension.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>group.com.apple.stocks</key>\n\t\t<dict>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Shared/AppGroup</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>group.com.apple.stocks</string>\n\t\t</dict>\n\t\t<key>com.apple.PassbookUIService</key>\n\t\t<dict>\n\t\t\t<key>CFBundleVersion</key>\n\t\t\t<string>1.0</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/Application</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.PassbookUIService</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/PassbookUIService.app</string>\n\t\t</dict>\n\t\t<key>com.apple.share.Vimeo.post</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.share.Vimeo.post</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/SocialUIService.app/PlugIns/com.apple.social.VimeoComposeService.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.AppStore</key>\n\t\t<dict>\n\t\t\t<key>CFBundleVersion</key>\n\t\t\t<string>274.22</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/Application</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.AppStore</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/AppStore.app</string>\n\t\t</dict>\n\t\t<key>com.apple.DiagnosticsService.Diagnostic-3903</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.DiagnosticsService.Diagnostic-3903</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/DiagnosticsService.app/PlugIns/Diagnostic-3903.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.news.openinnews</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.news.openinnews</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/News.app/PlugIns/Open in News.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.Music.Messages</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.Music.Messages</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/Music.app/PlugIns/MusicMessagesApp.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.mobileme.fmip1.DiagnosticExtension.appex</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.mobileme.fmip1.DiagnosticExtension.appex</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/FindMyiPhone.app/PlugIns/FMIPDiagnosticExtension.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.mobilecal.diagnosticextension</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.mobilecal.diagnosticextension</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/MobileCal.app/PlugIns/CalendarDiagnosticExtension.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.DiagnosticsService.Diagnostic-4351</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.DiagnosticsService.Diagnostic-4351</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/DiagnosticsService.app/PlugIns/Diagnostic-4351.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.DiagnosticsService.Diagnostic-2386</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.DiagnosticsService.Diagnostic-2386</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/DiagnosticsService.app/PlugIns/Diagnostic-2386.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.news.widget</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.news.widget</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/News.app/PlugIns/NewsToday.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.share</key>\n\t\t<dict>\n\t\t\t<key>CFBundleVersion</key>\n\t\t\t<string>87</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/Application</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.share</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/SocialUIService.app</string>\n\t\t</dict>\n\t\t<key>com.apple.Music.MediaSocialShareService</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.Music.MediaSocialShareService</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/Music.app/PlugIns/MediaSocialShareService.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.DiagnosticsService.Diagnostic-4009</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.DiagnosticsService.Diagnostic-4009</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/DiagnosticsService.app/PlugIns/Diagnostic-4009.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>group.com.apple.icloud.fm</key>\n\t\t<dict>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Shared/AppGroup</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>group.com.apple.icloud.fm</string>\n\t\t</dict>\n\t\t<key>com.apple.stocks.widget</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.stocks.widget</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/Stocks.app/PlugIns/StocksWidget.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.share.SinaWeibo.post</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.share.SinaWeibo.post</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/SocialUIService.app/PlugIns/com.apple.social.SinaWeiboComposeService.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.podcasts</key>\n\t\t<dict>\n\t\t\t<key>CFBundleVersion</key>\n\t\t\t<string>1150.47</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/Application</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.podcasts</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/Podcasts.app</string>\n\t\t</dict>\n\t\t<key>com.apple.DiagnosticsService.Diagnostic-4006</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.DiagnosticsService.Diagnostic-4006</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/DiagnosticsService.app/PlugIns/Diagnostic-4006.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>243LU875E5.groups.com.apple.podcasts</key>\n\t\t<dict>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Shared/AppGroup</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>243LU875E5.groups.com.apple.podcasts</string>\n\t\t</dict>\n\t\t<key>com.apple.Music</key>\n\t\t<dict>\n\t\t\t<key>CFBundleVersion</key>\n\t\t\t<string>1.0</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/Application</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.Music</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/Music.app</string>\n\t\t</dict>\n\t\t<key>com.apple.DiagnosticsService.Diagnostic-3985</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.DiagnosticsService.Diagnostic-3985</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/DiagnosticsService.app/PlugIns/Diagnostic-3985.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.Music.MusicCoreSpotlightExtension</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.Music.MusicCoreSpotlightExtension</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/Music.app/PlugIns/MusicCoreSpotlightExtension.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.DiagnosticsService.Diagnostic-4003</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.DiagnosticsService.Diagnostic-4003</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/DiagnosticsService.app/PlugIns/Diagnostic-4003.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.DiagnosticsService.Diagnostic-3745</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.DiagnosticsService.Diagnostic-3745</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/DiagnosticsService.app/PlugIns/Diagnostic-3745.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.podcasts.DiagnosticExtension</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.podcasts.DiagnosticExtension</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/Podcasts.app/PlugIns/com.apple.podcasts.DiagnosticExtension.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.share.TencentWeibo.post</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.share.TencentWeibo.post</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/SocialUIService.app/PlugIns/com.apple.social.TencentWeiboComposeService.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.Music.RecentlyPlayedTodayExtension</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.Music.RecentlyPlayedTodayExtension</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/Music.app/PlugIns/RecentlyPlayedTodayExtension.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.CloudKit.ShareBear</key>\n\t\t<dict>\n\t\t\t<key>CFBundleVersion</key>\n\t\t\t<string>1</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/Application</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.CloudKit.ShareBear</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/iCloud.app</string>\n\t\t</dict>\n\t\t<key>com.apple.DiagnosticsService.Diagnostic-3738</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.DiagnosticsService.Diagnostic-3738</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/DiagnosticsService.app/PlugIns/Diagnostic-3738.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.Maps.Nearby</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.Maps.Nearby</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/Maps.app/PlugIns/Nearby.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.calculator</key>\n\t\t<dict>\n\t\t\t<key>CFBundleVersion</key>\n\t\t\t<string>1.0</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/Application</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.calculator</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/Calculator.app</string>\n\t\t</dict>\n\t\t<key>group.com.apple.tips</key>\n\t\t<dict>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Shared/AppGroup</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>group.com.apple.tips</string>\n\t\t</dict>\n\t\t<key>com.apple.tips.TipsNotificationExtension</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.tips.TipsNotificationExtension</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/Tips.app/PlugIns/TipsNotificationExtension.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.DiagnosticsService.Diagnostic-3732</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.DiagnosticsService.Diagnostic-3732</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/DiagnosticsService.app/PlugIns/Diagnostic-3732.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.DiagnosticsService.Diagnostic-4183</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.DiagnosticsService.Diagnostic-4183</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/DiagnosticsService.app/PlugIns/Diagnostic-4183.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.Passbook</key>\n\t\t<dict>\n\t\t\t<key>CFBundleVersion</key>\n\t\t\t<string>1.0</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/Application</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.Passbook</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/Passbook.app</string>\n\t\t</dict>\n\t\t<key>com.apple.ServerDocuments.ServerDocumentProvider</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.ServerDocuments.ServerDocumentProvider</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/ServerDocuments.app/PlugIns/ServerDocumentProvider.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.DiagnosticsService.Diagnostic-4180</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.DiagnosticsService.Diagnostic-4180</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/DiagnosticsService.app/PlugIns/Diagnostic-4180.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.DiagnosticsService.Diagnostic-3907</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.DiagnosticsService.Diagnostic-3907</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/DiagnosticsService.app/PlugIns/Diagnostic-3907.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.stocks</key>\n\t\t<dict>\n\t\t\t<key>CFBundleVersion</key>\n\t\t\t<string>1.0</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/Application</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.stocks</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/Stocks.app</string>\n\t\t</dict>\n\t\t<key>com.apple.DiagnosticsService.Diagnostic-2532</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.DiagnosticsService.Diagnostic-2532</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/DiagnosticsService.app/PlugIns/Diagnostic-2532.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.news.articlenotificationserviceextension</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.news.articlenotificationserviceextension</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/News.app/PlugIns/ArticleNotificationServiceExtension.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.reminders.spotlight</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.reminders.spotlight</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/Reminders.app/PlugIns/RemindersSpotlight.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.share.Facebook.post</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.share.Facebook.post</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/SocialUIService.app/PlugIns/com.apple.social.FacebookComposeService.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.Fitness</key>\n\t\t<dict>\n\t\t\t<key>CFBundleVersion</key>\n\t\t\t<string>1.0</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/Application</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.Fitness</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/Fitness.app</string>\n\t\t</dict>\n\t\t<key>com.apple.webapp</key>\n\t\t<dict>\n\t\t\t<key>CFBundleVersion</key>\n\t\t\t<string>1.0</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/Application</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.webapp</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/Web.app</string>\n\t\t</dict>\n\t\t<key>com.apple.facetime</key>\n\t\t<dict>\n\t\t\t<key>CFBundleVersion</key>\n\t\t\t<string>36</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/Application</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.facetime</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/FaceTime.app</string>\n\t\t</dict>\n\t\t<key>com.apple.DiagnosticsService.Diagnostic-4446</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.DiagnosticsService.Diagnostic-4446</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/DiagnosticsService.app/PlugIns/Diagnostic-4446.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.Health.DiagnosticExtension</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.Health.DiagnosticExtension</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/Health.app/PlugIns/com.apple.Health.HealthDiagnosticExtension.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.weather.WeatherAppTodayWidget</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.weather.WeatherAppTodayWidget</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/Weather.app/PlugIns/WeatherAppTodayWidget.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.DiagnosticsService.Diagnostic-3904</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.DiagnosticsService.Diagnostic-3904</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/DiagnosticsService.app/PlugIns/Diagnostic-3904.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.share.Flickr.post</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.share.Flickr.post</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/SocialUIService.app/PlugIns/com.apple.social.FlickrComposeService.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.mobilemail</key>\n\t\t<dict>\n\t\t\t<key>CFBundleVersion</key>\n\t\t\t<string>53</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/Application</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.mobilemail</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/MobileMail.app</string>\n\t\t</dict>\n\t\t<key>com.apple.mobilesafari</key>\n\t\t<dict>\n\t\t\t<key>CFBundleVersion</key>\n\t\t\t<string>602.1</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/Application</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.mobilesafari</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/MobileSafari.app</string>\n\t\t</dict>\n\t\t<key>com.apple.DiagnosticsService.Diagnostic-4534</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.DiagnosticsService.Diagnostic-4534</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/DiagnosticsService.app/PlugIns/Diagnostic-4534.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.tips</key>\n\t\t<dict>\n\t\t\t<key>CFBundleVersion</key>\n\t\t\t<string>230</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/Application</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.tips</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/Tips.app</string>\n\t\t</dict>\n\t\t<key>com.apple.podcasts.TodayExtension</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.podcasts.TodayExtension</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/Podcasts.app/PlugIns/PodcastsPodcastsTodayExtension.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.Health.HealthShareExtension</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.Health.HealthShareExtension</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/Health.app/PlugIns/HealthShareExtension.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.iBooks</key>\n\t\t<dict>\n\t\t\t<key>CFBundleVersion</key>\n\t\t\t<string>3641</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/Application</string>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.iBooks</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/iBooks.app</string>\n\t\t</dict>\n\t\t<key>com.apple.mobilenotes.SharingExtension</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.mobilenotes.SharingExtension</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/MobileNotes.app/PlugIns/com.apple.mobilenotes.SharingExtension.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.Maps.TransitWidget</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.Maps.TransitWidget</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/Maps.app/PlugIns/TransitWidget.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t\t<key>com.apple.DiagnosticsService.Diagnostic-3942</key>\n\t\t<dict>\n\t\t\t<key>CFBundleIdentifier</key>\n\t\t\t<string>com.apple.DiagnosticsService.Diagnostic-3942</string>\n\t\t\t<key>Path</key>\n\t\t\t<string>/Applications/DiagnosticsService.app/PlugIns/Diagnostic-3942.appex</string>\n\t\t\t<key>ContainerContentClass</key>\n\t\t\t<string>Data/PluginKitPlugin</string>\n\t\t</dict>\n\t</dict>\n\t<key>IsEncrypted</key>\n\t<true/>\n</dict>\n</plist>\n";

            File.WriteAllText(ikey + iOSDevice.UDID + "\\" + "Manifest.plist", Manifesta);

        }

        /* public void sleep()
         {

             Thread.Sleep(11000);

         }*/



        private Process iproxy = null;

        private void cerrarIProxiesProcess()
        {
            var processesByName = Process.GetProcessesByName("iproxy");
            if (processesByName.Length >= 1)
            {
                foreach (var process in processesByName) process.Kill();
                iproxy = null;
            }
        }

        private void Killall()
        {
            try
            {
                foreach (Process process in Process.GetProcesses())
                {
                    if (process.ProcessName == "iTunes")
                        process.Kill();
                    if (process.ProcessName == "WireShark")
                    {
                        process.Kill();
                        Thread.Sleep(500);
                        MessageBox.Show("Hmmmm, i wonder what you want to do? close WireShark.");
                        cerrarIProxiesProcess();
                        Environment.Exit(1);
                    }
                    if (process.ProcessName == "CharlesProxy")
                    {

                        process.Kill();
                        Thread.Sleep(500);
                        MessageBox.Show("Hmmmm, i wonder what you want to do? close CharlesProxy.");
                        cerrarIProxiesProcess();
                        Environment.Exit(1);
                    }
                    if (process.ProcessName == "Fiddler")
                    {
                        process.Kill();
                        Thread.Sleep(500);
                        MessageBox.Show("Hmmmm, i wonder what you want to do? close fiddler.");
                        cerrarIProxiesProcess();
                        Environment.Exit(1);
                    }
                    if (process.ProcessName == "Fiddler Everywhere")
                    {
                        process.Kill();
                        Thread.Sleep(500);
                        MessageBox.Show("Hmmmm, i wonder what you want to do? close fiddler.");
                        cerrarIProxiesProcess();
                        Environment.Exit(1);
                    }
                    if (process.ProcessName == "Postman")
                    {
                        process.Kill();
                        Thread.Sleep(500);
                        MessageBox.Show("Hmmmm, i wonder what you want to do? close fiddler.");
                        cerrarIProxiesProcess();
                        Environment.Exit(1);
                    }
                    if (process.ProcessName == "Notepad")
                    {
                        process.Kill();
                        Thread.Sleep(500);
                        MessageBox.Show("Hmmmm, i wonder what you want to do? close Notepad++.");
                        cerrarIProxiesProcess();
                        Environment.Exit(1);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void PairLoop()
        {
            //Pairing
            while (Pair("pair") == false)
            {
                MessageBox.Show(getMainForm(), "Accept trust dialog on the iPhone screen", "[NOTIFICATION]", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public bool Pair(string argument)
        {
            string path = Directory.GetCurrentDirectory();
            Process proc;

            try
            {
                if (argument == "pair")
                {
                    proc = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = path + @"\ref\Apple\Libimobiledevice\idevicepair.exe",
                            Arguments = "pair",
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            CreateNoWindow = true
                        }
                    };
                }
                else
                {
                    proc = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = path + @"\ref\Apple\Libimobiledevice\idevicepair.exe",
                            Arguments = "validate",
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            CreateNoWindow = true
                        }
                    };
                }

                try
                {
                    proc.Start();
                    StreamReader reader = proc.StandardOutput;
                    string result = reader.ReadToEnd();

                    Thread.Sleep(2000);
                    try { proc.Kill(); }
                    catch { }
                    if (result.Contains("SUCCESS"))
                    {
                        reader.Dispose();
                        return true;
                    }
                    else { return false; }
                }
                catch { }

            }
            catch (System.ComponentModel.Win32Exception)
            {
                MessageBox.Show(getMainForm(), "Idevicepair not found");
                return false;
            }

            return false;
        }

        public void cURL(string URLarguments)
        {
            proceso = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = ".\\ref\\Apple\\Libimobiledevice\\curl.exe",
                    Arguments = URLarguments,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };
            proceso.Start();
            proceso.WaitForExit();
        }


        private void ReportErrorMessage(Exception e = null)
        {
            string errmsg = "MDM Process error!";

            if (e != null)
            {
                errmsg = e.Message + e.StackTrace;
            }

            string errorStr = "Error in MDM Process " +
                              "\n STEP " + step.ToString() +
                              "\n SERIAL " + iOSDevice.Serial +
                              "\n IOS " + iOSDevice.iOS + " MODEL " + iOSDevice.Model;
            try
            {
                //Report the error to the server if you want [Not necesary]
                //Network.Error(errorStr + "\n TRACE: " + errmsg);
            }
            catch
            {
            }
            //RichLog.RichLogs(e.Message + " " + errorStr + "MDM process failed, take a screenshot of this image and send", System.Drawing.Color.Gold, true, true);
        }

        private zeroknox getMainForm()
        {
            zeroknox frm1 = (zeroknox)Application.OpenForms["Form"];
            return frm1;
        }



    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroKnox_Removal
{
    internal class iOSDevice
    {
        // Token: 0x04000063 RID: 99
        public static bool debugMode = false;

        // Token: 0x04000064 RID: 100
        public static string Model = "";

        // Token: 0x04000065 RID: 101
        public static string Name = "";

        // Token: 0x04000066 RID: 102
        public static string ECID = "";

        // Token: 0x04000067 RID: 103
        public static string ipwndfu = "";

        // Token: 0x04000068 RID: 104
        public static string ProductType = "";

        // Token: 0x04000069 RID: 105
        public static string mode = "";

        // Token: 0x0400006A RID: 106
        public static string HelloSerial = "";

        // Token: 0x0400006B RID: 107
        public static string Serial = "";

        // Token: 0x0400006C RID: 108
        public static string IOSVersion = "";

        // Token: 0x0400006D RID: 109
        public static bool isMEID = false;

        // Token: 0x0400006E RID: 110
        public static string CPID = "";

        // Token: 0x0400006F RID: 111
        public static string after_boot = "";

        // Token: 0x04000070 RID: 112
        public static string filename = "";
        public static string progress = "";
        public static string UDID = "";
        public static string ActivationState = "Unactivated";
        public static string SIMStatus = "";
        public static bool isSIMInserted = false;
        public static string IMEI = "";
        public static string BuildVersion = "";
        public static string build = "";
        public static string stat = "";
        public static string stat2 = "";
        public static string skip = "";
        public static string wall = "";
        public static string ota = "";
        public static string MyType = "";
        public static string iOS = "";
        public static string MEID = "";

        public static string DeviceName = "";



        public static string register = "";

        public static string ModelServer = "";


        public static void setProductType(string AproductType)
        {
            ProductType = AproductType;

            try
            {
                Model = DetermineModel(AproductType);

            }
            catch (System.Exception)
            {

            }
        }

        public static string DetermineModel(string productType)
        {
            string result = "N/A";

            string a = productType;

            uint num = CalculateModelNumber(a);

            if (num <= 1605455532U)
            {
                if (num <= 450846996U)
                {
                    if (num <= 277956311U)
                    {
                        if (num <= 227623454U)
                        {
                            if (num <= 194068216U)
                            {
                                if (num != 13713525U)
                                {
                                    if (num != 80824001U)
                                    {
                                        if (num == 194068216U)
                                        {
                                            if (a == "iPhone5,1")
                                            {
                                                result = "iPhone 5 (AT&T/Canada)";
                                            }
                                        }
                                    }
                                    else if (a == "iPad6,8")
                                    {
                                        result = "iPad PRO 12.9 Wifi + Cellular";
                                    }
                                }
                                else if (a == "iPad6,4")
                                {
                                    result = "iPad PRO 9.7 Wifi + Cellular";
                                }
                            }
                            else if (num != 201230688U)
                            {
                                if (num != 218008307U)
                                {
                                    if (num == 227623454U)
                                    {
                                        if (a == "iPhone5,3")
                                        {
                                            result = "iPhone 5c";
                                        }
                                    }
                                }
                                else if (a == "iPad7,3")
                                {
                                    result = "iPad PRO 10.5 Wifi";
                                }
                            }
                            else if (a == "iPad7,2")
                            {
                                result = "iPad PRO 12.9 Wifi + Cellular";
                            }
                        }
                        else if (num <= 244401073U)
                        {
                            if (num != 235638739U)
                            {
                                if (num != 240921132U)
                                {
                                    if (num == 244401073U)
                                    {
                                        if (a == "iPhone5,2")
                                        {
                                            result = "iPhone 5";
                                        }
                                    }
                                }
                                else if (a == "iPad6,12")
                                {
                                    result = "iPad (5th) Wifi + Cellular";
                                }
                            }
                            else if (a == "iPhone4,1")
                            {
                                result = "iPhone 4S";
                            }
                        }
                        else if (num != 251563545U)
                        {
                            if (num != 268341164U)
                            {
                                if (num == 277956311U)
                                {
                                    if (a == "iPhone5,4")
                                    {
                                        result = "iPhone 5c";
                                    }
                                }
                            }
                            else if (a == "iPad7,6")
                            {
                                result = "iPad (6th) WiFi + Cellular";
                            }
                        }
                        else if (a == "iPad7,1")
                        {
                            result = "iPad PRO 12.9 Wifi";
                        }
                    }
                    else if (num <= 383736520U)
                    {
                        if (num <= 318674021U)
                        {
                            if (num != 291253989U)
                            {
                                if (num != 301896402U)
                                {
                                    if (num == 318674021U)
                                    {
                                        if (a == "iPad7,5")
                                        {
                                            result = "iPad (6th) WiFi";
                                        }
                                    }
                                }
                                else if (a == "iPad7,4")
                                {
                                    result = "iPad PRO 10.5 Wifi + Cellular";
                                }
                            }
                            else if (a == "iPad6,11")
                            {
                                result = "iPad (5th) Wifi";
                            }
                        }
                        else if (num != 342808911U)
                        {
                            if (num != 350181282U)
                            {
                                if (num == 383736520U)
                                {
                                    if (a == "iPhone11,6")
                                    {
                                        result = "iPhone XS Max China";
                                    }
                                }
                            }
                            else if (a == "iPhone11,8")
                            {
                                result = "iPhone XR";
                            }
                        }
                        else if (a == "iPad8,8")
                        {
                            result = "iPad PRO 12.9 1TB, WiFi + Cellular";
                        }
                    }
                    else if (num <= 417291758U)
                    {
                        if (num != 393141768U)
                        {
                            if (num != 409919387U)
                            {
                                if (num == 417291758U)
                                {
                                    if (a == "iPhone11,4")
                                    {
                                        result = "iPhone XS Max";
                                    }
                                }
                            }
                            else if (a == "iPad8,4")
                            {
                                result = "iPad PRO 11 1TB, WiFi + Cellular";
                            }
                        }
                        else if (a == "iPad8,5")
                        {
                            result = "iPad PRO 12.9 WiFi";
                        }
                    }
                    else if (num != 426697006U)
                    {
                        if (num != 443474625U)
                        {
                            if (num == 450846996U)
                            {
                                if (a == "iPhone11,2")
                                {
                                    result = "iPhone XS";
                                }
                            }
                        }
                        else if (a == "iPad8,6")
                        {
                            result = "iPad PRO 12.9 1TB, WiFi";
                        }
                    }
                    else if (a == "iPad8,7")
                    {
                        result = "iPad PRO 12.9 WiFi + Cellular";
                    }
                }
                else if (num <= 977265701U)
                {
                    if (num <= 755807492U)
                    {
                        if (num <= 510585101U)
                        {
                            if (num != 460252244U)
                            {
                                if (num != 493807482U)
                                {
                                    if (num == 510585101U)
                                    {
                                        if (a == "iPad8,2")
                                        {
                                            result = "iPad PRO 11 1TB, WiFi";
                                        }
                                    }
                                }
                                else if (a == "iPad8,3")
                                {
                                    result = "iPad PRO 11 WiFi + Cellular";
                                }
                            }
                            else if (a == "iPad8,1")
                            {
                                result = "iPad PRO 11 WiFi";
                            }
                        }
                        else if (num != 519927770U)
                        {
                            if (num != 688697016U)
                            {
                                if (num == 755807492U)
                                {
                                    if (a == "iPhone12,1")
                                    {
                                        result = "iPhone 11";
                                    }
                                }
                            }
                            else if (a == "iPhone12,5")
                            {
                                result = "iPhone 11 Pro Max";
                            }
                        }
                        else if (a == "iPod4,1")
                        {
                            result = "iPod Touch Fourth Generation";
                        }
                    }
                    else if (num <= 897947417U)
                    {
                        if (num != 789362730U)
                        {
                            if (num != 876599987U)
                            {
                                if (num == 897947417U)
                                {
                                    if (a == "iPod9,1")
                                    {
                                        result = "iPod Touch 7th Generation";
                                    }
                                }
                            }
                            else if (a == "iPhone9,4")
                            {
                                result = "iPhone 7 Plus (GSM)";
                            }
                        }
                        else if (a == "iPhone12,3")
                        {
                            result = "iPhone 11 Pro";
                        }
                    }
                    else if (num != 926932844U)
                    {
                        if (num != 960488082U)
                        {
                            if (num == 977265701U)
                            {
                                if (a == "iPhone9,2")
                                {
                                    result = "iPhone 7 Plus (CDMA)";
                                }
                            }
                        }
                        else if (a == "iPhone9,3")
                        {
                            result = "iPhone 7 (GSM)";
                        }
                    }
                    else if (a == "iPhone9,1")
                    {
                        result = "iPhone 7 (CDMA)";
                    }
                }
                else if (num <= 1118200753U)
                {
                    if (num <= 1027150186U)
                    {
                        if (num != 993594948U)
                        {
                            if (num != 1010372567U)
                            {
                                if (num == 1027150186U)
                                {
                                    if (a == "iPhone3,1")
                                    {
                                        result = "iPhone 4 (GSM)";
                                    }
                                }
                            }
                            else if (a == "iPhone3,2")
                            {
                                result = "iPhone 4 (GSM Rev A)";
                            }
                        }
                        else if (a == "iPhone3,3")
                        {
                            result = "iPhone 4 (CDMA/Verizon/Sprint)";
                        }
                    }
                    else if (num != 1084645515U)
                    {
                        if (num != 1101423134U)
                        {
                            if (num == 1118200753U)
                            {
                                if (a == "iPad5,3")
                                {
                                    result = "iPad AIR 2 Wifi";
                                }
                            }
                        }
                        else if (a == "iPad5,2")
                        {
                            result = "iPad Mini 4 Wifi + Cellular";
                        }
                    }
                    else if (a == "iPad5,1")
                    {
                        result = "iPad Mini 4 Wifi";
                    }
                }
                else if (num <= 1538345056U)
                {
                    if (num != 1134978372U)
                    {
                        if (num != 1158652399U)
                        {
                            if (num == 1538345056U)
                            {
                                if (a == "iPad3,6")
                                {
                                    result = "iPad 4 Wifi + Cellular";
                                }
                            }
                        }
                        else if (a == "iPod7,1")
                        {
                            result = "iPod Touch 6th Generation";
                        }
                    }
                    else if (a == "iPad5,4")
                    {
                        result = "iPad AIR 2 Wifi + Cellular";
                    }
                }
                else if (num != 1571900294U)
                {
                    if (num != 1588677913U)
                    {
                        if (num == 1605455532U)
                        {
                            if (a == "iPad3,2")
                            {
                                result = "iPad 3 Wifi + Cellular";
                            }
                        }
                    }
                    else if (a == "iPad3,5")
                    {
                        result = "iPad 4 Wifi + Cellular";
                    }
                }
                else if (a == "iPad3,4")
                {
                    result = "iPad 4 Wifi";
                }
            }
            else if (num <= 2760723989U)
            {
                if (num <= 2286986705U)
                {
                    if (num <= 1760014814U)
                    {
                        if (num <= 1655788389U)
                        {
                            if (num != 1613858532U)
                            {
                                if (num != 1622233151U)
                                {
                                    if (num == 1655788389U)
                                    {
                                        if (a == "iPad3,1")
                                        {
                                            result = "iPad 3 Wifi";
                                        }
                                    }
                                }
                                else if (a == "iPad3,3")
                                {
                                    result = "iPad 3 Wifi + Cellular";
                                }
                            }
                            else if (a == "iPhone1,1")
                            {
                                result = "iPhone 1";
                            }
                        }
                        else if (num != 1664191389U)
                        {
                            if (num != 1743237195U)
                            {
                                if (num == 1760014814U)
                                {
                                    if (a == "iPhone7,1")
                                    {
                                        result = "iPhone 6 Plus";
                                    }
                                }
                            }
                            else if (a == "iPhone7,2")
                            {
                                result = "iPhone 6";
                            }
                        }
                        else if (a == "iPhone1,2")
                        {
                            result = "iPhone 3G";
                        }
                    }
                    else if (num <= 2081752929U)
                    {
                        if (num != 1886294147U)
                        {
                            if (num != 2031420072U)
                            {
                                if (num == 2081752929U)
                                {
                                    if (a == "iPhone6,1")
                                    {
                                        result = "iPhone 5s";
                                    }
                                }
                            }
                            else if (a == "iPhone6,2")
                            {
                                result = "iPhone 5s (Global)";
                            }
                        }
                        else if (a == "iPod3,1")
                        {
                            result = "iPod Touch Third Generation";
                        }
                    }
                    else if (num != 2253431467U)
                    {
                        if (num != 2270209086U)
                        {
                            if (num == 2286986705U)
                            {
                                if (a == "iPhone10,4")
                                {
                                    result = "iPhone 8 (GSM)";
                                }
                            }
                        }
                        else if (a == "iPhone10,5")
                        {
                            result = "iPhone 8 Plus (GSM)";
                        }
                    }
                    else if (a == "iPhone10,6")
                    {
                        result = "iPhone X (GSM)";
                    }
                }
                else if (num <= 2643280656U)
                {
                    if (num <= 2337319562U)
                    {
                        if (num != 2303764324U)
                        {
                            if (num != 2320541943U)
                            {
                                if (num == 2337319562U)
                                {
                                    if (a == "iPhone10,1")
                                    {
                                        result = "iPhone 8 (CDMA)";
                                    }
                                }
                            }
                            else if (a == "iPhone10,2")
                            {
                                result = "iPhone 8 Plus (CDMA)";
                            }
                        }
                        else if (a == "iPhone10,3")
                        {
                            result = "iPhone X (CDMA)";
                        }
                    }
                    else if (num != 2509658711U)
                    {
                        if (num != 2526436330U)
                        {
                            if (num == 2643280656U)
                            {
                                if (a == "iPad4,1")
                                {
                                    result = "iPad AIR Wifi";
                                }
                            }
                        }
                        else if (a == "iPad1,2")
                        {
                            result = "iPad 1 Wifi + Cellular";
                        }
                    }
                    else if (a == "iPad1,1")
                    {
                        result = "iPad 1 Wifi)";
                    }
                }
                else if (num <= 2710391132U)
                {
                    if (num != 2676835894U)
                    {
                        if (num != 2693613513U)
                        {
                            if (num == 2710391132U)
                            {
                                if (a == "iPad4,5")
                                {
                                    result = "iPad Mini 2 Wifi + Cellular";
                                }
                            }
                        }
                        else if (a == "iPad4,2")
                        {
                            result = "iPad AIR Wifi + Cellular";
                        }
                    }
                    else if (a == "iPad4,3")
                    {
                        result = "iPad AIR Wifi + Cellular";
                    }
                }
                else if (num != 2727168751U)
                {
                    if (num != 2743946370U)
                    {
                        if (num == 2760723989U)
                        {
                            if (a == "iPad4,6")
                            {
                                result = "iPad Mini 2 Wifi + Cellular";
                            }
                        }
                    }
                    else if (a == "iPad4,7")
                    {
                        result = "iPad Mini 3 Wifi";
                    }
                }
                else if (a == "iPad4,4")
                {
                    result = "iPad Mini 2 Wifi";
                }
            }
            else if (num <= 3430040502U)
            {
                if (num <= 2989097949U)
                {
                    if (num <= 2900469187U)
                    {
                        if (num != 2777501608U)
                        {
                            if (num != 2794279227U)
                            {
                                if (num == 2900469187U)
                                {
                                    if (a == "iPad11,4")
                                    {
                                        result = "iPad Air 3rd Gen Wifi  + Cellular";
                                    }
                                }
                            }
                            else if (a == "iPad4,8")
                            {
                                result = "iPad Mini 3 Wifi + Cellular";
                            }
                        }
                        else if (a == "iPad4,9")
                        {
                            result = "iPad Mini 3 Wifi + Cellular";
                        }
                    }
                    else if (num != 2950802044U)
                    {
                        if (num != 2984357282U)
                        {
                            if (num == 2989097949U)
                            {
                                if (a == "iPod5,1")
                                {
                                    result = "iPod Touch 5th Generation";
                                }
                            }
                        }
                        else if (a == "iPad11,3")
                        {
                            result = "iPad Air 3rd Gen Wifi ";
                        }
                    }
                    else if (a == "iPad11,1")
                    {
                        result = "iPad mini 5th Gen WiFi";
                    }
                }
                else if (num <= 3317288369U)
                {
                    if (num != 3001134901U)
                    {
                        if (num != 3266955512U)
                        {
                            if (num == 3317288369U)
                            {
                                if (a == "iPad7,12")
                                {
                                    result = "iPad (7th)WiFi + Cellular";
                                }
                            }
                        }
                        else if (a == "iPad7,11")
                        {
                            result = "iPad (7th)WiFi";
                        }
                    }
                    else if (a == "iPad11,2")
                    {
                        result = "iPad mini 5th Gen Wifi  + Cellular";
                    }
                }
                else if (num != 3396485264U)
                {
                    if (num != 3413262883U)
                    {
                        if (num == 3430040502U)
                        {
                            if (a == "iPad2,5")
                            {
                                result = "iPad Mini Wifi";
                            }
                        }
                    }
                    else if (a == "iPad2,6")
                    {
                        result = "iPad Mini Wifi + Cellular";
                    }
                }
                else if (a == "iPad2,7")
                {
                    result = "iPad Mini Wifi + Cellular";
                }
            }
            else if (num <= 3579376904U)
            {
                if (num <= 3480373359U)
                {
                    if (num != 3446818121U)
                    {
                        if (num != 3463595740U)
                        {
                            if (num == 3480373359U)
                            {
                                if (a == "iPad2,2")
                                {
                                    result = "iPad 2 GSM";
                                }
                            }
                        }
                        else if (a == "iPad2,3")
                        {
                            result = "iPad 2 3G";
                        }
                    }
                    else if (a == "iPad2,4")
                    {
                        result = "iPad 2 Wifi";
                    }
                }
                else if (num != 3497150978U)
                {
                    if (num != 3506766125U)
                    {
                        if (num == 3579376904U)
                        {
                            if (a == "iPhone8,4")
                            {
                                result = "iPhone SE";
                            }
                        }
                    }
                    else if (a == "iPhone2,1")
                    {
                        result = "iPhone 3GS";
                    }
                }
                else if (a == "iPad2,1")
                {
                    result = "iPad 2 Wifi";
                }
            }
            else if (num <= 3721962577U)
            {
                if (num != 3663264999U)
                {
                    if (num != 3680042618U)
                    {
                        if (num == 3721962577U)
                        {
                            if (a == "iPod1,1")
                            {
                                result = "iPod Touch";
                            }
                        }
                    }
                    else if (a == "iPhone8,2")
                    {
                        result = "iPhone 6S Plus";
                    }
                }
                else if (a == "iPhone8,1")
                {
                    result = "iPhone 6S";
                }
            }
            else if (num != 3981813096U)
            {
                if (num != 4191237488U)
                {
                    if (num == 4258347964U)
                    {
                        if (a == "iPad6,7")
                        {
                            result = "iPad PRO 12.9 Wifi";
                        }
                    }
                }
                else if (a == "iPad6,3")
                {
                    result = "iPad PRO 9.7 Wifi";
                }
            }
            else if (a == "iPod2,1")
            {
                result = "iPod Touch Second Generation";
            }
            return result;
        }
        internal static uint CalculateModelNumber(string productType)
        {
            uint num = 0;
            if (productType != null)
            {
                num = 2166136261U;
                for (int i = 0; i < productType.Length; i++)
                {
                    num = ((uint)productType[i] ^ num) * 16777619U;
                }
            }
            return num;
        }

        public static bool clasifyGSMorMEID()
        {
            if (iOSDevice.Model.Contains("GSM"))
            {
                return false;
            }

            if (iOSDevice.Model.Contains("iPod") || iOSDevice.Model.Contains("iPad"))
            {
                return true;
            }

            if (iOSDevice.Model.Contains("CDMA")
                || iOSDevice.Model.Contains("iPhone 6")
                || iOSDevice.Model.Contains("iPhone SE")
                || iOSDevice.Model.Contains("Global")
                || iOSDevice.Model.Contains("iPod")
                || iOSDevice.Model.Contains("iPad")
                )
            {
                return true;
            }

            if (iOSDevice.MEID == "")
            {
                return false;
            }

            return true;
        }

        //In Authorization stage we determine if it a Warranty or Primary activation by server "I-KING-WARRANTY" header reply
        public static bool isWarranty = false;



    }
}

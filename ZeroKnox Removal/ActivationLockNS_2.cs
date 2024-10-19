using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using Claunia.PropertyList;
using Form1.Properties;
using Renci.SshNet;
using ZeroProRam.Resources;

namespace ZeroKnox_Removal
{
    public class ActivationLockNS_2
    {
        public static string Aldaz = ToolDir + ".\\ref\\Apple\\filles\\utils\\";
        public static string iUn = Application.StartupPath + ".\\ref\\Apple\\files\\utils\\iuntethered.plist";
        public static string SwapPCDir2 = ToolDir + ".\\ref\\Apple\\files\\utils\\";
        public static string aldaz2 = ToolDir + ".\\ref\\Apple\\files\\";
        public static string aldaz3 = ToolDir + ".\\ref\\Apple\\files\\utils\\";
        public static string iUnD = Application.StartupPath + ".\\ref\\Apple\\files\\utils\\iuntethered.dylib";
        public static string aldazMEID = ToolDir + ".\\ref\\Apple\\files\\vip\\";
        private byte[] nobackup_plist = Convert.FromBase64String("PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiPz4KPCFET0NUWVBFIHBsaXN0IFBVQkxJQyAiLS8vQXBwbGUvL0RURCBQTElTVCAxLjAvL0VOIiAiaHR0cDovL3d3dy5hcHBsZS5jb20vRFREcy9Qcm9wZXJ0eUxpc3QtMS4wLmR0ZCI+CjxwbGlzdCB2ZXJzaW9uPSIxLjAiPgo8ZGljdD4KCTxrZXk+a1Bvc3Rwb25lbWVudFRpY2tldDwva2V5PgoJPGRpY3Q+CgkJPGtleT5BY3Rpdml0eVVSTDwva2V5PgoJCTxzdHJpbmc+aHR0cHM6Ly9hbGJlcnQuYXBwbGUuY29tL2RldmljZXNlcnZpY2VzL2FjdGl2aXR5PC9zdHJpbmc+CgkJPGtleT5BY3RpdmF0aW9uVGlja2V0PC9rZXk+CgkJPHN0cmluZz5QRDk0Yld3Z2RtVnljMmx2YmowaU1TNHdJaUJsYm1OdlpHbHVaejBpVlZSR0xUZ2lQejRLUENGRVQwTlVXVkJGSUhCc2FYTjBJRkJWUWt4SlF5QWlMUzh2UVhCd2JHVXZMMFJVUkNCUVRFbFRWQ0F4TGpBdkwwVk9JaUFpYUhSMGNEb3ZMM2QzZHk1aGNIQnNaUzVqYjIwdlJGUkVjeTlRY205d1pYSjBlVXhwYzNRdE1TNHdMbVIwWkNJK0NqeHdiR2x6ZENCMlpYSnphVzl1UFNJeExqQWlQZ284WkdsamRENEtDVHhyWlhrK1FXTjBhWFpoZEdsdmJsSmxjWFZsYzNSSmJtWnZQQzlyWlhrK0NnazhaR2xqZEQ0S0NRazhhMlY1UGtGamRHbDJZWFJwYjI1U1lXNWtiMjF1WlhOelBDOXJaWGsrQ2drSlBITjBjbWx1Wno0ek1HSTJNR1prTUMwMk5qYzBMVFEzTnpndFltSXhOQzFtTkdaaE9UUTBNV1EwWXpnOEwzTjBjbWx1Wno0S0NRazhhMlY1UGtGamRHbDJZWFJwYjI1VGRHRjBaVHd2YTJWNVBnb0pDVHh6ZEhKcGJtYytWVzVoWTNScGRtRjBaV1E4TDNOMGNtbHVaejRLQ1FrOGEyVjVQa1pOYVZCQlkyTnZkVzUwUlhocGMzUnpQQzlyWlhrK0Nna0pQSFJ5ZFdVdlBnb0pQQzlrYVdOMFBnb0pQR3RsZVQ1Q1lYTmxZbUZ1WkZKbGNYVmxjM1JKYm1adlBDOXJaWGsrQ2drOFpHbGpkRDRLQ1FrOGEyVjVQa0ZqZEdsMllYUnBiMjVTWlhGMWFYSmxjMEZqZEdsMllYUnBiMjVVYVdOclpYUThMMnRsZVQ0S0NRazhkSEoxWlM4K0Nna0pQR3RsZVQ1Q1lYTmxZbUZ1WkVGamRHbDJZWFJwYjI1VWFXTnJaWFJXWlhKemFXOXVQQzlyWlhrK0Nna0pQSE4wY21sdVp6NVdNand2YzNSeWFXNW5QZ29KQ1R4clpYaytRbUZ6WldKaGJtUkRhR2x3U1VROEwydGxlVDRLQ1FrOGFXNTBaV2RsY2o0eE1qTTBOVFkzUEM5cGJuUmxaMlZ5UGdvSkNUeHJaWGsrUW1GelpXSmhibVJOWVhOMFpYSkxaWGxJWVhOb1BDOXJaWGsrQ2drSlBITjBjbWx1Wno0NFEwSXhNRGN3UkRrMVFqbERSVVUwUXpnd01EQXdOVVV5TVRrNVFrSTRSa0l4T0ROQ01ESTNNVE5CTlRKRVFqVkZOelZEUVRKQk5qRTFOVE0yTVRneVBDOXpkSEpwYm1jK0Nna0pQR3RsZVQ1Q1lYTmxZbUZ1WkZObGNtbGhiRTUxYldKbGNqd3ZhMlY1UGdvSkNUeGtZWFJoUGdvSkNVVm5hR1JEZHowOUNna0pQQzlrWVhSaFBnb0pDVHhyWlhrK1NXNTBaWEp1WVhScGIyNWhiRTF2WW1sc1pVVnhkV2x3YldWdWRFbGtaVzUwYVhSNVBDOXJaWGsrQ2drSlBITjBjbWx1Wno0eE1qTTBOVFkzT0RreE1qTTBOVFk4TDNOMGNtbHVaejRLQ1FrOGEyVjVQazF2WW1sc1pVVnhkV2x3YldWdWRFbGtaVzUwYVdacFpYSThMMnRsZVQ0S0NRazhjM1J5YVc1blBqRXlNelExTmpjNE9URXlNelExUEM5emRISnBibWMrQ2drSlBHdGxlVDVUU1UxVGRHRjBkWE04TDJ0bGVUNEtDUWs4YzNSeWFXNW5QbXREVkZOSlRWTjFjSEJ2Y25SVFNVMVRkR0YwZFhOT2IzUkpibk5sY25SbFpEd3ZjM1J5YVc1blBnb0pDVHhyWlhrK1UzVndjRzl5ZEhOUWIzTjBjRzl1WlcxbGJuUThMMnRsZVQ0S0NRazhkSEoxWlM4K0Nna0pQR3RsZVQ1clExUlFiM04wY0c5dVpXMWxiblJKYm1adlVGSk1UbUZ0WlR3dmEyVjVQZ29KQ1R4cGJuUmxaMlZ5UGpBOEwybHVkR1ZuWlhJK0Nna0pQR3RsZVQ1clExUlFiM04wY0c5dVpXMWxiblJKYm1adlUyVnlkbWxqWlZCeWIzWnBjMmx2Ym1sdVoxTjBZWFJsUEM5clpYaytDZ2tKUEdaaGJITmxMejRLQ1R3dlpHbGpkRDRLQ1R4clpYaytSR1YyYVdObFEyVnlkRkpsY1hWbGMzUThMMnRsZVQ0S0NUeGtZWFJoUGdvSlRGTXdkRXhUTVVOU1ZXUktWR2xDUkZKV1NsVlRWVnBLVVRCR1ZWSlRRbE5TVmtaV1VsWk9WVXhUTUhSTVV6QkxWRlZzU2xGdWFFVlJNRTVDVlhwQ1JGRldSa0lLQ1dReVpGcFVXR2hOVmtWR2VWRnRaRTlXYTBwQ1ZGWlNTMUpWYTNwVWJYUlNUVVUxUmxKVVZrMVdWbXQ2VkdwQ1VtUkZOVVpWVkVKT1lWUkJNRlZXVmt0Ulp6QkxDZ2xVUmxKeVpVWktjVmRyVmxOU1JXdzFWV3RXUzFJd05YRlNWWGhPVVZkMFNGRlVSbFpTVlVwdlZGVk9WMVpyTVRSUk0zQkNVMnRLYmxSc1drTlJWMlJWVVZkMFR3b0pZV3BhZUZOVmJIUlVibXhYVTIxV01VNXNUVEpWYWs0MFVWY3hUMVJYTldGalJFcEhURE5vUlZOSVJqVmlWbXhWVDFab1QxSkZkekJqUmxKYVlqRm5NbUY2UW1zS0NWRnJNVk5UV0dSR1VWWnNSVlpzUmxKVFFUQkxVbGhrYzFKSFVsbFJiWGhxWW14S2QxbHRNRFJsUlZZMlVWWktRMW93TlZkUmEwWjJWa1ZPY2xKdVpHcFNNMmh6Q2dsVFZWWnpaRlpzTlU1SWFFVmxhMFpQVVcxa1QxWnJTa0pqTVZKRFlsZDRVbGxWWXpWa1VUQkxWMnhTUkZGdE5UWlJWVFZEV2pKMGVHRkhkSEJTZW13elRVVktRZ29KVVZVeFFrMUZaRVJWTTBaSVZUQnNhVTB3VWxKU1ZVcERWVlpXUWxGVVVraFJhMFpFVERKNGVXSkhWbEpVYW1SM1VWRXdTMDB5YUVoV1Zsa3dVMFpzVTFsWGRIWUtDV0ZyYXpSUFYzZDRZVVpLZG1ScVFsUk9SRUpQVFVoQmVVMVVhSEpVVjI5NVlrUkdUMkV6VVhkV1dFSnhWMnM1UlU1V1ZsZGxWR1JEVDBWc1QxRnJTbTFSTW14TkNnbE5aekJMV25rNGRreDVkSHBhVlZab1ZqRmpNR0ZFV1hkVU0wcE9aRzVLYkZGV1FUQk5SMHBzVlRKYVVGbHVjRTFXUjNoWVV6SkdWMk5YUm5KTlYxSkdWR3BTU2dvSlRrZDRUVlJZYUhCbFZGVnlZak53U1ZwcVdtbFdkekJMVkdsMGJsZEZTbFZOTWpsNFdraFdSRkY2UmxkV2VsWktWMjVhTWxwRlVsTldSV3gzWVVab05tRXlSVXNLQ1ZWVlZrZFJWVVpRVVcxd1VsRllaRzVYVjNSRVdqRnNSbEZZU2xWaE1WcEZaREJHVjAxSWJIUlphelZXVW0xNE1FMHllRXhqTUhSQ1drRXdTMkp1WXpCVFJscFBDZ2xhTUVaMVVraG9hV1JSTUV0UlZVcFhWMVZTTWxOR2FFSk5SRVpOVjBaT1MxRjVkSFJrYW1kNVZGWlNTV1F5U2s1T1JWRjJWMnhKY2xKRmFGcFJWMWt5V1hsek5Rb0pZVmMxVGxKdFVrOVBSMnhhVjBoU1NXRkZPWGRqVjNNd1lWZDRUbFIzTUV0Wk1uUnVXV3RzTmxNd2IzbE9XRkpQV1RGS1ZXTXdPWGRXVlU1Q1pEQldRbEZYUmtJS0NVeFRNSFJNVXpGR1ZHdFJaMUV3VmxOV1JXeEhVMVZPUWxaRlZXZFZhMVpTVmxWV1ZGWkRNSFJNVXpCMENnazhMMlJoZEdFK0NnazhhMlY1UGtSbGRtbGpaVWxFUEM5clpYaytDZ2s4WkdsamRENEtDUWs4YTJWNVBsTmxjbWxoYkU1MWJXSmxjand2YTJWNVBnb0pDVHh6ZEhKcGJtYytSbEl4VURKSFNEaEtPRmhJUEM5emRISnBibWMrQ2drSlBHdGxlVDVWYm1seGRXVkVaWFpwWTJWSlJEd3ZhMlY1UGdvSkNUeHpkSEpwYm1jK1pEazRPVEl3T1RaalpqTTBNVEZsWVRnM1pEQXdNalF5WVdNeE16QXdNRE5tTXpReE1XVTBNand2YzNSeWFXNW5QZ29KUEM5a2FXTjBQZ29KUEd0bGVUNUVaWFpwWTJWSmJtWnZQQzlyWlhrK0NnazhaR2xqZEQ0S0NRazhhMlY1UGtKMWFXeGtWbVZ5YzJsdmJqd3ZhMlY1UGdvSkNUeHpkSEpwYm1jK01UaEdNREE4TDNOMGNtbHVaejRLQ1FrOGEyVjVQa1JsZG1salpVTnNZWE56UEM5clpYaytDZ2tKUEhOMGNtbHVaejVwVUdodmJtVThMM04wY21sdVp6NEtDUWs4YTJWNVBrUmxkbWxqWlZaaGNtbGhiblE4TDJ0bGVUNEtDUWs4YzNSeWFXNW5Qa0k4TDNOMGNtbHVaejRLQ1FrOGEyVjVQazF2WkdWc1RuVnRZbVZ5UEM5clpYaytDZ2tKUEhOMGNtbHVaejVOVEV4T01qd3ZjM1J5YVc1blBnb0pDVHhyWlhrK1QxTlVlWEJsUEM5clpYaytDZ2tKUEhOMGNtbHVaejVwVUdodmJtVWdUMU04TDNOMGNtbHVaejRLQ1FrOGEyVjVQbEJ5YjJSMVkzUlVlWEJsUEM5clpYaytDZ2tKUEhOMGNtbHVaejVwVUdodmJtVXdMREE4TDNOMGNtbHVaejRLQ1FrOGEyVjVQbEJ5YjJSMVkzUldaWEp6YVc5dVBDOXJaWGsrQ2drSlBITjBjbWx1Wno0eE5DNHdMakE4TDNOMGNtbHVaejRLQ1FrOGEyVjVQbEpsWjJsdmJrTnZaR1U4TDJ0bGVUNEtDUWs4YzNSeWFXNW5Qa3hNUEM5emRISnBibWMrQ2drSlBHdGxlVDVTWldkcGIyNUpibVp2UEM5clpYaytDZ2tKUEhOMGNtbHVaejVNVEM5QlBDOXpkSEpwYm1jK0Nna0pQR3RsZVQ1U1pXZDFiR0YwYjNKNVRXOWtaV3hPZFcxaVpYSThMMnRsZVQ0S0NRazhjM1J5YVc1blBrRXhNak0wUEM5emRISnBibWMrQ2drSlBHdGxlVDVUYVdkdWFXNW5SblZ6WlR3dmEyVjVQZ29KQ1R4MGNuVmxMejRLQ1FrOGEyVjVQbFZ1YVhGMVpVTm9hWEJKUkR3dmEyVjVQZ29KQ1R4cGJuUmxaMlZ5UGpFeU16UTFOamM0T1RFeU16UThMMmx1ZEdWblpYSStDZ2s4TDJScFkzUStDZ2s4YTJWNVBsSmxaM1ZzWVhSdmNubEpiV0ZuWlhNOEwydGxlVDRLQ1R4a2FXTjBQZ29KQ1R4clpYaytSR1YyYVdObFZtRnlhV0Z1ZER3dmEyVjVQZ29KQ1R4emRISnBibWMrUWp3dmMzUnlhVzVuUGdvSlBDOWthV04wUGdvSlBHdGxlVDVUYjJaMGQyRnlaVlZ3WkdGMFpWSmxjWFZsYzNSSmJtWnZQQzlyWlhrK0NnazhaR2xqZEQ0S0NRazhhMlY1UGtWdVlXSnNaV1E4TDJ0bGVUNEtDUWs4ZEhKMVpTOCtDZ2s4TDJScFkzUStDZ2s4YTJWNVBsVkpTME5sY25ScFptbGpZWFJwYjI0OEwydGxlVDRLQ1R4a2FXTjBQZ29KQ1R4clpYaytRbXgxWlhSdmIzUm9RV1JrY21WemN6d3ZhMlY1UGdvSkNUeHpkSEpwYm1jK1ptWTZabVk2Wm1ZNlptWTZabVk2Wm1ZOEwzTjBjbWx1Wno0S0NRazhhMlY1UGtKdllYSmtTV1E4TDJ0bGVUNEtDUWs4YVc1MFpXZGxjajR5UEM5cGJuUmxaMlZ5UGdvSkNUeHJaWGsrUTJocGNFbEVQQzlyWlhrK0Nna0pQR2x1ZEdWblpYSStNekkzTmpnOEwybHVkR1ZuWlhJK0Nna0pQR3RsZVQ1RmRHaGxjbTVsZEUxaFkwRmtaSEpsYzNNOEwydGxlVDRLQ1FrOGMzUnlhVzVuUG1abU9tWm1PbVptT21abU9tWm1PbVptUEM5emRISnBibWMrQ2drSlBHdGxlVDVWU1V0RFpYSjBhV1pwWTJGMGFXOXVQQzlyWlhrK0Nna0pQR1JoZEdFK0Nna0pUVWxKUkRCM1NVSkJha05EUVRoM1JVbFFORU16YzNGUmRGQXhVekpvZDBKYWVrTnZTR056YjBneWVFNTFOV01yWVRSUk5EVnZTakZOUzBZekNna0pRa1ZGUlRKbE9UTmxiMVpQZUhWbU1HVkxVRlZ4VGtWbk5ucE5iRUp6VG5FcmFuSXJVbkZOUVhoVGFGWkJMMk5VTlc5dWEzSXdkQ3RGTUVoTENna0pibE5rZGtoTk1pOUdaWFJ5VDNGcFQwazBSSFpJVUVsRVZ6QkVNblZCVVZFemFXOWlVSGRoUVd4R2JGaElVRmR5T0UxS0x5dDNVVkZIVkd4dUNna0pSVmhQTVRaT2RESnJWVVVyZHk4dlFteEhkMVE0VjNoU1pYa3ZTVTQxU1cxTmJHdFplbHBzU25wYWNrODNkVmwwYkhCbFozazJLM2hKYVd3eUNna0pRakpZYkhrMGFVZDRVbHBwVWxkNU5YTkxjRkZ2TWxsNmIwcEZiVzFYVTI1bWFuVXdZMVV5TDNKaU9VWkNkblZXYVM5clYxTkdia0ZyZERSNUNna0pjVkYzTkdzd2FXSjBjRFZYSzFsVlEwTnZabTh6ZVdWdWFrMHlWV013Yml0NVNFeHlVMjB3UlRsUFVETndkRXhVTjNaSGNuSm1hM0l6V0ZKcENna0pkSGRFY0dSQ1QzTnpLMWg2U0VGUldFdDFjRzg1V0dreFVXMU9iR3AxVkdveGFrcFpielpOYzFreU9VUllPVVZhY0ZkRWRtcEpjMGw1VEhkNENna0pRalJqYlVsVFZXWTRRbTV5VWxGSE9VUkJNMDFsWXpaaWFGUmtVRUpqZFV0WFpIQkNibTVETWxZNFYzQm1UWEJ3VlVRMlUyUm5kVzVwZWpaNkNna0pURWN3Tm1OR1IzZHZVWFp1V1hoUmExVnJhMnBrV1dSNk5HODVlWE01TDNaeFEySnhabkJ1TkhSalpFa3lNV001WjI5TmQweG9SSE5vWW1zMUNna0pVRU5hUW5Ob05VWTBVMUpTYVdkQlYzSkJVME5CZWprNE1rSTNiemh3UTBOYUwycFpLM2xhUTNwQmIzSjZTRzV6UjJaMmQwdHBTbEJCVFdwcENna0paVEEwUnpScVNrMDRjRXBSVVU1dVdtRmhVQ3QwUm1Wc1pHaEVSMUZ1YnpBMGRtWktSRmt6T0VaR1RTdGhaVU4zZWxKeVF5OURVR0pyWlZwUkNna0pOWFI1TlRkTVNYTnpNVWh5VW1VelNURmpLMFpNTlhCdVptd3ZhRXN4UWpGMVFUUkhSRFJXYkZreFUweE1NWGsxYWpSSGRVWlVNMWhUZUhwaUNna0pXbEl2Wm1KRWExVjVWSE5VTTNJMmVHZG9XblJOTkVKWVNXOWhOakphUkVNelNWQnRUMko0UzJKb2JHRkxRVFJ0U3pKek0xRkNORlpqTmxNdkNna0piVFoxWVRaUWFrd3ZRakUxUXpCalRHcHlNVU5OYjB4MEx6YzRURlZSVjIxR1JYVjVTa2hrZG5SVE5uaEliV3RNUkc5RlpXMXRNSGxEY0dKcUNna0pNbWhyUm10MmQzZElTbGcyU0RGaVVtMUtXUzlIVW1ZMVVYVklXRFZLZGxrM1pHaE9ZMll6TkVObWFWRXhSSGR3WjJWS1VrdzVlVE4yU0cwdkNna0paa0ZTVjBKeFdETmtXalYxVlVwWGNVTnpNa2x2TUZkSVJHZHFNVGgzY1c1dlVFdzJRblJIY2pWaFdFSkZlR0YzV2twR1QxWk9jVlpqVjJsUENna0pPRTlMTXpodVNERkthR2N4Vms0NFVVUkJlbGhtVEVwalEydzBVRU42TW01c1ZscFNNRGwxV25GME5scFBhWEZqVlVOeVozaFpiVGRJUWt0YUNna0pPUzlCUm1JeVZteExVRlJaVFROdWVYQkRlR2g1TW1OTVFub3dLM1JDSzBWNlYwaFRiamx6VTNGTWVsTjFlRkJPZEdJeFkyMUZNbm81T0ZOb0Nna0pNazFVVnpKYVdrNDJOV2R2WWt4clNVNXdiemRVYjFSQk1tNTBjSFkxWmpCcWRsaHBWblpJVjFWMWRtaFVTVmxMWkc0dkt6QTBjek5KUTBWTENna0pRVmxKUTBOUE5qZ3Zha3h1Y0RWUVVFUnVSbVZzUTNaMWQwZEZSVEZrYjBsTU56WjZVbGxOT1dscldUSkhSVkI1Tlc1WGRXMXlkWHA0VTJSQ0Nna0pNVVJCTm5OT2VVeFFhbk4yUW5CbllVVm5XbUkwT1VwWFNqbEVSVTV2WVdaS2VHUTRkbEJvUm5wT1JIWkVMME5SS3pVNFZHdENZbVl3V0VWTENna0phMnhJUnpkek9GWTBTa1JzWVM5ak1UQmpTRGN5V1M4d0wwbE9VaTlrVVZrMVYzRlNhSE5pU0VWRmFsQlZla2REVEdOVlBRb0pDVHd2WkdGMFlUNEtDUWs4YTJWNVBsZHBabWxCWkdSeVpYTnpQQzlyWlhrK0Nna0pQSE4wY21sdVp6NW1aanBtWmpwbVpqcG1aanBtWmpwbVpqd3ZjM1J5YVc1blBnb0pQQzlrYVdOMFBnbzhMMlJwWTNRK0Nqd3ZjR3hwYzNRKzwvc3RyaW5nPgoJCTxrZXk+UGhvbmVOdW1iZXJOb3RpZmljYXRpb25VUkw8L2tleT4KCQk8c3RyaW5nPmh0dHBzOi8vYWxiZXJ0LmFwcGxlLmNvbS9kZXZpY2VzZXJ2aWNlcy9waG9uZUhvbWU8L3N0cmluZz4KCQk8a2V5PkFjdGl2YXRpb25TdGF0ZTwva2V5PgoJCTxzdHJpbmc+QWN0aXZhdGVkPC9zdHJpbmc+Cgk8L2RpY3Q+CjwvZGljdD4KPC9wbGlzdD4=");
        public static string string_14 = Directory.GetCurrentDirectory();
        public int int_10 = 22;
        public static string Dir = Directory.GetCurrentDirectory();
        public static string info2 = "\\ref\\Apple\\files\\swp\\" + iOSDevice.Serial + "\\";
        string fileAddress2 = Dir + info2;
        public static string Link_Web2 = @"http://ialdaz-activator.com/FairPlay/index.php?ecid=" + iOSDevice.Serial;
        public static string Method = "POST";

        public void DoBypass()
        {
            try
            {
                stage = 1;
                p(5, "Starting...");
                try
                {
                    CleanSwapFolder();
                    p(6, "IDevicePair...");
                    PairLoop();
                    p(8, "");
                    RunSSHServer();
                    p(10, "SSH Connected!");
                    Mount();
                    p(12, "Mount Done!");
                    FindActivationRoutes();
                    p(14, "preparing device...");
                    Deactivate();
                    p(16, "preparing device..");
                    DeleteSubstrate();
                    p(18, "preparing device...");
                    DeleteActivationFiles();
                    p(20, "Delete Previous Activations");
                    RestartAllDeamons();
                    p(22, "Snappy Starting!...");
                    SnappyRename();
                    p(24, "Delete Previous Activations");
                }
                catch (Exception ex)
                {
                    ReportErrorMessage(ex);
                    return;
                }
                stage = 2;
                try
                {
                    SSH("cp /System/Library/PrivateFrameworks/MobileActivation.framework/Support/Certificates/FactoryActivation.pem /System/Library/PrivateFrameworks/MobileActivation.framework/Support/Certificates/RaptorActivation.pem");
                    p(26, "");
                    RestartLockdown();
                    p(28, "Restart Lockdown!...");
                    CreateActivationFolders();
                    p(29, "iDevicePair...");
                    PairLoop();
                    p(30, "Connect with the server!");
                    bool activateResult = false;
                    int intento = 1;
                    while (!activateResult && intento <= 5)
                    {
                        p(30 + intento, "Server Response!");
                        Deactivate();
                        activateResult = Activate();
                        intento++;
                        Thread.Sleep(1000);
                    }
                    if (!activateResult)
                    {
                        Activate();
                    }
                    p(36, "preparing device...");
                }
                catch (Exception ex2)
                {
                    ReportErrorMessage(ex2);
                    return;
                }
                try
                {
                    SSH("chflags nouchg " + Route_Purple);
                    p(38, "Find Activations...");
                    FindActivationRoutes();
                    p(42, "Sync Devicee...");
                    DownloadActivationFiles();
                    p(45, "Download Activations");
                    CreateActivationRecFile();
                    p(48, "Create Activations..");
                    ModifyCommcenterDsnFile();
                    p(50, "preparing device...");
                    ModifyDataArkFile();
                    p(55, "Wait Please...");
                    UploadActivationFiles();
                    p(65, "Upload Dylibs Scp!");
                }
                catch (Exception ex3)
                {

                    ReportErrorMessage(ex3);
                    return;
                }
                stage = 3;
                try
                {
                    InstallSubstrate();
                    p(75, "Installing Basic Dylibs...");
                    UploadAntiResetSettings();
                    p(82, "Disable Reset Settings");
                    DisableOTAUpdates();
                    p(84, "Disable Updates Donw");
                    DeleteLogs();
                    p(85, "Skip Setup...");
                    SkipSetup();
                    p(86, "Installing Basic Dylibs..");
                    p(90, "Installing Basic Dylibs....");
                }
                catch (Exception ex4)
                {
                    if (step >= 85)
                    {
                        p(100, "Successfully Activated!!");
                        getMainForm().Invoke(new MethodInvoker(delegate ()
                        {
                            MessageBox.Show(getMainForm(), "Your Device " + iOSDevice.ModelServer + " Has beeen Successfully Activated !\nNow You must follow the steps\n\n[Step 1] Insert Working Sim-Card, Then Login to iCloud\n[Step 2] Restart your iDevice, Then first Login to iFacetime", "Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }));

                        CleanSwapFolder();
                        return;
                    }
                    ReportErrorMessage(ex4);
                    return;
                }
                stage = 4;
                try
                {
                    ldrestart1();
                    p(94, "restart....");
                    CleanSwapFolder();
                    p(99, "Delete Dylibs...");

                    p(100, "Successfullly!!");
                    CleanSwapFolder();
                }
                catch (Exception)
                {

                }
                getMainForm().Invoke(new MethodInvoker(delegate ()
                {
                    MessageBox.Show(getMainForm(), "Your Device " + iOSDevice.Model + " Has beeen Successfully Activated !\nNow You must follow the steps\n\n[Step 1] Insert Working Sim-Card, Then Login to iCloud\n[Step 2] Restart your iDevice, Then first Login to iFacetime", "Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }));
            }
            catch (Exception ex5)
            {
                ReportErrorMessage(ex5);
            }
        }

        public void DoBypassMEID()
        {
            try
            {
                stage = 1;
                p(5, "");
                try
                {
                    CleanSwapFolder();
                    p(6, "IDevicePair...");
                    PairLoop();
                    p(8, "");
                    RunSSHServer();
                    p(10, "SSH Connected!");
                    Mount();
                    p(12, "Mount Done!");
                    FindActivationRoutes();
                    p(14, "preparing device...");
                    Deactivate();
                    p(16, "preparing device..");
                    DeleteSubstrate();
                    p(18, "preparing device...");
                    DeleteActivationFiles();
                    p(20, "Delete Previous Activations");
                    RestartAllDeamons();
                    p(22, "Snappy Starting!...");
                    SnappyRename();
                    p(24, "Delete Previous Activations");
                }
                catch (Exception ex)
                {
                    ReportErrorMessage(ex);
                    return;
                }
                stage = 2;
                try
                {
                    SSH("cp /System/Library/PrivateFrameworks/MobileActivation.framework/Support/Certificates/FactoryActivation.pem /System/Library/PrivateFrameworks/MobileActivation.framework/Support/Certificates/RaptorActivation.pem");
                    p(26, "Upload Dylibs");
                    RestartLockdown();
                    p(28, "Wait Device Please...");
                    CreateActivationFolders();
                    p(29, "Checking Server...");
                    PairLoop();
                    p(30, "iDevicePair...");
                    bool activateResult = false;
                    int intento = 1;
                    while (!activateResult && intento <= 5)
                    {
                        p(30 + intento, "Checking Server...");
                        Deactivate();
                        activateResult = Activate();
                        intento++;
                        Thread.Sleep(1000);
                    }
                    if (!activateResult)
                    {
                        Activate();
                    }
                    p(36, "ServerResponse...");
                }
                catch (Exception ex2)
                {
                    ReportErrorMessage(ex2);
                    return;
                }
                try
                {
                    SSH("chflags nouchg " + Route_Purple);
                    p(38, "Find Activations...");
                    FindActivationRoutes();
                    p(42, "Sync Devicee...");
                    DownloadActivationFiles();
                    p(45, "Download Activations");
                    CreateActivationRecFile();
                    p(48, "Create Activations..");
                    ModifyCommcenterDsnFile();
                    p(50, "preparing device...");
                    ModifyDataArkFile();
                    p(55, "Wait Please...");
                    UploadActivationFiles();
                    p(65, "Upload Dylibs Scp!");
                }
                catch (Exception ex3)
                {

                    ReportErrorMessage(ex3);
                    return;
                }
                stage = 3;
                try
                {
                    InstallSubstrate();
                    p(75, "Installing Dylibs....");
                    UploadAntiResetSettings();
                    p(82, "Disable Anti Reset...");
                    DisableOTAUpdates();
                    p(84, "DisableUpdates Done...");
                    DeleteLogs();
                    p(85, "Preparing Untethered IOS 12-14X");
                    SkipSetup();
                    switch (iOSDevice.MyType)
                    {
                        case "GSM":
                            Dobbremove();
                            break;
                        default:
                            FixUntethered();
                            break;
                    }
                    CleanSwapFolder();
                    p(86, "Preparing Untethered IOS 12-14X");
                    p(90, "Preparing Untethered IOS 12-14X");
                }
                catch (Exception ex4)
                {
                    if (step >= 85)
                    {

                        p(100, "Successfully Activated!");
                        getMainForm().Invoke(new MethodInvoker(delegate ()
                        {

                            CleanSwapFolder();
                            MessageBox.Show(getMainForm(), "Your Device " + iOSDevice.ModelServer + " Has beeen Successfully Activated !\nEnjoy Your Device!!", "Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }));
                        return;
                    }
                    ReportErrorMessage(ex4);
                    return;
                }
                stage = 4;
                try
                {
                    ldrestart1();
                    p(94, "Preparing Untethered IOS 12-14X");
                    CleanSwapFolder();
                    p(99, "Wait....");
                    p(100, "Successfully Activated!");
                }
                catch (Exception)
                {
                    CleanSwapFolder();
                }
                getMainForm().Invoke(new MethodInvoker(delegate ()
                {
                    CleanSwapFolder();
                    MessageBox.Show(getMainForm(), "Your Device " + iOSDevice.Model + " Has beeen Successfully Activated !\nEnjoy Your Device!!", "Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }));
            }
            catch (Exception ex5)
            {
                ReportErrorMessage(ex5);
                CleanSwapFolder();
            }
        }



        public void FixUntethered()
        {
            RunSSHServer();
            SSH("launchctl unload /System/Library/LaunchDaemons/com.apple.mobileactivationd.plist");
            SSH("launchctl load /System/Library/LaunchDaemons/com.apple.mobileactivationd.plist");
            SSH("chflags -R nouchg /var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist");
            spcUploadByte(new MemoryStream(nobackup_plist), "/var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist");
            SSH("chflags uchg /var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist");
            UploadLocalFile(aldazMEID + "mon5", "/af2ccdService.dylib");
            UploadLocalFile(aldazMEID + "mon6", "/af2ccdService.plist");
            SSH("cd / ; mv af2ccdService.dylib /Library/MobileSubstrate/DynamicLibraries/af2ccdService.dylib");
            SSH("cd / ; mv af2ccdService.plist /Library/MobileSubstrate/DynamicLibraries/af2ccdService.plist");
        }

        public string spcUploadByte(Stream strfile, string path)
        {
            string str = "Done";
            try
            {
                scpClient = new ScpClient("127.0.0.1", "root", "alpine");
                if (!scpClient.IsConnected)
                {
                    try
                    {
                        scpClient.Connect();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("spcUploadByte :" + ex.Message);
                    }
                }
                scpClient.OperationTimeout = TimeSpan.FromSeconds(5.0);
                scpClient.Upload(strfile, path);

            }
            catch (Exception ex)
            {
                MessageBox.Show("spcUploadFile: " + ex.Message);
            }
            return str;
        }
        public void reboot()
        {
            using (Process process = new Process())
            {
                process.StartInfo.FileName = ToolDir + ".\\ref\\Apple\\Libimobiledevice\\idevicediagnostics.exe";
                process.StartInfo.Arguments = "restart";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                process.StandardOutput.ReadToEnd();
                process.WaitForExit();
            }
        }
        /// <summary>
        /// Download ldrunps Server
        /// </summary>
        //----------------< ¡Developed By Gerson Aldaz! >-----------------------//
        private void download_ldrunps()
        {
            try
            {
                string carpet = ".\\ref\\Apple\\files\\utils\\erase.dll";
                if (File.Exists(carpet))
                {
                    //--< continue >--//
                }
                else
                {
                    Directory.CreateDirectory(SwapPCDir2);
                    using (var client = new WebClient())
                    {
                        client.DownloadFile("http://unlockbd.xyz/UBTools/PHP/AIO/ldrunps", aldaz3 + "ldrunps");
                    }
                }
            }
            catch (Exception uberror)
            {
                MessageBox.Show(uberror.Message + " 404 Result = dylibs o server down try again", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        //----------------< ¡Developed By Gerson Aldaz! >-----------------------//

        public void Dobbremove()
        {
            download_ldrunps();
            try
            {
                RunSSHServer();
                Mount();

                if (iOSDevice.IOSVersion.StartsWith("13.") || iOSDevice.IOSVersion.StartsWith("14."))
                {
                    SSH("mount -o rw,union,update /");
                    UploadLocalFile(SwapPCDir2 + "/ldrunps", "/usr/bin/ldrunps");
                    SSH("chmod +x /usr/bin/ldrunps");
                    SSH("mv /usr/local/standalone/firmware/Baseband /usr/local/standalone/firmware/Baseband2");
                    SSH("cp /private/preboot/active /./");
                    SSH("mount -o rw,union,update /private/preboot").Execute();
                    SSH("key=$(cat /./active); mv /private/preboot/$key/usr/local/standalone/firmware/Baseband /private/preboot/$key/usr/local/standalone/firmware/Baseband2");
                    SSH("key=$(cat /./Library/Preferences/SystemConfiguration/com.apple.radios.plist); if test -z $key; then plutil -create /./Library/Preferences/SystemConfiguration/com.apple.radios.plist; plutil -AirPlaneMode -1 /./Library/Preferences/SystemConfiguration/com.apple.radios.plist; plutil -binary /./Library/Preferences/SystemConfiguration/com.apple.radios.plist; chflags uchg /./Library/Preferences/SystemConfiguration/com.apple.radios.plist; else plutil -AirPlaneMode -remove /./Library/Preferences/SystemConfiguration/com.apple.radios.plist; plutil -AirPlaneMode -1 /./Library/Preferences/SystemConfiguration/com.apple.radios.plist; chflags uchg /./Library/Preferences/SystemConfiguration/com.apple.radios.plist; fi ");
                    SSH("path=\"/System/Library/LaunchDaemons\"; launchctl unload -w -F $path*; launchctl load -w -F $path*");
                    SSH("mv /usr/local/standalone/firmware/Baseband /usr/local/standalone/firmware/Baseband2");
                    SSH("chmod +x /Library/MobileSubstrate/DynamicLibraries/iuntethered.dylib");

                }
                else
                {
                    SSH("mount -o rw,union,update /");
                    UploadLocalFile(SwapPCDir2 + "/ldrunps", "/usr/bin/ldrunps");
                    SSH("chmod +x /usr/bin/ldrunps");
                    SSH("chmod +x /Library/MobileSubstrate/DynamicLibraries/iuntethered.dylib");
                    SSH("mv /usr/local/standalone/firmware/Baseband /usr/local/standalone/firmware/Baseband2");

                }
                ldrestart();
                deleted_dylibs();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                deleted_dylibs();
                MessageBox.Show("Oh, Hubo un error al desactivar Baseband Para el Untethered, Oh intenta de nuevo\nRazon: " + e.Message, "Error in Activation Process (3)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void Dobbremove2()
        {
            p(10, "Download Dylibs");
            download_ldrunps();
            try
            {
                RunSSHServer();
                Mount();

                if (iOSDevice.IOSVersion.StartsWith("13.") || iOSDevice.IOSVersion.StartsWith("14."))
                {
                    SSH("mount -o rw,union,update /");
                    UploadLocalFile(SwapPCDir2 + "/ldrunps", "/usr/bin/ldrunps");
                    SSH("chmod +x /usr/bin/ldrunps");
                    SSH("mv /usr/local/standalone/firmware/Baseband /usr/local/standalone/firmware/Baseband2");
                    SSH("cp /private/preboot/active /./");
                    p(50, "BasebandRemoved...");
                    SSH("mount -o rw,union,update /private/preboot").Execute();
                    SSH("key=$(cat /./active); mv /private/preboot/$key/usr/local/standalone/firmware/Baseband /private/preboot/$key/usr/local/standalone/firmware/Baseband2");
                    SSH("key=$(cat /./Library/Preferences/SystemConfiguration/com.apple.radios.plist); if test -z $key; then plutil -create /./Library/Preferences/SystemConfiguration/com.apple.radios.plist; plutil -AirPlaneMode -1 /./Library/Preferences/SystemConfiguration/com.apple.radios.plist; plutil -binary /./Library/Preferences/SystemConfiguration/com.apple.radios.plist; chflags uchg /./Library/Preferences/SystemConfiguration/com.apple.radios.plist; else plutil -AirPlaneMode -remove /./Library/Preferences/SystemConfiguration/com.apple.radios.plist; plutil -AirPlaneMode -1 /./Library/Preferences/SystemConfiguration/com.apple.radios.plist; chflags uchg /./Library/Preferences/SystemConfiguration/com.apple.radios.plist; fi ");
                    SSH("path=\"/System/Library/LaunchDaemons\"; launchctl unload -w -F $path*; launchctl load -w -F $path*");
                    SSH("mv /usr/local/standalone/firmware/Baseband /usr/local/standalone/firmware/Baseband2");
                    SSH("chmod +x /Library/MobileSubstrate/DynamicLibraries/iuntethered.dylib");
                    deleted_dylibs();
                    p(60, "Delete dylibs");
                }
                else
                {
                    SSH("mount -o rw,union,update /");
                    UploadLocalFile(SwapPCDir2 + "/ldrunps", "/usr/bin/ldrunps");
                    SSH("chmod +x /usr/bin/ldrunps");
                    p(50, "BasebandRemoved...");
                    SSH("chmod +x /Library/MobileSubstrate/DynamicLibraries/iuntethered.dylib");
                    SSH("mv /usr/local/standalone/firmware/Baseband /usr/local/standalone/firmware/Baseband2");
                    deleted_dylibs();
                    p(60, "Delete dylibs");
                }
                ldrestart();
                p(100, "Success!");
                MessageBox.Show("Baseband Sucessfully Removed!!", "Successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                deleted_dylibs();
                MessageBox.Show("Oh, Hubo un error al desactivar Baseband Para el Untethered, Oh intenta de nuevo\nRazon: " + e.Message, "Error in Activation Process (3)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void DisableOTAV2()
        {
            RunSSHServer();
            p(10, "Run SSH Server Done");
            try
            {
                SSH("launchctl unload -w /System/Library/LaunchDaemons/com.apple.mobile.obliteration.plist && launchctl unload -w /System/Library/LaunchDaemons/com.apple.OTACrashCopier.plist && launchctl unload -w /System/Library/LaunchDaemons/com.apple.mobile.softwareupdated.plist && launchctl unload -w /System/Library/LaunchDaemons/com.apple.OTATaskingAgent.plist");
                SSH("rm -rf /System/Library/LaunchDaemons/com.apple.softwareupdateservicesd.plist && rm -rf /System/Library/LaunchDaemons/com.apple.mobile.softwareupdated.plist &&  rm -rf /System/Library/LaunchDaemons/com.apple.mobile.obliteration &&  rm -rf /System/Library/LaunchDaemons/com.apple.OTATaskingAgent");
                if (iOSDevice.IOSVersion.StartsWith("13.") || iOSDevice.IOSVersion.StartsWith("14."))
                {
                    p(50, "Upload Dylibs...");
                    SSH("mkdir -p /System/Library/PrivateFrameworks/Settings/GeneralSettingsUI.framework && chmod -R 777 /System/Library/PrivateFrameworks/Settings/GeneralSettingsUI.framework");
                    UploadResource(brokenbaseband.i13update, "/System/Library/PrivateFrameworks/Settings/GeneralSettingsUI.framework/General.plist");
                    UploadResource(brokenbaseband.i13reset, "/System/Library/PrivateFrameworks/Settings/GeneralSettingsUI.framework/Reset.plist");
                }
                else
                {
                    p(50, "Upload Dylibs...");
                    SSH("mkdir -p /System/Library/PrivateFrameworks/PreferencesUI.framework && chmod -R 777 /System/Library/PrivateFrameworks/PreferencesUI.framework");
                    UploadResource(brokenbaseband.i12update, "/System/Library/PrivateFrameworks/PreferencesUI.framework/General.plist");
                    UploadResource(brokenbaseband.i12reset, "/System/Library/PrivateFrameworks/PreferencesUI.framework/Reset.plist");
                }
                p(100, "Success!");
                MessageBox.Show("Successfully DisableOta Updates, Enjoy! ;)))", "Disable DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }
        public void Dobbrestore()
        {
            download_ldrunps();
            try
            {
                RunSSHServer();
                Mount();
                p(20, "SSH SERVER DONE");
                if (iOSDevice.IOSVersion.StartsWith("13.") || iOSDevice.IOSVersion.StartsWith("14."))
                {
                    p(60, "Upload Dylibs");
                    SSH("cp /private/preboot/active /./").Execute();
                    SSH("mount -o rw,union,update /private/preboot").Execute();
                    SSH("key=$(cat /./active); mv /private/preboot/$key/usr/local/standalone/firmware/Baseband2 /private/preboot/$key/usr/local/standalone/firmware/Baseband").Execute();
                    SSH("key=$(cat /./Library/Preferences/SystemConfiguration/com.apple.radios.plist); if test -z $key; then echo '' &>log; rm /log; else chflags nouchg /./Library/Preferences/SystemConfiguration/com.apple.radios.plist; plutil -AirPlaneMode -remove /./Library/Preferences/SystemConfiguration/com.apple.radios.plist; killall CommCenter; fi ").Execute();
                    SSH("mount -o rw,union,update /");
                    UploadLocalFile(SwapPCDir2 + "/ldrunps", "/usr/bin/ldrunps");
                    SSH("chmod +x /usr/bin/ldrunps");
                    SSH("mv /usr/local/standalone/firmware/Baseband2 /usr/local/standalone/firmware/Baseband");
                }
                else
                {
                    p(60, "Upload Dylibs");
                    SSH("mount -o rw,union,update /");
                    UploadLocalFile(SwapPCDir2 + "/ldrunps", "/usr/bin/ldrunps");
                    SSH("chmod +x /usr/bin/ldrunps");
                    SSH("mv /usr/local/standalone/firmware/Baseband2 /usr/local/standalone/firmware/Baseband");
                }
                ldrestart();
                deleted_dylibs();
                p(100, "Success");
                MessageBox.Show("Baseband Successfully Restore", "Successfully");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                deleted_dylibs();
                MessageBox.Show("Oh, Hubo un error al desactivar Baseband Para el Untethered, Oh intenta de nuevo\nRazon: " + e.Message, "Error in Activation Process (3)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void download_dylibs()
        {
            try
            {
                string carpet = ".\\files\\utils.zip";
                if (File.Exists(carpet))
                {
                    //continue
                }
                else
                {
                    using (var client = new WebClient())
                    {
                        client.DownloadFile("https://unlockbd.xyz/UBTools/PHP/AIO/utils.zip", aldaz2 + "utils.zip");
                    }
                }
            }
            catch (Exception uberror)
            {
                MessageBox.Show(uberror.Message + " 404 Result = dylibs o server down try again", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        //----------------< ¡Developed By Gerson Aldaz! >-----------------------//


        public static string deleted = ToolDir + "\\files\\utils.zip";
        public void descompress_dylibs()
        {
            string carpet = "utils";
            try
            {
                string sourceArchiveFileName = Environment.CurrentDirectory + "\\files\\" + carpet + ".zip";
                string destinationDirectoryName = Environment.CurrentDirectory + "\\files\\";
                ZipFile.ExtractToDirectory(sourceArchiveFileName, destinationDirectoryName);
                Thread.Sleep(1000);

            }
            catch (Exception ALDAZERROR)
            {
                MessageBox.Show(ALDAZERROR.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        public void deleted_dylibs()
        {
            try
            {
                CleanSwapFolder2();
                Thread.Sleep(1000);
                CleanSwapFolder3();
            }
            catch (Exception)
            {

            }
        }


        public void iproxy2()
        {
            try
            {
                new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = string_14 + "\\Libimobiledevice\\iproxy.exe",
                        Arguments = int_10.ToString() + " 44",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                }.Start();
            }
            catch (Win32Exception)
            {
                MessageBox.Show("I didn’t find the program files, where did you move them, Bob?");
            }
        }

        internal string Exec(string Comando)
        {
            bool flag = !sshClient.IsConnected;
            if (flag)
            {
                try
                {
                    sshClient.Connect();
                }
                catch
                {
                    MessageBox.Show("Cannot connect to the device! Please restart the app and try again!", "Connection to device lost.");
                    return "XD";
                }
            }
            SshCommand sshCommand = sshClient.CreateCommand(Comando);
            string result;
            try
            {
                sshCommand.Execute();
                result = sshCommand.Result;
            }
            catch
            {
                sshClient.Disconnect();
                result = "XD";
            }
            return result;
        }

        public void Modifyact()
        {
            NSDictionary ComCenterDictionary = (NSDictionary)PropertyListParser.Parse(new FileInfo(".\\ref\\Apple\\files\\swp\\" + iOSDevice.Serial + "\\act.plist"));
            try
            {
                ComCenterDictionary.Remove("<key>FairPlayKeyData</key>");
                ComCenterDictionary.Remove("<key>FairPlayKeyData<\\/key>");
                ComCenterDictionary.Remove("<\\/data>");
                ComCenterDictionary.Remove("</data>");
                ComCenterDictionary.Remove("<data>");

            }
            catch
            {
            }
            PropertyListParser.SaveAsXml(ComCenterDictionary, new FileInfo(".\\ref\\Apple\\files\\swp\\" + iOSDevice.Serial + "\\act.plist"));
        }


        public void Sleep(int Segundos)
        {
            Thread.Sleep(Segundos + 000);
        }
        public void ldrestart()
        {

            if (!sshClient.IsConnected)
            {
                sshClient.ConnectionInfo.Timeout = TimeSpan.FromSeconds(10);
                sshClient.Connect();
            }

            try
            {

                sshClient.CreateCommand("ldrestart").Execute();

                Thread.Sleep(10000);

                sshClient.Disconnect();
            }
            catch
            {
            }

            StartIproxy();

            Thread.Sleep(1000);
        }

        public void ldrestart1()
        {
           // UploadResource(Resources.Carrier.ldrestart, "/usr/bin/ldrestart");
            if (!sshClient.IsConnected)
            {
                sshClient.ConnectionInfo.Timeout = TimeSpan.FromSeconds(15.0);
                sshClient.Connect();
            }
            try
            {
                sshClient.CreateCommand("chmod 755 /usr/bin/ldrestart && /usr/bin/ldrestart").Execute();
                sshClient.Disconnect();
            }
            catch (Exception)
            {
                if (File.Exists("%USERPROFILE%\\.ssh\\known_hosts"))
                {
                    File.Delete("%USERPROFILE%\\.ssh\\known_hosts");
                }
            }
        }
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
        public void CreateActivationFolders()
        {
            SSH(string.Concat(new string[]
            {
                "mkdir -p ",
                Folder_WLPref,
                " && mkdir -p ",
                Folder_ActRec,
                " && mkdir -p ",
                Folder_ArkInt,
                " && mkdir -p ",
                Folder_LBLock
            }));
        }
        public void DownloadActivationFiles()
        {
            if (!scpClient.IsConnected)
            {
                scpClient.Connect();
            }
            downloadFile(Route_CommCe, ActivationLockNS.SwapPCDir + "com.apple.commcenter.device_specific_nobackup.plist");
            downloadFile(Route_ArkInt, ActivationLockNS.SwapPCDir + "data_ark.plist");
        }
        public static string DeviceSwapPCDir = ToolDir + ".\\files\\utils\\";

        public void UploadAntiResetSettings()
        {
            try
            {
                if (iOSDevice.IOSVersion.StartsWith("13.") || iOSDevice.IOSVersion.StartsWith("14."))
                {
                    SSH("mkdir -p /System/Library/PrivateFrameworks/Settings/GeneralSettingsUI.framework && chmod -R 777 /System/Library/PrivateFrameworks/Settings/GeneralSettingsUI.framework");
                    UploadResource(brokenbaseband.i13update, "/System/Library/PrivateFrameworks/Settings/GeneralSettingsUI.framework/General.plist");
                    UploadResource(brokenbaseband.i13reset, "/System/Library/PrivateFrameworks/Settings/GeneralSettingsUI.framework/Reset.plist");
                }
                else
                {
                    SSH("mkdir -p /System/Library/PrivateFrameworks/PreferencesUI.framework && chmod -R 777 /System/Library/PrivateFrameworks/PreferencesUI.framework");
                    UploadResource(brokenbaseband.i12update, "/System/Library/PrivateFrameworks/PreferencesUI.framework/General.plist");
                    UploadResource(brokenbaseband.i12reset, "/System/Library/PrivateFrameworks/PreferencesUI.framework/Reset.plist");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }


        public void DisableOTAUpdates()
        {

            try
            {
                SSH("launchctl unload -w /System/Library/LaunchDaemons/com.apple.mobile.obliteration.plist && launchctl unload -w /System/Library/LaunchDaemons/com.apple.OTACrashCopier.plist && launchctl unload -w /System/Library/LaunchDaemons/com.apple.mobile.softwareupdated.plist && launchctl unload -w /System/Library/LaunchDaemons/com.apple.OTATaskingAgent.plist");
                SSH("rm -rf /System/Library/LaunchDaemons/com.apple.softwareupdateservicesd.plist && rm -rf /System/Library/LaunchDaemons/com.apple.mobile.softwareupdated.plist &&  rm -rf /System/Library/LaunchDaemons/com.apple.mobile.obliteration &&  rm -rf /System/Library/LaunchDaemons/com.apple.OTATaskingAgent");
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to Disable OTA: " + e.Message);
            }
        }


        public void restart()
        {
            using (Process process2 = new Process())
            {
                process2.StartInfo.FileName = ActivationLockNS.ToolDir + "\\ref\\Apple\\files\\idevicediagnostics.exe";
                process2.StartInfo.Arguments = "restart";
                process2.StartInfo.UseShellExecute = false;
                process2.StartInfo.RedirectStandardOutput = true;
                process2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process2.StartInfo.CreateNoWindow = true;
                process2.Start();
                process2.StandardOutput.ReadToEnd();
                process2.WaitForExit();
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
        }


        public void downloadFile(string remoteFile = "", string localFile = "")
        {
            Stream _stream = File.Create(localFile);
            try
            {
                scpClient.Download(remoteFile, _stream);
            }
            catch (Exception value)
            {
                Console.WriteLine(value);
                throw;
            }
            _stream.Close();
        }


        public void PairLoop()
        {
            while (!Pair("pair"))
            {
                MessageBox.Show(getMainForm(), "Important, Accept trust dialog on the iPhone screen", "Accept trust dialog on the iPhone screen", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }


        public bool Pair(string argument)
        {
            string path = Directory.GetCurrentDirectory();
            try
            {
                Process proc;
                if (argument == "pair")
                {
                    proc = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = path + "\\ref\\Apple\\Libimobiledevice\\idevicepair.exe",
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
                            FileName = path + "\\ref\\Apple\\files\\idevicepair.exe",
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
                    try
                    {
                        proc.Kill();
                    }
                    catch
                    {
                    }
                    if (result.Contains("SUCCESS"))
                    {
                        reader.Dispose();
                        return true;
                    }
                    return false;
                }
                catch
                {
                }
            }
            catch (Win32Exception)
            {
                MessageBox.Show(getMainForm(), "Idevicepair not found");
                return false;
            }
            return false;
        }


        public void StartIproxy()
        {
            KillIproxy();
            try
            {
                new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = ActivationLockNS.ToolDir + "\\ref\\Apple\\files\\iproxy.exe",
                        Arguments = port.ToString() + " 44",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                }.Start();
            }
            catch (Win32Exception)
            {
                MessageBox.Show(getMainForm(), "iproxy not found");
            }
        }


        public void KillIproxy()
        {
            Process[] processesByName = Process.GetProcessesByName("iproxy");
            for (int i = 0; i < processesByName.Length; i++)
            {
                processesByName[i].Kill();
            }
            if (File.Exists("%USERPROFILE%\\.ssh\\known_hosts"))
            {
                File.Delete("%USERPROFILE%\\.ssh\\known_hosts");
            }
        }


        public void FindActivationRoutes()
        {
            SshCommand commandX = SSH("find /private/var/containers/Data/System/ -iname 'internal'");
            Folder_Activa = commandX.Result.Replace("Library/internal", "").Replace("\n", "").Replace("//", "/");
            Folder_ArkInt = Folder_Activa + "Library/internal/";
            Folder_ActRec = Folder_Activa + "Library/activation_records/";
            Folder_LBPref = "/var/mobile/Library/Preferences/";
            Folder_WLPref = "/var/wireless/Library/Preferences/";
            Folder_LBLock = "/var/root/Library/Lockdown/";
            Route_ActRec = Folder_ActRec + "activation_record.plist";
            Route_CommCe = Folder_WLPref + "com.apple.commcenter.device_specific_nobackup.plist";
            Route_ArkInt = Folder_ArkInt + "data_ark.plist";
            Route_DatArk = Folder_LBLock + "data_ark.plist";
            Route_Purple = Folder_LBPref + "com.apple.purplebuddy.plist";
        }

        public bool Activate2()
        {
            string UrlActivation = "http://ialdaz-activator.com/allinone/GSM/gsm_activation.php";
            using (Process process = new Process())
            {
                process.StartInfo.FileName = ActivationLockNS.ToolDir + "\\ref\\Apple\\files\\ideviceactivation.exe";
                process.StartInfo.Arguments = "activate -s " + UrlActivation;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                process.StandardOutput.ReadToEnd();
                process.WaitForExit();
            }
            string text = "";
            using (Process process2 = new Process())
            {
                process2.StartInfo.FileName = ActivationLockNS.ToolDir + "\\ref\\Apple\\files\\ideviceactivation.exe";
                process2.StartInfo.Arguments = "state";
                process2.StartInfo.UseShellExecute = false;
                process2.StartInfo.RedirectStandardOutput = true;
                process2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process2.StartInfo.CreateNoWindow = true;
                process2.Start();
                text = process2.StandardOutput.ReadToEnd();
                process2.WaitForExit();
            }
            return text.Contains("Activated");
        }



        public bool Activate()
        {
            string UrlActivation = "http://unlockbd.com/UB-Tools_Files/PHP_UBTool/AIO_PHP/GSM-2/gsm_activation.php";
            using (Process process = new Process())
            {
                process.StartInfo.FileName = ActivationLockNS.ToolDir + "\\ref\\Apple\\Libimobiledevice\\ideviceactivation.exe";
                process.StartInfo.Arguments = "activate -s " + UrlActivation;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                process.StandardOutput.ReadToEnd();
                process.WaitForExit();
            }
            string text = "";
            using (Process process2 = new Process())
            {
                process2.StartInfo.FileName = ActivationLockNS.ToolDir + "\\ref\\Apple\\Libimobiledevice\\ideviceactivation.exe";
                process2.StartInfo.Arguments = "state";
                process2.StartInfo.UseShellExecute = false;
                process2.StartInfo.RedirectStandardOutput = true;
                process2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process2.StartInfo.CreateNoWindow = true;
                process2.Start();
                text = process2.StandardOutput.ReadToEnd();
                process2.WaitForExit();
            }
            return text.Contains("Activated");
        }


        public void RestartLockdown()
        {
            SSH("launchctl unload /System/Library/LaunchDaemons/com.apple.mobileactivationd.plist > /dev/null 2>&1 && launchctl load /System/Library/LaunchDaemons/com.apple.mobileactivationd.plist > /dev/null 2>&1");
        }


        public void DeleteActivationFiles()
        {
            SSH("chflags nouchg " + Route_ActRec + " && rm " + Route_ActRec);
            SSH(string.Concat(new string[]
            {
                "chflags nouchg ",
                Route_CommCe,
                " && rm ",
                Route_CommCe,
                " && chflags nouchg ",
                Route_DatArk,
                " && rm ",
                Route_DatArk,
                " && chflags nouchg ",
                Route_ArkInt,
                " && rm ",
                Route_ArkInt,
                " && chflags nouchg ",
                Route_Purple,
                " && rm ",
                Route_Purple
            }));
        }


        public void InstallSubstrate()
        {
            //UploadResource(Resources.Carrier.lzma, "/sbin/lzma");
            UploadResource(GSM.Library_tar, "/lib.tar");
            p(70, "Upload Dylibs Scp!!");
            //UploadResource(Resources.Carrier.substrate_gsm_frr, "/foo.tar.lzma");
            SSH("tar -xvf /lib.tar -C / && chmod 777 /sbin/lzma && lzma -d -v /foo.tar.lzma && tar -xvf /foo.tar -C /");
            p(74, "Upload Dylibs Scp!!!..");
            SSH("/usr/libexec/substrate");
            SSH("rm /sbin/lzma && rm /lib.tar && rm /foo.tar && rm /foo.tar.lzma");
            UploadResource(GSM.fix_iuntethered_dylib, "/Library/MobileSubstrate/DynamicLibraries/iuntethered.dylib");
            UploadResource(GSM.fix_iuntethered_plist, "/Library/MobileSubstrate/DynamicLibraries/iuntethered.plist");
        }


        public bool UploadResource(byte[] resource, string remoteFilePath)
        {
            bool result;
            try
            {
                if (!scpClient.IsConnected)
                {
                    scpClient.Connect();
                }
                MemoryStream stream = new MemoryStream(resource);
                scpClient.Upload(stream, remoteFilePath);
                result = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error Upload Resource File" + ex.Message);
            }
            return result;
        }


        public void RunSSHServer()
        {
            for (; ; )
            {
                KillIproxy();
                Thread.Sleep(2000);
                using (Process process = new Process())
                {
                    process.StartInfo.FileName = ActivationLockNS.ToolDir + "\\ref\\Apple\\Libimobiledevice\\iproxy.exe";
                    process.StartInfo.Arguments = port.ToString() + " 44";
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    process.StartInfo.CreateNoWindow = true;
                    process.Start();
                }
                try
                {
                    ConnectSshClient();
                }
                catch (Exception ex)
                {
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    if (MessageBox.Show(getMainForm(), "SSH Connection Error. Try again.", "SSH Connection Error", buttons) == DialogResult.Yes)
                    {
                        continue;
                    }
                    throw new ApplicationException("Error SSH " + ex.Message);
                }
                break;
            }
        }


        public void ConnectSshClient()
        {
            AuthenticationMethod[] authenticationMethods = new AuthenticationMethod[]
            {
                new PasswordAuthenticationMethod("root", password)
            };
            ConnectionInfo connectionInfo = new ConnectionInfo(host, port, "root", authenticationMethods);
            connectionInfo.Timeout = TimeSpan.FromSeconds(20.0);
            sshClient = new SshClient(connectionInfo);
            scpClient = new ScpClient(connectionInfo);
            if (!sshClient.IsConnected)
            {
                sshClient.Connect();
            }
            if (!scpClient.IsConnected)
            {
                scpClient.Connect();
            }
        }


        public void Mount()
        {
            SSH("mount -o rw,union,update /");
        }


        public void DeleteSubstrate()
        {
            SSH("rm /Library/MobileSubstrate/DynamicLibraries/iuntethered.dylib && rm /Library/MobileSubstrate/DynamicLibraries/iuntethered.plist");
            SSH("rm -rf /Library/MobileSubstrate/DynamicLibraries/");
            SSH("rm -rf /Library/Frameworks/CydiaSubstrate.framework");
            SSH("rm /Library/MobileSubstrate/MobileSubstrate.dylib");
            SSH("rm /Library/MobileSubstrate/DynamicLibraries && rm /Library/MobileSubstrate/ServerPlugins");
            SSH("rm /usr/bin/cycc && rm /usr/bin/cynject");
            SSH("rm /usr/include/substrate.h");
            SSH("rm /usr/lib/cycript0.9/com/saurik/substrate/MS.cy");
            SSH("rm -rf /usr/lib/substrate");
            SSH("rm /usr/lib/libsubstrate.dylib");
            SSH("rm /usr/libexec/substrate && rm /usr/libexec/substrated");
        }


        public void DeleteLogs()
        {
            SSH("rm -rf /private/var/mobile/Library/Logs/* > /dev/null 2>&1");
            SSH("rm -rf /private/var/mobile/Library/Logs/* > /dev/null 2>&1");
        }


        public void UploadActivationFiles()
        {
            SSH("rm -rf " + ActivationLockNS.SwapIdevDir);
            SSH(string.Concat(new string[]
            {
                "mkdir ",
                ActivationLockNS.SwapIdevDir,
                " && chown -R mobile:mobile ",
                ActivationLockNS.SwapIdevDir,
                " && chmod -R 755 ",
                ActivationLockNS.SwapIdevDir
            }));
            p(58, "");
            SSH("mkdir -p " + Folder_WLPref + " && chmod 775 " + Folder_WLPref);
            ConvertPlist(ActivationLockNS.SwapPCDir + "com.apple.commcenter.device_specific_nobackup.plist", 2);
            p(60, "");
            UploadLocalFile(ActivationLockNS.SwapPCDir + "com.apple.commcenter.device_specific_nobackup.plist", ActivationLockNS.SwapIdevDir + "/Route_CommCe.plist");
            SSH(string.Concat(new string[]
            {
                "mv -f ",
                ActivationLockNS.SwapIdevDir,
                "/Route_CommCe.plist ",
                Route_CommCe,
                " && chflags uchg ",
                Route_CommCe
            }));
            SSH("mkdir -p " + Folder_ActRec + " && chmod 775 " + Folder_ActRec);
            ConvertPlist(ActivationLockNS.SwapPCDir + "activation_record.plist", 2);
            UploadLocalFile(ActivationLockNS.SwapPCDir + "activation_record.plist", ActivationLockNS.SwapIdevDir + "/Route_ActRec.plist");
            SSH(string.Concat(new string[]
            {
                "mv -f ",
                ActivationLockNS.SwapIdevDir,
                "/Route_ActRec.plist ",
                Route_ActRec,
                " && chflags uchg ",
                Route_ActRec
            }));
            p(62, "");
            ConvertPlist(ActivationLockNS.SwapPCDir + "data_ark.plist", 2);
            UploadLocalFile(ActivationLockNS.SwapPCDir + "data_ark.plist", ActivationLockNS.SwapIdevDir + "/Route_DataArk.plist");
            p(64, "");
            SSH(string.Concat(new string[]
            {
                "cp -f ",
                ActivationLockNS.SwapIdevDir,
                "/Route_DataArk.plist ",
                Route_ArkInt,
                " && mv -f ",
                ActivationLockNS.SwapIdevDir,
                "/Route_DataArk.plist ",
                Route_DatArk,
                " && chflags uchg ",
                Route_ArkInt,
                " && chflags uchg ",
                Route_DatArk
            }));
        }


        public bool ConvertPlist(string fileName, int method)
        {
            bool result;
            try
            {
                Process process = new Process();
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                processStartInfo.FileName = ActivationLockNS.ToolDir + "\\ref\\Apple\\Libimobiledevice\\win-plutil.exe";
                if (method == 1)
                {
                    string str = string.Format("\"{0}\"", fileName);
                    processStartInfo.Arguments = "-convert xml1 " + str;
                    process.StartInfo = processStartInfo;
                    process.Start();
                    process.WaitForExit();
                    result = true;
                }
                else
                {
                    string str2 = string.Format("\"{0}\"", fileName);
                    processStartInfo.Arguments = "-convert binary1 " + str2;
                    process.StartInfo = processStartInfo;
                    process.Start();
                    process.WaitForExit();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Convert Error: " + ex.Message);
            }
            return result;
        }


        public void SnappyRename()
        {
            string str = DeleteLines(SSH("snappy -f / -l").Result, 1).Replace("\n", "").Replace("\r", "");
            string command = "snappy -f / -r " + str + " --to orig-fs";
            SSH(command);
        }


        public void RestartAllDeamons()
        {
            SSH("launchctl unload -F /System/Library/LaunchDaemons/* && launchctl load -w -F /System/Library/LaunchDaemons/*");
        }


        public SshCommand SSH(string command)
        {
            if (!sshClient.IsConnected)
            {
                sshClient.ConnectionInfo.Timeout = TimeSpan.FromSeconds(15.0);
                sshClient.Connect();
            }
            SshCommand result;
            try
            {
                SshCommand commando = sshClient.CreateCommand(command);
                commando.CommandTimeout = TimeSpan.FromSeconds(30.0);
                commando.Execute();
                if (iOSDevice.debugMode)
                {
                    Console.WriteLine("=================");
                    Console.WriteLine("Command Name = {0} " + commando.CommandText);
                    Console.WriteLine("Return Value = {0}", commando.ExitStatus);
                    Console.WriteLine("Error = {0}", commando.Error);
                    Console.WriteLine("Result = {0}", commando.Result);
                }
                result = commando;
            }
            catch
            {
                if (!(command == "ls") && !(command == "uicache --all"))
                {
                    if (iOSDevice.debugMode)
                    {
                        Console.WriteLine("SSH Error caused by:" + command);
                    }
                    else
                    {
                        Thread.Sleep(2000);
                    }
                    StartIproxy();
                    SSH(command);
                }
                result = null;
            }
            return result;
        }


        public void ldrestart2()
        {
            //UploadResource(Resources.Carrier.ldrestart, "/usr/bin/ldrestart");
            p(82, "");
            if (!sshClient.IsConnected)
            {
                sshClient.ConnectionInfo.Timeout = TimeSpan.FromSeconds(15.0);
                sshClient.Connect();
            }
            try
            {
                sshClient.CreateCommand("chmod 755 /usr/bin/ldrestart && /usr/bin/ldrestart").Execute();
                sshClient.Disconnect();
            }
            catch (Exception)
            {
                if (File.Exists("%USERPROFILE%\\.ssh\\known_hosts"))
                {
                    File.Delete("%USERPROFILE%\\.ssh\\known_hosts");
                }
            }
            Thread.Sleep(11000);
            PairLoop();
            p(86, "");
            RunSSHServer();
            p(87, "");
            Thread.Sleep(1000);
            Mount();
            p(88, "");
        }


        public void UploadLocalFile(string localFile, string remoteFile)
        {
            Stream stream = File.Open(localFile, FileMode.Open);
            scpClient.Upload(stream, remoteFile);
            stream.Close();
        }


        public void Deactivate()
        {
            using (Process process = new Process())
            {
                process.StartInfo.FileName = ActivationLockNS.ToolDir + "\\ref\\Apple\\Libimobiledevice\\ideviceactivation.exe";
                process.StartInfo.Arguments = "deactivate";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                process.StandardOutput.ReadToEnd();
                process.WaitForExit();
            }
        }



        public void CreateActivationRecFile()
        {
            string serverResponse = GetActivationRecordFromServer();
            serverResponse = serverResponse.ToString().Replace("\n", "").Replace("\r", "").Replace("\t", "");
            File.WriteAllText(ActivationLockNS.SwapPCDir + "act_rec.plist.tmp", serverResponse);
            NSDictionary actRecDictionary = (NSDictionary)PropertyListParser.Parse(new FileInfo(ActivationLockNS.SwapPCDir + "act_rec.plist.tmp"));
            WildcardTicket = GetPlistProperty(actRecDictionary, "WildcardTicketToRemove", 4);
            actRecDictionary.Remove("WildcardTicketToRemove");
            PropertyListParser.SaveAsXml(actRecDictionary, new FileInfo(ActivationLockNS.SwapPCDir + "activation_record.plist"));
        }

        public string GetActivationRecordFromServer()
        {
            HttpWebRequest httpWebRequest = WebRequest.CreateHttp("http://unlockbd.com/UB-Tools_Files/PHP_UBTool/AIO_PHP/GSM-2/gsm_activation_record.php?serial=" + iOSDevice.Serial);
            httpWebRequest.AutomaticDecompression = (DecompressionMethods.GZip | DecompressionMethods.Deflate);
            httpWebRequest.Timeout = 12000;
            string responseBodyFromRemoteServer;
            using (HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    responseBodyFromRemoteServer = reader.ReadToEnd();
                }
            }
            return responseBodyFromRemoteServer;
        }

        public void CreateActivationRecFile2()
        {
            string serverResponse = GetActivationRecordFromServer2();
            serverResponse = serverResponse.ToString().Replace("\n", "").Replace("\r", "").Replace("\t", "");
            File.WriteAllText(ActivationLockNS.SwapPCDir + "act_rec.plist.tmp", serverResponse);
            NSDictionary actRecDictionary = (NSDictionary)PropertyListParser.Parse(new FileInfo(ActivationLockNS.SwapPCDir + "act_rec.plist.tmp"));
            WildcardTicket = GetPlistProperty(actRecDictionary, "WildcardTicketToRemove", 4);
            actRecDictionary.Remove("WildcardTicketToRemove");
            PropertyListParser.SaveAsXml(actRecDictionary, new FileInfo(ActivationLockNS.SwapPCDir + "activation_record.plist"));
        }

        public string GetActivationRecordFromServer2()
        {
            HttpWebRequest httpWebRequest = WebRequest.CreateHttp("http://ialdaz-activator.com/allinone/GSM/gsm_activation_record.php?serial=" + iOSDevice.Serial);
            httpWebRequest.AutomaticDecompression = (DecompressionMethods.GZip | DecompressionMethods.Deflate);
            httpWebRequest.Timeout = 12000;
            string responseBodyFromRemoteServer;
            using (HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    responseBodyFromRemoteServer = reader.ReadToEnd();
                }
            }
            return responseBodyFromRemoteServer;
        }


        public void Deactivate2()
        {
            using (Process process = new Process())
            {
                process.StartInfo.FileName = ToolDir + ".\\ref\\Apple\\files\\ideviceactivation.exe"; ;
                process.StartInfo.Arguments = "deactivate";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                process.StandardOutput.ReadToEnd();
                process.WaitForExit();
            }

            RestartLockdown();
        }
        public static string uikit = Application.StartupPath + ".\\ref\\Apple\\files\\utils\\uikit.tar";
        public static string bin = Application.StartupPath + ".\\ref\\Apple\\files\\utils\\91.bin";
        public static string BC = Application.StartupPath + ".\\ref\\Apple\\files\\utils\\BC";
        public void InstalarDepBasicas()
        {
            RunSSHServer();
            UploadResource2(uikit, "/uikit.tar");
            SSH("cd / && tar -xvf uikit.tar && chmod 755 /usr/bin/*");
            UploadResource2(bin, "/boot.tar.lzma");
            UploadResource2(BC, "/sbin/lzma");
            SSH("chmod 777 /sbin/lzma");
            SSH("lzma -d -v /boot.tar.lzma");
            SSH("tar -C / -xvf /boot.tar && rm -r /boot.tar");
            SSH("chmod +x /usr/libexec/substrate && /usr/libexec/substrate");
            SSH("chmod +x /usr/libexec/substrated && /usr/libexec/substrated");
            SSH("killall - 9 backboardd");
        }


        public void RestartAllDeamons2(bool reconnect = true)
        {
            SSH("launchctl unload -F /System/Library/LaunchDaemons/*");
            SSH("launchctl load -w -F /System/Library/LaunchDaemons/*");
            if (reconnect)
            {
                PairLoop();
            }
        }
        public bool UploadResource2(string fileToUpload, string remotePath)
        {
            bool result;
            try
            {
                bool flag = !scpClient.IsConnected;
                if (flag)
                {
                    scpClient.Connect();
                }
                scpClient.Upload(new FileInfo(fileToUpload), remotePath);
                result = true;
            }
            catch (Exception)
            {
                throw new ApplicationException("Error Upload Resource File");
            }
            return result;
        }
        public void ModifyCommcenterDsnFile()
        {
            NSDictionary ComCenterDictionary = (NSDictionary)PropertyListParser.Parse(new FileInfo(ActivationLockNS.SwapPCDir + "com.apple.commcenter.device_specific_nobackup.plist"));
            try
            {
                ComCenterDictionary.Remove("kPostponementTicket");
            }
            catch
            {
            }
            p(49, "");
            ComCenterDictionary.Add("kPostponementTicket", new NSDictionary
            {
                {
                    "ActivationState",
                    "Activated"
                },
                {
                    "ActivationTicket",
                    WildcardTicket
                },
                {
                    "ActivityURL",
                    "https://albert.apple.com/deviceservices/activity"
                },
                {
                    "PhoneNumberNotificationURL",
                    "https://albert.apple.com/deviceservices/phoneHome"
                }
            });
            PropertyListParser.SaveAsXml(ComCenterDictionary, new FileInfo(ActivationLockNS.SwapPCDir + "com.apple.commcenter.device_specific_nobackup.plist"));
        }

        public void ModifyDataArkFile()
        {
            NSDictionary DataArkDictionary = (NSDictionary)PropertyListParser.Parse(new FileInfo(ActivationLockNS.SwapPCDir + "data_ark.plist"));
            try
            {
                DataArkDictionary.Remove("-UCRTOOBForbidden");
                DataArkDictionary.Remove("ActivationState");
                DataArkDictionary.Remove("-ActivationState");
                DataArkDictionary.Add("ActivationState", "Activated");
            }
            catch (Exception)
            {
            }
            PropertyListParser.SaveAsXml(DataArkDictionary, new FileInfo(ActivationLockNS.SwapPCDir + "data_ark.plist"));
        }


        public string GetPlistProperty(NSDictionary plist, string NombreObjeto, int LineasArriba = 4)
        {
            NSObject nsobject;
            plist.TryGetValue(NombreObjeto, out nsobject);
            return DeleteLines(nsobject.ToXmlPropertyList().ToString(), LineasArriba).Replace("\n", "").Replace("\r", "").Replace("</data>", "").Replace("</plist>", "").Replace("</string>", "").Replace("<string>", "").Trim();
        }

        public string DeleteLines(string str, int linesToRemove)
        {
            return str.Split(Environment.NewLine.ToCharArray(), linesToRemove + 1).Skip(linesToRemove).FirstOrDefault<string>();
        }

        public void CleanSwapFolder()
        {
            try
            {
                if (Directory.Exists(ActivationLockNS.SwapPCDir))
                {
                    Directory.Delete(ActivationLockNS.SwapPCDir, true);
                }
                Thread.Sleep(1000);
                Directory.CreateDirectory(ActivationLockNS.SwapPCDir);
            }
            catch (Exception)
            {
            }
        }
        public void CleanSwapFolder2()
        {
            try
            {
                if (Directory.Exists(ActivationLockNS.SwapPCDir3))
                {
                    Directory.Delete(ActivationLockNS.SwapPCDir3, true);
                }
                Thread.Sleep(1000);
            }
            catch (Exception)
            {
            }
        }
        public void CleanSwapFolder3()
        {
            try
            {
                if (Directory.Exists(ActivationLockNS.SwapPCDir4))
                {
                    Directory.Delete(ActivationLockNS.SwapPCDir4, true);
                }
                Thread.Sleep(1000);
            }
            catch (Exception)
            {
            }
        }

        private void ReportErrorMessage(Exception e = null)
        {
            string errmsg = "Activation error!";

            if (e != null)
            {
                errmsg = e.Message + e.StackTrace;
            }

            string errorStr = "Error in Activation Process " +
                              "\n STEP " + step.ToString() +
                              "\n SERIAL " + iOSDevice.Serial +
                              "\n IOS: " + iOSDevice.IOSVersion + " MODEL: " + iOSDevice.ModelServer;
            try
            {
                //Report the error to the server if you want [Not necesary]
                //Network.Error(errorStr + "\n TRACE: " + errmsg);
            }
            catch
            {
            }

            MessageBox.Show(getMainForm(), e.Message + " " + errorStr, "Activation process failed, take a screenshot of this image and send", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public void ChangeSimStatus()
        {
            try
            {
                foreach (string item in SSH("ls /System/Library/PrivateFrameworks/SystemStatusServer.framework").Result.Split(new char[]
                {
                    '\n'
                }).ToList<string>())
                {
                    if (item.Contains("lproj"))
                    {
                        UploadResource(brokenbaseband.SystemStatusServer, "/System/Library/PrivateFrameworks/SystemStatusServer.framework/" + item + "/SystemStatusServer.strings");
                    }
                }
                SSH("chmod 755 /System/Library/PrivateFrameworks/SystemStatusServer.framework");
            }
            catch
            {
            }
        }

        private zeroknox getMainForm()
        {
            return (zeroknox)Application.OpenForms["Form1"];
        }


        private void SkipSetup()
        {
            UploadResource(brokenbaseband.purple, "/var/mobile/Library/Preferences/com.apple.purplebuddy.plist");
            SSH("chmod 600 /var/mobile/Library/Preferences/com.apple.purplebuddy.plist");
        }

        private SshClient sshClient;


        private ScpClient scpClient;


        public string host = "127.0.0.1";


        public string password = "alpine";


        public int port = 22;

        private string Folder_Activa = "";

        private string Folder_ArkInt = "";


        private string Folder_ActRec = "";


        private string Folder_LBPref = "/var/mobile/Library/Preferences/";


        private string Folder_WLPref = "/var/wireless/Library/Preferences/";


        private string Folder_LBLock = "/var/root/Library/Lockdown/";


        private string Route_ActRec = "///activation_record.plist";


        private string Route_CommCe = "///com.apple.commcenter.device_specific_nobackup.plist";


        private string Route_DatArk = "///data_ark.plist";


        private string Route_ArkInt = "///data_ark.plist";


        private string Route_Purple = "///com.apple.purplebuddy.plist";


        public static string ToolDir = Directory.GetCurrentDirectory();


        public static string SwapPCDir = ActivationLockNS.ToolDir + "\\files\\swp\\";
        public static string SwapPCDir3 = ActivationLockNS.ToolDir + "\\files\\utils\\";
        public static string SwapPCDir4 = ActivationLockNS.ToolDir + "\\files\\utils.zip";

        public static string SwapIdevDir = "/tmp/Backup";

        private string WildcardTicket;
        private int stage;
        private int step;
    }
}


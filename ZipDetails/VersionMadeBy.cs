using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZipDetails
{
    class VersionMadeBy
    {
        static string[] OSList = new string[] {
            "MS-DOS",
            "Amiga",
            "OpenVMS",
            "Unix",
            "VM/CMS",
            "Atari ST",
            "HPFS (OS/2, NT 3.x)",
            "Macintosh",
            "Z-System",
            "CP/M",
            "Windows NTFS or TOPS-20",
            "MVS or NTFS",
            "VSE or SMS/QDOS",
            "Acorn RISC OS",
            "VFAT",
            "alternate MVS",
            "BeOS",
            "Tandem",
            "OS/400",
            "OS/X (Darwin)",
            "AtheOS/Syllable"
        };

        public static string GetVersionInfo(byte[] versionData)
        {
            string versionText = "";
            if (versionData[1]<OSList.Length)
                versionText = OSList[versionData[1]];
            else
                 versionText= "Unknown OS (" + versionData[1] + ")";

            versionText += " Version: " + versionData[0] / 10 + "." + versionData[1] % 10;
            return versionText;
        }
    }
}

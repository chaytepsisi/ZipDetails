using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZipDetails
{
    class VersionData
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

        public string OS_Name { get; set; }
        public string Version { get; set; }

        public VersionData(byte[] versionData)
        {
            if (versionData[1] < OSList.Length)
                OS_Name = OSList[versionData[1]];
            else
                OS_Name = "Unknown OS (" + versionData[1] + ")";

            Version = versionData[0] / 10 + "." + versionData[1] % 10;
        }

        public override string ToString()
        {
            return OS_Name + " Version: " + Version + "";
        }
    }
}

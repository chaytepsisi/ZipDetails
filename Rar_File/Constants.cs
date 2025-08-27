using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Rar_File
{
    class Constants
    {
        public static int ARCHIVE_HEADER = 0x73;
        public static int FILE_HEADER = 0x74;
        public static int COMMENT_HEADER = 0x75; 
        public static int OLD_STYLE_AUHT_INFO=0x76;
        public static int OLD_STYLE_SUBBLOCK=0x77;
        public static int OLD_STYLE_RECOVERY_RECORD=0x78;
        public static int OLD_STYLE_AUHT_INFO2 = 0x79;
        public static int SUBBLOCK = 0x7a;

        public static int VERSION5_CRC_SIZE = 4;
    }
}

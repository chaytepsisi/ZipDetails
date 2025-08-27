using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rar_File
{
    class Commons
    {
        public static int GetIntFromVInt(List<byte> varInt)
        {
            int result = 0;
            varInt.Reverse();
            for (int i = 0; i < varInt.Count; i++)
            {
                result <<= 7;
                result ^= (varInt[i] & 0x7F);
            }
            return result;
        }
    }
}

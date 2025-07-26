using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZipDetails
{
    class Commons
    {
        public static string ByteToHexString(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                sb.Append(b.ToString("X2") + " ");
            }
            return sb.ToString().Trim();
        }
        public static int GetLength(byte[] lengthArray)
        {
            int length = 0;
            for (int i = lengthArray.Length - 1; i >= 0; i--)
            {
                length += lengthArray[i];
                if (i != 0)
                    length <<= 8;
            }
            return length;
        }
        public static string ParseTime(byte[] timeArray)
        {
            timeArray = new byte[2] { timeArray[1], timeArray[0] }; // Reverse the order of bytes for time parsing
            int hour = (timeArray[0] & 0xF8) >> 3; // Bits 3-7
            int minute = (timeArray[0] & 0x07) << 3 | (timeArray[1] & 0xE0) >> 5; // Bits 0-2 of first byte and bits 5-7 of second byte
            int second = (timeArray[1] & 0x1F) * 2; // Bits 0-4 of second byte, multiplied by 2 for seconds
            return $"{hour:D2}:{minute:D2}:{second:D2}";
        }
        public static string ParseDate(byte[] dateArray)
        {
            dateArray = new byte[2] { dateArray[1], dateArray[0] }; // Reverse the order of bytes for date parsing
            int year = dateArray[0] >> 1; // Bits 9-15
            int month = (dateArray[0] & 0x01) << 3 | (dateArray[1] & 0xE0) >> 5; // Bits 0 of first byte and bits 5-7 of second byte
            int day = dateArray[0] & 0x1F; // Bits 0-4 of second byte
            year += 1980; // ZIP files use a base year of 1980
            return $"{year:D4}-{month:D2}-{day:D2}"; // Format as YYYY-MM-DD
        }
    }
}

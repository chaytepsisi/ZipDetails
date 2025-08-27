using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rar_File
{
    class RarFile
    {/*
      *    Each block begins with the following fields:

        HEAD_CRC       2 bytes     CRC of total block or block part
        HEAD_TYPE      1 byte      Block type
        HEAD_FLAGS     2 bytes     Block flags
        HEAD_SIZE      2 bytes     Block size
        ADD_SIZE       4 bytes     Optional field - added block size
      * 
      * */
        List<Rar> RarList { get; set; }
        private byte[] Data { get; set; }

        public RarFile(byte[] fileContent)
        {
            Data = fileContent;
        }
        public void ParseFile()
        {
            RarList = new List<Rar>();
            int index=GetRarVersion(Data, out int version);
            if (version == -1)
            {
                RarList = new List<Rar>();
                throw new Exception("Dosyada geçerli bir RAR dosyası değil.");
            }
            Console.WriteLine("RAR dosyası bulundu. RAR sürümü: {0}", version);

            byte[] crc=Data.Skip(index).Take(Constants.VERSION5_CRC_SIZE).ToArray();
            index += Constants.VERSION5_CRC_SIZE - 1;
            List<byte> varInt = new List<byte>();
            do
            {
                index++;
                varInt.Add(Data[index]);
            } while ((Data[index] & 0x80) == 0x80);
            int headSize = Commons.GetIntFromVInt(varInt);
            Console.WriteLine("HeadSize: {0}", headSize);

            varInt = new List<byte>();
            do
            {
                index++;
                varInt.Add(Data[index]);
            } while ((Data[index] & 0x80) == 0x80);
            int blockType = Commons.GetIntFromVInt(varInt);
            Console.WriteLine("BlockType: {0}", blockType);



            //varInt = new List<byte>();
            //do
            //{
            //    index++;
            //    varInt.Add(Data[index]);
            //} while ((Data[index] & 0x80) == 0x80);
            //int blockType = Commons.GetIntFromVInt(varInt);
            //Console.WriteLine("BlockType: {0}", blockType);



        }
        public string GetInfo()
        {
            StringBuilder info = new StringBuilder();
            foreach (var rar in RarList)
            {
                info.AppendLine(rar.ToString());
            }
            return info.ToString();
        }

        int GetRarVersion(byte[] data, out int version)
        {
            if (data[0] == 0x52 && data[1] == 0x61 && data[2] == 0x72 && data[3] == 0x21 && data[4] == 0x1A && data[5] == 0x07 && data[6] == 0x00)
            {
                version = 4;
                return 7;
            }
            else if (data[0] == 0x52 && data[1] == 0x61 && data[2] == 0x72 && data[3] == 0x21 && data[4] == 0x1A && data[5] == 0x07 && data[6] == 0x01 && data[7] == 0x00)
            {
                version = 5;
                return 8;
            }
            else
            {
                version = -1;
                return -1; // Not a valid RAR file
            }
        }
    }
}
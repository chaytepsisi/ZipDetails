using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZipDetails
{
    class ZipFile
    {
        public List<Zip> ZipList { get; set; }
        public EndOfCentralDirectoryHeader EndOfCentralDirectory { get; set; }
        private byte[] Data { get; set; }

        public ZipFile(byte[] fileContent)
        {
            Data = fileContent;
        }
        public void ParseFile()
        {
            List<int> localHeaderOffsets = new List<int>();
            List<int> centralDirectoryHeaderOffsets = new List<int>();
            ZipList = new List<Zip>();
            int centralDirecotryEndOffset = 0;

            for (int i = 0; i < Data.Length - 4; i++)
            {
                if (Data[i] == 0x50 && Data[i + 1] == 0x4b && Data[i + 2] == 0x03 && Data[i + 3] == 0x04)
                    localHeaderOffsets.Add(i);

                if (Data[i] == 0x50 && Data[i + 1] == 0x4b && Data[i + 2] == 0x01 && Data[i + 3] == 0x02)
                    centralDirectoryHeaderOffsets.Add(i);

                if (Data[i] == 0x50 && Data[i + 1] == 0x4b && Data[i + 2] == 0x05 && Data[i + 3] == 0x06)
                    centralDirecotryEndOffset = i;
            }

            for (int i = 0; i < localHeaderOffsets.Count; i++)
            {
                Zip zip = new Zip();
                zip.ZipLocalHeader = new LocalHeader(Data.Skip(localHeaderOffsets[i]).ToArray());
                zip.ZipCentralDirectoryHeader = new CentralDirectoryHeader(Data.Skip(centralDirectoryHeaderOffsets[i]).ToArray());
                ZipList.Add(zip);
                //MessageBox.Show(zip.ToString());
            }

            EndOfCentralDirectory = new EndOfCentralDirectoryHeader(Data.Skip(centralDirecotryEndOffset).ToArray());
        }
        public override string ToString()
        {
            string Message = String.Empty;
            for (int i = 0; i < ZipList.Count; i++)
                Message += ZipList[i].ToString() + "\n---------------\n";

            Message += EndOfCentralDirectory.ToString();
            return Message;
        }

    }
}

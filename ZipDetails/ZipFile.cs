using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
            int centralDirectoryEndOffset = 0;

            for (int i = 0; i < Data.Length - 4; i++)
            {
                if (Data[i] == 0x50 && Data[i + 1] == 0x4b && Data[i + 2] == 0x03 && Data[i + 3] == 0x04)
                    localHeaderOffsets.Add(i);

                if (Data[i] == 0x50 && Data[i + 1] == 0x4b && Data[i + 2] == 0x01 && Data[i + 3] == 0x02)
                    centralDirectoryHeaderOffsets.Add(i);

                if (Data[i] == 0x50 && Data[i + 1] == 0x4b && Data[i + 2] == 0x05 && Data[i + 3] == 0x06)
                    centralDirectoryEndOffset = i;
            }

            for (int i = 0; i < localHeaderOffsets.Count; i++)
            {
                Zip zip = new Zip
                {
                    ZipLocalHeader = new LocalHeader(Data.Skip(localHeaderOffsets[i]).ToArray()),
                    ZipCentralDirectoryHeader = new CentralDirectoryHeader(Data.Skip(centralDirectoryHeaderOffsets[i]).ToArray())
                };
                int limit;
                if (i != localHeaderOffsets.Count - 1)
                    limit = localHeaderOffsets[i + 1];
                else limit = centralDirectoryHeaderOffsets[0];

                int overHead = Constants.LOCAL_FILE_HEADER_SIZE +  zip.ZipLocalHeader.FileName.Length + zip.ZipLocalHeader.ExtraFieldArray.Length;
                if (overHead+zip.ZipLocalHeader.CompressedSize > limit- localHeaderOffsets[i])
                {
                    zip.ZipLocalHeader.IsCorrupted = true;
                    zip.ZipLocalHeader.Info = "~~~~ Eksik Bölüm: " + zip.ZipLocalHeader.CompressedSize + " / " + (limit - localHeaderOffsets[i] - overHead) + " ~~~~\n";
                    zip.ZipLocalHeader.CompressedData = zip.ZipLocalHeader.CompressedData.Take(limit - localHeaderOffsets[i] - overHead).ToArray();
                }

                ZipList.Add(zip);
                //MessageBox.Show(zip.ToString());
            }

            EndOfCentralDirectory = new EndOfCentralDirectoryHeader(Data.Skip(centralDirectoryEndOffset).ToArray());
        }

        public void Unzip()
        {
            string parentDir = Path.Combine(Environment.CurrentDirectory, "Unzipped");
            if (!Directory.Exists(parentDir))
                Directory.CreateDirectory(parentDir);
            string fileList = "";
            for (int i = 0; i < ZipList.Count; i++)
            {
                string success = "+";
                byte[] uncompressedData = null;
                if (ZipList[i].IsDirectory())
                {
                    Directory.CreateDirectory(Path.Combine(parentDir, ZipList[i].ZipLocalHeader.FileName));
                }
                else
                {
                    try
                    {
                        if (ZipList[i].ZipLocalHeader.CompressionMethod == 0x08)
                            uncompressedData = Deflate.Decompress(ZipList[i].ZipLocalHeader.CompressedData);
                        else if (ZipList[i].ZipLocalHeader.CompressionMethod == 0x00)
                            uncompressedData = ZipList[i].ZipLocalHeader.CompressedData;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error Deflate @LocalHeader_" + i + " " + ZipList[i].ZipLocalHeader.FileName + "\n" + ex.Message);
                        success = "-";
                    }
                    try
                    {
                        if (uncompressedData != null && uncompressedData.Length > 0)
                        {
                            string localFileName = Path.Combine(parentDir, ZipList[i].ZipLocalHeader.FileName);
                            FileInfo fileInfo = new FileInfo(localFileName);
                            if (!Directory.Exists(fileInfo.DirectoryName))
                                Directory.CreateDirectory(fileInfo.DirectoryName);
                            File.WriteAllBytes(localFileName, uncompressedData);
                        }
                        else
                        {
                            success = "-";
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error @FileWrite " + ex.Message);
                        success= "-";   
                    }
                }

                fileList += success+ZipList[i].ZipLocalHeader.FileName + "\n";
            }
            File.WriteAllText(Path.Combine(parentDir, "FileList.txt"), fileList);
            Process.Start(parentDir);
        }
        public string ToString(bool flag=false)
        {
            string Message = String.Empty;
            for (int i = 0; i < ZipList.Count; i++)
                Message += ZipList[i].ToString(flag) + "\n---------------\n";

            Message += EndOfCentralDirectory.ToString();
            return Message;
        }
        //public string ToString(bool verbose)
        //{
        //    string Message = String.Empty;
        //    for (int i = 0; i < ZipList.Count; i++)
        //        Message += ZipList[i].ToString() + "\n---------------\n";

        //    Message += EndOfCentralDirectory.ToString();

        //    if (verbose)
        //    {
        //        Message += "\n---------------\n";
        //        for (int i = 0; i < ZipList.Count; i++) {
        //            Message += Encoding.UTF8.GetString(ZipList[i].ZipLocalHeader.CompressedData) + "\n-------\n";
        //            Message += Commons.ByteToHexString(ZipList[i].ZipLocalHeader.CompressedData) + "\n- - - - - - - - - - - -\n";
        //        }
        //    }
        //    return Message;
        //}
    }
}

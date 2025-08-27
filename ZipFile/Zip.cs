using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zip_File
{
    public class Zip
    {
        public LocalHeader ZipLocalHeader { get; set; }
        public CentralDirectoryHeader ZipCentralDirectoryHeader { get; set; }
        public bool IsDirectory() { return ZipCentralDirectoryHeader.IsDirectory; }
        public int EncodingValue(){ return ZipCentralDirectoryHeader.EncodingValue; }
        public bool IsCorrupted() { return ZipLocalHeader.IsCorrupted; }

        //public EndOfCentralDirectoryHeader ZipEndOfCentralDirectoryHeader { get; set; }
        public string ToString(bool flag=false)
        {
            string message = "";
            if(IsCorrupted())
                message = "-- BOZUK BÖLÜM -- " + Environment.NewLine;
            if (IsDirectory())
                message = "-- Directory -- " + Environment.NewLine;
            message+= "Zip Details:" + Environment.NewLine +
                   "Local Header: " + Environment.NewLine + ZipLocalHeader.ToString(flag) + Environment.NewLine + "---------------" + Environment.NewLine +
                   "Central Directory Header: " + Environment.NewLine + ZipCentralDirectoryHeader.ToString(flag);
            return message;
            //+ Environment.NewLine + "End of Central Directory Header: " + Environment.NewLine + ZipEndOfCentralDirectoryHeader.ToString();
        }   

        public string GetInfo()
        {
            string infoMessage = "";
            if (ZipLocalHeader != null)
            {
                if (!IsDirectory())
                {
                    infoMessage += ZipLocalHeader.FileName + "\n\t";
                    if (ZipLocalHeader.UnCompressedSize != 0)
                    {
                        infoMessage += "Available Data: " + ZipLocalHeader.CompressedSize + " / " + ZipLocalHeader.CompressedData.Length;
                        if (ZipLocalHeader.CompressedSize != ZipLocalHeader.CompressedData.Length)
                            infoMessage += " -- MISSING DATA: "+ (ZipLocalHeader.CompressedSize - ZipLocalHeader.CompressedData.Length)+" bytes";
                        infoMessage += "\n\tCompression Rate: %" + (100 - ZipLocalHeader.CompressedSize * 100.0 / ZipLocalHeader.UnCompressedSize);
                    }
                    else infoMessage += " - - - DATA NOT AVAILABLE - - -";
                    infoMessage += "\n";
                }
                else
                {
                    infoMessage += "Directory: " + ZipLocalHeader.FileName + "\n";
                }
            }
            else
            {
                if (ZipCentralDirectoryHeader != null)
                {
                    infoMessage += ZipCentralDirectoryHeader.FileName + "\n\t - - - DATA NOT AVAILABLE - - -";
                }
            }
            return infoMessage;
        }
    }
}

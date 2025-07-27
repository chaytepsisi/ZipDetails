using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZipDetails
{
    class EndOfCentralDirectoryHeader
    {/* var diskNumber = Data.Skip(Constants.END_OF_CENTRAL_DIR_DISK_NUMBER_OFFSET).Take(Constants.END_OF_CENTRAL_DIR_DISK_NUMBER_LENGTH).ToArray();
            var diskWithCentralDirStart = Data.Skip(Constants.END_OF_CENTRAL_DIR_DISK_WITH_CENTRAL_DIR_START_OFFSET).Take(Constants.END_OF_CENTRAL_DIR_DISK_WITH_CENTRAL_DIR_START_LENGTH).ToArray();
            var totalEntriesOnDisk = Data.Skip(Constants.END_OF_CENTRAL_DIR_TOTAL_ENTRIES_OFFSET).Take(Constants.END_OF_CENTRAL_DIR_TOTAL_ENTRIES_LENGTH).ToArray();
            var totalEntries = Data.Skip(Constants.END_OF_CENTRAL_DIR_TOTAL_ENTRIES_ON_CENTRAL_DIR_LENGTH).Take(Constants.END_OF_CENTRAL_DIR_TOTAL_ENTRIES_ON_CENTRAL_DIR_LENGTH).ToArray();
            var centralDirectorySize = Data.Skip(Constants.END_OF_CENTRAL_DIR_SIZE_OF_CENTRAL_DIR_OFFSET).Take(Constants.END_OF_CENTRAL_DIR_SIZE_OF_CENTRAL_DIR_LENGTH).ToArray();
            var centralDirectoryOffset = Data.Skip(Constants.END_OF_CENTRAL_DIR_OFFSET_OFFSET).Take(Constants.END_OF_CENTRAL_DIR_OFFSET_LENGTH).ToArray();
            var zipFileCommentLength = Data.Skip(Constants.END_OF_CENTRAL_DIR_COMMENT_LENGTH_OFFSET).Take(Constants.END_OF_CENTRAL_DIR_COMMENT_LENGTH_LENGTH).ToArray();
            var zipFileComment = Data.Skip(Constants.END_OF_CENTRAL_DIR_COMMENT_OFFSET).Take(Commons.GetLength(zipFileCommentLength)).ToArray();*/

        public byte[] Signature { get; set; }
        public byte[] DiskNumber { get; set; }
        public byte[] DiskWithCentralDirStart { get; set; }
        public byte[] TotalEntriesOnDisk { get; set; }
        public byte[] TotalEntries { get; set; }
        public byte[] CentralDirectorySize { get; set; }
        public byte[] CentralDirectoryOffset { get; set; }
        public byte[] ZipFileCommentLength { get; set; }
        public byte[] ZipFileCommentArray { get; set; }
        public int NextHeaderOffset { get; set; }
        public byte[] Data { get; set; }

        public string ZipComment { get; set; }
       
        public EndOfCentralDirectoryHeader(byte[] data)
        {
            Data = data;
            Parse();
        }
        void Parse()
        {
            Signature = Data.Skip(Constants.END_OF_CENTRAL_DIR_SIGNATURE_OFFSET).Take(Constants.END_OF_CENTRAL_DIR_SIGNATURE_LENGTH).ToArray();
            DiskNumber = Data.Skip(Constants.END_OF_CENTRAL_DIR_DISK_NUMBER_OFFSET).Take(Constants.END_OF_CENTRAL_DIR_DISK_NUMBER_LENGTH).ToArray();
            DiskWithCentralDirStart = Data.Skip(Constants.END_OF_CENTRAL_DIR_DISK_WITH_CENTRAL_DIR_START_OFFSET).Take(Constants.END_OF_CENTRAL_DIR_DISK_WITH_CENTRAL_DIR_START_LENGTH).ToArray();
            TotalEntriesOnDisk = Data.Skip(Constants.END_OF_CENTRAL_DIR_TOTAL_ENTRIES_OFFSET).Take(Constants.END_OF_CENTRAL_DIR_TOTAL_ENTRIES_LENGTH).ToArray();
            TotalEntries = Data.Skip(Constants.END_OF_CENTRAL_DIR_TOTAL_ENTRIES_ON_CENTRAL_DIR_OFFSET).Take(Constants.END_OF_CENTRAL_DIR_TOTAL_ENTRIES_ON_CENTRAL_DIR_LENGTH).ToArray();
            CentralDirectorySize = Data.Skip(Constants.END_OF_CENTRAL_DIR_SIZE_OF_CENTRAL_DIR_OFFSET).Take(Constants.END_OF_CENTRAL_DIR_SIZE_OF_CENTRAL_DIR_LENGTH).ToArray();
            CentralDirectoryOffset = Data.Skip(Constants.END_OF_CENTRAL_DIR_OFFSET_OFFSET).Take(Constants.END_OF_CENTRAL_DIR_OFFSET_LENGTH).ToArray();
            ZipFileCommentLength = Data.Skip(Constants.END_OF_CENTRAL_DIR_COMMENT_LENGTH_OFFSET).Take(Constants.END_OF_CENTRAL_DIR_COMMENT_LENGTH_LENGTH).ToArray();
            ZipFileCommentArray = Data.Skip(Constants.END_OF_CENTRAL_DIR_COMMENT_OFFSET).Take(Commons.GetLength(ZipFileCommentLength)).ToArray();
            if(ZipFileCommentArray.Length > 0)
            {
                ZipComment = Encoding.Default.GetString(ZipFileCommentArray);
            }
            else
            {
                ZipComment = string.Empty;
            }
            NextHeaderOffset = Constants.END_OF_CENTRAL_DIR_COMMENT_OFFSET + Commons.GetLength(ZipFileCommentLength);
            if (Signature[0] != 0x50 || Signature[1] != 0x4b || Signature[2] != 0x05 || Signature[3] != 0x06)
            {
                throw new Exception("Invalid End of Central Directory Signature");
            }
        }

        public override string ToString()
        {
            return "End of Central Directory Header:" + Constants.NEWLINE +
                   "Signature: " + Commons.ByteToHexString(Signature) + Constants.NEWLINE +
                   "Disk Number: " + Commons.ByteToHexString(DiskNumber) + Constants.NEWLINE +
                   "Disk with Central Directory Start: " + Commons.ByteToHexString(DiskWithCentralDirStart) + Constants.NEWLINE +
                   "Total Entries on Disk: " + Commons.ByteToHexString(TotalEntriesOnDisk) + Constants.NEWLINE +
                   "Total Entries in Central Directory: " + Commons.ByteToHexString(TotalEntries) + Constants.NEWLINE +
                   "Central Directory Size: " + Commons.ByteToHexString(CentralDirectorySize) + Constants.NEWLINE +
                   "Central Directory Offset: " + Commons.ByteToHexString(CentralDirectoryOffset) + Constants.NEWLINE +
                   "Zip File Comment Length: " + Commons.ByteToHexString(ZipFileCommentLength) + Constants.NEWLINE +
                   "Zip File Comment: " + ZipComment;
        }
    }
}

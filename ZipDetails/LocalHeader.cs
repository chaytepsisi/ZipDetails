using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ZipDetails
{
    class LocalHeader
    {
        public byte[] VersionNeeded { get; set; }
        public byte[] GeneralPurposeFlag { get; set; }
        public byte[] CompressionMethodBytes { get; set; }
        public byte[] LastModFileTime { get; set; }
        public byte[] LastModFileDate { get; set; }
        public byte[] Crc32 { get; set; }
        public byte[] CompressedSizeArray { get; set; }
        public byte[] UncompressedSize { get; set; }
        public byte[] FileNameLength { get; set; }
        public byte[] ExtraFieldLength { get; set; }
        public byte[] FileNameArray { get; set; }
        public byte[] CompressedData { get; set; }
        public int NextHeaderOffset { get; set; }
        public byte[] Data { get; set; }
        public byte[] ExtraFieldArray { get; set; }
        public string FileName { get; set; }
        public int CompressionMethod { get; set; }
        public bool IsCorrupted{ get; set; }
        public int CompressedSize { get; set; }

        public string Info { get; set; }
        public LocalHeader(byte[] data)
        {
            Data = data;
            IsCorrupted = false;
            Parse();
        }
        void Parse()
        {
            VersionNeeded = Data.Skip(Constants.VERSION_NEEDED_OFFSET).Take(Constants.VERSION_NEEDED_LENGTH).ToArray();
            GeneralPurposeFlag = Data.Skip(Constants.GENERAL_PURPOSE_FLAG_OFFSET).Take(Constants.GENERAL_PURPOSE_FLAG_LENGTH).ToArray();
            CompressionMethodBytes = Data.Skip(Constants.COMPRESSION_METHOD_OFFSET).Take(Constants.COMPRESSION_METHOD_LENGTH).ToArray();
            LastModFileTime = Data.Skip(Constants.LAST_MOD_FILE_TIME_OFFSET).Take(Constants.LAST_MOD_FILE_TIME_LENGTH).ToArray();
            LastModFileDate = Data.Skip(Constants.LAST_MOD_FILE_DATE_OFFSET).Take(Constants.LAST_MOD_FILE_DATE_LENGTH).ToArray();
            Crc32 = Data.Skip(Constants.CRC32_OFFSET).Take(Constants.CRC32_LENGTH).ToArray();
            CompressedSizeArray = Data.Skip(Constants.COMPRESSED_SIZE_OFFSET).Take(Constants.COMPRESSED_SIZE_LENGTH).ToArray();
            UncompressedSize = Data.Skip(Constants.UNCOMPRESSED_SIZE_OFFSET).Take(Constants.UNCOMPRESSED_SIZE_LENGTH).ToArray();
            FileNameLength = Data.Skip(Constants.FILENAME_LENGTH_OFFSET).Take(Constants.FILENAME_LENGTH_LENGTH).ToArray();
            ExtraFieldLength = Data.Skip(Constants.EXTRA_FIELD_LENGTH_OFFSET).Take(Constants.EXTRA_FIELD_LENGTH_LENGTH).ToArray();
            FileNameArray = Data.Skip(Constants.FILENAME_LENGTH_LENGTH + Constants.EXTRA_FIELD_LENGTH_OFFSET)
                .Take(Commons.GetValue(FileNameLength)).ToArray();
            ExtraFieldArray = Data.Skip(Constants.FILENAME_LENGTH_LENGTH + Constants.EXTRA_FIELD_LENGTH_OFFSET + Commons.GetValue(FileNameLength)).Take(Commons.GetValue(ExtraFieldLength)).ToArray();
            var dataStartOffset = Constants.FILENAME_LENGTH_LENGTH + Constants.EXTRA_FIELD_LENGTH_OFFSET + Commons.GetValue(FileNameLength) + Commons.GetValue(ExtraFieldLength);
            CompressedSize = Commons.GetValue(CompressedSizeArray);
            CompressedData = Data.Skip(dataStartOffset).Take(CompressedSize).ToArray(); // Extract the compressed data

            NextHeaderOffset = dataStartOffset + CompressedSize; // Calculate the size of the local header
           FileName= Encoding.Default.GetString(FileNameArray);
            CompressionMethod = Commons.GetValue(CompressionMethodBytes);

            if (BitConverter.ToInt16(GeneralPurposeFlag, 0) == 0 && Commons.GetValue(CompressedSizeArray) == 0)
                IsCorrupted = true;
        }

        public string ToString(bool flag = false)
        {
            string message= "Version: " + Commons.ByteToHexString(VersionNeeded) + Constants.NEWLINE +
                    "Flag: " + Commons.ByteToHexString(GeneralPurposeFlag) + Constants.NEWLINE +
                    "Comp. Method: " + Commons.ByteToHexString(CompressionMethodBytes) + Constants.NEWLINE +
                    "Last Modified Time: " + Commons.ByteToHexString(LastModFileTime) + " --> " + Commons.ParseTime(LastModFileTime) + Constants.NEWLINE +
                    Convert.ToString(BitConverter.ToInt16(LastModFileTime, 0), 2) + Constants.NEWLINE +
                    "Last Modified Date: " + Commons.ByteToHexString(LastModFileDate) + " --> " + Commons.ParseDate(LastModFileDate) + Constants.NEWLINE +
                    Convert.ToString(BitConverter.ToInt16(LastModFileDate, 0), 2) + Constants.NEWLINE +
                    "CRC32 : " + Commons.ByteToHexString(Crc32) + Constants.NEWLINE +
                    "Compressed Size: " + Commons.ByteToHexString(CompressedSizeArray) + " --> " + Commons.GetValue(CompressedSizeArray) + Constants.NEWLINE +
                    "Uncompressed Size: " + Commons.ByteToHexString(UncompressedSize) + " --> " + Commons.GetValue(UncompressedSize) + Constants.NEWLINE +
                    "File Name Length: " + Commons.ByteToHexString(FileNameLength) + " --> " + Commons.GetValue(FileNameLength) + Constants.NEWLINE +
                    "Extra Field Length: " + Commons.ByteToHexString(ExtraFieldLength) + Constants.NEWLINE +
                    "File Name: "+Encoding.Default.GetString(FileNameArray) + Constants.NEWLINE;
            if (!flag)
                return Info + message;
            else
            {
                if (ExtraFieldArray.Length > 0)
                    message += Commons.ByteToHexString(ExtraFieldArray) + Constants.NEWLINE;
                if (CompressedData.Length > 0)
                    message += "CompressedData: \n" + Commons.ByteToHexString(CompressedData) + Constants.NEWLINE;
                return Info + Constants.NEWLINE + message;
            }
        }
    }
}

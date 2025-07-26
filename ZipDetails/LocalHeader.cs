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
        public byte[] CompressionMethod { get; set; }
        public byte[] LastModFileTime { get; set; }
        public byte[] LastModFileDate { get; set; }
        public byte[] Crc32 { get; set; }
        public byte[] CompressedSize { get; set; }
        public byte[] UncompressedSize { get; set; }
        public byte[] FileNameLength { get; set; }
        public byte[] ExtraFieldLength { get; set; }
        public byte[] FileNameArray { get; set; }
        public byte[] CompressedData { get; set; }
        public int NextHeaderOffset { get; set; }
        public byte[] Data { get; set; }

        public string FileName { get; set; }
        public LocalHeader(byte[] data)
        {
            Data = data;
            Parse();
        }
        void Parse()
        {
            VersionNeeded = Data.Skip(Constants.VERSION_NEEDED_OFFSET).Take(Constants.VERSION_NEEDED_LENGTH).ToArray();
            GeneralPurposeFlag = Data.Skip(Constants.GENERAL_PURPOSE_FLAG_OFFSET).Take(Constants.GENERAL_PURPOSE_FLAG_LENGTH).ToArray();
            CompressionMethod = Data.Skip(Constants.COMPRESSION_METHOD_OFFSET).Take(Constants.COMPRESSION_METHOD_LENGTH).ToArray();
            LastModFileTime = Data.Skip(Constants.LAST_MOD_FILE_TIME_OFFSET).Take(Constants.LAST_MOD_FILE_TIME_LENGTH).ToArray();
            LastModFileDate = Data.Skip(Constants.LAST_MOD_FILE_DATE_OFFSET).Take(Constants.LAST_MOD_FILE_DATE_LENGTH).ToArray();
            Crc32 = Data.Skip(Constants.CRC32_OFFSET).Take(Constants.CRC32_LENGTH).ToArray();
            CompressedSize = Data.Skip(Constants.COMPRESSED_SIZE_OFFSET).Take(Constants.COMPRESSED_SIZE_LENGTH).ToArray();
            UncompressedSize = Data.Skip(Constants.UNCOMPRESSED_SIZE_OFFSET).Take(Constants.UNCOMPRESSED_SIZE_LENGTH).ToArray();
            FileNameLength = Data.Skip(Constants.FILENAME_LENGTH_OFFSET).Take(Constants.FILENAME_LENGTH_LENGTH).ToArray();
            ExtraFieldLength = Data.Skip(Constants.EXTRA_FIELD_LENGTH_OFFSET).Take(Constants.EXTRA_FIELD_LENGTH_LENGTH).ToArray();
            FileNameArray = Data.Skip(Constants.FILENAME_LENGTH_LENGTH + Constants.EXTRA_FIELD_LENGTH_OFFSET)
                .Take(Commons.GetLength(FileNameLength)).ToArray();
            var dataStartOffset = Constants.FILENAME_LENGTH_LENGTH + Constants.EXTRA_FIELD_LENGTH_OFFSET + Commons.GetLength(FileNameLength) + Commons.GetLength(ExtraFieldLength);
            CompressedData = Data.Skip(dataStartOffset).Take(Commons.GetLength(CompressedSize)).ToArray(); // Extract the compressed data
            
            NextHeaderOffset = dataStartOffset + Commons.GetLength(CompressedSize); // Calculate the size of the local header

           FileName= Encoding.Default.GetString(FileNameArray);
        }

        public override string ToString()
        {
            return "Version: " + Commons.ByteToHexString(VersionNeeded) + Constants.NEWLINE +
                    "Flag: " + Commons.ByteToHexString(GeneralPurposeFlag) + Constants.NEWLINE +
                    "Comp. Method: " + Commons.ByteToHexString(CompressionMethod) + Constants.NEWLINE +
                    "Last Modified Time: " + Commons.ByteToHexString(LastModFileTime) + "-->" + Commons.ParseTime(LastModFileTime) + Constants.NEWLINE +
                    Convert.ToString(BitConverter.ToInt16(LastModFileTime, 0), 2) + Constants.NEWLINE +
                    "Last Modified Date: " + Commons.ByteToHexString(LastModFileDate) + "-->" + Commons.ParseDate(LastModFileDate) + Constants.NEWLINE +
                    Convert.ToString(BitConverter.ToInt16(LastModFileDate, 0), 2) + Constants.NEWLINE +
                    "CRC32 : " + Commons.ByteToHexString(Crc32) + Constants.NEWLINE +
                    "Compressed Size: " + Commons.ByteToHexString(CompressedSize) + Constants.NEWLINE + "-->" + Commons.GetLength(CompressedSize) + Constants.NEWLINE +
                    "Uncompressed Size: " + Commons.ByteToHexString(UncompressedSize) + Constants.NEWLINE + "-->" + Commons.GetLength(UncompressedSize) + Constants.NEWLINE +
                    "File Name Length: " + Commons.ByteToHexString(FileNameLength) + Constants.NEWLINE + "-->" + Commons.GetLength(FileNameLength) + Constants.NEWLINE +
                    "Extra Field Length: " + Commons.ByteToHexString(ExtraFieldLength) + Constants.NEWLINE +
                    Encoding.Default.GetString(FileNameArray);
        }
    }
}

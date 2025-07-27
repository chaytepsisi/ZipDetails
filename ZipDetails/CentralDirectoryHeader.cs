using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZipDetails
{
    class CentralDirectoryHeader
    {
        /*  public static int CENTRAL_DIR_SIGNATURE = 0x504b0102;
        public static int LAST_MOD_FILE_TIME_OFFSET_CENTRAL = 12;
        public static int LAST_MOD_FILE_TIME_LENGTH_CENTRAL = 2;
        public static int LAST_MOD_FILE_DATE_OFFSET_CENTRAL = 14;
        public static int LAST_MOD_FILE_DATE_LENGTH_CENTRAL = 2;
        public static int CRC32_OFFSET_CENTRAL = 16;
        public static int CRC32_LENGTH_CENTRAL = 4;
        public static int COMPRESSED_SIZE_OFFSET_CENTRAL = 20;
        public static int COMPRESSED_SIZE_LENGTH_CENTRAL = 4;
        public static int UNCOMPRESSED_SIZE_OFFSET_CENTRAL = 24;
        public static int UNCOMPRESSED_SIZE_LENGTH_CENTRAL = 4;
        public static int FILENAME_LENGTH_OFFSET_CENTRAL = 28;
        public static int FILENAME_LENGTH_LENGTH_CENTRAL = 2;
        public static int EXTRA_FIELD_LENGTH_OFFSET_CENTRAL = 30;
        public static int EXTRA_FIELD_LENGTH_LENGTH_CENTRAL = 2;
        public static int FILE_COMMENT_LENGTH_OFFSET_CENTRAL = 32;
        public static int FILE_COMMENT_LENGTH_LENGTH_CENTRAL = 2;
        public static int DISK_NUMBER_START_OFFSET_CENTRAL = 34;
        public static int DISK_NUMBER_START_LENGTH_CENTRAL = 2;
        public static int INTERNAL_FILE_ATTRIBUTES_OFFSET_CENTRAL = 36;
        public static int INTERNAL_FILE_ATTRIBUTES_LENGTH_CENTRAL = 2;
        public static int EXTERNAL_FILE_ATTRIBUTES_OFFSET_CENTRAL = 38;
        public static int EXTERNAL_FILE_ATTRIBUTES_LENGTH_CENTRAL = 4;
        public static int RELATIVE_OFFSET_OF_LOCAL_HEADER_OFFSET_CENTRAL = 42;
        public static int RELATIVE_OFFSET_OF_LOCAL_HEADER_LENGTH_CENTRAL = 4;*/
        public byte[] VersionMadeBy { get; set; }
        public byte[] VersionNeededToExtract { get; set; }
        public byte[] GeneralPurposeBitFlag { get; set; }
        public byte[] CompressionMethod { get; set; }
        public byte[] LastModFileTime { get; set; }
        public byte[] LastModFileDate { get; set; }
        public byte[] Crc32 { get; set; }
        public byte[] CompressedSize { get; set; }
        public byte[] UncompressedSize { get; set; }
        public byte[] FileNameLength { get; set; }
        public byte[] ExtraFieldLength { get; set; }
        public byte[] FileCommentLength { get; set; }
        public byte[] DiskNumberStart { get; set; }
        public byte[] InternalFileAttributes { get; set; }
        public byte[] ExternalFileAttributes { get; set; }
        public byte[] RelativeOffsetOfLocalHeader { get; set; }
        public byte[] FileNameArray { get; set; }
        public byte[] ExtraFieldArray { get; set; }
        public byte[] FileCommentArray { get; set; }
        public int NextHeaderOffset { get; set; }
        public byte[] Data { get; set; }
        public string FileName { get; set; }
        public CentralDirectoryHeader(byte[] data)
        {
            Data = data;
            Parse();
        }

        void Parse()
        {
            VersionMadeBy = Data.Skip(Constants.VERSION_MADE_BY_OFFSET).Take(Constants.VERSION_MADE_BY_LENGTH).ToArray();
            VersionNeededToExtract = Data.Skip(Constants.VERSION_NEEDED_TO_EXTRACT_OFFSET).Take(Constants.VERSION_NEEDED_TO_EXTRACT_LENGTH).ToArray();
            GeneralPurposeBitFlag = Data.Skip(Constants.GENERAL_PURPOSE_BIT_FLAG_OFFSET).Take(Constants.GENERAL_PURPOSE_BIT_FLAG_LENGTH).ToArray();
            CompressionMethod = Data.Skip(Constants.COMPRESSION_METHOD_OFFSET_CENTRAL).Take(Constants.COMPRESSION_METHOD_LENGTH_CENTRAL).ToArray();
            LastModFileTime = Data.Skip(Constants.LAST_MOD_FILE_TIME_OFFSET_CENTRAL).Take(Constants.LAST_MOD_FILE_TIME_LENGTH_CENTRAL).ToArray();
            LastModFileDate = Data.Skip(Constants.LAST_MOD_FILE_DATE_OFFSET_CENTRAL).Take(Constants.LAST_MOD_FILE_DATE_LENGTH_CENTRAL).ToArray();
            Crc32 = Data.Skip(Constants.CRC32_OFFSET_CENTRAL).Take(Constants.CRC32_LENGTH_CENTRAL).ToArray(); 
            CompressedSize = Data.Skip(Constants.COMPRESSED_SIZE_OFFSET_CENTRAL).Take(Constants.COMPRESSED_SIZE_LENGTH_CENTRAL).ToArray();
            UncompressedSize = Data.Skip(Constants.UNCOMPRESSED_SIZE_OFFSET_CENTRAL).Take(Constants.UNCOMPRESSED_SIZE_LENGTH_CENTRAL).ToArray();
            FileNameLength = Data.Skip(Constants.FILENAME_LENGTH_OFFSET_CENTRAL).Take(Constants.FILENAME_LENGTH_LENGTH_CENTRAL).ToArray();
            ExtraFieldLength = Data.Skip(Constants.EXTRA_FIELD_LENGTH_OFFSET_CENTRAL).Take(Constants.EXTRA_FIELD_LENGTH_LENGTH_CENTRAL).ToArray();
            FileCommentLength = Data.Skip(Constants.FILE_COMMENT_LENGTH_OFFSET_CENTRAL).Take(Constants.FILE_COMMENT_LENGTH_LENGTH_CENTRAL).ToArray();
            DiskNumberStart = Data.Skip(Constants.DISK_NUMBER_START_OFFSET_CENTRAL).Take(Constants.DISK_NUMBER_START_LENGTH_CENTRAL).ToArray();
            InternalFileAttributes = Data.Skip(Constants.INTERNAL_FILE_ATTRIBUTES_OFFSET_CENTRAL).Take(Constants.INTERNAL_FILE_ATTRIBUTES_LENGTH_CENTRAL).ToArray();
            ExternalFileAttributes = Data.Skip(Constants.EXTERNAL_FILE_ATTRIBUTES_OFFSET_CENTRAL).Take(Constants.EXTERNAL_FILE_ATTRIBUTES_LENGTH_CENTRAL).ToArray();
            RelativeOffsetOfLocalHeader = Data.Skip(Constants.RELATIVE_OFFSET_OF_LOCAL_HEADER_OFFSET_CENTRAL).Take(Constants.RELATIVE_OFFSET_OF_LOCAL_HEADER_LENGTH_CENTRAL).ToArray();
            int arrayIndexes = Constants.RELATIVE_OFFSET_OF_LOCAL_HEADER_OFFSET_CENTRAL + Constants.RELATIVE_OFFSET_OF_LOCAL_HEADER_LENGTH_CENTRAL;
            FileNameArray = Data.Skip(arrayIndexes).Take(Commons.GetLength(FileNameLength)).ToArray();
            arrayIndexes += FileNameArray.Length;
            ExtraFieldArray = Data.Skip(arrayIndexes).Take(Commons.GetLength(ExtraFieldLength)).ToArray();
            arrayIndexes += ExtraFieldArray.Length;
            FileCommentArray = Data.Skip(arrayIndexes).Take(Commons.GetLength(FileCommentLength)).ToArray();
            arrayIndexes += FileCommentArray.Length;
            //var dataStartOffset = arrayIndexes;
            //HeaderData = Data.Skip(dataStartOffset).Take(Commons.GetLength(CompressedSize)).ToArray(); // Extract the compressed data
            //NextHeaderOffset = dataStartOffset + HeaderData.Length; // Calculate the size of the local header
            NextHeaderOffset = arrayIndexes; // The next header offset is the end of the current header
            FileName = Encoding.Default.GetString(FileNameArray);
        }

        public override string ToString()
        {
            VersionData version = new VersionData(VersionNeededToExtract);
            return "Version Needed: " + version.ToString() + Constants.NEWLINE +
                   "Flag: " + Commons.ByteToHexString(GeneralPurposeBitFlag) + Constants.NEWLINE +
                   "Comp. Method: " + Commons.ByteToHexString(CompressionMethod) + Constants.NEWLINE +
                   "Last Modified Time: " + Commons.ByteToHexString(LastModFileTime) + "-->" + Commons.ParseTime(LastModFileTime) + Constants.NEWLINE +
                   "Last Modified Date: " + Commons.ByteToHexString(LastModFileDate) + "-->" + Commons.ParseDate(LastModFileDate) + Constants.NEWLINE +
                   "CRC32: " + Commons.ByteToHexString(Crc32) + Constants.NEWLINE +
                   "Compressed Size: " + Commons.ByteToHexString(CompressedSize) + Constants.NEWLINE +
                   "Uncompressed Size: " + Commons.ByteToHexString(UncompressedSize) + Constants.NEWLINE +
                   "File Name Length: " + Commons.ByteToHexString(FileNameLength) + Constants.NEWLINE +
                   "Extra Field Length: " + Commons.ByteToHexString(ExtraFieldLength) + Constants.NEWLINE +
                   "File Comment Length: " + Commons.ByteToHexString(FileCommentLength) + Constants.NEWLINE +
                   "Disk Number Start: " + Commons.ByteToHexString(DiskNumberStart) + Constants.NEWLINE +
                   "Internal File Attributes: " + Commons.ByteToHexString(InternalFileAttributes) + Constants.NEWLINE +
                   "External File Attributes: " + Commons.ByteToHexString(ExternalFileAttributes) + Constants.NEWLINE +
                   "Relative Offset of Local Header: " + Commons.ByteToHexString(RelativeOffsetOfLocalHeader) + Constants.NEWLINE +
                   "File Name: " + FileName;
        }
    }

}

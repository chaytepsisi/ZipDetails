using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZipDetails
{
    class ZipParser
    {
        
        int NextHeaderOffset= 0;
        public byte[] Data { get; set; }
        public ZipParser(byte[] content)
        {
            Data = content;
        }
        
        
        /*string ParseLocalHeader()
        {
            var versionNeeded = Data.Skip(Constants.VERSION_NEEDED_OFFSET).Take(Constants.VERSION_NEEDED_LENGTH).ToArray();
            var generalPurposeFlag = Data.Skip(Constants.GENERAL_PURPOSE_FLAG_OFFSET).Take(Constants.GENERAL_PURPOSE_FLAG_LENGTH).ToArray();
            var compressionMethod = Data.Skip(Constants.COMPRESSION_METHOD_OFFSET).Take(Constants.COMPRESSION_METHOD_LENGTH).ToArray();
            var lastModFileTime = Data.Skip(Constants.LAST_MOD_FILE_TIME_OFFSET).Take(Constants.LAST_MOD_FILE_TIME_LENGTH).ToArray();
            var lastModFileDate = Data.Skip(Constants.LAST_MOD_FILE_DATE_OFFSET).Take(Constants.LAST_MOD_FILE_DATE_LENGTH).ToArray();
            var crc32 = Data.Skip(Constants.CRC32_OFFSET).Take(Constants.CRC32_LENGTH).ToArray();
            var compressedSize = Data.Skip(Constants.COMPRESSED_SIZE_OFFSET).Take(Constants.COMPRESSED_SIZE_LENGTH).ToArray();
            var uncompressedSize = Data.Skip(Constants.UNCOMPRESSED_SIZE_OFFSET).Take(Constants.UNCOMPRESSED_SIZE_LENGTH).ToArray();
            var fileNameLength = Data.Skip(Constants.FILENAME_LENGTH_OFFSET).Take(Constants.FILENAME_LENGTH_LENGTH).ToArray();
            var extraFieldLength = Data.Skip(Constants.EXTRA_FIELD_LENGTH_OFFSET).Take(Constants.EXTRA_FIELD_LENGTH_LENGTH).ToArray();

            var FileNameArray = Data.Skip(Constants.FILENAME_LENGTH_LENGTH + Constants.EXTRA_FIELD_LENGTH_OFFSET)
                .Take(Commons.GetLength(fileNameLength)).ToArray();
            var dataStartOffset = Constants.FILENAME_LENGTH_LENGTH + Constants.EXTRA_FIELD_LENGTH_OFFSET + Commons.GetLength(fileNameLength) + Commons.GetLength(extraFieldLength);
             CompressedData = Data.Skip(dataStartOffset).Take(Commons.GetLength(compressedSize)).ToArray(); // Extract the compressed data
            NextHeaderOffset = dataStartOffset+Commons.GetLength(compressedSize); // Calculate the size of the local header
            
            string LocalHeaderText = "Version: " + Commons.ByteToHexString(versionNeeded) + Constants.NEWLINE +
                "Flag: " + Commons.ByteToHexString(generalPurposeFlag) + Constants.NEWLINE +
                "Comp. Method: " + Commons.ByteToHexString(compressionMethod) + Constants.NEWLINE +
                "Last Modified Time: " + Commons.ByteToHexString(lastModFileTime) + "-->" + ParseTime(lastModFileTime) + Constants.NEWLINE +
                Convert.ToString(BitConverter.ToInt16(lastModFileTime, 0), 2) + Constants.NEWLINE +
                "Last Modified Date: " + Commons.ByteToHexString(lastModFileDate) + "-->" + ParseDate(lastModFileDate) + Constants.NEWLINE +
                Convert.ToString(BitConverter.ToInt16(lastModFileDate, 0), 2) + Constants.NEWLINE +
                "CRC32 : " + Commons.ByteToHexString(crc32) + Constants.NEWLINE +
                "Compressed Size: " + Commons.ByteToHexString(compressedSize) + Constants.NEWLINE + "-->" + Commons.GetLength(compressedSize) + Constants.NEWLINE +
                "Uncompressed Size: " + Commons.ByteToHexString(uncompressedSize) + Constants.NEWLINE + "-->" + Commons.GetLength(uncompressedSize) + Constants.NEWLINE +
                "File Name Length: " + Commons.ByteToHexString(fileNameLength) + Constants.NEWLINE + "-->" + Commons.GetLength(fileNameLength) + Constants.NEWLINE +
                "Extra Field Length: " + Commons.ByteToHexString(extraFieldLength) + Constants.NEWLINE +
                Encoding.Default.GetString(FileNameArray);

            Clipboard.SetText(LocalHeaderText);
            return LocalHeaderText;

        }*/

        string ParseCentralHeader()
        {
            var versionMadeBy = Data.Skip(Constants.VERSION_MADE_BY_OFFSET).Take(Constants.VERSION_MADE_BY_LENGTH).ToArray();
            var versionNeededToExtract = Data.Skip(Constants.VERSION_NEEDED_TO_EXTRACT_OFFSET).Take(Constants.VERSION_NEEDED_TO_EXTRACT_LENGTH).ToArray();
            var generalPurposeBitFlag = Data.Skip(Constants.GENERAL_PURPOSE_BIT_FLAG_OFFSET).Take(Constants.GENERAL_PURPOSE_BIT_FLAG_LENGTH).ToArray();
            var compressionMethodCentral = Data.Skip(Constants.COMPRESSION_METHOD_OFFSET_CENTRAL).Take(Constants.COMPRESSION_METHOD_LENGTH_CENTRAL).ToArray();
            var lastModFileTimeCentral = Data.Skip(Constants.LAST_MOD_FILE_TIME_OFFSET_CENTRAL).Take(Constants.LAST_MOD_FILE_TIME_LENGTH_CENTRAL).ToArray();
            var lastModFileDateCentral = Data.Skip(Constants.LAST_MOD_FILE_DATE_OFFSET_CENTRAL).Take(Constants.LAST_MOD_FILE_DATE_LENGTH_CENTRAL).ToArray();
            var crc32Central = Data.Skip(Constants.CRC32_OFFSET_CENTRAL).Take(Constants.CRC32_LENGTH_CENTRAL).ToArray();
            var compressedSizeCentral = Data.Skip(Constants.COMPRESSED_SIZE_OFFSET_CENTRAL).Take(Constants.COMPRESSED_SIZE_LENGTH_CENTRAL).ToArray();
            var uncompressedSizeCentral = Data.Skip(Constants.UNCOMPRESSED_SIZE_OFFSET_CENTRAL).Take(Constants.UNCOMPRESSED_SIZE_LENGTH_CENTRAL).ToArray();
            var fileNameLengthCentral = Data.Skip(Constants.FILENAME_LENGTH_OFFSET_CENTRAL).Take(Constants.FILENAME_LENGTH_LENGTH_CENTRAL).ToArray();
            var extraFieldLengthCentral = Data.Skip(Constants.EXTRA_FIELD_LENGTH_OFFSET_CENTRAL).Take(Constants.EXTRA_FIELD_LENGTH_LENGTH_CENTRAL).ToArray();
            var fileCommentLengthCentral = Data.Skip(Constants.FILE_COMMENT_LENGTH_OFFSET_CENTRAL).Take(Constants.FILE_COMMENT_LENGTH_LENGTH_CENTRAL).ToArray();
            var diskNumberStart = Data.Skip(Constants.DISK_NUMBER_START_OFFSET_CENTRAL).Take(Constants.DISK_NUMBER_START_LENGTH_CENTRAL).ToArray();
            var internalFileAttributes = Data.Skip(Constants.INTERNAL_FILE_ATTRIBUTES_OFFSET_CENTRAL).Take(Constants.INTERNAL_FILE_ATTRIBUTES_LENGTH_CENTRAL).ToArray();
            var externalFileAttributes = Data.Skip(Constants.EXTERNAL_FILE_ATTRIBUTES_OFFSET_CENTRAL).Take(Constants.EXTERNAL_FILE_ATTRIBUTES_LENGTH_CENTRAL).ToArray();
            var relativeOffsetOfLocalHeader = Data.Skip(Constants.RELATIVE_OFFSET_OF_LOCAL_HEADER_OFFSET_CENTRAL)
                .Take(Constants.RELATIVE_OFFSET_OF_LOCAL_HEADER_LENGTH_CENTRAL).ToArray();

            var newOffset = Constants.RELATIVE_OFFSET_OF_LOCAL_HEADER_OFFSET_CENTRAL + Constants.RELATIVE_OFFSET_OF_LOCAL_HEADER_LENGTH_CENTRAL;
            var FileNameArray = Data.Skip(newOffset).Take(Commons.GetLength(fileNameLengthCentral)).ToArray();
            newOffset += Commons.GetLength(fileNameLengthCentral);
            var ExtraField = Data.Skip(newOffset).Take(Commons.GetLength(extraFieldLengthCentral)).ToArray();
            newOffset += Commons.GetLength(extraFieldLengthCentral);
            var FileComment = Data.Skip(newOffset).Take(Commons.GetLength(fileCommentLengthCentral)).ToArray();

            NextHeaderOffset = newOffset + Commons.GetLength(fileCommentLengthCentral); // Update the offset for the next header

            var CentralHeaderText = "Version Made By: " + Commons.ByteToHexString(versionMadeBy) + Constants.NEWLINE + "-->" + VersionMadeBy.GetVersionInfo(versionMadeBy) + Constants.NEWLINE +
                "Version Needed to Extract: " + Commons.ByteToHexString(versionNeededToExtract) + Constants.NEWLINE +
                "General Purpose Bit Flag: " + Commons.ByteToHexString(generalPurposeBitFlag) + Constants.NEWLINE +
                "Comp. Method: " + Commons.ByteToHexString(compressionMethodCentral) + Constants.NEWLINE +
                "Last Modified Time: " + Commons.ByteToHexString(lastModFileTimeCentral) + "-->" + Commons.ParseTime(lastModFileTimeCentral) + Constants.NEWLINE +
                Convert.ToString(BitConverter.ToInt16(lastModFileTimeCentral, 0), 2) + Constants.NEWLINE +
                "Last Modified Date: " + Commons.ByteToHexString(lastModFileDateCentral) + "-->" + Commons.ParseDate(lastModFileDateCentral) + Constants.NEWLINE +
                Convert.ToString(BitConverter.ToInt16(lastModFileDateCentral, 0), 2) + Constants.NEWLINE +
                "CRC32 : " + Commons.ByteToHexString(crc32Central) + Constants.NEWLINE +
                "Compressed Size: " + Commons.ByteToHexString(compressedSizeCentral) + Constants.NEWLINE + "-->" + Commons.GetLength(compressedSizeCentral) + Constants.NEWLINE +
                "Uncompressed Size: " + Commons.ByteToHexString(uncompressedSizeCentral) + Constants.NEWLINE + "-->" + Commons.GetLength(uncompressedSizeCentral) + Constants.NEWLINE +
                "File Name Length: " + Commons.ByteToHexString(fileNameLengthCentral) + Constants.NEWLINE + "-->" + Commons.GetLength(fileNameLengthCentral) + Constants.NEWLINE +
                "Extra Field Length: " + Commons.ByteToHexString(extraFieldLengthCentral) + Constants.NEWLINE +
                "File Comment Length: " + Commons.ByteToHexString(fileCommentLengthCentral) + Constants.NEWLINE +
                "Disk Number Start: " + Commons.ByteToHexString(diskNumberStart) + Constants.NEWLINE +
                "Internal File Attributes: " + Commons.ByteToHexString(internalFileAttributes) + Constants.NEWLINE +
                "External File Attributes: " + Commons.ByteToHexString(externalFileAttributes) + Constants.NEWLINE;

            if (FileNameArray.Length > 0)
                CentralHeaderText += "FileName: " + Encoding.Default.GetString(FileNameArray) + Constants.NEWLINE;
            if(ExtraField.Length > 0)
                CentralHeaderText += "ExtraField: " + Commons.ByteToHexString(ExtraField) + Constants.NEWLINE;
            if (FileComment.Length > 0)
                CentralHeaderText += "FileComment: " + Encoding.Default.GetString(ExtraField);

            return CentralHeaderText;
        }

        string ParseCentralDirectoryEnd()
        {
            var endOfCentralDirSignature = Data.Skip( Constants.END_OF_CENTRAL_DIR_SIGNATURE_OFFSET).Take(Constants.END_OF_CENTRAL_DIR_SIGNATURE_LENGTH).ToArray();
            if (endOfCentralDirSignature[0] != 0x50 || endOfCentralDirSignature[1] != 0x4b || endOfCentralDirSignature[2] != 0x05 || endOfCentralDirSignature[3] != 0x06)
            {
                throw new Exception("Not a valid End of Central Directory signature");
            }
            var diskNumber = Data.Skip(Constants.END_OF_CENTRAL_DIR_DISK_NUMBER_OFFSET).Take(Constants.END_OF_CENTRAL_DIR_DISK_NUMBER_LENGTH).ToArray();
            var diskWithCentralDirStart = Data.Skip(Constants.END_OF_CENTRAL_DIR_DISK_WITH_CENTRAL_DIR_START_OFFSET).Take(Constants.END_OF_CENTRAL_DIR_DISK_WITH_CENTRAL_DIR_START_LENGTH).ToArray();
            var totalEntriesOnDisk = Data.Skip(Constants.END_OF_CENTRAL_DIR_TOTAL_ENTRIES_OFFSET).Take(Constants.END_OF_CENTRAL_DIR_TOTAL_ENTRIES_LENGTH).ToArray();
            var totalEntries = Data.Skip(Constants.END_OF_CENTRAL_DIR_TOTAL_ENTRIES_ON_CENTRAL_DIR_LENGTH).Take(Constants.END_OF_CENTRAL_DIR_TOTAL_ENTRIES_ON_CENTRAL_DIR_LENGTH).ToArray();
            var centralDirectorySize = Data.Skip(Constants.END_OF_CENTRAL_DIR_SIZE_OF_CENTRAL_DIR_OFFSET).Take(Constants.END_OF_CENTRAL_DIR_SIZE_OF_CENTRAL_DIR_LENGTH).ToArray();
            var centralDirectoryOffset = Data.Skip(Constants.END_OF_CENTRAL_DIR_OFFSET_OFFSET).Take(Constants.END_OF_CENTRAL_DIR_OFFSET_LENGTH).ToArray();
            var zipFileCommentLength = Data.Skip(Constants.END_OF_CENTRAL_DIR_COMMENT_LENGTH_OFFSET).Take(Constants.END_OF_CENTRAL_DIR_COMMENT_LENGTH_LENGTH).ToArray();
            var zipFileComment = Data.Skip(Constants.END_OF_CENTRAL_DIR_COMMENT_OFFSET).Take(Commons.GetLength(zipFileCommentLength)).ToArray();

            NextHeaderOffset += Constants.END_OF_CENTRAL_DIR_COMMENT_OFFSET + Commons.GetLength(zipFileCommentLength);

            var CentralDirectoryEndText = "End of Central Directory Signature: " + Commons.ByteToHexString(endOfCentralDirSignature) + Constants.NEWLINE +
                "Disk Number: " + Commons.ByteToHexString(diskNumber) + Constants.NEWLINE +
                "Disk with Central Directory Start: " + Commons.ByteToHexString(diskWithCentralDirStart) + Constants.NEWLINE +
                "Total Entries on Disk: " + Commons.ByteToHexString(totalEntriesOnDisk) + Constants.NEWLINE + "-->" + Commons.GetLength(totalEntriesOnDisk) + Constants.NEWLINE +
                "Total Entries in Central Directory: " + Commons.ByteToHexString(totalEntries) + Constants.NEWLINE + "-->" + Commons.GetLength(totalEntries) + Constants.NEWLINE +
                "Size of Central Directory: " + Commons.ByteToHexString(centralDirectorySize) + Constants.NEWLINE + "-->" + Commons.GetLength(centralDirectorySize) + Constants.NEWLINE +
                "Offset of Central Directory: " + Commons.ByteToHexString(centralDirectoryOffset) + Constants.NEWLINE + "-->" + Commons.GetLength(centralDirectoryOffset) + Constants.NEWLINE +
                "ZIP File Comment Length: " + Commons.ByteToHexString(zipFileCommentLength) + Constants.NEWLINE +
                Encoding.Default.GetString(zipFileComment);

            return CentralDirectoryEndText;
        }
        public void Parse()
        {
            if (Data[0] != 0x50 || Data[1] != 0x4b || Data[2] != 0x03 || Data[3] != 0x04)
            {
                throw new Exception("Not a valid ZIP file");
            }
            else
            {
                LocalHeader localHeader = new LocalHeader(Data);

                MessageBox.Show(localHeader.ToString());
                if (localHeader.CompressedData.Length > 0)
                {
                    string zippedDataHex = Commons.ByteToHexString(localHeader.CompressedData.Take(10).ToArray());
                    MessageBox.Show("Zipped Data Starts with: \n" + zippedDataHex);
                }
                else
                {
                    MessageBox.Show("No remaining data after local header.");
                }
                NextHeaderOffset = localHeader.NextHeaderOffset;

                Data =Data.Skip(NextHeaderOffset).ToArray();
                MessageBox.Show( ParseCentralHeader());

                Data=Data.Skip(NextHeaderOffset).ToArray();
                MessageBox.Show(ParseCentralDirectoryEnd());
            }
        }
    }
}

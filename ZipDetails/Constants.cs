using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZipDetails
{
    class Constants
    {
        public static string NEWLINE = "\r\n";

        public static int LOCAL_FILE_HEADER_SIGNATURE = 0x504b0304;
        public static int LOCAL_FILE_HEADER_SIGNATURE_OFFSET = 0;
        public static int LOCAL_FILE_HEADER_SIGNATURE_LENGTH = 4;
        public static int VERSION_NEEDED_OFFSET = 4;
        public static int GENERAL_PURPOSE_FLAG_OFFSET = 6;
        public static int COMPRESSION_METHOD_OFFSET = 8;
        public static int LAST_MOD_FILE_TIME_OFFSET = 10;
        public static int LAST_MOD_FILE_DATE_OFFSET = 12;
        public static int CRC32_OFFSET = 14;
        public static int COMPRESSED_SIZE_OFFSET = 18;
        public static int UNCOMPRESSED_SIZE_OFFSET = 22;
        public static int FILENAME_LENGTH_OFFSET = 26;
        public static int EXTRA_FIELD_LENGTH_OFFSET = 28;
        public static int VERSION_NEEDED_LENGTH = 2;
        public static int GENERAL_PURPOSE_FLAG_LENGTH = 2;
        public static int COMPRESSION_METHOD_LENGTH = 2;
        public static int LAST_MOD_FILE_TIME_LENGTH = 2;
        public static int LAST_MOD_FILE_DATE_LENGTH = 2;
        public static int CRC32_LENGTH = 4;
        public static int COMPRESSED_SIZE_LENGTH = 4;
        public static int UNCOMPRESSED_SIZE_LENGTH = 4;
        public static int FILENAME_LENGTH_LENGTH = 2;
        public static int EXTRA_FIELD_LENGTH_LENGTH = 2;
        public static int LOCAL_FILE_HEADER_SIZE = 30;

        public static int CENTRAL_DIR_SIGNATURE = 0x504b0102;
        public static int VERSION_MADE_BY_OFFSET = 4;
        public static int VERSION_MADE_BY_LENGTH = 2;
        public static int VERSION_NEEDED_TO_EXTRACT_OFFSET = 6;
        public static int VERSION_NEEDED_TO_EXTRACT_LENGTH = 2;
        public static int GENERAL_PURPOSE_BIT_FLAG_OFFSET = 8;
        public static int GENERAL_PURPOSE_BIT_FLAG_LENGTH = 2;
        public static int COMPRESSION_METHOD_OFFSET_CENTRAL = 10;
        public static int COMPRESSION_METHOD_LENGTH_CENTRAL = 2;
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
        public static int RELATIVE_OFFSET_OF_LOCAL_HEADER_LENGTH_CENTRAL = 4;
        public static int CENTRAL_DIR_HEADER_LENGTH = 46;

        public static int END_OF_CENTRAL_DIR_SIGNATURE = 0x504b0506;
        public static int END_OF_CENTRAL_DIR_SIGNATURE_OFFSET = 0;
        public static int END_OF_CENTRAL_DIR_SIGNATURE_LENGTH = 4;
        public static int END_OF_CENTRAL_DIR_DISK_NUMBER_OFFSET = 4;
        public static int END_OF_CENTRAL_DIR_DISK_NUMBER_LENGTH = 2;
        public static int END_OF_CENTRAL_DIR_DISK_WITH_CENTRAL_DIR_START_OFFSET = 6;
        public static int END_OF_CENTRAL_DIR_DISK_WITH_CENTRAL_DIR_START_LENGTH = 2;
        public static int END_OF_CENTRAL_DIR_TOTAL_ENTRIES_OFFSET = 8;
        public static int END_OF_CENTRAL_DIR_TOTAL_ENTRIES_LENGTH = 2;
        public static int END_OF_CENTRAL_DIR_TOTAL_ENTRIES_ON_CENTRAL_DIR_OFFSET = 10;
        public static int END_OF_CENTRAL_DIR_TOTAL_ENTRIES_ON_CENTRAL_DIR_LENGTH = 2;
        public static int END_OF_CENTRAL_DIR_SIZE_OF_CENTRAL_DIR_OFFSET = 12;
        public static int END_OF_CENTRAL_DIR_SIZE_OF_CENTRAL_DIR_LENGTH = 4;
        public static int END_OF_CENTRAL_DIR_OFFSET_OFFSET = 16;
        public static int END_OF_CENTRAL_DIR_OFFSET_LENGTH = 4;
        public static int END_OF_CENTRAL_DIR_COMMENT_LENGTH_OFFSET = 20;
        public static int END_OF_CENTRAL_DIR_COMMENT_LENGTH_LENGTH = 2;
        public static int END_OF_CENTRAL_DIR_COMMENT_OFFSET = 22; // This is the offset where the comment starts, after the length
        public static int END_OF_CENTRAL_DIR_COMMENT_LENGTH = 0; // This will be set dynamically based on the comment length


    }
}

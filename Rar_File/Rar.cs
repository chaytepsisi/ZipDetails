using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rar_File
{
    class Rar
    {
        public int Version { get; set; }
        public int Flags { get; set; }
        public int CompressionMethod { get; set; }
        public int CompressedSize { get; set; }
        public int UncompressedSize { get; set; }
        public int FileNameLength { get; set; }
        public string FileName { get; set; }
        public byte[] CompressedData { get; set; }
        public bool IsCorrupted { get; set; }
    }
}

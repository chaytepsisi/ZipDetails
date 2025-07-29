using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZipDetails
{
    internal class Deflate
    {
        /*static void Run(byte[] compressedBytes)
        {
            // Compress a random string value
            //string value = Path.GetRandomFileName();

            using (var writer = new StreamWriter(new MemoryStream()))
            {
                writer.Write(value);
                writer.Flush();
                writer.BaseStream.Position = 0;

                compressedBytes = Compress(writer.BaseStream);
            }
            Stream decompressedStream = Decompress(compressedBytes);

            using (var reader = new StreamReader(decompressedStream))
            {
                string decompressedValue = reader.ReadToEnd();

                if (value == decompressedValue)
                    Console.WriteLine("Success");
                else
                    Console.WriteLine("Failed");
            }
        }

        private static byte[] Compress(Stream input)
        {
            using (var compressStream = new MemoryStream())
            {
                using (var compressor = new DeflateStream(compressStream, CompressionMode.Compress))
                {
                    input.CopyTo(compressor);
                    compressor.Close();
                    return compressStream.ToArray();
                }
            }
        }
        */
        public static byte[] Decompress(byte[] input)
        {
            var output = new MemoryStream();

            using (var compressStream = new MemoryStream(input))
            {
                using (var decompressor = new DeflateStream(compressStream, CompressionMode.Decompress))
                {
                    decompressor.CopyTo(output);
                }
            }
            output.Position = 0;
            return output.ToArray();
        }

        public static void CompressFile(string OriginalFileName, string CompressedFileName)
        {
            using (FileStream originalFileStream = File.Open(OriginalFileName, FileMode.Open))
            using (FileStream compressedFileStream = File.Create(CompressedFileName))
            using (var compressor = new DeflateStream(compressedFileStream, CompressionMode.Compress))
                originalFileStream.CopyTo(compressor);

        }
        public static void DecompressFile(string CompressedFileName, string DecompressedFileName)
        {
            using (FileStream compressedFileStream = File.Open(CompressedFileName, FileMode.Open))
            using (FileStream outputFileStream = File.Create(DecompressedFileName))
            using (var decompressor = new DeflateStream(compressedFileStream, CompressionMode.Decompress))
                decompressor.CopyTo(outputFileStream);

        }
    }
}
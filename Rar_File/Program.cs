using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rar_File
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string fileName = @"C:\Users\onurk\Desktop\test.rar";
                byte[] fileContent = System.IO.File.ReadAllBytes(fileName);
                RarFile rarFile = new RarFile(fileContent);
                rarFile.ParseFile();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}

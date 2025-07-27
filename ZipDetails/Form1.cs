using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ZipDetails
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using(OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Zip Files|*.zip";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    byte[] fileContent = System.IO.File.ReadAllBytes(openFileDialog.FileName);
                    ZipParser parser = new ZipDetails.ZipParser(fileContent);
                    parser.Parse();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Zip Files|*.zip";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    List<int> localHeaderOffsets=new List<int>();
                    List<int> centralDirectoryHeaderOffsets=new List<int>();

                    int centralDirecotryEndOffset = 0;

                    byte[] fileContent = System.IO.File.ReadAllBytes(openFileDialog.FileName);

                    for (int i = 0; i < fileContent.Length-4; i++)
                    {
                        if (fileContent[i] == 0x50 || fileContent[i + 1] == 0x4b || fileContent[i + 2] == 0x03 || fileContent[i + 3] == 0x04)
                            localHeaderOffsets.Add(i);
                        
                        if (fileContent[i] == 0x50 || fileContent[i + 1] == 0x4b || fileContent[i + 2] == 0x01 || fileContent[i + 3] == 0x02)
                            centralDirectoryHeaderOffsets.Add(i);

                        if (fileContent[i] == 0x50 || fileContent[i + 1] == 0x4b || fileContent[i + 2] == 0x05 || fileContent[i + 3] == 0x06)
                            centralDirecotryEndOffset = i;
                    }

                    var Data = fileContent.Skip(centralDirecotryEndOffset).ToArray();
                    ZipParser parser = new ZipDetails.ZipParser(Data);
                    
                    MessageBox.Show(parser.ParseCentralDirectoryEnd());
                }
            }
        }
    }
}

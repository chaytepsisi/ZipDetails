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
    }
}

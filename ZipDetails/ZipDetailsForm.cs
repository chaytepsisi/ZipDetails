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
    public partial class ZipDetailsForm : Form
    {
        ZipFile zipFile;
        public ZipDetailsForm()
        {
            InitializeComponent();
        }

        private void SelectFileButton_Click(object sender, EventArgs e)
        {
            bool showContent = ShowContentCbx.Checked;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                //openFileDialog.Filter = "Zip Files|*.zip";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = openFileDialog.FileName;
                    byte[] fileContent = System.IO.File.ReadAllBytes(openFileDialog.FileName);
                    zipFile = new ZipFile(fileContent);
                    zipFile.ParseFile();
                    richTextBox1.Text = zipFile.ToString(showContent);
                    
                }
            }
        }

        private void UnzipButton_Click(object sender, EventArgs e)
        {
            try
            {
                zipFile.Unzip(RecoverCbx.Checked);
            }catch (Exception ex)
            {
                MessageBox.Show("Error @UnzipButton_Click\n" + ex.Message);
            }
        }
    }
}

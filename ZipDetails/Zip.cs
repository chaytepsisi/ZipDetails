using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZipDetails
{
    class Zip
    {
        public LocalHeader ZipLocalHeader { get; set; }
        public CentralDirectoryHeader ZipCentralDirectoryHeader { get; set; }
        //public EndOfCentralDirectoryHeader ZipEndOfCentralDirectoryHeader { get; set; }
        public override string ToString()
        {
            return "Zip Details:" + Environment.NewLine +
                   "Local Header: " + Environment.NewLine + ZipLocalHeader.ToString() + Environment.NewLine + "---------------" + Environment.NewLine +
                   "Central Directory Header: " + Environment.NewLine + ZipCentralDirectoryHeader.ToString();
            //+ Environment.NewLine + "End of Central Directory Header: " + Environment.NewLine + ZipEndOfCentralDirectoryHeader.ToString();
        }   
    }
}

﻿using System;
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
        public bool IsDirectory() { return ZipCentralDirectoryHeader.IsDirectory; }
        public int EncodingValue(){ return ZipCentralDirectoryHeader.EncodingValue; }
        public bool IsCorrupted() { return ZipLocalHeader.IsCorrupted; }

        //public EndOfCentralDirectoryHeader ZipEndOfCentralDirectoryHeader { get; set; }
        public string ToString(bool flag=false)
        {
            string message = "";
            if(IsCorrupted())
                message = "-- BOZUK BÖLÜM -- " + Environment.NewLine;
            if (IsDirectory())
                message = "-- Directory -- " + Environment.NewLine;
            message+= "Zip Details:" + Environment.NewLine +
                   "Local Header: " + Environment.NewLine + ZipLocalHeader.ToString(flag) + Environment.NewLine + "---------------" + Environment.NewLine +
                   "Central Directory Header: " + Environment.NewLine + ZipCentralDirectoryHeader.ToString(flag);
            return message;
            //+ Environment.NewLine + "End of Central Directory Header: " + Environment.NewLine + ZipEndOfCentralDirectoryHeader.ToString();
        }   
    }
}

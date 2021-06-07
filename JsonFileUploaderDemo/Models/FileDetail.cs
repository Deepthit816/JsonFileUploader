using System;
using System.Collections.Generic;

#nullable disable

namespace JsonFileUploaderDemo.Models
{
    public partial class FileDetail
    {
        public int Guid { get; set; }
        public string FileName { get; set; }
        public byte[] FileBlob { get; set; }
        public int FileSize { get; set; }
        public string FileType { get; set; }
    }
}

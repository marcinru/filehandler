using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileHandler2.Api.Dto
{
    public class FileDownloadResponseDto
    {
        public byte[] Content { get; set; }

        public string Type { get; set; }

        public int Size { get; set; }
        public string FileName { get; set; }
    }
}

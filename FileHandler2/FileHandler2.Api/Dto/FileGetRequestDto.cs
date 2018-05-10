using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileHandler2.Api.Dto
{
    public class FileGetRequestDto
    {
        public DateTime? MinDate { get; set; }

        public DateTime? MaxDate { get; set; }

        public string FileName { get; set; }

        public string Tags { get; set; }

        public Guid AccountUid { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileHandler2.Api.Dto
{
    public class FileGetResponseDto
    {
        public FileGetResponseDto()
        {
            Files = new List<FileGetResponseDetailsDto>();
        }

       public List<FileGetResponseDetailsDto> Files { get; set; }
    }
}

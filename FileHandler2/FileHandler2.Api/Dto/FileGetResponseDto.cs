using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileHandler2.Api.Dto
{
    public class FileGetResponseDto
    {
       public List<FileGetResponseDetailsDto> Files { get; set; }
    }
}

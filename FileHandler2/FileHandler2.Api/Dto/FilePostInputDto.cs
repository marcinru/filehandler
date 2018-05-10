using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileHandler2.Api.Dto
{
    public class FilePostInputDto
    {
        public string Name { get; set; }

        public byte[] Content { get; set; }

        public string UserName { get; set; }

        public List<string> Tags { get; set; }
    }
}

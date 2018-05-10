using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FileHandler2.Api.Dto
{
    public class FilePostInputDto
    {
        public string Name { get; set; }

        public string Content { get; set; }

        public string UserName { get; set; }

        public List<string> Tags { get; set; }

        public int Size { get; set; }
    }
}

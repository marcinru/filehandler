using System;

namespace FileHandler2.Api.Dto
{
    public class FileGetResponseDetailsDto
    {
        public string FileName { get; set; }

        public Guid FileUid { get; set; }

        public DateTime DateCreated { get; set; }

        public string CreatedBy { get; set; }
    }
}
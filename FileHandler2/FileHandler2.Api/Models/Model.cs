using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FileHandler2.Api.Models
{
    public class Model
    {
        public class FileHandlerContext : DbContext
        {
            public FileHandlerContext(DbContextOptions<FileHandlerContext> options)
                : base(options)
            { }

            public DbSet<FileReference> FileReferences { get; set; }
        }
    }

    [Table("FileReference")]
    public class FileReference
    {
        //[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
        public int Id { get; set; }

        //[FileUid] UNIQUEIDENTIFIER NOT NULL,
        public Guid FileUid  { get; set; }

        //[Name] nvarchar(128) NOT NULL,
        public string Name { get; set; }

        //[Url] nvarchar(300) NOT NULL,
        public string Url { get; set; }

        //[Tags] xml NULL,
        public string Tags { get; set; }
        
        //Actual smallint DEFAULT 1,
        public Int16 Actual { get; set; }

        //FileSize int NOT NULL,
        public int FileSize { get; set; }
        
        //[CDate] datetime NOT NULL DEFAULT GETDATE(),
        public DateTime CDate { get; set; }

        //[CBy] varchar(50) NOT NULL DEFAULT suser_sname()
        public string CBy { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FileHandler2.Api.Dto;
using FileHandler2.Api.Helpers;
using FileHandler2.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FileHandler2.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class FileController : Controller
    {
        private readonly Model.FileHandlerContext _context;
        private readonly AzureStorageConfig _storageConfig;

        public FileController(Model.FileHandlerContext context, IOptions<AzureStorageConfig> storageConfig)
        {
            _context = context;
            _storageConfig = storageConfig.Value;
        }

        [HttpPost]
        public FileGetResponseDto List([FromBody]FileGetRequestDto input)
        {
            var fileListDb = _context.FileReferences.Where(a => a.Actual > 0).ToList();

            FileGetResponseDto response = new FileGetResponseDto();

            foreach (var dbFileReference in fileListDb)
            {
                FileGetResponseDetailsDto details = new FileGetResponseDetailsDto()
                {
                    FileUid = dbFileReference.FileUid,
                    DateCreated = dbFileReference.CDate,
                    CreatedBy = dbFileReference.CBy,
                    FileName = dbFileReference.Name,
                    Tags = XmlHelper.GetTagsCsv(dbFileReference.Tags)
                };

                response.Files.Add(details);
            }

            return response;
        }

        [HttpGet]
        public async Task<FileDownloadResponseDto> Download([FromQuery]Guid fileUid)
        {
            //db side
            var dbFileData = _context.FileReferences.FirstOrDefault(u => u.FileUid == fileUid);

            if (dbFileData != null)
            {
                MemoryStream response = await StorageHelper.GetFileContent(_storageConfig, fileUid);
                

                return new FileDownloadResponseDto()
                {
                    Size = dbFileData.FileSize,
                    Type = "file",
                    Content = response.ToArray(),
                    FileName = dbFileData.Name
                };
            }
            else
            {
                throw new HttpRequestException("File not found");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody]FilePostInputDto input)
        {

            //convert base64 to stream
            byte[] fileContent = Convert.FromBase64String(input.Content);
            Guid fileUid = Guid.NewGuid();


            var result = await StorageHelper.UploadFileToStorage(new MemoryStream(fileContent), fileUid.ToString(), _storageConfig);

            var existing = _context.FileReferences.Where(a => a.Name == input.Name).AsEnumerable();

            foreach (var entry in existing)
            {
                entry.Actual = 0;
            }

            if (result != null && result.Success)
            {

                // insert to database
                _context.FileReferences.Add(new FileReference()
                {
                    FileUid = fileUid,
                    Name = input.Name,
                    Actual = 1,
                    CBy = input.UserName,
                    CDate = DateTime.Now,
                    Tags = XmlHelper.GetTagsXml(input.Tags),
                    Url = result.FileUrl,
                    FileSize = input.Size
                });

                _context.SaveChanges();

                return StatusCode(200);
            }

            return StatusCode(500);
        }

        [HttpDelete]
        public void Delete([FromQuery]Guid fileUid)
        {
        }
    }
}

namespace FileHandler2.Api.Helpers
{
}

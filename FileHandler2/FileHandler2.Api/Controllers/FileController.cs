using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileHandler2.Api.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FileHandler2.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class FileController : Controller
    {
        [HttpPost]
        public FileGetResponseDto List([FromBody]FileGetRequestDto input)
        {
            return new FileGetResponseDto()
            {
                Files = new List<FileGetResponseDetailsDto>()
                {
                    new FileGetResponseDetailsDto()
                    {
                        FileName = "test file 1",
                        DateCreated = DateTime.Now,
                        FileUid = Guid.NewGuid(),
                        CreatedBy = "api mock"
                    },
                    new FileGetResponseDetailsDto()
                    {
                        FileName = "test file 2",
                        DateCreated = DateTime.Now,
                        FileUid = Guid.NewGuid(),
                        CreatedBy = "api mock"
                    },
                    new FileGetResponseDetailsDto()
                    {
                        FileName = "test file 3",
                        DateCreated = DateTime.Now,
                        FileUid = Guid.NewGuid(),
                        CreatedBy = "api mock"
                    }
                }
            };
        }

        [HttpGet]
        public FileDownloadResponseDto Download([FromQuery]Guid fileUid)
        {
            return new FileDownloadResponseDto()
            {
                Size = 100,
                Type = "image",
                Content = null
            };
        }


        [HttpPost]
        public void Post([FromBody]FilePostInputDto input)
        {
        }

        [HttpDelete]
        public void Delete([FromQuery]Guid fileUid)
        {
        }
    }
}

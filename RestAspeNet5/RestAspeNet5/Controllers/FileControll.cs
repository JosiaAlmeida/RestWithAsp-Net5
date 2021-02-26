using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestAspeNet5.Business;
using RestAspeNet5.Data.VO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RestAspeNet5.Controllers
{
    [ApiVersion("1")]
    [Authorize("Bearer")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class FileControll : ControllerBase
    {
        private readonly IFileBusiness _fileBusiness;

        public FileControll(IFileBusiness fileBusiness)
        {
            _fileBusiness = fileBusiness;
        }
        [HttpPost("UploadFile")]
        [ProducesResponseType((200), Type = typeof(FileDetaVo))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/json")]
        public async Task<IActionResult> UploadOneFile([FromForm] IFormFile file)
        {
            FileDetaVo detail = await _fileBusiness.SaveFileToDisk(file);
            return new OkObjectResult(detail);
        }

        [HttpPost("uploadMultFile")]
        [ProducesResponseType((200), Type = typeof(List<FileDetaVo>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/json")]
        public async Task<IActionResult> UploadManyFiles([FromForm] List<IFormFile> files)
        {
            List<FileDetaVo> details = await _fileBusiness.SaveFilesToDisk(files);
            return new OkObjectResult(details);
        }
        [HttpGet("DownLoadFile/{fileName}")]
        [ProducesResponseType((200), Type = typeof(byte[]))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/octet-stream")]
        public async Task<IActionResult> DownLoad(string fileName)
        {
            byte[] buffer = _fileBusiness.GetFile(fileName);
            if(buffer != null)
            {
                HttpContext.Response.ContentType = 
                    $"application/{Path.GetExtension(fileName).Replace(".","")}";
                HttpContext.Response.Headers.Add("content-length", buffer.Length.ToString());
                await HttpContext.Response.Body.WriteAsync(buffer, 0, buffer.Length);
            }

            return new ContentResult();
        }
    }
}

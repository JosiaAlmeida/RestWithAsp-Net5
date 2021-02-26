using Microsoft.AspNetCore.Http;
using RestAspeNet5.Data.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAspeNet5.Business
{
    //Para salvar arquivos
    public interface IFileBusiness
    {
        public byte[] GetFile(string filename);
        public Task<FileDetaVo> SaveFileToDisk (IFormFile file);
        public Task<List<FileDetaVo>> SaveFilesToDisk(IList<IFormFile> files);
    }
}

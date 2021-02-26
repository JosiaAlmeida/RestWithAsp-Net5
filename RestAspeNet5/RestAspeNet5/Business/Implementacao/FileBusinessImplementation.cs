using Microsoft.AspNetCore.Http;
using RestAspeNet5.Data.VO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RestAspeNet5.Business.Implementacao
{
    public class FileBusinessImplementation : IFileBusiness
    {
        private readonly string _basePath;
        private readonly IHttpContextAccessor _context;
        public FileBusinessImplementation(IHttpContextAccessor context)
        {
            _context = context;
            //Setando e Pegando indereço
            _basePath = Directory.GetCurrentDirectory() + "\\UpLoad\\";
        }

        public byte[] GetFile(string filename)
        {
            var filePath = _basePath + filename;
            return File.ReadAllBytes(filePath);
        }
        //Responsavel pra salvar os arquivos
        public async Task<FileDetaVo> SaveFileToDisk(IFormFile file)
        {
            FileDetaVo FileDetail = new FileDetaVo();
            //Pegando a extensão ou tipo de ficheiro
            var FileType = Path.GetExtension(file.FileName);
            //Pega o endereço url incluindo a porta onde roda a aplicação
            var baseUrl = _context.HttpContext.Request.Host;
            if (FileType.ToLower() == ".pdf" || FileType.ToLower() == ".jpg" ||
                FileType.ToLower() == ".png" || FileType.ToLower() == ".jpeg")
            {
                //Pega o nome do ficheiro
                var docName = Path.GetFileName(file.FileName);
                if (file != null && file.Length > 0)
                {
                    //Onde sera salvo "Destino"
                    var destination = Path.Combine(_basePath, "", docName);
                    FileDetail.DocumentName = docName;
                    FileDetail.DocType = FileType;
                    FileDetail.DocUrl = Path.Combine(baseUrl + "/api/file/v1" + FileDetail.DocumentName);

                    //Abrindo o sistema de arquivos e gravação
                    using var stream = new FileStream(destination, FileMode.Create);
                    //Salvando
                    await file.CopyToAsync(stream);
                }
            }
            return FileDetail;
        }
        public async Task<List<FileDetaVo>> SaveFilesToDisk(IList<IFormFile> files)
        {
            List<FileDetaVo> list = new List<FileDetaVo>();
            foreach (var filers in files)
            {
                list.Add(await SaveFileToDisk(filers));
            }
            return list;
        }
    }
}

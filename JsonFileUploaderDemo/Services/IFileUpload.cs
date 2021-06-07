using JsonFileUploaderDemo.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace JsonFileUploaderDemo.Services
{
   public interface IFileUpload
    {
        FileDetail GetFile(int id);
        IEnumerable<FileDetail> GetAllFile();
        Task<int> UploadFile(FileUploadModel file);
        bool CheckFileFormat(IFormFile formFile);
        Task<string> UpdateFile(FileUploadModel file);
    }
}


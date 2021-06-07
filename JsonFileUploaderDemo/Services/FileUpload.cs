using JsonFileUploaderDemo.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace JsonFileUploaderDemo.Services
{
    public class FileUpload : IFileUpload
    {
        public bool CheckFileFormat(IFormFile formFile)
        {
            var extension = "." + formFile.FileName.Split('.')[formFile.FileName.Split('.').Length - 1];
            return (extension == ".json");
        }
        public FileDetail GetFile(int id)
        {
            
            FileDetail result;
            using (var context = new JSONFileUploaderContext())
            {
                result = context.FileDetails.Where(x => x.Guid == id).FirstOrDefault();
            }
            return result;
        }
        public IEnumerable<FileDetail> GetAllFile()
        {
            using (var context = new JSONFileUploaderContext())
            {
                return context.FileDetails.ToList();
            }
        }
        public Task<string> UpdateFile(FileUploadModel fileModel)
        {
            if (fileModel.file.Length > 0)
            {

                byte[] binary;
                using (BinaryReader br = new BinaryReader(fileModel.file.OpenReadStream()))
                {
                    // Convert the image in to bytes
                    binary = br.ReadBytes((int)fileModel.file.OpenReadStream().Length);
                }
                if (fileModel.id != 0)
                {
                    using (var context = new JSONFileUploaderContext())
                    {
                        FileDetail fileDetail = context.FileDetails.Where(x => x.Guid == fileModel.id).FirstOrDefault();
                        fileDetail.FileBlob = binary;
                        fileDetail.FileName = fileModel.file.FileName;
                        fileDetail.FileSize = (int)fileModel.file.Length;
                        fileDetail.FileType = fileModel.file.ContentType;
                        context.SaveChanges();

                        return Task.FromResult("Updated");
                    }
                }
            }
            return Task.FromResult("Failed");
        }
        public Task<int> UploadFile(FileUploadModel fileModel)
        {
            if (fileModel.file.Length > 0)
            {

                byte[] binary;
                int guId;
                using (BinaryReader br = new BinaryReader(fileModel.file.OpenReadStream()))
                {
                    // Convert the image in to bytes
                    binary = br.ReadBytes((int)fileModel.file.OpenReadStream().Length);
                }
                using (var context = new JSONFileUploaderContext())
                {
                    var fileDetail = new FileDetail()
                    {
                        FileName = fileModel.file.FileName,
                        FileBlob = binary,
                        FileSize = (int)fileModel.file.Length,
                        FileType = fileModel.file.ContentType
                    };

                    context.Add(fileDetail);
                    context.SaveChanges();
                    guId = fileDetail.Guid;
                }
                return Task.FromResult(guId);
            }
            return Task.FromResult(0);
        }
    }
}

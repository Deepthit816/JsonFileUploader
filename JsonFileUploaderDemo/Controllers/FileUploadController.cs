using JsonFileUploaderDemo.Models;
using JsonFileUploaderDemo.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JsonFileUploaderDemo.Controllers
{
    [EnableCors("MyAllowSpecificOrigins")]
    [Route("api/FileUpload")]
    public class FileUploadController : ControllerBase
    {
        private readonly IFileUpload m_fileUpload;

        public FileUploadController(IFileUpload fileUpload)
        {
            m_fileUpload = fileUpload;
        }

        [HttpGet("{id}")]

        public FileDetail GetFile(int id)
        {
            return m_fileUpload.GetFile(id);
        }
        [Route("GetAllFileFromDb")]
        [HttpGet]
        public IEnumerable<FileDetail> GetAllFile()
        {
            return m_fileUpload.GetAllFile();
        }

        [Route("InsertFile")]
        [HttpPost]
        public async Task<int> InsertFile([FromForm] FileUploadModel fileModel)
        {
            if (!m_fileUpload.CheckFileFormat(fileModel.file))
            {
                return 0;
            }
            return await m_fileUpload.UploadFile(fileModel);
        }
        [Route("UpdateFile")]
        [HttpPut]
        public async Task<string> UpdateFile([FromForm] FileUploadModel fileModel)
        {
            if (!m_fileUpload.CheckFileFormat(fileModel.file))
            {
                return "Invalid Format";
            }
            return await m_fileUpload.UpdateFile(fileModel);
        }
    }

}

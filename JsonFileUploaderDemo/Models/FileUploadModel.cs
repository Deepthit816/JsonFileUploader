using Microsoft.AspNetCore.Http;
 

namespace JsonFileUploaderDemo.Models
{
    public class FileUploadModel
    {
        public IFormFile file { get; set; }
        public int id { get; set; }
    }
}

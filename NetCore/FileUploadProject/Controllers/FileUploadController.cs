using FileUploadProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data.OleDb;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace FileUploadProject.Controllers
{
    public class FileUploadController : Controller
    {
        public static IWebHostEnvironment _environment;
        public FileUploadController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        [HttpPost]
        [Route("UploadImg")]
        public IActionResult Post([FromForm] ImageUploadModel objFile)
        {
            FileUpload070723 obj = new FileUpload070723();
            sdirectdbContext _db = new sdirectdbContext();

            if (!Directory.Exists(_environment.WebRootPath + "\\Upload"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "\\Upload\\");
            }
            string val = DateTime.Now.ToString();
            val = val.Replace("-", string.Empty);
            val = val.Replace(":", string.Empty);
            val = val.Replace(" ", string.Empty);
            using (FileStream filestream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + val + objFile.image.FileName))
            {

                objFile.image.CopyTo(filestream);
                filestream.Flush();
                obj.ImgLoc = "\\Upload\\" + objFile.image.FileName + val;
                _db.FileUpload070723s.Add(obj);
                _db.SaveChanges();
            }

            return Ok("uploaded");
        }
    }

}


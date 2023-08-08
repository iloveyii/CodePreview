using Accounting.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> upload([FromForm] IFormFile file)
        {
            // Check if the file is there
            if (file == null)
            {
                return BadRequest("File is null");
            }

            // Get the file name
            var fileName = file.FileName;

            // Get the extension
            var extension = Path.GetExtension(fileName);

            // Validate extension if required

            // Generated a new file name to avoid overwrite = (Filename without extension - GUID.extension
            var newFileName = $"{Path.GetFileNameWithoutExtension(fileName)}-{Guid.NewGuid().ToString()}{extension}";

            // Create the directory path to store the file
            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Data");
            var fullPath = Path.Combine(directoryPath, newFileName);

            // Create path if not exists
            Directory.CreateDirectory(directoryPath);

            // Create a file stream at the destination directory and copy contents of file to this stream
            using (var fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            {
                // Copy file to the new stream
                await file.CopyToAsync(fileStream);
            }

            // You are done - return the url of the file
            return Ok($"/Data/{newFileName}");
        }

        [HttpGet]
        public string Get()
        {
            return "Get is ok";
        }
    }
}

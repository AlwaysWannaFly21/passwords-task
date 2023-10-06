using Microsoft.AspNetCore.Mvc;
using password_task.Interfaces;

namespace password_task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("upload-file")]
        public IActionResult UploadFile(IFormFile file)
        {
            return Ok(_fileService.GetValidPasswordsCount(file));
        }
    }
}

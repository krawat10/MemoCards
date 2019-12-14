using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MemoCards.DTOs;
using MemoCards.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace MemoCards.Controllers
{
    public class FileController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public FileController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpGet]
        [Route("css/{filename}")]
        public async Task<IActionResult> Css(string filename)
        {
            var stream = new FileStream(Path.Combine(_env.WebRootPath, "Static", "css", filename), FileMode.Open);

            return new FileStreamResult(stream, "text/css");
        }
    }
}
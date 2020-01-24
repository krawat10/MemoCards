using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using MemoCards.Data;
using MemoCards.DTOs;
using MemoCards.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

namespace MemoCards.Controllers
{
    public class FileController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly ApplicationDbContext _context;

        public FileController(IWebHostEnvironment env, ApplicationDbContext context)
        {
            _env = env;
            _context = context;

            if (!_context.Words.Any())
            {
                InitializeWords("en.json", Language.EN);
                InitializeWords("pl.json", Language.PL);
            }
        }

        private void InitializeWords(string filename, Language language)
        {
            var json = System.IO.File.ReadAllText(Path.Combine(_env.WebRootPath, "Static", "lang", filename));
            var words = JsonSerializer.Deserialize<IEnumerable<Word>>(json);

            foreach (var word in words)
            {
                word.Language = language;
                _context.Add(word);
            }

            _context.SaveChanges();
        }

        [HttpGet]
        [Route("css/{filename}")]
        public async Task<IActionResult> Css(string filename)
        {
            var stream = new FileStream(Path.Combine(_env.WebRootPath, "Static", "css", filename), FileMode.Open);

            return new FileStreamResult(stream, "text/css");
        }

        [HttpGet]
        [Route("lang")] 
        public async Task<IActionResult> Lang()
        {
            var lang = "en";

            if (HttpContext.Request.Cookies.ContainsKey("lang"))
            {
                HttpContext.Request.Cookies.TryGetValue("lang", out lang); // Get cookie value
                lang = lang.Split('-').First().ToUpper();
            }

            Language language = Language.EN;

            if (Enum.IsDefined(typeof(Language), lang))
            {
                language = Enum.Parse<Language>(lang);
            }

            var words = await _context.Words
                .Where(word => word.Language == language)
                .ToListAsync();

            XElement element = new XElement("root", words.Select(word => new XElement(word.Key, word.Value)));


            return Content(element.ToString(), "text/xml"); // Returns translations
        }
    }
}
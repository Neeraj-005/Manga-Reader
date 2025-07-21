using MangaServerBackend.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MangaServerBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MangaController : ControllerBase
    {
        private readonly service mangaservice;
        public MangaController(service service)
        {
            mangaservice = service;
        }
        [HttpGet("library")] 
        public IActionResult GetMangaLibrary()
        {
            List<Manga> mangalist = mangaservice.GetMangas();

            if (mangalist.Count == 0)
            {
                return NotFound("No manga PDF files found or the specified directory does not exist/is inaccessible.");
            }

            return Ok(mangalist);
        }

        [HttpGet("search")]
        public IActionResult GetMangaLibraryWithName([FromQuery] string searchTerm)
        {
            List<Manga> mangalist = mangaservice.GetMangasByName(searchTerm);

            if (mangalist.Count == 0)
            {
                return NotFound("No manga PDF files found or the specified directory does not exist/is inaccessible.");
            }

            return Ok(mangalist);
        }

    }
}

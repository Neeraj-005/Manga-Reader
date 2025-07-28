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
        [HttpGet("series")]
        public IActionResult GetMangaSeries()
        {
            List<string> mangalist = mangaservice.GetMangasSeries();

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

        [HttpGet("series/{seriesName}/chapters")]
        public IActionResult GetMangaChapters(string seriesName)
        {
            List<Manga> mangalist = mangaservice.GetMangasChapters(seriesName);

            if (mangalist.Count == 0)
            {
                return NotFound("No manga PDF files found or the specified directory does not exist/is inaccessible.");
            }

            return Ok(mangalist);
        }

        [HttpGet("files/{id}")]
        public IActionResult GetMangaPDF(string id)
        {
            Manga manga = mangaservice.GetMangasByID(id);
            if (manga == null)
            {
                return NotFound($"Manga with ID '{id}' not found.");
            }

            string filePath = manga.Filepath;

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound($"File not found on server for ID '{id}'. Path: {filePath}");
            }

            try 
            {
                var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                Response.Headers.ContentDisposition = "inline; filename=\"" + manga.Name + ".pdf\"";

                return new FileStreamResult(fileStream, "application/pdf");
            }
            catch (UnauthorizedAccessException ex) 
            {
               
                Console.WriteLine($"Error: Access denied to '{filePath}'. Details: {ex.Message}");
                return Forbid($"Access denied to file '{id}'."); 
            }
            catch (IOException ex) 
            {

                Console.WriteLine($"Error reading file '{filePath}'. Details: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error reading file '{id}'.");
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"An unexpected error occurred while serving file '{filePath}'. Details: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"An unexpected error occurred while serving file '{id}'.");
            }
        }

    }
}

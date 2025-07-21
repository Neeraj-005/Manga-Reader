using Microsoft.Extensions.Configuration;
using System.IO;
using System;
using MangaServerBackend.Models.Domain;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace MangaServerBackend
{
    public class service
    {
        private readonly IConfiguration configuration;
        private readonly string mangafilepath;
        public service(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.mangafilepath = configuration["MangaLibraryPath"];
            if (string.IsNullOrEmpty(this.mangafilepath))
            {

                throw new ArgumentException("File path has not been configured in the appsettings.json");
            }
        }
        public List<Manga> GetMangas()
        {
            List<Manga> mangas = new List<Manga>();
            if (!Directory.Exists(this.mangafilepath))
            {
                return mangas;
            }
            try
            {
                string[] pdffiles = Directory.GetFiles(mangafilepath, "*.pdf", searchOption: SearchOption.AllDirectories);
                foreach (string file in pdffiles)
                {
                    Manga manga = new Manga();
                    manga.Id = Guid.NewGuid();
                    manga.Name = Path.GetFileNameWithoutExtension(file);
                    manga.Filepath = file;

                    mangas.Add(manga);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Error: Access denied to '{mangafilepath}'. Check file permissions. Details: {ex.Message}");
            }
            catch (PathTooLongException ex)
            {
                Console.WriteLine($"Error: One or more paths are too long. Details: {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error reading files from '{mangafilepath}'. Details: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred during manga scan: {ex.Message}");
            }
            return mangas;
        }

        public List<Manga> GetMangasByName(string str)
        {
            List<Manga> mangas = new List<Manga>();
            if (!Directory.Exists(this.mangafilepath))
            {
                return mangas;
            }
            try
            {
                string[] pdffiles = Directory.GetFiles(mangafilepath, "*.pdf", searchOption: SearchOption.AllDirectories);
                foreach (string file in pdffiles)
                {
                    Manga manga = new Manga();
                    manga.Id = Guid.NewGuid();
                    manga.Name = Path.GetFileNameWithoutExtension(file);
                    manga.Filepath = file;

                    if (manga.Name.Contains(str, StringComparison.OrdinalIgnoreCase))
                    {
                        mangas.Add(manga);
                    }
                    
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Error: Access denied to '{mangafilepath}'. Check file permissions. Details: {ex.Message}");
            }
            catch (PathTooLongException ex)
            {
                Console.WriteLine($"Error: One or more paths are too long. Details: {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error reading files from '{mangafilepath}'. Details: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred during manga scan: {ex.Message}");
            }
            return mangas;
        }



    }
}




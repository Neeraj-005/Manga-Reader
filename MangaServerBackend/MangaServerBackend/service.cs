using MangaServerBackend.Models.Domain;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.Extensions.Configuration;
using System;
using System.Drawing.Text;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
namespace MangaServerBackend
{
    public class service
    {
        private readonly IConfiguration configuration;
        private readonly string mangafilepath;
        private List<Manga> _mangaCache;
        public service(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.mangafilepath = configuration["MangaLibraryPath"];
            if (string.IsNullOrEmpty(this.mangafilepath))
            {

                throw new ArgumentException("File path has not been configured in the appsettings.json");
            }
            _mangaCache = ScanFilesAndBuildMangaList();
        }
        private List<Manga> ScanFilesAndBuildMangaList() 
        {
            List<Manga> scannedMangas = new List<Manga>();

             if (Directory.Exists(this.mangafilepath))
            {
                string[] pdffiles = Directory.GetFiles(mangafilepath, "*.pdf", SearchOption.AllDirectories);
                foreach (string file in pdffiles)
                {
                    Manga manga = new Manga();
                    manga.Id = StableGuidFromPath(file);
                    manga.Name = Path.GetFileNameWithoutExtension(file);
                    manga.Filepath = file;

                    string seriesTitle = NameExtracter(manga.Name).ToLower(); 

                    if (!string.IsNullOrWhiteSpace(seriesTitle))
                    {
                        
                        string coversRootPath = Path.Combine(this.mangafilepath, "Covers"); 

                        string cleanedSeriesTitle = seriesTitle.Replace(" ", ""); 
                        string[] potentialImageExtensions = { ".jpg", ".jpeg", ".png", ".webp" };
                        string foundCoverImagePath = null;

                        foreach (string ext in potentialImageExtensions)
                        {
                            
                            string candidatePath = Path.Combine(coversRootPath, cleanedSeriesTitle + ext);
                            if (System.IO.File.Exists(candidatePath))
                            {
                                foundCoverImagePath = candidatePath;
                                break; 
                            }
                        }

                        if (foundCoverImagePath != null)
                        { 

                            string relativePathFromMangaRoot = Path.GetRelativePath(this.mangafilepath, foundCoverImagePath); 
                            string urlSafePath = relativePathFromMangaRoot.Replace("\\", "/"); 

                            manga.CoverImage = $"https://localhost:7149/static-manga-files/{urlSafePath}"; 
                        }
                        else
                        {
                            manga.CoverImage = null;
                        }
                    }
                    else
                    {
                        manga.CoverImage = null;
                    }

                    scannedMangas.Add(manga);
                }
            }
            return scannedMangas;
        }
        public  Guid StableGuidFromPath(string filePath)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(filePath);

            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(bytes);

                return new Guid(hash);
            }
        }
        public string NameExtracter(string fileName)
        {
            //^(.+?)\s*\[CH\s*\d+\]   @"^(.+?)\s*CH\s*-\s*\d+"
            var bol = false;
            string nameFound = "";
            while (!bol)
            {
                var match = Regex.Match(fileName, @"^(.+?)\s*\[CH\s*(\d+)\]", RegexOptions.IgnoreCase);
                if (match.Success) { bol = true;nameFound = match.Groups[1].Value.Trim();  }
                var match1 = Regex.Match(fileName, @"^(.+?)\s*CH\s*-\s*\d+", RegexOptions.IgnoreCase);
                if (match1.Success) { bol = true; nameFound = match1.Groups[1].Value.Trim(); }
                else { bol = true; }
            }
            return nameFound;
        }
        public List<Manga> GetMangas()
        {
            List<Manga> mangas = _mangaCache;
            return mangas;
        }

        public List<string> GetMangasSeries()
        {
            List<Manga> directory = _mangaCache;
            List<string> mangas = new List<String>();

                
                foreach (Manga file in directory)
                {

                    string fileName = Path.GetFileNameWithoutExtension(file.Name);
                    var seriesName = NameExtracter(fileName);


                
                        if (!mangas.Contains(seriesName))
                        {
                            mangas.Add(seriesName);
                        }
                    
                }
            
           
            return mangas;
        }

        public List<Manga> GetMangasByName(string str)
        {
            List<Manga> mangas = new List<Manga>();
            
          
                
                foreach (Manga file in _mangaCache)
                {
                   

                    if (file.Name.Contains(str, StringComparison.OrdinalIgnoreCase))
                    {
                        mangas.Add(file);
                    }
                    
                }
            
            
            return mangas;
        }

        public Manga GetMangasByID(string str)
        {
            Manga mangas = new Manga();



            foreach (Manga file in _mangaCache)
            {


                if (file.Id.ToString() == str)
                {
                    mangas = file;
                }

            }


            return mangas;
        }

        public List<Manga> GetMangasChapters(string str)
         {
            List<Manga> mangas = new List<Manga>();
            if (!Directory.Exists(this.mangafilepath))
            {
                return mangas;
            }
          
                
                foreach (Manga file in _mangaCache)
                {
                    

                    if (file.Name.Contains(str, StringComparison.OrdinalIgnoreCase))
                    {
                        mangas.Add(file);
                    }

                }
            
            
            return mangas;
        }



    }
}




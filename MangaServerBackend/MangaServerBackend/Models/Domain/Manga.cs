namespace MangaServerBackend.Models.Domain
{
    public class Manga
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Filepath { get; set; }

        public string CoverImage { get; set; }

    }
}

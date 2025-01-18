namespace Biblia.Models
{
    public class BibliaApiModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string Description { get; set; }
        public Language Language { get; set; }
    }

    public class Language
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}

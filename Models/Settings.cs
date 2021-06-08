namespace Mp3TagReader.Models
{
    public class Settings
    {
        public string SongsOutputJson { get; set; }
        public string CoversOutputJson { get; set; }
        public string Mp3SourceFolder { get; set; }
        public string DefaultConnectionString { get; set; } 
        public string DbName { get; set; }
    }
}
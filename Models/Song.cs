using System.ComponentModel.DataAnnotations;

namespace Mp3TagReader.Models
{
    public class Song
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        [MaxLength(200)]
        public string Artist { get; set; }

        [Required]
        [MaxLength(100)]
        public string Album { get; set; }

        [Required]
        public uint Year { get; set; }

        [Required]
        [MaxLength(200)]
        public string Genre { get; set; }

        [MaxLength(2000)]
        public string Comment { get; set; }

        public Cover Cover { get; set; }
    }
}
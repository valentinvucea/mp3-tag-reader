using System.ComponentModel.DataAnnotations;

namespace Mp3TagReader.Models
{
    public class Cover
    {
        public int Id { get; set; }

        [Required]
        public byte[] Img { get; set; }

        [Required]
        public int SongId { get; set; }

        [Required]
        public Song Song { get; set; }
    }
}
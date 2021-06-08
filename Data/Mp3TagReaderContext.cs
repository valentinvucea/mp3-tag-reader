using Microsoft.EntityFrameworkCore;
using Mp3TagReader.Models;

namespace Mp3TagReader.Data
{
    public class Mp3TagReaderContext : DbContext
    {
        private string _connectionString;

        public Mp3TagReaderContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbSet<Song> Songs { get; set; }
        
        public DbSet<Cover> Covers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            optionsBuilder.UseSqlServer(@_connectionString);
        }        
    }
}
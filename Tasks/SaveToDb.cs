using System;
using System.Collections.Generic;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Mp3TagReader.Data;
using Mp3TagReader.Models;

namespace Mp3TagReader.Tasks
{
    public class SaveToDb
    {
        public SaveToDb(Settings config)
        {
            List<Song> songsList = new ReadFolderToList(config).GetList();
            
            using (var context = new Mp3TagReaderContext(config.DefaultConnectionString))
            {
                // Truncate first
                //TruncateTables(config);

                context.BulkInsert(songsList, options => {
                    options.IncludeGraph = true;
                    options.BatchSize = 100;
                });  
            }          

            Console.WriteLine("Finished");
        }

        private void TruncateTables(Settings config)
        {
            using (var context = new Mp3TagReaderContext(config.DefaultConnectionString))
            {
                context.Database.ExecuteSqlRaw("DELETE FROM Covers");
                context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('" + config.DbName + ".dbo.Covers',RESEED, 0)");

                context.Database.ExecuteSqlRaw("DELETE FROM Songs");
                context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('" + config.DbName + ".dbo.Songs',RESEED, 0)");
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using EFCore.BulkExtensions;
using Mp3TagReader.Components.Helpers;
using Mp3TagReader.Data;
using Mp3TagReader.Models;

namespace Mp3TagReader.Tasks
{
    public class PopulateCoverThumbs
    {
        public PopulateCoverThumbs(Settings config)
        {
            using (var context = new Mp3TagReaderContext(config.DefaultConnectionString))
            {
                List<Cover> covers = context.Covers.ToList();

                foreach (Cover item in covers)
                {
                    byte[] thumb = (new ImageResizer(item.Img)).Process();
                    item.Thumb = thumb;
                }

                context.BulkUpdate(covers, options => {
                    options.BatchSize = 100;
                });
            }

            Console.WriteLine("Finished");
        }
    }
}
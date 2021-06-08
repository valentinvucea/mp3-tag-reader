using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Mp3TagReader.Models;
using TagLib;

namespace Mp3TagReader.Tasks
{
    public class ReadFolderToList
    {
        private Settings _config;

        public ReadFolderToList(Settings config)
        {
            _config = config;
        }

        public List<Song> GetList()
        {
            var sourceFolder = @_config.Mp3SourceFolder;
            var songsList = new List<Song>{};
            int i = 1;

            if (Directory.Exists(sourceFolder))
            {
                // Process the list of files found in the directory.
                string[] fileEntries = Directory.GetFiles(sourceFolder);

                foreach (string fileName in fileEntries)
                {
                    string uri = fileName;
                    Console.WriteLine(fileName);
                    TagLib.File file;

                    // Check if the file is readable
                    try
                    {
                        var file_info = new FileInfo(uri);
                        uri = new Uri(file_info.FullName).ToString();
                    }
                    catch
                    {
                        Console.WriteLine("There is an error reading this file");
                    }

                    // Check if the format is supported
                    try
                    {
                        file = TagLib.File.Create(new FileAbstraction(fileName));
                    }
                    catch (UnsupportedFormatException)
                    {
                        Console.WriteLine($"UNSUPPORTED FILE: {uri}");
                        Console.WriteLine();
                        Console.WriteLine("---------------------------------------");
                        Console.WriteLine();
                        continue;
                    }

                    string[] artists = file.Tag.Performers;
                    string[] genres = file.Tag.Genres;
                    var bin = (byte[])(file.Tag.Pictures[0].Data.Data);

                    songsList.Add(
                        new Song
                        {
                            Id = i,
                            Title = file.Tag.Title,
                            Album = file.Tag.Album,
                            Year = file.Tag.Year,
                            Artist = artists[0],
                            Genre = genres[0],
                            Cover = new Cover
                            {
                                Id = i,
                                SongId = i,
                                Img = bin
                            }
                        }
                    );

                    Console.WriteLine("---------------------------------------");
                    Console.WriteLine();
                    i++;
                }

                Console.WriteLine("Finished");
            }
            else
            {
                Console.WriteLine("{0} is not a valid file or directory.", sourceFolder);
            }

            return songsList;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text.Json;
using Mp3TagReader.Models;

namespace Mp3TagReader.Tasks
{
    public class SaveToJson
    {
        public SaveToJson(Settings config)
        {
            List<Song> songsList = new ReadFolderToList(config).GetList();
            string jsonString = JsonSerializer.Serialize(songsList);
            
            System.IO.File.WriteAllText(config.SongsOutputJson, jsonString);
            Console.WriteLine("JSON file created");
        }
    }
}
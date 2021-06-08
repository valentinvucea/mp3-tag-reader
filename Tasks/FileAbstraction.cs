using System;
using System.IO;

namespace Mp3TagReader.Tasks
{
    public class FileAbstraction : TagLib.File.IFileAbstraction
    {
        public FileAbstraction(string file)
        {
            Name = file;
            Console.WriteLine(Name);
        }

        public string Name { get; }

        public Stream ReadStream => new FileStream(Name, FileMode.Open);

        public Stream WriteStream => new FileStream(Name, FileMode.Open);

        public void CloseStream(Stream stream)
        {
            stream.Close();
        }
    }
}
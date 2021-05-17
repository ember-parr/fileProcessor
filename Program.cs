using System;
using System.IO;

namespace fileProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            // we need to iterate through files in uploaded folder
            foreach (var subfolder in Directory.GetDirectories("./data")) 
            {
            // get metadata file
                var metadataFilePath = Path.Combine(subfolder, "metadata.json");
                Console.WriteLine($"Reading {metadataFilePath}");
            // extract info from metadata file, including audio file info

            // for each audio file: 
            // -- get absolute path
            // -- verify the checksum
            // -- generate unique identifier 
            // -- compress it
            // -- create a standalone metadata file
            }
        }
    }
}

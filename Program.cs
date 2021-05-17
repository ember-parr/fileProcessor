using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;


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
                var metadataCollection = GetMetadata(metadataFilePath);
                
            // for each audio file: 
            // -- get absolute path
            // -- verify the checksum
            // -- generate unique identifier 
            // -- compress it
            // -- create a standalone metadata file
                foreach(var metadata in metadataCollection)
                {
                    var audioFilePath = Path.Combine(subfolder, metadata.File.FileName);
                    var md5Checksum = GetChecksum(audioFilePath);
                }
            }
        }

        static List<Metadata> GetMetadata(string metadataFilePath) 
        {
            var metadataFileStream = File.Open(metadataFilePath, FileMode.Open);
            var settings = new DataContractJsonSerializerSettings
            {
                DateTimeFormat = new DateTimeFormat("yyyy-MM-dd'T'HH:mm:ssZ")
            };
            var serializer = new DataContractJsonSerializer(typeof(List<Metadata>), settings);
            return (List<Metadata>)serializer.ReadObject(metadataFileStream);
        }

        static string GetChecksum(string filePath)
        {
            var fileStream = File.Open(filePath, FileMode.Open);
            var md5 = System.Security.Cryptography.MD5.Create();
            var md5Bytes = md5.ComputeHash(fileStream);
            return BitConverter.ToString(md5Bytes);
        }
    }
}

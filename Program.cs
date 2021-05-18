using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;


namespace fileProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            // iterate through files in uploaded folder
            foreach (var subfolder in Directory.GetDirectories("./data")) 
            {
                // get metadata file
                var metadataFilePath = Path.Combine(subfolder, "metadata.json");
                Console.WriteLine($"Reading {metadataFilePath}");

                // extract info from metadata file, including audio file info
                var metadataCollection = GetMetadata(metadataFilePath);
                
                // for each audio file in the folder... 
                foreach(var metadata in metadataCollection)
                {
                    // get the file's absolute path
                    var audioFilePath = Path.Combine(subfolder, metadata.File.FileName);
                    Console.WriteLine($"audioFilePath: {audioFilePath}");
                    
                    // verify the checksum of the file
                    var md5Checksum = GetChecksum(audioFilePath);
                    if(md5Checksum.Replace("-", "").ToLower() != metadata.File.Md5Checksum)
                    {
                        throw new Exception("Checksum does not match! CODE RED... something's wrong");
                    }

                    // generates unique file name 
                    var uniqueFileName = Guid.NewGuid();
                    metadata.File.FileName = uniqueFileName + ".WAV"; 

                    // generates new path for file -- directing to folder 'ready_to_send' 
                    var newPath = Path.Combine("./ready_to_send", uniqueFileName + ".WAV");

                    // compress the audio file
                    CreateCompressedFile(audioFilePath, newPath);
                    // create a standalone metadata file to accompany the audio file. 
                }
            }
        }

        static void CreateCompressedFile(string inputFilePath, string outputFilePath)
        {
            outputFilePath += ".gz";
            Console.WriteLine($"Creating: {outputFilePath}");

            var inputFileStream = File.Open(inputFilePath, FileMode.Open);
            var outputFileStream = File.Create(outputFilePath);
            var gzipFileStream = new GZipStream(outputFileStream, CompressionLevel.Optimal);

            inputFileStream.CopyTo(gzipFileStream);

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
            fileStream.Dispose();
            return BitConverter.ToString(md5Bytes);
        }
    }
}

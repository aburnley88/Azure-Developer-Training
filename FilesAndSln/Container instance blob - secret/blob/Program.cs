using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace demoblob
{
    class Program
    {
        static string storageconnstring = "";
        static string containerName = "demo";
        static string filename = "sample.html";
        
        
        static async Task Main(string[] args)
        {
            CreateBlob().Wait();
            Console.WriteLine("Complete");
            
        }

        
        static async Task CreateBlob()
        {

            string mountpath = "/mounts/secrets";
            
            // Will get the directory - volumesecret
            var folders = Directory.GetDirectories(mountpath);
            foreach(var folder in folders)
            {
                Console.WriteLine($"Folder : {folder.ToString()}");
                var AllFiles = Directory.GetFiles(folder);
                foreach(var file in AllFiles)
                {
                    storageconnstring = File.ReadAllText(file);
                    Console.WriteLine(storageconnstring);
                }                   
                }
            
        
            BlobServiceClient blobServiceClient = new BlobServiceClient(storageconnstring);
            
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            
            BlobClient blobClient = containerClient.GetBlobClient(filename);

            String str = "This is a sample file";
            using (var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(str)))
            {
                await blobClient.UploadAsync(memoryStream, overwrite: true);
            }


        }        
    }
}

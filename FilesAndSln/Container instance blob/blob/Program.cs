//this program connects to a storage account and container and uploads a string as an html file into the blob
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace whizlabblob
{
    class Program
    {
        static string storageconnstring = "DefaultEndpointsProtocol=https;AccountName=storburnley;AccountKey=ugcLkAh8IS/s6D5JKPE4cQI1nkatzf7qkXi1LA03t9ZH2AuCXf7YRnwktqdBw5Lb5hYOI2NheZJqaCnYZtdKPw==;EndpointSuffix=core.windows.net";
        static string containerName = "demo";
        static string filename = "sample.html";
        
        
        static async Task Main(string[] args)
        {
            CreateBlob().Wait();
            Console.WriteLine("Complete");
            
        }

        
        static async Task CreateBlob()
        {
            //create a new blob handler
            BlobServiceClient blobServiceClient = new BlobServiceClient(storageconnstring);
            
            //use the handler to access the container
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

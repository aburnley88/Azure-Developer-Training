using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace blobprogram
{
    class Program
    {
        static string storageconnstring = "DefaultEndpointsProtocol=https;AccountName=stagingstore40000;AccountKey=oDLfJiinjQWuXSros/lne23zi1sJA53lf3iVCYjfbdCrCL6tdxIVLAzu1OquRTTbSzlzcBxdc/agglqoZ8zydg==;EndpointSuffix=core.windows.net";
        static string containerName = "data";
        static string filename = "sample.txt";
        static string filepath="C:\\Users\\aburn\\OneDrive\\Desktop\\sample.txt";
        static string downloadpath = "C:\\Work\\sample.txt";
        static async Task Main(string[] args)
        {
            //Container().Wait();
            //CreateBlob().Wait();
            GetBlobs().Wait();
            //GetBlob().Wait();
            Console.WriteLine("Complete");
            Console.ReadKey();
        }

        static async Task Container()
        {
         
            BlobServiceClient blobServiceClient = new BlobServiceClient(storageconnstring);
         
            BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);
        }

        static async Task CreateBlob()
        {
            
            BlobServiceClient blobServiceClient = new BlobServiceClient(storageconnstring);
            
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            
            BlobClient blobClient = containerClient.GetBlobClient(filename);

            
            using FileStream uploadFileStream = File.OpenRead(filepath);
            
            await blobClient.UploadAsync(uploadFileStream, true);
            uploadFileStream.Close();
        }


        static async Task GetBlobs()
        {
            
            BlobServiceClient blobServiceClient = new BlobServiceClient(storageconnstring);
            
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            
            
            await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
            {
                Console.WriteLine("\t" + blobItem.Name);
            }

        }

        static async Task GetBlob()
        {
            
            BlobServiceClient blobServiceClient = new BlobServiceClient(storageconnstring);
            
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            
            BlobClient blob = containerClient.GetBlobClient(filename);
            
            BlobDownloadInfo blobdata = await blob.DownloadAsync();

            
            using (FileStream downloadFileStream = File.OpenWrite(downloadpath))
            {
                await blobdata.Content.CopyToAsync(downloadFileStream);
                downloadFileStream.Close();
            }


            // Read the new file
            using (FileStream downloadFileStream = File.OpenRead(downloadpath))
            {
                using var strreader = new StreamReader(downloadFileStream);
                string line;
                while ((line = strreader.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }

        }
    }
}

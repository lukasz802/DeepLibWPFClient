using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace AzureStorage
{
    static class BlobFunctions
    {
        private static string storageConnectionString = "";

        internal static async Task<string> CreateBlobItem(string blobPath)
        {
            storageConnectionString = BaseConfiguration.Configuration["appsettings:storageConnectionString"];
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            string tempBlobPath = blobPath.ToLower();

            if (File.Exists(tempBlobPath))
            {
                ContainerResultSegment containers = await blobClient.ListContainersSegmentedAsync(null);
                CloudBlobContainer selectedContainer = containers.Results.Where(x => x.Name == "deeplibcontainer").FirstOrDefault();

                if (selectedContainer != null)
                {
                    CloudBlockBlob blob = selectedContainer.GetBlockBlobReference((DateTime.Now.Day.ToString("X") +
                                DateTime.Now.Month.ToString("X") + DateTime.Now.Year.ToString("X") +
                                DateTime.Now.Hour.ToString("X") + DateTime.Now.Minute.ToString("X") +
                                DateTime.Now.Second.ToString("X") + Path.GetExtension(blobPath)).ToLower());
                    await blob.UploadFromFileAsync(blobPath);
                    return blob.Uri.ToString();
                }
            }
            return null;
        }

        internal static async Task DeleteBlobItem(string blobUri)
        {
            storageConnectionString = BaseConfiguration.Configuration["appsettings:storageConnectionString"];
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            ContainerResultSegment containers = await blobClient.ListContainersSegmentedAsync(null);
            CloudBlobContainer selectedContainer = containers.Results.Where(x => x.Name == "deeplibcontainer").FirstOrDefault();
            BlobResultSegment blobs = await selectedContainer.ListBlobsSegmentedAsync(null);
            CloudBlockBlob blob = (from CloudBlockBlob cloudBlob in blobs.Results
                                   where cloudBlob.Uri.ToString() == blobUri
                                   select cloudBlob).FirstOrDefault();
            if (blob != null) { await blob.DeleteIfExistsAsync(); }
            else { return; }
        }
    }
}

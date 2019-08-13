using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureStorage
{
    public static class CoverRepository
    {
        public static async Task<string> Update(string blobPath, string blobUri)
        {
            await ContainerFunctions.CreateContainerItem();
            string result = await BlobFunctions.CreateBlobItem(blobPath);
            await BlobFunctions.DeleteBlobItem(blobUri);
            return result;
        }

        public static async Task<string> Upload(string blobPath)
        {
            await ContainerFunctions.CreateContainerItem();
            string result = await BlobFunctions.CreateBlobItem(blobPath);
            return result;
        }
    }
}

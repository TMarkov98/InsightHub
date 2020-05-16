using Azure.Storage.Blobs;
using InsightHub.Services.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using InsightHub.Models;
using Azure.Storage.Blobs.Models;
using System.Runtime.CompilerServices;

namespace InsightHub.Services
{
    public class BlobServices : IBlobServices
    {
        // https://www.youtube.com/watch?v=9ZpMpf9dNDA

        private readonly BlobServiceClient _blobServiceClient;

        public BlobServices(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task<BlobFile> GetBlobAsync(string name)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("reports");
            var blobClient = containerClient.GetBlobClient(name);
            var blobDownloadInfo = await blobClient.DownloadAsync();
            return new BlobFile(blobDownloadInfo.Value.Content, blobDownloadInfo.Value.ContentType);
        }
        public async Task UploadFileBlobAsync(string filePath, string fileName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("reports");
            var blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(filePath, new BlobHttpHeaders { ContentType = "application/pdf" });
        }

        public async Task DeleteBlobAsync(string blobName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("reports");
            var blobClient = containerClient.GetBlobClient(blobName);
            await blobClient.DeleteIfExistsAsync();
        }

    }
}

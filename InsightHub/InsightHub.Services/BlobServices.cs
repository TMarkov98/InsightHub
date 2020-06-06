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
using System.IO;

namespace InsightHub.Services
{
    public class BlobServices : IBlobServices
    {

        private readonly BlobServiceClient _blobServiceClient;

        public BlobServices(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        /// <summary>
        /// Gets a target Blob from Azure
        /// </summary>
        /// <param name="name">The name of the Blob</param>
        /// <returns>BlobFile</returns>
        public async Task<BlobFile> GetBlobAsync(string name)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("reports");
            var blobClient = containerClient.GetBlobClient(name);
            var blobDownloadInfo = await blobClient.DownloadAsync();
            return new BlobFile(blobDownloadInfo.Value.Content, blobDownloadInfo.Value.ContentType);
        }

        /// <summary>
        /// Uploads a new file to Blob
        /// </summary>
        /// <param name="file">The data of the given file.</param>
        /// <param name="fileName">The name of the new Blob file.</param>
        public async Task UploadFileBlobAsync(Stream file, string fileName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("reports");
            var blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(file, overwrite: true);
        }

        /// <summary>
        /// Deletes a target Blob from Azure.
        /// </summary>
        /// <param name="blobName">The name of the target Blob.</param>
        public async Task DeleteBlobAsync(string blobName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("reports");
            var blobClient = containerClient.GetBlobClient(blobName);
            await blobClient.DeleteIfExistsAsync();
        }

    }
}

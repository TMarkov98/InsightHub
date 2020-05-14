using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InsightHub.Services.Contracts
{
    public interface IBlobService
    {
        Task<BlobInfo> GetBlobAsync(string name);
        Task<IEnumerable<string>> ListBlobsAsync();
        Task UploadFileBlobAsync(string filePath, string fileName);
        Task UploadContentBlobAsync(string content, string fileName);
        Task DeleteBlobAsync(string blobName);
    }
}

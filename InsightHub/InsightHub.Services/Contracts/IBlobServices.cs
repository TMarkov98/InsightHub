using InsightHub.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InsightHub.Services.Contracts
{
    public interface IBlobServices
    {
        Task<BlobFile> GetBlobAsync(string name);
        Task UploadFileBlobAsync(string filePath, string fileName);
        Task DeleteBlobAsync(string blobName);
    }
}

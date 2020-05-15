using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InsightHub.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InsightHub.Models;

namespace InsightHub.Web.Controllers.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlobAPIController : ControllerBase
    {
        private readonly IBlobServices _blobServices;

        public BlobAPIController(IBlobServices blobServices)
        {
            this._blobServices = blobServices;
        }

        // GET: api/BlobAPI/5
        [HttpGet("{blobName}")]
        public async Task<IActionResult> Get(string blobName)
        {
            var data = await _blobServices.GetBlobAsync(blobName);
            return File(data.Content, data.ContentType);
        }

        // POST: api/BlobAPI
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UploadFileRequest request)
        {
            await _blobServices.UploadFileBlobAsync(request.FilePath, request.FileName);
            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{blobName}")]
        public async Task<IActionResult> Delete(string blobName)
        {
            await _blobServices.DeleteBlobAsync(blobName);
            return Ok();
        }
    }
}

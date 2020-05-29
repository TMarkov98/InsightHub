using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace InsightHub.Models
{
    public class BlobFile
    {
        public BlobFile(Stream content, string contentType)
        {
            this.Content = content;
            this.ContentType = contentType;
        }
        public Stream Content { get; set; }
        public string ContentType { get; set; }
    }
}

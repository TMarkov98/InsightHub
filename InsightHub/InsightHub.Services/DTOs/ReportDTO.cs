using InsightHub.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace InsightHub.Services.DTOs
{
    public class ReportDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public List<string> Tags { get; set; }
        public string Industry { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime DeletedOn { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsFeatured { get; set; }
    }
}

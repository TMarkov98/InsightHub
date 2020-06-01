using InsightHub.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InsightHub.Models
{
    public class ReportModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Title { get; set; }
        [Required]
        [StringLength(300, MinimumLength = 5)]
        public string Summary { get; set; }
        [Required]
        [StringLength(5000, MinimumLength = 5)]
        public string Description { get; set; }
        public string Author { get; set; }
        public string Tags { get; set; }
        [Required]
        public string Industry { get; set; }
        public int DownloadsCount { get; set; }
        public string ImgUrl { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime DeletedOn { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsPending { get; set; }
    }
}

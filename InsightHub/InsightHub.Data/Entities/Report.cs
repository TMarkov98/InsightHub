﻿using InsightHub.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace InsightHub.Data.Entities
{
    public class Report : IApprovable, IAudible, IDeletable, IFeaturable
    {
        public Report()
        {
            this.ReportTags = new List<ReportTag>();
            this.Downloads = new List<DownloadedReport>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public User Author { get; set; }
        public int AuthorId { get; set; }
        public List<ReportTag> ReportTags { get; set; }
        public List<DownloadedReport> Downloads { get; set; }
        public Industry Industry { get; set; }
        public int IndustryId { get; set; }
        public string ImgUrl { get; set; }
        public bool IsPending { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedOn { get; set; }
        public bool IsFeatured { get; set; }
    }
}

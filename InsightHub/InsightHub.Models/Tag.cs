using InsightHub.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace InsightHub.Models
{
    public class Tag : IDeletable, IAudible
    {
        public Tag()
        {
            this.Reports = new List<ReportTag>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ReportTag> Reports { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}

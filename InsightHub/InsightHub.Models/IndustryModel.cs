using System;
using System.Collections.Generic;
using System.Text;

namespace InsightHub.Models
{
    public class IndustryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public List<ReportModel> Reports { get; set; }
        public string ImgUrl { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedOn { get; set; }
    }
}

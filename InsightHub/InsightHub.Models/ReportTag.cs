using System;
using System.Collections.Generic;
using System.Text;

namespace InsightHub.Models
{
    public class ReportTag
    {
        public int TagId { get; set; }
        public int ReportId { get; set; }
        public Tag Tag { get; set; }
        public Report Report { get; set; }
    }
}

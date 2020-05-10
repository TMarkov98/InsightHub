using System;
using System.Collections.Generic;
using System.Text;

namespace InsightHub.Models
{
    public class IndustryReport
    {
        public int IndustryId { get; set; }
        public int ReportId { get; set; }
        public Report Report { get; set; }
        public Industry Industry { get; set; }
    }
}

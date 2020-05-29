using System;
using System.Collections.Generic;
using System.Text;

namespace InsightHub.Data.Entities
{
    public class DownloadedReport
    {
        public int UserId { get; set; }
        public int ReportId { get; set; }
        public Report Report { get; set; }
        public User User { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace InsightHub.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int DownloadedReportsCount { get; set; }
        public List<ReportModel> DownloadedReports { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int IndustrySubscriptionsCount { get; set; }
        public bool IsPending { get; set; }
        public bool IsBanned { get; set; }
        public string BanReason { get; set; }
    }
}

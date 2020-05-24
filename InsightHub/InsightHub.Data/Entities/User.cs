﻿using InsightHub.Models.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace InsightHub.Data.Entities
{
    public class User : IdentityUser<int>, IApprovable, IAudible
    {
        public User()
        {
            this.Reports = new List<DownloadedReport>();
            this.IndustrySubscriptions = new List<IndustrySubscription>();
            this.IsPending = true;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Role Role { get; set; }
        public List<DownloadedReport> Reports { get; set; }
        public bool IsPending { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public List<IndustrySubscription> IndustrySubscriptions { get; set; }
        public bool IsBanned { get; set; }
        public string BanReason { get; set; }
    }
}

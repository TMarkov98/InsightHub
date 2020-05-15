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
        public string Role { get; set; }
        public int ReportsCount { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int IndustrySubscriptionsCount { get; set; }
        public int TagSubscriptionsCount { get; set; }
    }
}

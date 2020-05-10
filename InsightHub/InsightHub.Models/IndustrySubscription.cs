using System;
using System.Collections.Generic;
using System.Text;

namespace InsightHub.Models
{
    public class IndustrySubscription
    {
        public int IndustryId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public Industry Industry { get; set; }
    }
}

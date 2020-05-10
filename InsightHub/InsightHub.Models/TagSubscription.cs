using System;
using System.Collections.Generic;
using System.Text;

namespace InsightHub.Models
{
    public class TagSubscription
    {
        public int TagId { get; set; }
        public int UserId { get; set; }
        public Tag Tag { get; set; }
        public User User { get; set; }
    }
}

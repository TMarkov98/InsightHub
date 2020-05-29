using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsightHub.Web.Models
{
    public class SendEmailModel
    {
        public string SentFrom { get; set; }
        public string SendTo { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}

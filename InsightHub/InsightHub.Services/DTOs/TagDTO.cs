using System;
using System.Collections.Generic;
using System.Text;

namespace InsightHub.Services.DTOs
{
    public class TagDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public List<string> Reports { get; set; }
        public bool IsDeleted { get; set; }
    }
}

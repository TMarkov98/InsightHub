using System;
using System.Collections.Generic;
using System.Text;

namespace InsightHub.Services.DTOs
{
    public class IndustryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public List<ReportDTO> Reports { get; set; }
    }
}

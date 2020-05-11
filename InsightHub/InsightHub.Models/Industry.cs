﻿using InsightHub.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace InsightHub.Models
{
    public class Industry : IDeletable, IAudible
    {

        public Industry()
        {
            this.Reports = new List<Report>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public List<Report> Reports { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}

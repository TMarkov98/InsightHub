using InsightHub.Models.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InsightHub.Data.Entities
{
    public class Industry : IDeletable, IAudible
    {

        public Industry()
        {
            this.Reports = new List<Report>();
            this.SubscribedUsers = new List<IndustrySubscription>();
            if(this.ImgUrl == null || this.ImgUrl == string.Empty)
            {
                this.ImgUrl = "https://i.imgur.com/AoW6Iqh.png";
            }
        }

        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Name { get; set; }
        public List<Report> Reports { get; set; }
        public string ImgUrl { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public List<IndustrySubscription> SubscribedUsers { get; set; }
    }
}

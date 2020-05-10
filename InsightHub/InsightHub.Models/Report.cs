using InsightHub.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace InsightHub.Models
{
    public class Report : IApprovable, IAudible, IDeletable, IFeaturable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public User Author { get; set; }
        public int AuthorId { get; set; }
        public List<Tag> Tags { get; set; }
        public Industry Industry { get; set; }
        public int IndustryId { get; set; }
        public string FileUrl { get; set; }
        public bool IsPending { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedOn { get; set; }
        public bool IsFeatured { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace InsightHub.Models.Contracts
{
    /// <summary>
    /// Adds IsDeleted and DeletedOn properties to help with soft-deleting and restoring items.
    /// </summary>
    public interface IDeletable
    {
        bool IsDeleted { get; set; }
        DateTime DeletedOn { get; set; }
    }
}

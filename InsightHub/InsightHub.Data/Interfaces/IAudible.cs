using System;
using System.Collections.Generic;
using System.Text;

namespace InsightHub.Models.Contracts
{
    /// <summary>
    /// Adds CreatedOn and ModifiedOn properties to help monitor and audit actions over items.
    /// </summary>
    public interface IAudible
    {
        DateTime CreatedOn { get; set; }
        DateTime ModifiedOn { get; set; }
    }
}

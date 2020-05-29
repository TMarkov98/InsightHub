using System;
using System.Collections.Generic;
using System.Text;

namespace InsightHub.Models.Contracts
{
    public interface IAudible
    {
        DateTime CreatedOn { get; set; }
        DateTime ModifiedOn { get; set; }
    }
}

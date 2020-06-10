using System;
using System.Collections.Generic;
using System.Text;

namespace InsightHub.Models.Contracts
{
    /// <summary>
    /// Adds IsFeatured property to help Admins display important items to Users in a quick fashion.
    /// </summary>
    public interface IFeaturable
    {
        bool IsFeatured { get; set; }
    }
}

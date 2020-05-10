using System;
using System.Collections.Generic;
using System.Text;

namespace InsightHub.Models.Contracts
{
    public interface IFeaturable
    {
        bool IsFeatured { get; set; }
    }
}

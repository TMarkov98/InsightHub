using System;
using System.Collections.Generic;
using System.Text;

namespace InsightHub.Models.Contracts
{
    public interface IApprovable
    {
        bool IsPending { get; set; }
    }
}

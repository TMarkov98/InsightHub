using System;
using System.Collections.Generic;
using System.Text;

namespace InsightHub.Models.Contracts
{
    /// <summary>
    /// Adds IsPending property, to allow Admins to Review and Approve items before they are displayed to Users.
    /// </summary>
    public interface IApprovable
    {
        bool IsPending { get; set; }
    }
}

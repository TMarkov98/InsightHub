using InsightHub.Data.Entities;
using InsightHub.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace InsightHub.Services.Mappers
{
    public class UserMapper
    {
        public static UserModel MapModelFromEntity(User user)
        {
            return new UserModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                CreatedOn = user.CreatedOn,
                ModifiedOn = user.ModifiedOn,
                DownloadedReportsCount = user.Reports.Count,
                IndustrySubscriptionsCount = user.IndustrySubscriptions.Count,
                TagSubscriptionsCount = user.TagSubscriptions.Count,
                IsPending = user.IsPending,
                IsBanned = user.IsBanned,
                BanReason = user.BanReason
            };
        }
    }
}

using InsightHub.Data.Entities;
using InsightHub.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace InsightHub.Services.Mappers
{
    public class UserMapper
    {
        /// <summary>
        /// Maps a User Model from an existing Entity
        /// </summary>
        /// <param name="user">The target User Entity</param>
        /// <returns>User Model</returns>
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
                IsPending = user.IsPending,
                IsBanned = user.IsBanned,
                BanReason = user.BanReason
            };
        }
    }
}

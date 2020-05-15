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
                Role = user.Role.Name,
                Email = user.Email,
                CreatedOn = user.CreatedOn,
                ModifiedOn = user.ModifiedOn,
                ReportsCount = user.Reports.Count,
                IndustrySubscriptionsCount = user.IndustrySubscriptions.Count,
                TagSubscriptionsCount = user.TagSubscriptions.Count
            };
        }
    }
}

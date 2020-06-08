using InsightHub.Data;
using InsightHub.Data.Entities;
using InsightHub.Models;
using InsightHub.Services.Contracts;
using InsightHub.Services.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace InsightHub.Services
{
    public class UserServices : IUserServices
    {
        private readonly InsightHubContext _context;

        public UserServices(InsightHubContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets a User from the context by its ID.
        /// </summary>
        /// <param name="id">The ID of the target User.</param>
        /// <returns>User Model on success. Throws ArgumentNullException if User does not exist.</returns>
        public async Task<UserModel> GetUser(int id)
        {
            var user = await _context.Users
                .Include(u => u.DownloadedReports)
                .Include(u => u.Role)
                .Include(u => u.IndustrySubscriptions)
                .Include(u => u.DownloadedReports)
                .FirstOrDefaultAsync(u => u.Id == id);

            ValidateUserExists(user);

            var userDTO = UserMapper.MapModelFromEntity(user);
            return userDTO;
        }

        /// <summary>
        /// Lists all Active users.
        /// </summary>
        /// <param name="search">Searches the collection by First Name, Last Name, Email.</param>
        /// <returns>ICollection of User Models.</returns>
        public async Task<ICollection<UserModel>> GetUsers(string search)
        {
            var users = await _context.Users
                .Where(u => !u.IsBanned && !u.IsPending)
                .Include(u => u.DownloadedReports)
                .Include(u => u.Role)
                .Include(u => u.IndustrySubscriptions)
                .Select(u => UserMapper.MapModelFromEntity(u))
                .ToListAsync();

            users = SearchUsers(search, users).ToList();

            return users;
        }

        /// <summary>
        /// Lists all Banned users.
        /// </summary>
        /// <param name="search">Searches the collection by First Name, Last Name, Email.</param>
        /// <returns>ICollection of User Models.</returns>
        public async Task<ICollection<UserModel>> GetBannedUsers(string search)
        {
            var users = await _context.Users
                .Where(u => u.IsBanned)
                .Include(u => u.Role)
                .Include(u => u.DownloadedReports)
                .Include(u => u.IndustrySubscriptions)
                .Select(u => UserMapper.MapModelFromEntity(u))
                .ToListAsync();

            users = SearchUsers(search, users).ToList();

            return users;
        }

        /// <summary>
        /// Lists all Pending users.
        /// </summary>
        /// <param name="search">Searches the collection by First Name, Last Name, Email.</param>
        /// <returns>ICollection of User Models.</returns>
        public async Task<ICollection<UserModel>> GetPendingUsers(string search)
        {
            var users = await _context.Users
                .Where(u => u.IsPending)
                .Include(u => u.Role)
                .Include(u => u.DownloadedReports)
                .Include(u => u.IndustrySubscriptions)
                .OrderBy(u => u.CreatedOn)
                .Select(u => UserMapper.MapModelFromEntity(u))
                .ToListAsync();

            users = SearchUsers(search, users).ToList();

            return users;
        }

        /// <summary>
        /// Lists all Users subscribed to an Industry.
        /// </summary>
        /// <param name="industry">The name of the target Industry.</param>
        /// <returns>String with Joined users' Email addresses.</returns>
        public async Task<string> GetSubscribedUsers(string industry)
        {
            var users = await _context.IndustrySubscriptions
                .Where(u => u.Industry.Name == industry)
                .Include(u => u.User)
                .Include(u => u.Industry)
                .ToListAsync();
            List<string> sendTo = new List<string>();
            foreach (var user in users)
            {
                sendTo.Add(user.User.Email);
            }
            return string.Join(',', sendTo);
        }

        /// <summary>
        /// Updates the properties of an existing User.
        /// </summary>
        /// <param name="id">The ID of the target User.</param>
        /// <param name="firstName">The new First Name for the User.</param>
        /// <param name="lastName">The new Last Name for the User.</param>
        /// <param name="isBanned">The new IsBanned property.</param>
        /// <param name="banReason">The new BanReason property.</param>
        /// <returns>User Model on success. Throws ArgumentNullException if User does not exist.</returns>
        public async Task<UserModel> UpdateUser(int id, string firstName, string lastName, bool isBanned, string banReason)
        {
            var user = await _context.Users
                .Include(u => u.DownloadedReports)
                .Include(u => u.IndustrySubscriptions)
                .FirstOrDefaultAsync(u => u.Id == id && !u.IsBanned);

            ValidateUserExists(user);

            user.FirstName = firstName;
            user.LastName = lastName;
            user.IsBanned = isBanned;
            user.BanReason = banReason;

            var userDTO = UserMapper.MapModelFromEntity(user);
            user.ModifiedOn = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return userDTO;
        }

        /// <summary>
        /// Sets the IsBanned property of a User to True.
        /// </summary>
        /// <param name="id">The ID of the target user.</param>
        /// <param name="reason">The reason for banning the user.</param>
        /// <returns>Throws ArgumentNullException if User does not exist. Throws ArgumentException if the User is already banned.</returns>
        public async Task BanUser(int id, string reason)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            ValidateUserExists(user);

            if(user.IsBanned)
            {
                throw new ArgumentException("User already banned.");
            }

            user.IsBanned = true;
            user.BanReason = reason;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Approves a Pending User.
        /// </summary>
        /// <param name="id">The ID of the target User.</param>
        /// <returns>Throws ArgumentNullException if User does not exist.</returns>
        public async Task ApproveUser(int id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            ValidateUserExists(user);

            user.IsPending = false;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Sets the IsBanned property of a User to False.
        /// </summary>
        /// <param name="id">The ID of the target User.</param>
        /// <returns>Throws ArgumentNullException if User does not exist. Throws ArgumentException if the User is already banned.</returns>
        public async Task UnbanUser(int id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            ValidateUserExists(user);

            if(!user.IsBanned)
            {
                throw new ArgumentException("Unable to unban user. User not banned.");
            }
            user.IsBanned = false;
            user.BanReason = string.Empty;
            _context.SaveChanges();
        }

        /// <summary>
        /// Permanently deletes a user from the context.
        /// </summary>
        /// <param name="id">The ID of the target User.</param>
        /// <returns>Throws ArgumentNullException if User does not exist.</returns>
        public async Task DeleteUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            ValidateUserExists(user);

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Gets all the Downloaded Reports for a User.
        /// </summary>
        /// <param name="userId">The ID of the target User.</param>
        /// <param name="search">Searches the Reports by Title and Summary.</param>
        /// <returns>ICollection of Report Models.</returns>
        public async Task<ICollection<ReportModel>> GetDownloadedReports(int userId, string search)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            ValidateUserExists(user);

            var reports = await _context.DownloadedReports
                .Include(ur => ur.Report)
                .ThenInclude(r => r.Author)
                .Include(ur => ur.Report)
                .ThenInclude(r => r.Industry)
                .Include(ur => ur.Report)
                .ThenInclude(r => r.ReportTags)
                .ThenInclude(r => r.Tag)
                .Include(ur => ur.Report)
                .ThenInclude(r => r.Downloads)
                .Where(ur => !ur.Report.IsDeleted)
                .Where(ur => ur.UserId == userId)
                .Select(ur => ReportMapper.MapModelFromEntity(ur.Report))
                .ToListAsync();

            reports = SearchReports(search, reports).ToList();

            return reports;
        }

        /// <summary>
        /// Gets all the Uploaded Reports from a User.
        /// </summary>
        /// <param name="userId">The ID of the target User.</param>
        /// <param name="search">Searches the Reports by Title and Summary.</param>
        /// <returns>ICollection of Report Models.</returns>
        public async Task<ICollection<ReportModel>> GetUploadedReports(int userId, string search)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            ValidateUserExists(user);

            var reports = await _context.Reports
                .Include(r => r.Author)
                .Include(r => r.Industry)
                .Include(r => r.ReportTags)
                .ThenInclude(r => r.Tag)
                .Include(r => r.Downloads)
                .Where(r => r.AuthorId == userId)
                .Select(r => ReportMapper.MapModelFromEntity(r))
                .ToListAsync();

            reports = SearchReports(search, reports).ToList();

            return reports;
        }

        /// <summary>
        /// Gets all the Industry Subscriptions for a User.
        /// </summary>
        /// <param name="userId">The target User ID.</param>
        /// <returns>ICollection of Industry Models.</returns>
        public async Task<ICollection<IndustryModel>> GetSubscriptions(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            ValidateUserExists(user);

            var industries = await _context.IndustrySubscriptions
                .Include(ui => ui.Industry)
                .ThenInclude(i => i.SubscribedUsers)
                .ThenInclude(ui => ui.User)
                .Where(ui => ui.UserId == userId)
                .Select(ui => IndustryMapper.MapModelFromEntity(ui.Industry))
                .ToListAsync();

            return industries;
        }
        /// <summary>
        /// Searches a collection of Users by First Name, Last Name, Email.
        /// </summary>
        /// <param name="search">The search query string.</param>
        /// <param name="users">The collection of Users.</param>
        /// <returns>ICollection of User Models.</returns>
        private ICollection<UserModel> SearchUsers(string search, List<UserModel> users)
        {
            if(search != null)
            {
                users = users.Where(u => u.FirstName.ToLower().Contains(search.ToLower())
                || u.LastName.ToLower().Contains(search.ToLower())
                || u.Email.ToLower().Contains(search.ToLower())).ToList();
            }
            return users;
        }

        /// <summary>
        /// Checks if a User exists. Throws ArgumentNullException if the User is Null.
        /// </summary>
        /// <param name="user"></param>
        private void ValidateUserExists(User user)
        {
            if(user == null)
            {
                throw new ArgumentNullException("User not found.");
            }
        }

        /// <summary>
        /// Searches a Collection of Reports by Title and Summary.
        /// </summary>
        /// <param name="search">The search query string.</param>
        /// <param name="reports">The Collection of Reports.</param>
        /// <returns>ICollection of Report Models.</returns>
        private ICollection<ReportModel> SearchReports(string search, ICollection<ReportModel> reports)
        {
            if (search != null)
            {
                reports = reports.Where(r => r.Title.ToLower().Contains(search.ToLower())
                    || r.Summary.ToLower().Contains(search.ToLower())).ToList();
            }
            return reports;
        }
    }
}

using InsightHub.Data.Entities;
using InsightHub.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace InsightHub.Data
{
    public class InsightHubContext : IdentityDbContext<User, Role, int>
    {
        public InsightHubContext(DbContextOptions<InsightHubContext> options)
            :base(options)
        {

        }
        public virtual DbSet<Report> Reports { get; set; }
        public override DbSet<User> Users { get; set; }
        public override DbSet<Role> Roles { get; set; }
        public virtual DbSet<Industry> Industries { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<DownloadedReport> DownloadedReports { get; set; }
        public virtual DbSet<IndustrySubscription> IndustrySubscriptions { get; set; }
        public virtual DbSet<ReportTag> ReportTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Report>().Property(r => r.Title).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Report>().Property(r => r.Summary).HasMaxLength(300).IsRequired();
            modelBuilder.Entity<Report>().Property(r => r.Description).HasMaxLength(5000).IsRequired();
            modelBuilder.Entity<Report>().HasOne(r => r.Author).WithMany(u => u.UploadedReports).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Report>().HasOne(r => r.Industry).WithMany(i => i.Reports);

            modelBuilder.Entity<Industry>().Property(i => i.Name).HasMaxLength(50).IsRequired();

            modelBuilder.Entity<User>().Property(u => u.FirstName).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.LastName).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Email).IsRequired();
            modelBuilder.Entity<User>().HasOne(u => u.Role);

            modelBuilder.Entity<DownloadedReport>().HasKey(ur => new { ur.UserId, ur.ReportId });
            modelBuilder.Entity<DownloadedReport>().HasOne(ur => ur.Report).WithMany(u => u.Downloads).HasForeignKey(ur => ur.ReportId);
            modelBuilder.Entity<DownloadedReport>().HasOne(ur => ur.User).WithMany(u => u.DownloadedReports).HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<IndustrySubscription>().HasKey(ui => new { ui.UserId, ui.IndustryId });
            modelBuilder.Entity<IndustrySubscription>().HasOne(ui => ui.User).WithMany(u => u.IndustrySubscriptions).HasForeignKey(ui => ui.UserId);
            modelBuilder.Entity<IndustrySubscription>().HasOne(ui => ui.Industry).WithMany(i => i.SubscribedUsers).HasForeignKey(ui => ui.IndustryId);

            modelBuilder.Entity<ReportTag>().HasKey(rt => new { rt.ReportId, rt.TagId });
            modelBuilder.Entity<ReportTag>().HasOne(rt => rt.Report).WithMany(u => u.ReportTags).HasForeignKey(rt => rt.ReportId);
            modelBuilder.Entity<ReportTag>().HasOne(rt => rt.Tag).WithMany(u => u.ReportTags).HasForeignKey(rt => rt.TagId);

            modelBuilder.SeedData();
            base.OnModelCreating(modelBuilder);
        }
    }
}

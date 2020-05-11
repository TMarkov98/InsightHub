﻿using InsightHub.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace InsightHub.Data
{
    public class InsightHubContext : IdentityDbContext<User, Role, int>
    {
        public InsightHubContext(DbContextOptions<InsightHubContext> options)
            :base(options)
        {

        }
        public DbSet<Report> Reports { get; set; }
        public override DbSet<User> Users { get; set; }
        public override DbSet<Role> Roles { get; set; }
        public DbSet<Industry> Industries { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<DownloadedReport> DownloadedReports { get; set; }
        public DbSet<IndustrySubscription> IndustrySubscriptions { get; set; }
        public DbSet<ReportTag> ReportTags { get; set; }
        public DbSet<TagSubscription> TagSubscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Report>().Property(r => r.Title).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Report>().HasOne(r => r.Author);
            modelBuilder.Entity<Report>().Property(r => r.Description).HasMaxLength(1000).IsRequired();
            modelBuilder.Entity<Report>().HasOne(r => r.Industry);

            modelBuilder.Entity<User>().Property(u => u.FirstName).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.LastName).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Email).IsRequired();
            modelBuilder.Entity<User>().HasOne(u => u.Role);

            modelBuilder.Entity<DownloadedReport>().HasKey(ur => new { ur.UserId, ur.ReportId });
            modelBuilder.Entity<DownloadedReport>().HasOne(ur => ur.User).WithMany(u => u.Reports).HasForeignKey(ur => ur.UserId).OnDelete(DeleteBehavior.Restrict);;

            modelBuilder.Entity<IndustrySubscription>().HasKey(ui => new { ui.UserId, ui.IndustryId });
            //modelBuilder.Entity<IndustrySubscription>().HasOne(ui => ui.User).WithMany(u => u.IndustrySubscriptions).HasForeignKey(ui => ui.UserId);

            modelBuilder.Entity<IndustryReport>().HasKey(ir => new { ir.IndustryId, ir.ReportId });
            modelBuilder.Entity<IndustryReport>().HasOne(ir => ir.Industry).WithMany(i => i.Reports).HasForeignKey(ir => ir.IndustryId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ReportTag>().HasKey(rt => new { rt.ReportId, rt.TagId });
            //modelBuilder.Entity<ReportTag>().HasOne(rt => rt.Report).WithMany(r => r.Tags).HasForeignKey(rt => rt.ReportId);

            modelBuilder.Entity<TagSubscription>().HasKey(ut => new { ut.UserId, ut.TagId });
            //modelBuilder.Entity<TagSubscription>().HasOne(ut => ut.User).WithMany(u => u.TagSubscriptions).HasForeignKey(ut => ut.UserId);

            modelBuilder.SeedData();
            base.OnModelCreating(modelBuilder);
        }
    }
}
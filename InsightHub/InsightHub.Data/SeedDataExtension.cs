using InsightHub.Data.Entities;
using InsightHub.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace InsightHub.Data
{
    public static class SeedDataExtension
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            //SEEDING MANAGER ACCOUNT
            var hasher = new PasswordHasher<User>();

            User admin = new User
            {
                Id = 1,
                FirstName = "Admincho",
                LastName = "Adminev",
                UserName = "admin@gmail.com",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                CreatedOn = DateTime.UtcNow,
                LockoutEnabled = true,
                SecurityStamp = "7I5VNHIJTSZNOT3KDWKNFUV5PVYBHGXN"
            };

            User author = new User
            {
                Id = 2,
                FirstName = "Authorcho",
                LastName = "Authorchevski",
                UserName = "author@gmail.com",
                NormalizedUserName = "AUTHOR@GMAIL.COM",
                Email = "author@gmail.com",
                NormalizedEmail = "AUTHOR@GMAIL.COM",
                CreatedOn = DateTime.UtcNow,
                LockoutEnabled = true,
                SecurityStamp = "7I5VNHIJTSZNOT3KDWKNFUV5PVYBHGXV"
            };
            User client = new User
            {
                Id = 3,
                FirstName = "Clientcho",
                LastName = "Clientev",
                UserName = "client@gmail.com",
                NormalizedUserName = "CLIENT@GMAIL.COM",
                Email = "client@gmail.com",
                NormalizedEmail = "CLIENT@GMAIL.COM",
                CreatedOn = DateTime.UtcNow,
                LockoutEnabled = true,
                SecurityStamp = "7I5VNHIJTSZNOT3KDWKNFUV5PVYBHGXF"
            };

            admin.PasswordHash = hasher.HashPassword(admin, "admin123");
            author.PasswordHash = hasher.HashPassword(author, "author123");
            client.PasswordHash = hasher.HashPassword(client, "client123");

            modelBuilder.Entity<User>().HasData(admin);
            modelBuilder.Entity<User>().HasData(author);
            modelBuilder.Entity<User>().HasData(client);

            modelBuilder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int>
                {
                    RoleId = 1,
                    UserId = 1
                });

            modelBuilder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int>
                {
                    RoleId = 2,
                    UserId = 2
                });
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int>
                {
                    RoleId = 3,
                    UserId = 3
                });

            //SEED ALL USER ROLES

            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    Name = "Admin",
                    NormalizedName = "ADMIN"

                });
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 2,
                    Name = "Author",
                    NormalizedName = "AUTHOR"

                });
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 3,
                    Name = "Client",
                    NormalizedName = "CLIENT"
                });

            //Seed Reports

            modelBuilder.Entity<Report>().HasData(
                new Report
                {
                    Id = 1,
                    Title = "First Report",
                    Description = "First report's description.",
                    AuthorId = 2,
                    IndustryId = 1,
                    ImgUrl = "First FileURL",
                    CreatedOn = DateTime.UtcNow,
                }
                );
            modelBuilder.Entity<Report>().HasData(
                new Report
                {
                    Id = 2,
                    Title = "Second Report",
                    Description = "Second report's description.",
                    AuthorId = 2,
                    IndustryId = 2,
                    ImgUrl = "Second FileURL",
                    CreatedOn = DateTime.UtcNow,
                    IsFeatured = true,
                }
                );
            modelBuilder.Entity<Report>().HasData(
                new Report
                {
                    Id = 3,
                    Title = "Third Report",
                    Description = "Third report's description.",
                    AuthorId = 2,
                    IndustryId = 2,
                    ImgUrl = "Third FileURL",
                    CreatedOn = DateTime.UtcNow,
                    IsFeatured = true,
                }
                );
            //Seed Industries

            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 1,
                    Name = "Next-Wave Logistics",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 2,
                    Name = "Space Technology",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 3,
                    Name = "Clean Water Services",
                    CreatedOn = DateTime.UtcNow
                }
                );
            //Seed Tags

            modelBuilder.Entity<Tag>().HasData(
                new Tag
                {
                    Id = 1,
                    Name = "Space",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Tag>().HasData(
                new Tag
                {
                    Id = 2,
                    Name = "Water",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Tag>().HasData(
                new Tag
                {
                    Id = 3,
                    Name = "Money",
                    CreatedOn = DateTime.UtcNow
                }
                );
            //Seed ReportTags

            modelBuilder.Entity<ReportTag>().HasData(
                new ReportTag
                {
                    ReportId = 1,
                    TagId = 1,
                });
            modelBuilder.Entity<ReportTag>().HasData(
                new ReportTag
                {
                    ReportId = 1,
                    TagId = 2,
                });
            modelBuilder.Entity<ReportTag>().HasData(
                new ReportTag
                {
                    ReportId = 2,
                    TagId = 1,
                });
            //Seed IndustrySubscriptions

            modelBuilder.Entity<IndustrySubscription>().HasData(
                new IndustrySubscription
                {
                    IndustryId = 1,
                    UserId = 3,
                });
        }
    }
}

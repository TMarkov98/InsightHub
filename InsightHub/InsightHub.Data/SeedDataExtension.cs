using InsightHub.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

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
                SecurityStamp = "7I5VNHIJTSZNOT3KDWKNFUV5PVYBHGXN",
                IsPending = false,
            };
            User author = new User
            {
                Id = 2,
                FirstName = "Georgi",
                LastName = "Petrov",
                UserName = "georgi.petrov@gmail.com",
                NormalizedUserName = "GEORGI.PETROV@GMAIL.COM",
                Email = "georgi.petrov@gmail.com",
                NormalizedEmail = "GEORGIPETROV@GMAIL.COM",
                CreatedOn = new DateTime(2020, 1, 18),
                LockoutEnabled = true,
                SecurityStamp = "7I5VNHIJTSZNOT3KDWKNFUV5PVYBHMHG",
                IsPending = false,
            };
            User author2 = new User
            {
                Id = 3,
                FirstName = "Miroslav",
                LastName = "Peev",
                UserName = "miroslav.peev@gmail.com",
                NormalizedUserName = "MIROSLAV.PEEV@GMAIL.COM",
                Email = "miroslav.peev@gmail.com",
                NormalizedEmail = "MIROSLAV.PEEV@GMAIL.COM",
                CreatedOn = new DateTime(2020, 3, 15),
                LockoutEnabled = true,
                SecurityStamp = "7I5VNHIJTSZNOT3KDWKNFUV5PVYBHPPT",
                IsPending = false,
            };
            User author3 = new User
            {
                Id = 4,
                FirstName = "Atanas",
                LastName = "Velev",
                UserName = "atanas.velev@gmail.com",
                NormalizedUserName = "ATANAS.VELEV@GMAIL.COM",
                Email = "atanas.velev@gmail.com",
                NormalizedEmail = "ATANAS.VELEV@GMAIL.COM",
                CreatedOn = new DateTime(2020, 5, 2),
                LockoutEnabled = true,
                SecurityStamp = "7I5VNHIJTSZNOT3KDWKNFUV5PVYBHGMN",
                IsPending = false,
            };
            User author4 = new User
            {
                Id = 5,
                FirstName = "Petyr",
                LastName = "Petrov",
                UserName = "petyr.petrov@gmail.com",
                NormalizedUserName = "PETYR.PETROV@GMAIL.COM",
                Email = "petyr.petrov@gmail.com",
                NormalizedEmail = "PETYR.PETROV@GMAIL.COM",
                CreatedOn = new DateTime(2020, 5, 28),
                LockoutEnabled = true,
                SecurityStamp = "7I5VNHIJTSZNOT3KDWKNFUV5PVYBHPPG",
                IsPending = false,
            };
            User client = new User
            {
                Id = 6,
                FirstName = "Valentina",
                LastName = "Dimitrova",
                UserName = "valentina.dimitrova@gmail.com",
                NormalizedUserName = "VALENTINA.DIMITROVA@GMAIL.COM",
                Email = "valentina.dimitrova@gmail.com",
                NormalizedEmail = "VALENTINA.DIMITROVA@GMAIL.COM",
                CreatedOn = new DateTime(2020, 4, 22),
                LockoutEnabled = true,
                SecurityStamp = "7I5VNHIJTSZNOT3KDWKNFUV5PVVBHGXF",
                IsPending = false,
            };

            User client2 = new User
            {
                Id = 7,
                FirstName = "Kaloyan",
                LastName = "Jekov",
                UserName = "kaloyan.jekov@gmail.com",
                NormalizedUserName = "KALOYAN.JEKOV@GMAIL.COM",
                Email = "kaloyan.jekov@gmail.com",
                NormalizedEmail = "KALOYAN.JEKOV@GMAIL.COM",
                CreatedOn = new DateTime(2020, 2, 16),
                LockoutEnabled = true,
                SecurityStamp = "7I5VNHIJTSZNOT3KDWKNFUV5PPGBHGXF",
                IsPending = false,
            };

            User client3 = new User
            {
                Id = 8,
                FirstName = "Preslav",
                LastName = "Mitev",
                UserName = "preslav.mitev@gmail.com",
                NormalizedUserName = "PRESLAV.MITEV@GMAIL.COM",
                Email = "preslav.mitev@gmail.com",
                NormalizedEmail = "PRESLAV.MITEV@GMAIL.COM",
                CreatedOn = new DateTime(2020, 4, 20),
                LockoutEnabled = true,
                SecurityStamp = "7I5VNHIJTSZNOT3KDWKNFUK3PVYBHGXF",
                IsPending = false,
            };

            User client4 = new User
            {
                Id = 9,
                FirstName = "Kristian",
                LastName = "Ivanov",
                UserName = "kristian.ivanov@gmail.com",
                NormalizedUserName = "KRISTIAN.IVANOV@GMAIL.COM",
                Email = "kristian.ivanov@gmail.com",
                NormalizedEmail = "KRISTIAN.IVANOV@GMAIL.COM",
                CreatedOn = new DateTime(2020, 1, 2),
                LockoutEnabled = true,
                SecurityStamp = "7I5VNHIJTSZNOT3KDWKNKSV5PVYBHGXF",
                IsPending = false,
            };

            User client5 = new User
            {
                Id = 10,
                FirstName = "Kiro",
                LastName = "Kirov",
                UserName = "kiro.kirov@gmail.com",
                NormalizedUserName = "KIRO.KIROV@GMAIL.COM",
                Email = "kiro.kirov@gmail.com",
                NormalizedEmail = "KIRO.KIROV@GMAIL.COM",
                CreatedOn = new DateTime(2020, 2, 6),
                LockoutEnabled = true,
                SecurityStamp = "7I5VNHIJTSZNOT3KDWKNFUV3LSYBHGXF",
                IsPending = false,
            };

            User admin2 = new User
            {
                Id = 11,
                FirstName = "Admin",
                LastName = "Second",
                UserName = "admin2@gmail.com",
                NormalizedUserName = "ADMIN2@GMAIL.COM",
                Email = "admin2@gmail.com",
                NormalizedEmail = "ADMIN2@GMAIL.COM",
                CreatedOn = DateTime.UtcNow,
                LockoutEnabled = true,
                SecurityStamp = "7I5VNHIJTSZNOT3KDWKNFUV5PVYBHGXB",
                IsPending = false,
            };

            admin.PasswordHash = hasher.HashPassword(admin, "admin123");
            author.PasswordHash = hasher.HashPassword(author, "georgi123");
            author2.PasswordHash = hasher.HashPassword(author2, "miroslav123");
            author3.PasswordHash = hasher.HashPassword(author3, "atanas123");
            author4.PasswordHash = hasher.HashPassword(author4, "petyr123");
            client.PasswordHash = hasher.HashPassword(client, "valentina123");
            client2.PasswordHash = hasher.HashPassword(client2, "kaloyan123");
            client3.PasswordHash = hasher.HashPassword(client3, "preslav123");
            client4.PasswordHash = hasher.HashPassword(client4, "kristian123");
            client5.PasswordHash = hasher.HashPassword(client5, "kiro123");

            modelBuilder.Entity<User>().HasData(admin);
            modelBuilder.Entity<User>().HasData(admin2);
            modelBuilder.Entity<User>().HasData(author);
            modelBuilder.Entity<User>().HasData(author2);
            modelBuilder.Entity<User>().HasData(author3);
            modelBuilder.Entity<User>().HasData(author4);
            modelBuilder.Entity<User>().HasData(client);
            modelBuilder.Entity<User>().HasData(client2);
            modelBuilder.Entity<User>().HasData(client3);
            modelBuilder.Entity<User>().HasData(client4);
            modelBuilder.Entity<User>().HasData(client5);


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
                    RoleId = 2,
                    UserId = 3
                });
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int>
                {
                    RoleId = 2,
                    UserId = 4
                });
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int>
                {
                    RoleId = 2,
                    UserId = 5
                });
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int>
                {
                    RoleId = 3,
                    UserId = 6
                });
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int>
                {
                    RoleId = 3,
                    UserId = 7
                });
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int>
                {
                    RoleId = 3,
                    UserId = 8
                });
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int>
                {
                    RoleId = 3,
                    UserId = 9
                });
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int>
                {
                    RoleId = 3,
                    UserId = 10
                });
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int>
                {
                    RoleId = 1,
                    UserId = 11
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
                    Title = "National Economic Outlook",
                    Description = "Real GDP contracted 4.8 percent at an annualized rate in the first quarter according to the advance estimate from the Bureau of Economic Analysis, worse than the consensus expectation for a 4.0 percent decline. The coronavirus pandemic and restrictions on movement led to big declines in consumer spending and business investment (down 7.6 percent and 8.6 percent annualized, respectively).",
                    Summary = "April 2020 National Economic Outlook by PINACLE",
                    AuthorId = 3,
                    IndustryId = 5,
                    ImgUrl = "https://www.agenda-bg.com/wp-content/uploads/2016/09/What-To-Expect-In-2015-An-Economic-Outlook-For-Small-And-Medium-Businesses-Making-It-TV.jpg",
                    CreatedOn = new DateTime(2020, 3, 15),
                    IsPending = false,
                }
                );

            modelBuilder.Entity<Report>().HasData(
                new Report
                {
                    Id = 2,
                    Title = "Market Expectations Survey",
                    Description = "PNC's Market Expectation Survey lists the current consensus forecast for key economic data releases for the upcoming week, as well as PNC's own forecast for each item. A comprehensive calendar listing recent data for key economic indicators is provided on the second page.",
                    Summary = "A comprehensive calendar listing recent data for key economic indicators.",
                    AuthorId = 4,
                    IndustryId = 5,
                    ImgUrl = "https://www.questionpro.com/blog/wp-content/uploads/2018/05/Market-Survey_Final-800x478.jpg",
                    CreatedOn = new DateTime(2020, 3, 16),
                    IsPending = false,
                }
                );

            modelBuilder.Entity<Report>().HasData(
                new Report
                {
                    Id = 3,
                    Title = "Fall Economic Outlook Survey",
                    Description = "Despite pricing pressures and tariff concerns, measures of optimism are approaching the record-setting highs measured in spring 2018, according to the latest PNC semi-annual survey of small and mid-size business owners and executives.",
                    Summary = "Optimism Still Springs This Fall for Small and Mid-size Business Owners",
                    AuthorId = 2,
                    IndustryId = 5,
                    ImgUrl = "https://images.financialexpress.com/2019/06/ECONOMIC_SURVEY_660.jpg",
                    CreatedOn = new DateTime(2020, 2, 10),
                    IsFeatured = true,
                    IsPending = false,
                });

            modelBuilder.Entity<Report>().HasData(
                new Report
                {
                    Id = 4,
                    Title = "Report to the President by the presidential commission",
                    Description = "IN MEMORIAM - \"The future is not free: the story of all human progress is one of a struggle against all odds.We learned again that this America, which Abraham Lincoln called the last,best hope of man on Earth, was built on heroism and noble sacrifice.It was built by men and women like our seven star voyagers, who answered a call beyond duty, who gave more than was expected or required and who gave it little thought of worldly reward.\"- President Ronald Reagan January 31, 1986",
                    Summary = "Report to the President By the presidential commission - on the Space Shuttle Challenger Accident",
                    AuthorId = 5,
                    IndustryId = 6,
                    ImgUrl = "https://i.imgur.com/jEPMztX.png",
                    CreatedOn = new DateTime(2019, 3, 13),
                    IsFeatured = true,
                    IsPending = false,
                });

            modelBuilder.Entity<Report>().HasData(
                new Report
                {
                    Id = 5,
                    Title = "Identifying the Drivers of Air Fares",
                    Description = "Foreword Airline fare setting is a complex and dynamic process, varying by carrier, route and time. It lies at the heart of an airline’s commercial strategy as it aims to maximise the return on its assets employed, namely its aircraft and its people. In the following report, we have sought to describe clearly and comprehensively how the numerous factors an airline has to juggle play into the fare that passengers ultimately face. As a team who has an airline background, and advises both airlines and airports on matters of strategy, ICF has been uniquely well placed to do this.",
                    Summary = "An ICF Report prepared for ACI Europe",
                    AuthorId = 3,
                    IndustryId = 2,
                    ImgUrl = "https://www.aljazeera.com/mritems/Images/2019/12/4/b604dd8d57c942a892cdc71f21d09973_18.jpg",
                    CreatedOn = new DateTime(2020, 4, 25),
                    IsPending = false,
                });

            modelBuilder.Entity<Report>().HasData(
                new Report
                {
                    Id = 6,
                    Title = "Five Trends Transforming the Automotive Industry",
                    Description = "Since the introduction of the smartphone, it has become clear that customers are quick to adopt even highly complex and expensive technology if it makes their lives easier. In other words, users value convenience and ease. These core values turned the automobile into the defining technical cultural item of the 20th century. Now it is time to translate these properties into the context of today's – and tomorrow's –  technology and society. ",
                    Summary = "Welcome to the age of radical change in the automotive industry",
                    AuthorId = 3,
                    IndustryId = 4,
                    ImgUrl = "https://www.car-brand-names.com/wp-content/uploads/2016/04/Cars.jpg",
                    CreatedOn = new DateTime(2020, 6, 3),
                    IsFeatured = true,
                    IsPending = false,
                });

            modelBuilder.Entity<Report>().HasData(
                new Report
                {
                    Id = 7,
                    Title = "Full report on the comprehensive review of the banking system",
                    Description = "This Report contains the conclusions from the comprehensive review of the Slovenian banking sector undertaken by the Bank of Sloveniain cooperation with the Slovenian Ministry of Finance over the period June to December 2013. This Asset Quality Review and Stress Test is a cornerstone in the broader initiative to restore the health in the Slovenian banking sector.",
                    Summary = "The Asset Quality Review and Stress Test were closely monitored by the international organisations (IOs), constituted of the European Commission, the European Central Bank, and the European Banking Authority.",
                    AuthorId = 3,
                    IndustryId = 5,
                    ImgUrl = "https://www.mindtree.com/sites/default/files/2018-08/Digital%20Banking%20Solution-Mindtree-H.jpg",
                    CreatedOn = new DateTime(2020, 2, 13),
                    IsPending = false,
                });

            modelBuilder.Entity<Report>().HasData(
                new Report
                {
                    Id = 8,
                    Title = "Distributed Ledger Technology",
                    Description = "Distributed Ledger Technology (DLT) (also known as blockchain technology or distributed database technology) has attracted significant interest and funding in the financial services industry in recent years. Several large financial institutions have established dedicated teams to explore the technology, and some market participants have formed consortia to create industry standards.2 According to a 2016 report by the World Economic Forum,3 over the past three years more than $1.4 billion has been invested in this technology to explore and implement uses in the financial services industry.",
                    Summary = "Implicationsof Blockchain for the Securities Industry",
                    AuthorId = 4,
                    IndustryId = 11,
                    ImgUrl = "https://www.jagranjosh.com/imported/images/E/Articles/What-is-Blockchain-Technology.png",
                    CreatedOn = new DateTime(2020, 3, 4),
                    IsFeatured = true,
                    IsPending = false,
                });

            modelBuilder.Entity<Report>().HasData(
                new Report
                {
                    Id = 9,
                    Title = "Construction Materials Report",
                    Description = "This report is intended to be of practical use in reducing the environmental impacts of construction. Nicole Lazarus will be glad to hear from any readers with feedback and examples of its application.",
                    Summary = "Toolkit for Carbon Neutral Developments",
                    AuthorId = 5,
                    IndustryId = 7,
                    ImgUrl = "https://5chat.com/wp-content/uploads/building-materials.jpg",
                    CreatedOn = new DateTime(2020, 2, 1),
                    IsPending = false,
                });

            modelBuilder.Entity<Report>().HasData(
                new Report
                {
                    Id = 10,
                    Title = "Professional and Business Services Sector Report",
                    Description = "This report covers Audit and accounting; Business services; Professional services; and Legal services.",
                    Summary = "This report covers: a description of the sector, the current EU regulatory regime, existing frameworks for how trade is facilitated between countries in this sector, and sector views. It does not contain commercially-, market-or negotiation-sensitive information.",
                    AuthorId = 3,
                    IndustryId = 1,
                    ImgUrl = "https://magneticonemobile.com/wp-content/uploads/2018/01/slide1-business.jpg",
                    CreatedOn = new DateTime(2020, 2, 11),
                    IsPending = false,
                });

            modelBuilder.Entity<Report>().HasData(
                new Report
                {
                    Id = 11,
                    Title = "Casino Gambling in America and Its Economic Impacts",
                    Description = "Casino Gambling has become a majorindustry in the United States over the past twodecades.  Nationwide, annual casino revenue tops $40billion. This report provides an analysis of casino gam-bling in the United States and discusses the economicissues surrounding casino gambling. The informationcontained in this report should prove useful to localofficials and policy-makers who may be considering theadoption of casino gambling or who already have casi-no gambling in their jurisdictions.",
                    Summary = "This report provides an analysis of casino gam - bling in the United States and discusses the economicissues surrounding casino gambling.",
                    AuthorId = 5,
                    IndustryId = 9,
                    ImgUrl = "https://royalepalmscasino-sofia.com/wp-content/uploads/2019/04/IMG_1710-1024x683.jpg",
                    CreatedOn = new DateTime(2020, 4, 9),
                    IsFeatured = true,
                    IsPending = false,
                });

            modelBuilder.Entity<Report>().HasData(
                new Report
                {
                    Id = 12,
                    Title = "Cattle Ranching and Deforestation",
                    Description = "Particularly in Central and South America, expansion of pastures for livestock production has been one of the driving forces behind this wholesale destruction.Deforestation causes incalculable environmental damage, releasing billions of tonnes of carbon dioxide into the atmosphere and driving thousands of species of life to extinction each year. Effective policies are urgently needed to discourage expansion of livestock production in forest areas and promote sustainable grazing systems that will halt the cycle of degradation and abandonment on cleared forest lands.",
                    Summary = "Over the past quarter century, forests have been cleared from an area the size of India.",
                    AuthorId = 5,
                    IndustryId = 8,
                    ImgUrl = "https://fayranches.com/wp-content/uploads/2016/01/savvy-investors-cattle-ranches.jpg",
                    CreatedOn = new DateTime(2020, 3, 12),
                    IsPending = false,
                });

            modelBuilder.Entity<Report>().HasData(
                new Report
                {
                    Id = 13,
                    Title = "Start-Up Space Report 2020",
                    Description = "The Start-Up Space series examines space investment in the 21st century and analyzes investment trends, focusing on companies that began as angel- and venture capital-backed startups. The report tracks publicly-reported seed, venture, and private equity investment in start-up space ventures as they grow and mature, from 2000 through the end of 2019. The report includes debt financing for these companies where applicable to provide a complete picture of the capital available to them, and also highlights merger and acquisition (M&A) and initial public offering (IPO) activity.",
                    Summary = "Update on Investment in Commercial Space Ventures",
                    AuthorId = 3,
                    IndustryId = 6,
                    ImgUrl = "https://i.imgur.com/9ir2Qak.png",
                    CreatedOn = new DateTime(2020, 2, 15),
                    IsPending = false,
                });

            modelBuilder.Entity<Report>().HasData(
                new Report
                {
                    Id = 14,
                    Title = "The Change in the Australian Work Force Since the End of World War 2",
                    Description = "This report discusses the changes that have occurred in the Australian workforce since the end of World War II (1945-2000). A review of some of the available literature provides insights into the changing role of women and migrants in the workforce, and the influence of new technologies and changing levels of unemployment have also been considered.",
                    Summary = "The information presented in this report has been gathered from secondary sources, and from Australian Bureau of Statistics’ data.",
                    AuthorId = 4,
                    IndustryId = 7,
                    ImgUrl = "https://upload.wikimedia.org/wikipedia/commons/1/1a/Chapin_Hall%2C_Williams_College_-_Williamstown%2C_Massachusetts.jpg",
                    CreatedOn = new DateTime(2020, 5, 10),
                    IsPending = false,
                });

            modelBuilder.Entity<Report>().HasData(
                new Report
                {
                    Id = 15,
                    Title = "Resources Usage of Windows Computer Laboratories",
                    Description = "In this paper we quantify the usage of main resources (CPU, main memory, disk space and network bandwidth) of Windows 2000 machines from classroom laboratories. For that purpose, 169 machines of 11 classroom laboratories of an academic institution were monitored over 77 consecutive days. Samples were collected from all machines every 15 minutes for a total of 583653 samples. Besides evaluating availability of machines (uptime and downtime) and usage habits of users, the paper assesses usage of main resources, focusing on the impact of interactive login sessions over resource consumptions. Also, recurring to Self Monitoring Analysis and Reporting Technology (SMART) parameters of hard disks, the study estimates the average uptime per hard drive power cycle for the whole life of monitored computers. The paper also analyzes the potential of non-dedicated classroom Windows machines for distributed and parallel computing, evaluating the mean stability of group of machines. Our results show that resources idleness in classroom computers is very high, with an average CPU idleness of 97.93%, unused memory averaging 42.06% and unused disk space of the order of gigabytes per machine. Moreover, this study confirms the 2:1 equivalence rule found out by similar works, with N non-dedicated resources delivering an average CPU computing power roughly similar to N/2 dedicated machines. These results confirm the potentiality of these systems for resource harvesting, especially for grid desktop computing schemes. However, the efficient exploitation of the computational power of these environments requires adaptive fault-tolerance schemes to overcome the high volatility of resources.",
                    Summary = "Studies focusing on Unix have shown that the vast majority of workstations and desktop computers remain idle for most of the time.",
                    AuthorId = 2,
                    IndustryId = 13,
                    ImgUrl = "https://i.imgur.com/CxJ0348.png",
                    CreatedOn = new DateTime(2020, 6, 7, 4, 22, 00),
                    IsPending = false,
                });

            modelBuilder.Entity<Report>().HasData(
                new Report
                {
                    Id = 16,
                    Title = "China’s Orbital Launch Activity",
                    Description = "A graphic that provides foundational data on China’s orbital launch sites and launch vehicles, as well as on the general structure of China’s state-managed space industry. Includes operational vehicles and vehicles with a high probably of entering operations during the next 2 years. Partial failures counted as failures in reliability calculation.",
                    Summary = "A graphic that provides foundational data on China’s orbital launch sites and launch vehicles, as well as on the general structure of China’s state-managed space industry.",
                    AuthorId = 3,
                    IndustryId = 6,
                    ImgUrl = "https://i.ytimg.com/vi/4wPSyl2Yb1M/maxresdefault.jpg",
                    CreatedOn = new DateTime(2020, 5, 5),
                    IsPending = false,
                });
                    
                    modelBuilder.Entity<Report>().HasData(
                new Report
                {
                    Id = 17,
                    Title = "USDA Crop Production Report 2019",
                    Description = "This page intentionally left blank.",
                    Summary = "Corn Production Down 4 Percent from 2018. Soybean Production Down 19 Percent from 2018. Cotton Production Up 23 Percent from 2018. Winter Wheat Production Up 3 Percent from July Forecast",
                    AuthorId = 2,
                    IndustryId = 8,
                    ImgUrl = "https://media.nationalgeographic.org/assets/photos/120/983/091a0e2f-b93d-481b-9a60-db520c87ec33.jpg",
                    CreatedOn = new DateTime(2020, 3, 5),
                    IsPending = false,
                });
            modelBuilder.Entity<Report>().HasData(
                new Report
                {
                    Id = 18,
                    Title = "Smallsat Launch Delays",
                    Description = "WASHINGTON — A study that found that every small satellite launched commercially in the last five years suffered delays is evidence of the need of greater standardization in payload accommodations so that smallsats can easily switch vehicles, one company argues.",
                    Summary = "Smallsat launch delays prompt push for greater standardization",
                    AuthorId = 3,
                    IndustryId = 6,
                    ImgUrl = "https://i.imgur.com/028Yd8V.png",
                    CreatedOn = new DateTime(2020, 6, 7, 12, 40, 00),
                    IsPending = false,
                });

            modelBuilder.Entity<Report>().HasData(
                new Report
                {
                    Id = 19,
                    Title = "Global Dairy Trends and Drivers 2019",
                    Description = "This article is based on the IFCN Dairy Report 2019. This annual report summarises the work of IFCN Research Partners from over 100 countries. IFCN is a global network for dairy economic research and consultancy. In 2019, researchers from over 100 countries and more than 140 agribusiness companies are members of the network. IFCN has created a better understanding of the dairy world for 20 years. Key insights 2019 will be the year of lowest milk production growth since 2013. As this did not translate into milk price increases, IFCN identifies a structural drop in demand growth as one of the reasons. Milk production trends by regions are highly diverse and dynamic. The 3-5% rule indicates that strong regions grow and weak ones decline by this rate every year. Dairy farm structure dynamics drive milk supply and the speed of change is under-estimated. IFCN recommends using the annual growth of milk production per farm as an indicator. In the EU and the USA farms grew by 8% per year. The key driver for farm structure developments lies in dairy farm economics and the current structure of economies of scale. The Dairy Report analyses this in over 50 countries. The IFCN Dairy Report has been published annually since 2000 and has become a guideline publication for researchers and companies involved in the dairy chain. It enables to gain a global holistic view of the industry and serves as a solid fact base for discussions and strategic decisions.",
                    Summary = "This annual report summarises the work of IFCN Research Partners from over 100 countries.",
                    AuthorId = 2,
                    IndustryId = 15,
                    ImgUrl = "https://i0.wp.com/edairynews.com/en/wp-content/uploads/2020/06/Global-Dairy-Commodity-Update-June-2020.jpg?fit=1024%2C683&ssl=1",
                    CreatedOn = new DateTime(2019, 2, 18),
                    IsPending = false,
                });


            modelBuilder.Entity<Report>().HasData(
                new Report
                {
                    Id = 20,
                    Title = "Aerospace Industry 2012 Report",
                    Description = "The Aerospace Industries Association (AIA) is the most authoritative and influential trade association representing the aerospace and defense industry. We are the leading voice for the industry on Capitol Hill, within the administration, and internationally.",
                    Summary = "Aerospace Industry Report 3rd Edition by The Aerospace Industries Association (AIA)",
                    AuthorId = 5,
                    IndustryId = 2,
                    ImgUrl = "https://i.imgur.com/jXUg87d.png",
                    CreatedOn = new DateTime(2020, 1, 13),
                    IsFeatured = true,
                    IsPending = false,
                });

            modelBuilder.Entity<Report>().HasData(
                new Report
                {
                    Id = 21,
                    Title = "Sustainability of the Fashion Industry",
                    Description = "The way we make, use and throwaway our clothes is unsustainable. Textile production contributes more to climate change than international aviation and shipping combined, consumes lake-sized volumes of fresh water and creates chemical and plastic pollution. Synthetic fibres are being found in the deep sea, in Arctic sea ice, in fish and shellfish. Our biggest retailers have ‘chased the cheap needle around the planet’, commissioning production  in  countries  with  low  pay,  little  trade  union  representation  and  weak  environmental protection. In many countries, poverty pay and conditions are standard for garment workers, most of whom are women. We are also concerned about the use of  child  labour,  prison  labour,  forced  labour  and  bonded  labour  in  factories  and  the  garment supply chain. Fast fashions’ overproduction and overconsumption of clothing is based on the globalisation of indifference towards these manual workers.",
                    Summary = "Fixing Fashion: Clothing Consumption and Sustainability",
                    AuthorId = 2,
                    IndustryId = 10,
                    ImgUrl = "http://intrigue.ie/media/2014/04/fashion-industry-1.jpg",
                    CreatedOn = new DateTime(2020, 6, 1, 11, 15, 00),
                    IsPending = false,
                });

            modelBuilder.Entity<Report>().HasData(
                new Report
                {
                    Id = 22,
                    Title = "2019 Cruise Trends & Industry Outlook",
                    Description = "Cruise Lines International Association (CLIA), the world’s largest cruise industry trade association, has released the 2019 Cruise Trends and State of the Cruise Industry Outlook. The report offers a look at the trends impacting cruise travel in the coming year and beyond as well as the overall global economic impact. Cruise Lines International Association (CLIA) is the unified global organizationhelping members succeed by advocating, educating and promoting for the common interests of the cruise community.",
                    Summary = "Instagram photos are driving interest in travel around the world. With onboard connectivity, cruise passengers are filling Instagram feeds with diverse travel experiences both onboard and on land from several cruise destinations.",
                    AuthorId = 2,
                    IndustryId = 16,
                    ImgUrl = "https://www.gannett-cdn.com/presto/2018/12/20/USAT/b3fa36d4-b0ed-48c5-a991-924c48685469-Costa_LNG__Side_Perspective.jpg?crop=2999,1687,x0,y299&width=2999&height=1687&format=pjpg&auto=webp",
                    CreatedOn = new DateTime(2020, 3, 2),
                    IsPending = false,
                });

            modelBuilder.Entity<Report>().HasData(
                new Report
                {
                    Id = 23,
                    Title = "Los Angeles College Monthly Construction Report",
                    Description = "Currently, Construction is 99.9% complete. Commissioning Pre-functional test was completed by ARUP and draft report has been received. MEOR is scheduled to review and evaluate draft report from ARUP. Once MEOR reviews draft report, ARUP will issue final  Commissioning  report.  Substantial  Completion  is  estimated  to  be  issued  in  August,  after  the  issuance  of the final Commissioning report and Punch list items are issued to the Contractor.",
                    Summary = "Los Angeles Mission College Gateway Science and Engineering Construction Management Monthly Progress Report",
                    AuthorId = 3,
                    IndustryId = 7,
                    ImgUrl = "https://specials-images.forbesimg.com/imageserve/5c0077cc31358e5b43383ffc/960x0.jpg?fit=scale",
                    CreatedOn = new DateTime(2020, 6, 4, 10, 30, 00),
                    IsPending = false,
                });

            modelBuilder.Entity<Report>().HasData(
                new Report
                {
                    Id = 24,
                    Title = "Mitigation of Orbital Debris in the New Space Age",
                    Description = "A wide range of new and existing commercial technologies depend on reliable communications with spacecraft.  The cost, integrity, and reliability of these communications can be negatively affected by orbital debris, which presents an ever-increasing threat to operational spacecraft.  The environment in space continues to change and evolve in the New Space Age as increasing numbers of satellites are launched and new satellite technology is developed.  The regulations we adopt today are designed to ensure that the Commission’s actions concerning radio communications, including licensing U.S. spacecraft and granting access to the U.S. market for non-U.S. spacecraft, mitigate the growth of orbital debris, while at the same time not creating undue regulatory obstacles to new satellite ventures.  This action will help to ensure that Commission decisions are consistent with the public interest in space remaining viable for future satellites and systems and the many services that those systems provide to the public.",
                    Summary = "Report and Order and Further Notice of Proposed Rulemaking, IB Docket No. 18-313 ",
                    AuthorId = 3,
                    IndustryId = 6,
                    ImgUrl = "https://aerospacedefenseforum.org/wp-content/uploads/2019/02/shutterstock_557310703_space_technology.jpg",
                    CreatedOn = new DateTime(2020, 4, 2),
                    IsPending = false,
                });

            modelBuilder.Entity<Report>().HasData(
                new Report
                {
                    Id = 25,
                    Title = "The British Pub Market 2019",
                    Description = "The report is divided into three sections. The first part reviews the supply side of the pub market, revealing numbers, trends and the contrasting fortunes of different sectors, and identifying some of the areas in which pubs are succeeding. The second section analyses the customer base: their demographics, habits and motivations. The final part takes a look at reasons for optimism, with insights into increasing appeal and sales and the emerging new breeds of pub in Britain. At a time of great challenges for both the out of home eating and drinking market and the UK economy as a whole, this report highlights the many positive trends and developments in the British pub market. We hope you enjoy reading it.",
                    Summary = "This report draws on CGA’s unrivalled suite of research services to provide a comprehensive picture of Great Britain’s pubs and their opportunities for growth",
                    AuthorId = 4,
                    IndustryId = 17,
                    ImgUrl = "https://imagesvc.meredithcorp.io/v3/mm/image?url=https%3A%2F%2Fstatic.onecms.io%2Fwp-content%2Fuploads%2Fsites%2F9%2F2019%2F12%2Fuk-pubs-growth-FT-BLOG1219.jpg&q=85",
                    CreatedOn = new DateTime(2020, 3, 4),
                    IsFeatured = true,
                    IsPending = false,
                });

            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 1,
                    Name = "Finance",
                    ImgUrl = "https://i.imgur.com/8Rkh6JW.png",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 2,
                    Name = "Airlines",
                    ImgUrl = "https://i.imgur.com/dX9t5lS.png",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 3,
                    Name = "Healthcare",
                    ImgUrl = "https://i.imgur.com/TC7qZPP.png",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 4,
                    Name = "Automobile",
                    ImgUrl = "https://i.imgur.com/AO7gOGs.png",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 5,
                    Name = "Business Services",
                    ImgUrl = "https://i.imgur.com/MWr58IA.png",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 6,
                    Name = "Space",
                    ImgUrl = "https://i.imgur.com/evwFFDj.png",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 7,
                    Name = "Building",
                    ImgUrl = "https://i.imgur.com/io9aGef.jpg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 8,
                    Name = "Agriculture",
                    ImgUrl = "https://i.imgur.com/0aD3uZj.jpg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 9,
                    Name = "Casino",
                    ImgUrl = "https://i.imgur.com/MIkU108.jpg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 10,
                    Name = "Fashion",
                    ImgUrl = "https://i.imgur.com/TFcnTmD.png",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 11,
                    Name = "Blockchain",
                    ImgUrl = "https://i.imgur.com/ArAIUQ0.png",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 12,
                    Name = "Education",
                    ImgUrl = "https://i.imgur.com/WeTQtUK.png",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 13,
                    Name = "Technology",
                    ImgUrl = "https://i.imgur.com/ApJqXTX.png",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 14,
                    Name = "Energy",
                    ImgUrl = "https://i.imgur.com/5pZJcpA.png",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 15,
                    Name = "Food",
                    ImgUrl = "https://i.imgur.com/oWIukyA.png",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 16,
                    Name = "Tourism",
                    ImgUrl = "https://i.imgur.com/GMNHXMs.png",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 17,
                    Name = "Restaurants",
                    ImgUrl = "https://i.imgur.com/fJ5s33U.png",
                    CreatedOn = DateTime.UtcNow
                }
                );

            //Seed Tags

            modelBuilder.Entity<Tag>().HasData(
                new Tag
                {
                    Id = 1,
                    Name = "space",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Tag>().HasData(
                new Tag
                {
                    Id = 2,
                    Name = "beauty",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Tag>().HasData(
                new Tag
                {
                    Id = 3,
                    Name = "money",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Tag>().HasData(
                new Tag
                {
                    Id = 4,
                    Name = "future",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Tag>().HasData(
                new Tag
                {
                    Id = 5,
                    Name = "ship",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Tag>().HasData(
                new Tag
                {
                    Id = 6,
                    Name = "food",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Tag>().HasData(
                new Tag
                {
                    Id = 7,
                    Name = "building",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Tag>().HasData(
                new Tag
                {
                    Id = 8,
                    Name = "bank",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Tag>().HasData(
                new Tag
                {
                    Id = 9,
                    Name = "sky",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Tag>().HasData(
                new Tag
                {
                    Id = 10,
                    Name = "tech",
                    CreatedOn = DateTime.UtcNow
                }
                );
            //Seed ReportTags

            modelBuilder.Entity<ReportTag>().HasData(
                new ReportTag
                {
                    ReportId = 18,
                    TagId = 1,
                });
            modelBuilder.Entity<ReportTag>().HasData(
                new ReportTag
                {
                    ReportId = 1,
                    TagId = 8,
                });
            modelBuilder.Entity<ReportTag>().HasData(
                new ReportTag
                {
                    ReportId = 1,
                    TagId = 3,
                });
            modelBuilder.Entity<ReportTag>().HasData(
                new ReportTag
                {
                    ReportId = 2,
                    TagId = 3,
                });
            modelBuilder.Entity<ReportTag>().HasData(
                new ReportTag
                {
                    ReportId = 18,
                    TagId = 9,
                });
            modelBuilder.Entity<ReportTag>().HasData(
                new ReportTag
                {
                    ReportId = 18,
                    TagId = 10,
                });
            modelBuilder.Entity<ReportTag>().HasData(
                new ReportTag
                {
                    ReportId = 20,
                    TagId = 9,
                });
            modelBuilder.Entity<ReportTag>().HasData(
                new ReportTag
                {
                    ReportId = 9,
                    TagId = 7,
                });
            modelBuilder.Entity<ReportTag>().HasData(
                new ReportTag
                {
                    ReportId = 21,
                    TagId = 2,
                });
            modelBuilder.Entity<ReportTag>().HasData(
                new ReportTag
                {
                    ReportId = 23,
                    TagId = 7,
                });
            //Seed IndustrySubscriptions

            modelBuilder.Entity<IndustrySubscription>().HasData(
                new IndustrySubscription
                {
                    IndustryId = 3,
                    UserId = 6,
                });
            modelBuilder.Entity<IndustrySubscription>().HasData(
                new IndustrySubscription
                {
                    IndustryId = 2,
                    UserId = 6,
                });
            modelBuilder.Entity<IndustrySubscription>().HasData(
                new IndustrySubscription
                {
                    IndustryId = 5,
                    UserId = 6,
                });
            modelBuilder.Entity<IndustrySubscription>().HasData(
                new IndustrySubscription
                {
                    IndustryId = 9,
                    UserId = 6,
                });
            modelBuilder.Entity<IndustrySubscription>().HasData(
                new IndustrySubscription
                {
                    IndustryId = 8,
                    UserId = 8,
                });
            modelBuilder.Entity<IndustrySubscription>().HasData(
                new IndustrySubscription
                {
                    IndustryId = 11,
                    UserId = 10,
                });
            modelBuilder.Entity<IndustrySubscription>().HasData(
                new IndustrySubscription
                {
                    IndustryId = 10,
                    UserId = 10,
                });
            modelBuilder.Entity<IndustrySubscription>().HasData(
                new IndustrySubscription
                {
                    IndustryId = 1,
                    UserId = 10,
                });
            modelBuilder.Entity<IndustrySubscription>().HasData(
                new IndustrySubscription
                {
                    IndustryId = 4,
                    UserId = 10,
                });
            modelBuilder.Entity<IndustrySubscription>().HasData(
                new IndustrySubscription
                {
                    IndustryId = 2,
                    UserId = 10,
                });
            modelBuilder.Entity<IndustrySubscription>().HasData(
                new IndustrySubscription
                {
                    IndustryId = 2,
                    UserId = 8,
                });
            modelBuilder.Entity<IndustrySubscription>().HasData(
                new IndustrySubscription
                {
                    IndustryId = 4,
                    UserId = 8,
                });
            modelBuilder.Entity<IndustrySubscription>().HasData(
                new IndustrySubscription
                {
                    IndustryId = 9,
                    UserId = 8,
                });
            modelBuilder.Entity<IndustrySubscription>().HasData(
                new IndustrySubscription
                {
                    IndustryId = 1,
                    UserId = 7,
                });
            modelBuilder.Entity<IndustrySubscription>().HasData(
                new IndustrySubscription
                {
                    IndustryId = 5,
                    UserId = 7,
                });
            modelBuilder.Entity<IndustrySubscription>().HasData(
                new IndustrySubscription
                {
                    IndustryId = 7,
                    UserId = 7,
                });
            modelBuilder.Entity<IndustrySubscription>().HasData(
                new IndustrySubscription
                {
                    IndustryId = 9,
                    UserId = 7,
                });
            modelBuilder.Entity<IndustrySubscription>().HasData(
                new IndustrySubscription
                {
                    IndustryId = 1,
                    UserId = 9,
                });
            modelBuilder.Entity<IndustrySubscription>().HasData(
                new IndustrySubscription
                {
                    IndustryId = 3,
                    UserId = 9,
                });
            modelBuilder.Entity<IndustrySubscription>().HasData(
                new IndustrySubscription
                {
                    IndustryId = 7,
                    UserId = 9,
                });
            modelBuilder.Entity<IndustrySubscription>().HasData(
                new IndustrySubscription
                {
                    IndustryId = 6,
                    UserId = 9,
                });
            modelBuilder.Entity<IndustrySubscription>().HasData(
                new IndustrySubscription
                {
                    IndustryId = 14,
                    UserId = 9,
                });
            //Seed DownloadedReports

            modelBuilder.Entity<DownloadedReport>().HasData(
                new DownloadedReport
                {
                    ReportId = 5,
                    UserId = 6,
                });

            modelBuilder.Entity<DownloadedReport>().HasData(
                new DownloadedReport
                {
                    ReportId = 7,
                    UserId = 6,
                });

            modelBuilder.Entity<DownloadedReport>().HasData(
                new DownloadedReport
                {
                    ReportId = 18,
                    UserId = 6,
                });
            modelBuilder.Entity<DownloadedReport>().HasData(
                new DownloadedReport
                {
                    ReportId = 9,
                    UserId = 6,
                });
            modelBuilder.Entity<DownloadedReport>().HasData(
                new DownloadedReport
                {
                    ReportId = 13,
                    UserId = 7,
                });
            modelBuilder.Entity<DownloadedReport>().HasData(
                new DownloadedReport
                {
                    ReportId = 23,
                    UserId = 7,
                });
            modelBuilder.Entity<DownloadedReport>().HasData(
                new DownloadedReport
                {
                    ReportId = 5,
                    UserId = 7,
                });
            modelBuilder.Entity<DownloadedReport>().HasData(
                new DownloadedReport
                {
                    ReportId = 18,
                    UserId = 7,
                });
            modelBuilder.Entity<DownloadedReport>().HasData(
                new DownloadedReport
                {
                    ReportId = 14,
                    UserId = 8,
                });
            modelBuilder.Entity<DownloadedReport>().HasData(
                new DownloadedReport
                {
                    ReportId = 3,
                    UserId = 8,
                });
            modelBuilder.Entity<DownloadedReport>().HasData(
                new DownloadedReport
                {
                    ReportId = 18,
                    UserId = 8,
                });
            modelBuilder.Entity<DownloadedReport>().HasData(
                new DownloadedReport
                {
                    ReportId = 4,
                    UserId = 8,
                });
            modelBuilder.Entity<DownloadedReport>().HasData(
                new DownloadedReport
                {
                    ReportId = 13,
                    UserId = 9,
                });
            modelBuilder.Entity<DownloadedReport>().HasData(
                new DownloadedReport
                {
                    ReportId = 18,
                    UserId = 9,
                });
            modelBuilder.Entity<DownloadedReport>().HasData(
                new DownloadedReport
                {
                    ReportId = 6,
                    UserId = 9,
                });
            modelBuilder.Entity<DownloadedReport>().HasData(
                new DownloadedReport
                {
                    ReportId = 8,
                    UserId = 9,
                });
            modelBuilder.Entity<DownloadedReport>().HasData(
                new DownloadedReport
                {
                    ReportId = 5,
                    UserId = 10,
                });
            modelBuilder.Entity<DownloadedReport>().HasData(
                new DownloadedReport
                {
                    ReportId = 19,
                    UserId = 10,
                });

            modelBuilder.Entity<DownloadedReport>().HasData(
                new DownloadedReport
                {
                    ReportId = 9,
                    UserId = 10,
                });

            modelBuilder.Entity<DownloadedReport>().HasData(
                new DownloadedReport
                {
                    ReportId = 18,
                    UserId = 10,
                });
        }
    }
}

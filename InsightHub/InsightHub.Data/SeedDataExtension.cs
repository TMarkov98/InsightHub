using InsightHub.Data.Entities;
using InsightHub.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
                SecurityStamp = "7I5VNHIJTSZNOT3KDWKNFUV5PVYBHGXN",
                IsPending = false,
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
                SecurityStamp = "7I5VNHIJTSZNOT3KDWKNFUV5PVYBHGXV",
                IsPending = false,
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
                SecurityStamp = "7I5VNHIJTSZNOT3KDWKNFUV5PVYBHGXF",
                IsPending = false,
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
                    Title = "National Economic Outlook",
                    Description = "Real GDP contracted 4.8 percent at an annualized rate in the first quarter according to the advance estimate from the Bureau of Economic Analysis, worse than the consensus expectation for a 4.0 percent decline. The coronavirus pandemic and restrictions on movement led to big declines in consumer spending and business investment (down 7.6 percent and 8.6 percent annualized, respectively).",
                    Summary = "April 2020 National Economic Outlook by PINACLE",
                    AuthorId = 2,
                    IndustryId = 27,
                    ImgUrl = "https://www.agenda-bg.com/wp-content/uploads/2016/09/What-To-Expect-In-2015-An-Economic-Outlook-For-Small-And-Medium-Businesses-Making-It-TV.jpg",
                    CreatedOn = DateTime.UtcNow,
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
                    AuthorId = 2,
                    IndustryId = 27,
                    ImgUrl = "https://www.questionpro.com/blog/wp-content/uploads/2018/05/Market-Survey_Final-800x478.jpg",
                    CreatedOn = DateTime.UtcNow,
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
                    IndustryId = 27,
                    ImgUrl = "https://images.financialexpress.com/2019/06/ECONOMIC_SURVEY_660.jpg",
                    CreatedOn = DateTime.UtcNow,
                    IsFeatured = true,
                    IsPending = false,
                });

            modelBuilder.Entity<Report>().HasData(
                new Report
                {
                    Id = 4,
                    Title = "Aerospace Industry 2012 Report",
                    Description = "The Aerospace Industries Association (AIA) is the most authoritative and influential trade association representing the aerospace and defense industry. We are the leading voice for the industry on Capitol Hill, within the administration, and internationally.",
                    Summary = "Aerospace Industry Report 3rd Edition by The Aerospace Industries Association (AIA)",
                    AuthorId = 2,
                    IndustryId = 2,
                    ImgUrl = "https://cdn.canadianmetalworking.com/a/aerospace-sector-report-a-jetstream-of-trends-1489163413.jpg?size=1000x1000",
                    CreatedOn = DateTime.UtcNow,
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
                    AuthorId = 2,
                    IndustryId = 3,
                    ImgUrl = "https://www.aljazeera.com/mritems/Images/2019/12/4/b604dd8d57c942a892cdc71f21d09973_18.jpg",
                    CreatedOn = DateTime.UtcNow,
                    IsPending = false,
                });

            modelBuilder.Entity<Report>().HasData(
                new Report
                {
                    Id = 6,
                    Title = "Five Trends Transforming the Automotive Industry",
                    Description = "Since the introduction of the smartphone, it has become clear that customers are quick to adopt even highly complex and expensive technology if it makes their lives easier. In other words, users value convenience and ease. These core values turned the automobile into the defining technical cultural item of the 20th century. Now it is time to translate these properties into the context of today's – and tomorrow's –  technology and society. ",
                    Summary = "Welcome to the age of radical change in the automotive industry",
                    AuthorId = 2,
                    IndustryId = 4,
                    ImgUrl = "https://eenews.cdnartwhere.eu/sites/default/files/styles/inner_article/public/sites/default/files/images/2019-10-17_automotive_cybersecurity_hacking_software_dark_web_intsights.jpg?itok=x-9upxnf",
                    CreatedOn = DateTime.UtcNow,
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
                    AuthorId = 2,
                    IndustryId = 5,
                    ImgUrl = "https://www.assignmentpoint.com/wp-content/uploads/2013/04/E-Banking-in-Bangladesh.jpg",
                    CreatedOn = DateTime.UtcNow,
                    IsPending = false,
                });

            modelBuilder.Entity<Report>().HasData(
                new Report
                {
                    Id = 8,
                    Title = "The British Pub Market 2019",
                    Description = "The report is divided into three sections. The first part reviews the supply side of the pub market, revealing numbers, trends and the contrasting fortunes of different sectors, and identifying some of the areas in which pubs are succeeding. The second section analyses the customer base: their demographics, habits and motivations. The final part takes a look at reasons for optimism, with insights into increasing appeal and sales and the emerging new breeds of pub in Britain. At a time of great challenges for both the out of home eating and drinking market and the UK economy as a whole, this report highlights the many positive trends and developments in the British pub market. We hope you enjoy reading it.",
                    Summary = "This report draws on CGA’s unrivalled suite of research services to provide a comprehensive picture of Great Britain’s pubs and their opportunities for growth",
                    AuthorId = 2,
                    IndustryId = 6,
                    ImgUrl = "https://imagesvc.meredithcorp.io/v3/mm/image?url=https%3A%2F%2Fstatic.onecms.io%2Fwp-content%2Fuploads%2Fsites%2F9%2F2019%2F12%2Fuk-pubs-growth-FT-BLOG1219.jpg&q=85",
                    CreatedOn = DateTime.UtcNow,
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
                    AuthorId = 2,
                    IndustryId = 7,
                    ImgUrl = "https://lh3.googleusercontent.com/proxy/yv-Fqqhl26oZXf_TqO-irARoppooPkNe6DcoAoldvOSdkXl42fpaATpfcnqeuCs7qyZ_lv40MmzP2DikBzqshhJAhaXb9fi0gaDOCAEE0W6rtkbyUMYPDlYu8ixTEfPHorTtC8PB",
                    CreatedOn = DateTime.UtcNow,
                    IsPending = false,
                });

            modelBuilder.Entity<Report>().HasData(
                new Report
                {
                    Id = 10,
                    Title = "Professional and Business Services Sector Report",
                    Description = "This report covers Audit and accounting; Business services; Professional services; and Legal services.",
                    Summary = "This report covers: a description of the sector, the current EU regulatory regime, existing frameworks for how trade is facilitated between countries in this sector, and sector views. It does not contain commercially-, market-or negotiation-sensitive information.",
                    AuthorId = 2,
                    IndustryId = 8,
                    ImgUrl = "https://www.antal.com/uploads/library/files/Business-Services.jpg",
                    CreatedOn = DateTime.UtcNow,
                    IsPending = false,
                });

            modelBuilder.Entity<Report>().HasData(
                new Report
                {
                    Id = 11,
                    Title = "Casino Gambling in America and Its Economic Impacts",
                    Description = "Casino Gambling has become a majorindustry in the United States over the past twodecades.  Nationwide, annual casino revenue tops $40billion. This report provides an analysis of casino gam-bling in the United States and discusses the economicissues surrounding casino gambling. The informationcontained in this report should prove useful to localofficials and policy-makers who may be considering theadoption of casino gambling or who already have casi-no gambling in their jurisdictions.",
                    Summary = "This report provides an analysis of casino gam - bling in the United States and discusses the economicissues surrounding casino gambling.",
                    AuthorId = 2,
                    IndustryId = 9,
                    ImgUrl = "https://royalepalmscasino-sofia.com/wp-content/uploads/2019/04/IMG_1710-1024x683.jpg",
                    CreatedOn = DateTime.UtcNow,
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
                    AuthorId = 2,
                    IndustryId = 10,
                    ImgUrl = "https://fayranches.com/wp-content/uploads/2016/01/savvy-investors-cattle-ranches.jpg",
                    CreatedOn = DateTime.UtcNow,
                    IsPending = false,
                });

            modelBuilder.Entity<Report>().HasData(
                new Report
                {
                    Id = 13,
                    Title = "Sustainability of the Fashion Industry",
                    Description = "The way we make, use and throwaway our clothes is unsustainable. Textile production contributes more to climate change than international aviation and shipping combined, consumes lake-sized volumes of fresh water and creates chemical and plastic pollution. Synthetic fibres are being found in the deep sea, in Arctic sea ice, in fish and shellfish. Our biggest retailers have ‘chased the cheap needle around the planet’, commissioning production  in  countries  with  low  pay,  little  trade  union  representation  and  weak  environmental protection. In many countries, poverty pay and conditions are standard for garment workers, most of whom are women. We are also concerned about the use of  child  labour,  prison  labour,  forced  labour  and  bonded  labour  in  factories  and  the  garment supply chain. Fast fashions’ overproduction and overconsumption of clothing is based on the globalisation of indifference towards these manual workers.",
                    Summary = "Fixing Fashion: Clothing Consumption and Sustainability",
                    AuthorId = 2,
                    IndustryId = 11,
                    ImgUrl = "https://dynaimage.cdn.cnn.com/cnn/c_fill,g_auto,w_1200,h_675,ar_16:9/https%3A%2F%2Fcdn.cnn.com%2Fcnnnext%2Fdam%2Fassets%2F190912152727-uglyleaddd.jpg",
                    CreatedOn = DateTime.UtcNow,
                    IsPending = false,
                });

            modelBuilder.Entity<Report>().HasData(
                new Report
                {
                    Id = 14,
                    Title = "The Change in the Australian Work Force Since the End of World War 2",
                    Description = "This report discusses the changes that have occurred in the Australian workforce since the end of World War II (1945-2000). A review of some of the available literature provides insights into the changing role of women and migrants in the workforce, and the influence of new technologies and changing levels of unemployment have also been considered.",
                    Summary = "The information presented in this report has been gathered from secondary sources, and from Australian Bureau of Statistics’ data.",
                    AuthorId = 2,
                    IndustryId = 12,
                    ImgUrl = "https://upload.wikimedia.org/wikipedia/commons/1/1a/Chapin_Hall%2C_Williams_College_-_Williamstown%2C_Massachusetts.jpg",
                    CreatedOn = DateTime.UtcNow,
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
                    ImgUrl = "https://lh3.googleusercontent.com/proxy/ii7FxKnpaVHEW-pQ2JB5BikxCX8RvaXGzDlT41yX3mN9eDfvQjJoJhaCe0lc9kVmLFwkIAoqFF0y58zYnnYiSQwQBB_SVD9G_yn8KJNGls_2vL4bKA",
                    CreatedOn = DateTime.UtcNow,
                    IsPending = false,
                });

            modelBuilder.Entity<Report>().HasData(
                new Report
                {
                    Id = 16,
                    Title = "Los Angeles College Monthly Construction Report",
                    Description = "Currently, Construction is 99.9% complete. Commissioning Pre-functional test was completed by ARUP and draft report has been received. MEOR is scheduled to review and evaluate draft report from ARUP. Once MEOR reviews draft report, ARUP will issue final  Commissioning  report.  Substantial  Completion  is  estimated  to  be  issued  in  August,  after  the  issuance  of the final Commissioning report and Punch list items are issued to the Contractor.",
                    Summary = "Los Angeles Mission College Gateway Science and Engineering Construction Management Monthly Progress Report",
                    AuthorId = 2,
                    IndustryId = 14,
                    ImgUrl = "https://specials-images.forbesimg.com/imageserve/5c0077cc31358e5b43383ffc/960x0.jpg?fit=scale",
                    CreatedOn = DateTime.UtcNow,
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
                    IndustryId = 15,
                    ImgUrl = "https://media.nationalgeographic.org/assets/photos/120/983/091a0e2f-b93d-481b-9a60-db520c87ec33.jpg",
                    CreatedOn = DateTime.UtcNow,
                    IsPending = false,
                });

            modelBuilder.Entity<Report>().HasData(
                new Report
                {
                    Id = 18,
                    Title = "2019 Cruise Trends & Industry Outlook",
                    Description = "Cruise Lines International Association (CLIA), the world’s largest cruise industry trade association, has released the 2019 Cruise Trends and State of the Cruise Industry Outlook. The report offers a look at the trends impacting cruise travel in the coming year and beyond as well as the overall global economic impact. Cruise Lines International Association (CLIA) is the unified global organizationhelping members succeed by advocating, educating and promoting for the common interests of the cruise community.",
                    Summary = "Instagram photos are driving interest in travel around the world. With onboard connectivity, cruise passengers are filling Instagram feeds with diverse travel experiences both onboard and on land from several cruise destinations.",
                    AuthorId = 2,
                    IndustryId = 16,
                    ImgUrl = "https://www.gannett-cdn.com/presto/2018/12/20/USAT/b3fa36d4-b0ed-48c5-a991-924c48685469-Costa_LNG__Side_Perspective.jpg?crop=2999,1687,x0,y299&width=2999&height=1687&format=pjpg&auto=webp",
                    CreatedOn = DateTime.UtcNow,
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
                    IndustryId = 17,
                    ImgUrl = "https://lh3.googleusercontent.com/proxy/B2Ha-PwtOsxqi3MXwm2SCOWM6CxKChDn4KM5xQ3G58LEULJkzZAS68_6Dz5k00fyK3WIca58WzfewwtRH5khlEgdF69EPlFKQ3QtiJF4KQGXKxv1v76dGKsixlpP",
                    CreatedOn = DateTime.UtcNow,
                    IsPending = false,
                });

            //modelBuilder.Entity<Report>().HasData(
            //    new Report
            //    {
            //        Id = 7,
            //        Title = "",
            //        Description = "",
            //        Summary = "",
            //        AuthorId = 2,
            //        IndustryId = 5,
            //        ImgUrl = "",
            //        CreatedOn = DateTime.UtcNow,
            //        IsPending = false,
            //    });
            //Seed Industries

            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 1,
                    Name = "Accountants",
                    ImgUrl = "https://help-investor.com/wp-content/uploads/2018/07/Accountant.jpg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 2,
                    Name = "Aerospace",
                    ImgUrl = "https://www.henkel.com/resource/image/946964/4x3/1120/840/e9b0d415e2ab94d019b20e32cf0f015/el/aerospace-industry-growth.jpg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 3,
                    Name = "Airlines (Air Transport)",
                    ImgUrl = "https://media.foxbusiness.com/BrightCove/854081161001/202005/2760/854081161001_6157982707001_6157986352001-vs.jpg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 4,
                    Name = "Automotive",
                    ImgUrl = "https://j2offshore.com/wp-content/uploads/2018/05/automative-2.jpg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 5,
                    Name = "Banking",
                    ImgUrl = "https://studyabroad.bg/wp-content/uploads/2016/04/Bank.jpg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 6,
                    Name = "Bars & Restaurants",
                    ImgUrl = "https://goguide.bg/upload/places/inner/1576236597Embassy.png",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 7,
                    Name = "Building Materials & Equipment",
                    ImgUrl = "https://rr-hk.com/Building_material/img/62614691_custom.jpg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 8,
                    Name = "Business Services",
                    ImgUrl = "https://ftguidetobusinesstraining.com/wp-content/uploads/2018/08/Business-Services01.jpg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 9,
                    Name = "Casinos & Gambling",
                    ImgUrl = "https://q-cf.bstatic.com/images/hotel/max1024x768/240/240066005.jpg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 10,
                    Name = "Cattle, Livestock & Ranching",
                    ImgUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d4/CH_cow_2_cropped.jpg/1200px-CH_cow_2_cropped.jpg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 11,
                    Name = "Clothing & Fashion",
                    ImgUrl = "https://scstylecaster.files.wordpress.com/2018/08/nyfw-fi.png",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 12,
                    Name = "Colleges, Universities & Schools",
                    ImgUrl = "https://cfhsprowler.com/wp-content/uploads/2020/01/download.jpg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 13,
                    Name = "Computers & Software",
                    ImgUrl = "https://ctbhost.com/wp-content/uploads/2019/07/computer-internet-franchise-1.jpg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 14,
                    Name = "Construction Services",
                    ImgUrl = "https://s27389.pcdn.co/wp-content/uploads/2019/07/digital-transformation-construction-industry-1024x440.jpeg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 15,
                    Name = "Crop Production & Processing",
                    ImgUrl = "https://www.netafim.com/48da28/globalassets/demo/products-and-solutions/open-fields/open_fields_headvisual-graded.jpg?height=620&width=1440&mode=crop&quality=80",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 16,
                    Name = "Cruise Ships & Lines",
                    ImgUrl = "https://www.maritime-executive.com/media/images/article/Photos/Cruise_Ships/Diamond-Princess-sailing-in-Japan-courtesy-Princess-Cruises.78319a.jpg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 17,
                    Name = "Dairy",
                    ImgUrl = "https://lh3.googleusercontent.com/proxy/B2Ha-PwtOsxqi3MXwm2SCOWM6CxKChDn4KM5xQ3G58LEULJkzZAS68_6Dz5k00fyK3WIca58WzfewwtRH5khlEgdF69EPlFKQ3QtiJF4KQGXKxv1v76dGKsixlpP",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 18,
                    Name = "Defense",
                    ImgUrl = "https://idsb.tmgrup.com.tr/2015/10/05/GenelBuyuk/1443984579449_rs.jpg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 19,
                    Name = "Dentists",
                    ImgUrl = "https://www.orthodonticslimited.com/wp-content/uploads/2017/12/Depositphotos_dentisttool.jpg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 20,
                    Name = "Doctors & Health Professionals",
                    ImgUrl = "https://miro.medium.com/max/3200/1*afTS3knLUSHCJDirSOlq1g.jpeg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 21,
                    Name = "Education",
                    ImgUrl = "https://www.healthcaresalestraining.com/photo/5.jpg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 22,
                    Name = "Electronics Manufacturing & Equipment",
                    ImgUrl = "https://www.e-spincorp.com/wp-content/uploads/2019/02/laser-micro-machining-electrical-industry-e1549935628710.jpg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 23,
                    Name = "Energy & Natural Resources",
                    ImgUrl = "https://www.power-technology.com/wp-content/uploads/sites/7/2018/01/Renewable_Energy_on_the_Grid.jpg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 24,
                    Name = "Entertainment",
                    ImgUrl = "https://www.marketingtochina.com/wp-content/uploads/2017/08/seo-marketing.jpg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 25,
                    Name = "Environment",
                    ImgUrl = "https://lh3.googleusercontent.com/proxy/Zm1otIkpcrSH9UHtVeqp3QWllwBRaPjVWkRHlKMDCT88mbJkkj7xzg1qO87ZdpMsFkhdfl8DoVSbQhbyitaQYqSDepQ_fpFWF0z8zXjWMz2AqN3jtOXcJhqYLBkV14SGJQGPDcLqIF-_OdlxCgoOlSG97VZdLubN",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 26,
                    Name = "Farms",
                    ImgUrl = "https://upload.wikimedia.org/wikipedia/commons/7/7e/Farm_Kartoffelfeld_Schweden.jpg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 27,
                    Name = "Finance & Credit",
                    ImgUrl = "https://www.newdma.org/wp-content/uploads/2015/01/Digital-marketing-for-the-Finance-industry.jpg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 28,
                    Name = "Food & Beverage",
                    ImgUrl = "https://miro.medium.com/max/5120/1*YENSrU0nehgyGk6W-G8Rpg.jpeg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 29,
                    Name = "Food Processing & Sales",
                    ImgUrl = "https://www.makeinethiopia.com/wp-content/uploads/2018/06/Food-Processing.png",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 30,
                    Name = "For-Profit Prisons",
                    ImgUrl = "https://news.northeastern.edu/wp-content/uploads/2016/07/prison_money-800x0-c-default.jpg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 31,
                    Name = "Forestry & Forest Products",
                    ImgUrl = "https://wallenius-sol.com/sites/wallenius-sol.com/files/the-forest-industry_photo-shutterstock_wallenius-sol_the-enabler_1920x1080px.jpg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 32,
                    Name = "Funeral Services",
                    ImgUrl = "https://wiedemanfuneralhome.com/4640/Ultra/Woodbridge_Gardening.jpg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 33,
                    Name = "Gas & Oil",
                    ImgUrl = "https://plsadaptive.s3.amazonaws.com/eco/images/channel_content/images/offshore_platform.jpg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 34,
                    Name = "General Contractors",
                    ImgUrl = "https://i.pinimg.com/originals/da/9b/60/da9b60f63d0957cc785967b309c7d775.jpg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 35,
                    Name = "Health Care",
                    ImgUrl = "https://geekculturepodcast.com/wp-content/uploads/2019/05/Healthcare-Industry.jpg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 36,
                    Name = "Hospitals & Nursing Homes",
                    ImgUrl = "https://cdn.aiidatapro.net/media/ec/a5/90/t780x490/eca590e038af4adfb46a726b3c0f9a98.jpg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 37,
                    Name = "Hotels, Motels, Tourism",
                    ImgUrl = "https://miro.medium.com/max/960/1*VghRFv3Ejzs18nJ0MHzg9g.jpeg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 38,
                    Name = "Human Rights",
                    ImgUrl = "https://multimedia.europarl.europa.eu/documents/20143/0/AP_97209887+%281%29.jpg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 39,
                    Name = "Insurance",
                    ImgUrl = "https://synchronium.io/wp-content/uploads/2019/02/blockchain-insurance-619x410.jpg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 40,
                    Name = "Labor",
                    ImgUrl = "https://w7.pngwing.com/pngs/595/460/png-transparent-industry-businessperson-labor-industrail-workers-and-engineers-miscellaneous-people-employment.png",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 41,
                    Name = "Liquor, Wine & Beer",
                    ImgUrl = "https://www.wallpaperup.com/uploads/wallpapers/2015/06/02/708905/8a78abe919157ab98171b9aa86e21eb7-700.jpg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 42,
                    Name = "Meat Processing & Products",
                    ImgUrl = "https://blogs.3ds.com/delmia/wp-content/uploads/sites/24/2018/06/meat-disassembly-blog.jpg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 43,
                    Name = "Music & Production",
                    ImgUrl = "https://www.tibco.com/blog/wp-content/uploads/2015/03/How-The-Music-Industry-is-Getting-Smarter.jpg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 44,
                    Name = "Newspaper, Magazine & Book Publishing",
                    ImgUrl = "https://lh3.googleusercontent.com/proxy/nYMe910fGr1aP9-4Vvjhd61EQ1vOgqPcfEcl9LsLAE-7jVV6-eadYp0lnn0v4BBtuY8mmzU1tUMEQG9qBPAHgYarOEk7VnCP3VpDb_Yg8BcMGrTbDHQH97ffNdBH-5w4ee7T4KTUfYD3WF0dfJ7YPcdNu50Uy1XE4tt9MA",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 45,
                    Name = "Nutritional & Dietary Supplements",
                    ImgUrl = "https://burdockgroup.com/wp-content/uploads/2018/05/dietary-supplements-wooden-spoons-copy.jpg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 46,
                    Name = "Other",
                    ImgUrl = "https://cdn.pixabay.com/photo/2015/11/03/08/56/question-mark-1019820_1280.jpg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 47,
                    Name = "Pharmaceuticals",
                    ImgUrl = "https://www.digitalais.com/wp-content/uploads/2017/06/Farmaceutick%C3%BD-pr%C5%AFmysl.jpg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 48,
                    Name = "Phone Companies",
                    ImgUrl = "https://www.marketingchina.agency/wp-content/uploads/2018/02/mobile-phone-industry-500x329.jpg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 49,
                    Name = "Postage & Postal Services",
                    ImgUrl = "https://www.uoe.co.uk/wp-content/uploads/2018/07/Post-box-1.jpg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 50,
                    Name = "Printing & Publishing",
                    ImgUrl = "https://erp.bg/wp-content/uploads/2017/05/1-print-industry_2.gif",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 51,
                    Name = "Power Utilities",
                    ImgUrl = "https://assets.kpmg/content/dam/kpmg/images/2015/02/electric-power-lines.jpg/jcr:content/renditions/original",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 52,
                    Name = "Private Investment",
                    ImgUrl = "https://w7.pngwing.com/pngs/734/1/png-transparent-business-interior-design-services-building-private-equity-money-business-building-people-investment.png",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 53,
                    Name = "Radio/TV",
                    ImgUrl = "https://userscontent2.emaze.com/images/52feb565-01b5-4632-ae33-217f1becc980/329bdafc34fc27857aa6f2a0cb0168a0.jpg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 54,
                    Name = "Railroads",
                    ImgUrl = "https://lh3.googleusercontent.com/proxy/V8LQ-lwAm3Ra4I_PvVE6U4WahtpFtpHwXd6BU77kyUPo0a-x0KXYBdRJSMuXxX85yaGNNIqczkOUCaq35M_4Qbh2YKUyJeMVn9yU01ywE9DuAi7mEZg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 55,
                    Name = "Real Estate",
                    ImgUrl = "https://miro.medium.com/max/660/1*XaXQRJiI5BSFY3uFhTFCAg.jpeg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 56,
                    Name = "Record Companies & Singers",
                    ImgUrl = "https://lh3.googleusercontent.com/proxy/u9QC1f3VUFnFbVfG8l7YXkRA-f7a8MwWjt9pD7doK-Taz9IiHr0klERuKsJF9tqnIEwjBIGZMhC2Ki5T6m9Sda8HNbtLT9XPZmKxz0BY9ikRVuKuq6kcWwfs60Iur1SGORnHJYo",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 57,
                    Name = "Recreation & Live Entertainment",
                    ImgUrl = "https://bookingprotect.com/wp-content/uploads/2019/08/live_entertainment.jpg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 58,
                    Name = "Retail Sales",
                    ImgUrl = "https://w0.pngwave.com/png/303/783/select-sweets-retail-sales-industry-ecommerce-payment-system-png-clip-art.png",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 59,
                    Name = "Sports, Arenas & Equipment",
                    ImgUrl = "https://www.liberty.edu/champion/wp-content/uploads/2020/03/SPORTS-800x280.jpg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 60,
                    Name = "Stock Brokers & Investment",
                    ImgUrl = "https://www.usnews.com/dims4/USNEWS/01e1de6/2147483647/thumbnail/640x420/quality/85/?url=http%3A%2F%2Fmedia.beam.usnews.com%2F00%2F58%2F5926109d42de9f633868cd479f65%2F190111-investingsectors-stock.jpg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 61,
                    Name = "Student Loans",
                    ImgUrl = "https://thehustle.co/wp-content/uploads/2019/02/share_me_now_brief_2019-02-27T021549.567Z-1.jpg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 62,
                    Name = "Telecom Services & Equipment",
                    ImgUrl = "https://plug-n-score.com/wp-content/files/2014/08/credit-in-the-telecom-industry.png",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 63,
                    Name = "Textiles",
                    ImgUrl = "https://www.see-industry.com/Files/statii/22017_02_Industry_Textile.jpg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 64,
                    Name = "Tobacco",
                    ImgUrl = "https://daxueconsulting.com/wp-content/uploads/2016/01/Daxue-Consulting-China-Tobacco-Industry-Market-Research.jpg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 65,
                    Name = "Transportation",
                    ImgUrl = "https://www.odtap.com/wp-content/uploads/2019/03/5-Top-Technology-Trends-in-Transportation-and-Logistics-Industry.png",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 66,
                    Name = "Truckery",
                    ImgUrl = "https://s29755.pcdn.co/wp-content/uploads/2019/08/2019_Top_Five_Class_5-Mack.jpg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 67,
                    Name = "TV Production",
                    ImgUrl = "https://lh3.googleusercontent.com/proxy/YZfiizLwa44Nzb58s3cKhWFgUbFevmGdK9QJQbaC7ASQtfVniCSHHZBL3RFv_e7XBowp4mSE-T6FyJvgTARAYSoJhFucvNE",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 68,
                    Name = "Unions",
                    ImgUrl = "https://www.unionindustry.eu/assets/uploads/2010/10/jobs-ants.jpg",
                    CreatedOn = DateTime.UtcNow
                }
                );

            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 69,
                    Name = "Vegetables & Fruits",
                    ImgUrl = "https://www.vegan.io/blog/assets/10-healthiest-vegetables-to-include-in-your-vegan-diet-2018-04-16/healthiest-vegetables-df1cf550711076d052eaade12c38289a2637c38e546182d3c0136a90cb0bb0b3.jpg",
                    CreatedOn = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<Industry>().HasData(
                new Industry
                {
                    Id = 70,
                    Name = "Non-Profit",
                    ImgUrl = "https://www.wyzowl.com/wp-content/uploads/2017/05/The-20-Best-Nonprofit-Explainer-Videos-Youll-Ever-See-ffb612.png",
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
                    UserId = 1,
                });
            modelBuilder.Entity<IndustrySubscription>().HasData(
                new IndustrySubscription
                {
                    IndustryId = 2,
                    UserId = 1,
                });
            modelBuilder.Entity<IndustrySubscription>().HasData(
                new IndustrySubscription
                {
                    IndustryId = 1,
                    UserId = 2,
                });

            //Seed DownloadedReports

            modelBuilder.Entity<DownloadedReport>().HasData(
                new DownloadedReport
                {
                    ReportId = 1,
                    UserId = 1,
                });

            modelBuilder.Entity<DownloadedReport>().HasData(
                new DownloadedReport
                {
                    ReportId = 1,
                    UserId = 2,
                });

            modelBuilder.Entity<DownloadedReport>().HasData(
                new DownloadedReport
                {
                    ReportId = 1,
                    UserId = 3,
                });
            modelBuilder.Entity<DownloadedReport>().HasData(
                new DownloadedReport
                {
                    ReportId = 2,
                    UserId = 1,
                });
            modelBuilder.Entity<DownloadedReport>().HasData(
                new DownloadedReport
                {
                    ReportId = 2,
                    UserId = 2,
                });
        }
    }
}

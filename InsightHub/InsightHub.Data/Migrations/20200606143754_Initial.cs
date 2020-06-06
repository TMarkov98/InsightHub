using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InsightHub.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Industries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    ImgUrl = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Industries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: false),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    RoleId = table.Column<int>(nullable: true),
                    IsPending = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    IsBanned = table.Column<bool>(nullable: false),
                    BanReason = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IndustrySubscriptions",
                columns: table => new
                {
                    IndustryId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndustrySubscriptions", x => new { x.UserId, x.IndustryId });
                    table.ForeignKey(
                        name: "FK_IndustrySubscriptions_Industries_IndustryId",
                        column: x => x.IndustryId,
                        principalTable: "Industries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IndustrySubscriptions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    Summary = table.Column<string>(maxLength: 300, nullable: false),
                    Description = table.Column<string>(maxLength: 5000, nullable: false),
                    AuthorId = table.Column<int>(nullable: false),
                    IndustryId = table.Column<int>(nullable: false),
                    ImgUrl = table.Column<string>(nullable: true),
                    IsPending = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: false),
                    IsFeatured = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reports_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reports_Industries_IndustryId",
                        column: x => x.IndustryId,
                        principalTable: "Industries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DownloadedReports",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    ReportId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DownloadedReports", x => new { x.UserId, x.ReportId });
                    table.ForeignKey(
                        name: "FK_DownloadedReports_Reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Reports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DownloadedReports_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReportTags",
                columns: table => new
                {
                    TagId = table.Column<int>(nullable: false),
                    ReportId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportTags", x => new { x.ReportId, x.TagId });
                    table.ForeignKey(
                        name: "FK_ReportTags_Reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Reports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 3, "27084566-5f47-4c6f-a7ac-b9b63af1cf8b", "Client", "CLIENT" },
                    { 2, "5b4b3792-6c47-42d3-a76c-6c9c61ce7d8b", "Author", "AUTHOR" },
                    { 1, "5501448e-8adb-4dc7-87cd-6ca22327e918", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BanReason", "ConcurrencyStamp", "CreatedOn", "Email", "EmailConfirmed", "FirstName", "IsBanned", "IsPending", "LastName", "LockoutEnabled", "LockoutEnd", "ModifiedOn", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RoleId", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 3, 0, null, "f4b2e51b-11cf-43dc-9392-daf90cc41066", new DateTime(2020, 6, 6, 14, 37, 53, 832, DateTimeKind.Utc).AddTicks(7258), "client@gmail.com", false, "Clientcho", false, false, "Clientev", true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "CLIENT@GMAIL.COM", "CLIENT@GMAIL.COM", "AQAAAAEAACcQAAAAEAssErdvvJtoY3S8GN7OyBxrSDvWWnHgYlXKTWJ0LLKlAAAV0k159OLNs3dxL4vUNA==", null, false, null, "7I5VNHIJTSZNOT3KDWKNFUV5PVYBHGXF", false, "client@gmail.com" },
                    { 1, 0, null, "3e238fad-d037-4362-8310-28e21ec5d1f4", new DateTime(2020, 6, 6, 14, 37, 53, 832, DateTimeKind.Utc).AddTicks(5870), "admin@gmail.com", false, "Admincho", false, false, "Adminev", true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAEAACcQAAAAEM27T2m8uFGJLxBPn8EahPo0vCFMDYhouQiXixChFlNARU1dA4HmLcfp8ssZJSCLNQ==", null, false, null, "7I5VNHIJTSZNOT3KDWKNFUV5PVYBHGXN", false, "admin@gmail.com" },
                    { 2, 0, null, "c0f8d775-de47-4af9-afe6-87f01a3b29e3", new DateTime(2020, 6, 6, 14, 37, 53, 832, DateTimeKind.Utc).AddTicks(7234), "author@gmail.com", false, "Authorcho", false, false, "Authorchevski", true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AUTHOR@GMAIL.COM", "AUTHOR@GMAIL.COM", "AQAAAAEAACcQAAAAEIy39Aiba604h9P6jhnrCJDZofn6ILxNCPFr+BXQqFYjyKx4LkVW4diD6HEyfs9V+w==", null, false, null, "7I5VNHIJTSZNOT3KDWKNFUV5PVYBHGXV", false, "author@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Industries",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "ImgUrl", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 17, new DateTime(2020, 6, 6, 14, 37, 53, 855, DateTimeKind.Utc).AddTicks(7987), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://i.imgur.com/fJ5s33U.png", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Restaurants" },
                    { 16, new DateTime(2020, 6, 6, 14, 37, 53, 855, DateTimeKind.Utc).AddTicks(7974), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://i.imgur.com/GMNHXMs.png", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tourism" },
                    { 15, new DateTime(2020, 6, 6, 14, 37, 53, 855, DateTimeKind.Utc).AddTicks(7961), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://i.imgur.com/oWIukyA.png", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Food" },
                    { 14, new DateTime(2020, 6, 6, 14, 37, 53, 855, DateTimeKind.Utc).AddTicks(7948), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://i.imgur.com/5pZJcpA.png", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Energy" },
                    { 12, new DateTime(2020, 6, 6, 14, 37, 53, 855, DateTimeKind.Utc).AddTicks(7923), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://i.imgur.com/WeTQtUK.png", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Education" },
                    { 13, new DateTime(2020, 6, 6, 14, 37, 53, 855, DateTimeKind.Utc).AddTicks(7936), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://i.imgur.com/ApJqXTX.png", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Technology" },
                    { 10, new DateTime(2020, 6, 6, 14, 37, 53, 855, DateTimeKind.Utc).AddTicks(7896), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://i.imgur.com/TFcnTmD.png", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fashion" },
                    { 9, new DateTime(2020, 6, 6, 14, 37, 53, 855, DateTimeKind.Utc).AddTicks(7881), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://i.imgur.com/MIkU108.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Casino" },
                    { 8, new DateTime(2020, 6, 6, 14, 37, 53, 855, DateTimeKind.Utc).AddTicks(7868), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://i.imgur.com/0aD3uZj.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Agriculture" },
                    { 7, new DateTime(2020, 6, 6, 14, 37, 53, 855, DateTimeKind.Utc).AddTicks(7854), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://i.imgur.com/io9aGef.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Building" },
                    { 6, new DateTime(2020, 6, 6, 14, 37, 53, 855, DateTimeKind.Utc).AddTicks(7840), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://i.imgur.com/evwFFDj.png", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Space" },
                    { 5, new DateTime(2020, 6, 6, 14, 37, 53, 855, DateTimeKind.Utc).AddTicks(7824), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://i.imgur.com/MWr58IA.png", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Business Services" },
                    { 4, new DateTime(2020, 6, 6, 14, 37, 53, 855, DateTimeKind.Utc).AddTicks(7806), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://i.imgur.com/AO7gOGs.png", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Automobile" },
                    { 3, new DateTime(2020, 6, 6, 14, 37, 53, 855, DateTimeKind.Utc).AddTicks(7704), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://i.imgur.com/TC7qZPP.png", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Healthcare" },
                    { 2, new DateTime(2020, 6, 6, 14, 37, 53, 855, DateTimeKind.Utc).AddTicks(7675), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://i.imgur.com/dX9t5lS.png", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Airlines" },
                    { 11, new DateTime(2020, 6, 6, 14, 37, 53, 855, DateTimeKind.Utc).AddTicks(7909), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://i.imgur.com/ArAIUQ0.png", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Blockchain" },
                    { 1, new DateTime(2020, 6, 6, 14, 37, 53, 855, DateTimeKind.Utc).AddTicks(7088), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://i.imgur.com/8Rkh6JW.png", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Finance" }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 6, 6, 14, 37, 53, 855, DateTimeKind.Utc).AddTicks(9661), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Space" },
                    { 2, new DateTime(2020, 6, 6, 14, 37, 53, 856, DateTimeKind.Utc).AddTicks(205), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Water" },
                    { 3, new DateTime(2020, 6, 6, 14, 37, 53, 856, DateTimeKind.Utc).AddTicks(234), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Money" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { 3, 3 },
                    { 2, 2 },
                    { 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "IndustrySubscriptions",
                columns: new[] { "UserId", "IndustryId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "Id", "AuthorId", "CreatedOn", "DeletedOn", "Description", "ImgUrl", "IndustryId", "IsDeleted", "IsFeatured", "IsPending", "ModifiedOn", "Summary", "Title" },
                values: new object[,]
                {
                    { 1, 2, new DateTime(2020, 6, 6, 14, 37, 53, 855, DateTimeKind.Utc).AddTicks(2137), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Real GDP contracted 4.8 percent at an annualized rate in the first quarter according to the advance estimate from the Bureau of Economic Analysis, worse than the consensus expectation for a 4.0 percent decline. The coronavirus pandemic and restrictions on movement led to big declines in consumer spending and business investment (down 7.6 percent and 8.6 percent annualized, respectively).", "https://www.agenda-bg.com/wp-content/uploads/2016/09/What-To-Expect-In-2015-An-Economic-Outlook-For-Small-And-Medium-Businesses-Making-It-TV.jpg", 5, false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "April 2020 National Economic Outlook by PINACLE", "National Economic Outlook" },
                    { 19, 2, new DateTime(2020, 6, 6, 14, 37, 53, 855, DateTimeKind.Utc).AddTicks(3872), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This article is based on the IFCN Dairy Report 2019. This annual report summarises the work of IFCN Research Partners from over 100 countries. IFCN is a global network for dairy economic research and consultancy. In 2019, researchers from over 100 countries and more than 140 agribusiness companies are members of the network. IFCN has created a better understanding of the dairy world for 20 years. Key insights 2019 will be the year of lowest milk production growth since 2013. As this did not translate into milk price increases, IFCN identifies a structural drop in demand growth as one of the reasons. Milk production trends by regions are highly diverse and dynamic. The 3-5% rule indicates that strong regions grow and weak ones decline by this rate every year. Dairy farm structure dynamics drive milk supply and the speed of change is under-estimated. IFCN recommends using the annual growth of milk production per farm as an indicator. In the EU and the USA farms grew by 8% per year. The key driver for farm structure developments lies in dairy farm economics and the current structure of economies of scale. The Dairy Report analyses this in over 50 countries. The IFCN Dairy Report has been published annually since 2000 and has become a guideline publication for researchers and companies involved in the dairy chain. It enables to gain a global holistic view of the industry and serves as a solid fact base for discussions and strategic decisions.", "https://lh3.googleusercontent.com/proxy/B2Ha-PwtOsxqi3MXwm2SCOWM6CxKChDn4KM5xQ3G58LEULJkzZAS68_6Dz5k00fyK3WIca58WzfewwtRH5khlEgdF69EPlFKQ3QtiJF4KQGXKxv1v76dGKsixlpP", 15, false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This annual report summarises the work of IFCN Research Partners from over 100 countries.", "Global Dairy Trends and Drivers 2019" },
                    { 18, 2, new DateTime(2020, 6, 6, 14, 37, 53, 855, DateTimeKind.Utc).AddTicks(3857), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cruise Lines International Association (CLIA), the world’s largest cruise industry trade association, has released the 2019 Cruise Trends and State of the Cruise Industry Outlook. The report offers a look at the trends impacting cruise travel in the coming year and beyond as well as the overall global economic impact. Cruise Lines International Association (CLIA) is the unified global organizationhelping members succeed by advocating, educating and promoting for the common interests of the cruise community.", "https://www.gannett-cdn.com/presto/2018/12/20/USAT/b3fa36d4-b0ed-48c5-a991-924c48685469-Costa_LNG__Side_Perspective.jpg?crop=2999,1687,x0,y299&width=2999&height=1687&format=pjpg&auto=webp", 16, false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Instagram photos are driving interest in travel around the world. With onboard connectivity, cruise passengers are filling Instagram feeds with diverse travel experiences both onboard and on land from several cruise destinations.", "2019 Cruise Trends & Industry Outlook" },
                    { 17, 2, new DateTime(2020, 6, 6, 14, 37, 53, 855, DateTimeKind.Utc).AddTicks(3840), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This page intentionally left blank.", "https://media.nationalgeographic.org/assets/photos/120/983/091a0e2f-b93d-481b-9a60-db520c87ec33.jpg", 8, false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Corn Production Down 4 Percent from 2018. Soybean Production Down 19 Percent from 2018. Cotton Production Up 23 Percent from 2018. Winter Wheat Production Up 3 Percent from July Forecast", "USDA Crop Production Report 2019" },
                    { 16, 2, new DateTime(2020, 6, 6, 14, 37, 53, 855, DateTimeKind.Utc).AddTicks(3825), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Currently, Construction is 99.9% complete. Commissioning Pre-functional test was completed by ARUP and draft report has been received. MEOR is scheduled to review and evaluate draft report from ARUP. Once MEOR reviews draft report, ARUP will issue final  Commissioning  report.  Substantial  Completion  is  estimated  to  be  issued  in  August,  after  the  issuance  of the final Commissioning report and Punch list items are issued to the Contractor.", "https://specials-images.forbesimg.com/imageserve/5c0077cc31358e5b43383ffc/960x0.jpg?fit=scale", 7, false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Los Angeles Mission College Gateway Science and Engineering Construction Management Monthly Progress Report", "Los Angeles College Monthly Construction Report" },
                    { 15, 2, new DateTime(2020, 6, 6, 14, 37, 53, 855, DateTimeKind.Utc).AddTicks(3810), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "In this paper we quantify the usage of main resources (CPU, main memory, disk space and network bandwidth) of Windows 2000 machines from classroom laboratories. For that purpose, 169 machines of 11 classroom laboratories of an academic institution were monitored over 77 consecutive days. Samples were collected from all machines every 15 minutes for a total of 583653 samples. Besides evaluating availability of machines (uptime and downtime) and usage habits of users, the paper assesses usage of main resources, focusing on the impact of interactive login sessions over resource consumptions. Also, recurring to Self Monitoring Analysis and Reporting Technology (SMART) parameters of hard disks, the study estimates the average uptime per hard drive power cycle for the whole life of monitored computers. The paper also analyzes the potential of non-dedicated classroom Windows machines for distributed and parallel computing, evaluating the mean stability of group of machines. Our results show that resources idleness in classroom computers is very high, with an average CPU idleness of 97.93%, unused memory averaging 42.06% and unused disk space of the order of gigabytes per machine. Moreover, this study confirms the 2:1 equivalence rule found out by similar works, with N non-dedicated resources delivering an average CPU computing power roughly similar to N/2 dedicated machines. These results confirm the potentiality of these systems for resource harvesting, especially for grid desktop computing schemes. However, the efficient exploitation of the computational power of these environments requires adaptive fault-tolerance schemes to overcome the high volatility of resources.", "https://lh3.googleusercontent.com/proxy/ii7FxKnpaVHEW-pQ2JB5BikxCX8RvaXGzDlT41yX3mN9eDfvQjJoJhaCe0lc9kVmLFwkIAoqFF0y58zYnnYiSQwQBB_SVD9G_yn8KJNGls_2vL4bKA", 13, false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Studies focusing on Unix have shown that the vast majority of workstations and desktop computers remain idle for most of the time.", "Resources Usage of Windows Computer Laboratories" },
                    { 14, 2, new DateTime(2020, 6, 6, 14, 37, 53, 855, DateTimeKind.Utc).AddTicks(3795), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This report discusses the changes that have occurred in the Australian workforce since the end of World War II (1945-2000). A review of some of the available literature provides insights into the changing role of women and migrants in the workforce, and the influence of new technologies and changing levels of unemployment have also been considered.", "https://upload.wikimedia.org/wikipedia/commons/1/1a/Chapin_Hall%2C_Williams_College_-_Williamstown%2C_Massachusetts.jpg", 7, false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The information presented in this report has been gathered from secondary sources, and from Australian Bureau of Statistics’ data.", "The Change in the Australian Work Force Since the End of World War 2" },
                    { 13, 2, new DateTime(2020, 6, 6, 14, 37, 53, 855, DateTimeKind.Utc).AddTicks(3780), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The way we make, use and throwaway our clothes is unsustainable. Textile production contributes more to climate change than international aviation and shipping combined, consumes lake-sized volumes of fresh water and creates chemical and plastic pollution. Synthetic fibres are being found in the deep sea, in Arctic sea ice, in fish and shellfish. Our biggest retailers have ‘chased the cheap needle around the planet’, commissioning production  in  countries  with  low  pay,  little  trade  union  representation  and  weak  environmental protection. In many countries, poverty pay and conditions are standard for garment workers, most of whom are women. We are also concerned about the use of  child  labour,  prison  labour,  forced  labour  and  bonded  labour  in  factories  and  the  garment supply chain. Fast fashions’ overproduction and overconsumption of clothing is based on the globalisation of indifference towards these manual workers.", "https://dynaimage.cdn.cnn.com/cnn/c_fill,g_auto,w_1200,h_675,ar_16:9/https%3A%2F%2Fcdn.cnn.com%2Fcnnnext%2Fdam%2Fassets%2F190912152727-uglyleaddd.jpg", 10, false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fixing Fashion: Clothing Consumption and Sustainability", "Sustainability of the Fashion Industry" },
                    { 12, 2, new DateTime(2020, 6, 6, 14, 37, 53, 855, DateTimeKind.Utc).AddTicks(3765), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Particularly in Central and South America, expansion of pastures for livestock production has been one of the driving forces behind this wholesale destruction.Deforestation causes incalculable environmental damage, releasing billions of tonnes of carbon dioxide into the atmosphere and driving thousands of species of life to extinction each year. Effective policies are urgently needed to discourage expansion of livestock production in forest areas and promote sustainable grazing systems that will halt the cycle of degradation and abandonment on cleared forest lands.", "https://fayranches.com/wp-content/uploads/2016/01/savvy-investors-cattle-ranches.jpg", 8, false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Over the past quarter century, forests have been cleared from an area the size of India.", "Cattle Ranching and Deforestation" },
                    { 11, 2, new DateTime(2020, 6, 6, 14, 37, 53, 855, DateTimeKind.Utc).AddTicks(3751), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Casino Gambling has become a majorindustry in the United States over the past twodecades.  Nationwide, annual casino revenue tops $40billion. This report provides an analysis of casino gam-bling in the United States and discusses the economicissues surrounding casino gambling. The informationcontained in this report should prove useful to localofficials and policy-makers who may be considering theadoption of casino gambling or who already have casi-no gambling in their jurisdictions.", "https://royalepalmscasino-sofia.com/wp-content/uploads/2019/04/IMG_1710-1024x683.jpg", 9, false, true, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This report provides an analysis of casino gam - bling in the United States and discusses the economicissues surrounding casino gambling.", "Casino Gambling in America and Its Economic Impacts" },
                    { 8, 2, new DateTime(2020, 6, 6, 14, 37, 53, 855, DateTimeKind.Utc).AddTicks(3704), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The report is divided into three sections. The first part reviews the supply side of the pub market, revealing numbers, trends and the contrasting fortunes of different sectors, and identifying some of the areas in which pubs are succeeding. The second section analyses the customer base: their demographics, habits and motivations. The final part takes a look at reasons for optimism, with insights into increasing appeal and sales and the emerging new breeds of pub in Britain. At a time of great challenges for both the out of home eating and drinking market and the UK economy as a whole, this report highlights the many positive trends and developments in the British pub market. We hope you enjoy reading it.", "https://imagesvc.meredithcorp.io/v3/mm/image?url=https%3A%2F%2Fstatic.onecms.io%2Fwp-content%2Fuploads%2Fsites%2F9%2F2019%2F12%2Fuk-pubs-growth-FT-BLOG1219.jpg&q=85", 17, false, true, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This report draws on CGA’s unrivalled suite of research services to provide a comprehensive picture of Great Britain’s pubs and their opportunities for growth", "The British Pub Market 2019" },
                    { 7, 2, new DateTime(2020, 6, 6, 14, 37, 53, 855, DateTimeKind.Utc).AddTicks(3689), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This Report contains the conclusions from the comprehensive review of the Slovenian banking sector undertaken by the Bank of Sloveniain cooperation with the Slovenian Ministry of Finance over the period June to December 2013. This Asset Quality Review and Stress Test is a cornerstone in the broader initiative to restore the health in the Slovenian banking sector.", "https://www.assignmentpoint.com/wp-content/uploads/2013/04/E-Banking-in-Bangladesh.jpg", 5, false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Asset Quality Review and Stress Test were closely monitored by the international organisations (IOs), constituted of the European Commission, the European Central Bank, and the European Banking Authority.", "Full report on the comprehensive review of the banking system" },
                    { 6, 2, new DateTime(2020, 6, 6, 14, 37, 53, 855, DateTimeKind.Utc).AddTicks(3670), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Since the introduction of the smartphone, it has become clear that customers are quick to adopt even highly complex and expensive technology if it makes their lives easier. In other words, users value convenience and ease. These core values turned the automobile into the defining technical cultural item of the 20th century. Now it is time to translate these properties into the context of today's – and tomorrow's –  technology and society. ", "https://eenews.cdnartwhere.eu/sites/default/files/styles/inner_article/public/sites/default/files/images/2019-10-17_automotive_cybersecurity_hacking_software_dark_web_intsights.jpg?itok=x-9upxnf", 4, false, true, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Welcome to the age of radical change in the automotive industry", "Five Trends Transforming the Automotive Industry" },
                    { 5, 2, new DateTime(2020, 6, 6, 14, 37, 53, 855, DateTimeKind.Utc).AddTicks(3606), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Foreword Airline fare setting is a complex and dynamic process, varying by carrier, route and time. It lies at the heart of an airline’s commercial strategy as it aims to maximise the return on its assets employed, namely its aircraft and its people. In the following report, we have sought to describe clearly and comprehensively how the numerous factors an airline has to juggle play into the fare that passengers ultimately face. As a team who has an airline background, and advises both airlines and airports on matters of strategy, ICF has been uniquely well placed to do this.", "https://www.aljazeera.com/mritems/Images/2019/12/4/b604dd8d57c942a892cdc71f21d09973_18.jpg", 2, false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "An ICF Report prepared for ACI Europe", "Identifying the Drivers of Air Fares" },
                    { 4, 2, new DateTime(2020, 6, 6, 14, 37, 53, 855, DateTimeKind.Utc).AddTicks(3562), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Aerospace Industries Association (AIA) is the most authoritative and influential trade association representing the aerospace and defense industry. We are the leading voice for the industry on Capitol Hill, within the administration, and internationally.", "https://cdn.canadianmetalworking.com/a/aerospace-sector-report-a-jetstream-of-trends-1489163413.jpg?size=1000x1000", 2, false, true, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aerospace Industry Report 3rd Edition by The Aerospace Industries Association (AIA)", "Aerospace Industry 2012 Report" },
                    { 3, 2, new DateTime(2020, 6, 6, 14, 37, 53, 855, DateTimeKind.Utc).AddTicks(3126), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Despite pricing pressures and tariff concerns, measures of optimism are approaching the record-setting highs measured in spring 2018, according to the latest PNC semi-annual survey of small and mid-size business owners and executives.", "https://images.financialexpress.com/2019/06/ECONOMIC_SURVEY_660.jpg", 5, false, true, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Optimism Still Springs This Fall for Small and Mid-size Business Owners", "Fall Economic Outlook Survey" },
                    { 2, 2, new DateTime(2020, 6, 6, 14, 37, 53, 855, DateTimeKind.Utc).AddTicks(3089), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "PNC's Market Expectation Survey lists the current consensus forecast for key economic data releases for the upcoming week, as well as PNC's own forecast for each item. A comprehensive calendar listing recent data for key economic indicators is provided on the second page.", "https://www.questionpro.com/blog/wp-content/uploads/2018/05/Market-Survey_Final-800x478.jpg", 5, false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A comprehensive calendar listing recent data for key economic indicators.", "Market Expectations Survey" },
                    { 10, 2, new DateTime(2020, 6, 6, 14, 37, 53, 855, DateTimeKind.Utc).AddTicks(3735), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This report covers Audit and accounting; Business services; Professional services; and Legal services.", "https://www.antal.com/uploads/library/files/Business-Services.jpg", 1, false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This report covers: a description of the sector, the current EU regulatory regime, existing frameworks for how trade is facilitated between countries in this sector, and sector views. It does not contain commercially-, market-or negotiation-sensitive information.", "Professional and Business Services Sector Report" },
                    { 9, 2, new DateTime(2020, 6, 6, 14, 37, 53, 855, DateTimeKind.Utc).AddTicks(3719), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This report is intended to be of practical use in reducing the environmental impacts of construction. Nicole Lazarus will be glad to hear from any readers with feedback and examples of its application.", "https://lh3.googleusercontent.com/proxy/yv-Fqqhl26oZXf_TqO-irARoppooPkNe6DcoAoldvOSdkXl42fpaATpfcnqeuCs7qyZ_lv40MmzP2DikBzqshhJAhaXb9fi0gaDOCAEE0W6rtkbyUMYPDlYu8ixTEfPHorTtC8PB", 7, false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Toolkit for Carbon Neutral Developments", "Construction Materials Report" }
                });

            migrationBuilder.InsertData(
                table: "DownloadedReports",
                columns: new[] { "UserId", "ReportId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 1, 2 },
                    { 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "ReportTags",
                columns: new[] { "ReportId", "TagId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RoleId",
                table: "AspNetUsers",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_DownloadedReports_ReportId",
                table: "DownloadedReports",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_IndustrySubscriptions_IndustryId",
                table: "IndustrySubscriptions",
                column: "IndustryId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_AuthorId",
                table: "Reports",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_IndustryId",
                table: "Reports",
                column: "IndustryId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTags_TagId",
                table: "ReportTags",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "DownloadedReports");

            migrationBuilder.DropTable(
                name: "IndustrySubscriptions");

            migrationBuilder.DropTable(
                name: "ReportTags");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Industries");

            migrationBuilder.DropTable(
                name: "AspNetRoles");
        }
    }
}

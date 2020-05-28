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
                    Name = table.Column<string>(nullable: true),
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
                        name: "FK_IndustrySubscriptions_Industries_UserId",
                        column: x => x.UserId,
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
                    { 3, "9c3aac45-ca00-41ea-ae42-549c45833e0f", "Client", "CLIENT" },
                    { 2, "2e2fce54-862b-4537-936c-9ab713741d5a", "Author", "AUTHOR" },
                    { 1, "e8f4ba3f-f71b-4455-b08a-37ccf21fa131", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BanReason", "ConcurrencyStamp", "CreatedOn", "Email", "EmailConfirmed", "FirstName", "IsBanned", "IsPending", "LastName", "LockoutEnabled", "LockoutEnd", "ModifiedOn", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RoleId", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 3, 0, null, "7abcd29f-9548-4a3c-a6fe-862412584cdd", new DateTime(2020, 5, 28, 14, 29, 16, 103, DateTimeKind.Utc).AddTicks(8051), "client@gmail.com", false, "Clientcho", false, false, "Clientev", true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "CLIENT@GMAIL.COM", "CLIENT@GMAIL.COM", "AQAAAAEAACcQAAAAENtV5RJ4wc4GbqKB5YPgSVdUxTZ1Dh4jkLeIOtKVkRd4zr1NmaTOeOtS6G/CtS1YQw==", null, false, null, "7I5VNHIJTSZNOT3KDWKNFUV5PVYBHGXF", false, "client@gmail.com" },
                    { 1, 0, null, "0edd0ac3-a0fd-45ff-b396-d885d50aabdb", new DateTime(2020, 5, 28, 14, 29, 16, 103, DateTimeKind.Utc).AddTicks(6855), "admin@gmail.com", false, "Admincho", false, false, "Adminev", true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAEAACcQAAAAEK/XREK+qSK0Fb0Zq9hxPiJAVP+vLf1HtLAhT/4XGonN8edSgUd8K9SEBXlJrSiRNw==", null, false, null, "7I5VNHIJTSZNOT3KDWKNFUV5PVYBHGXN", false, "admin@gmail.com" },
                    { 2, 0, null, "e15d1724-71cf-497a-bfe1-496964b58dd4", new DateTime(2020, 5, 28, 14, 29, 16, 103, DateTimeKind.Utc).AddTicks(8038), "author@gmail.com", false, "Authorcho", false, false, "Authorchevski", true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AUTHOR@GMAIL.COM", "AUTHOR@GMAIL.COM", "AQAAAAEAACcQAAAAEATtPlIfeuQbxVT6odTt4UxC0zQdtARa2wbITG7lg6tPNOe9n3m/bdJghZD9oCCFGg==", null, false, null, "7I5VNHIJTSZNOT3KDWKNFUV5PVYBHGXV", false, "author@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Industries",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "ImgUrl", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 53, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7631), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://userscontent2.emaze.com/images/52feb565-01b5-4632-ae33-217f1becc980/329bdafc34fc27857aa6f2a0cb0168a0.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Radio/TV" },
                    { 52, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7618), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://w7.pngwing.com/pngs/734/1/png-transparent-business-interior-design-services-building-private-equity-money-business-building-people-investment.png", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Private Investment" },
                    { 51, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7606), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://assets.kpmg/content/dam/kpmg/images/2015/02/electric-power-lines.jpg/jcr:content/renditions/original", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Power Utilities" },
                    { 50, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7592), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://erp.bg/wp-content/uploads/2017/05/1-print-industry_2.gif", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Printing & Publishing" },
                    { 49, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7579), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://www.uoe.co.uk/wp-content/uploads/2018/07/Post-box-1.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Postage & Postal Services" },
                    { 48, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7535), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://www.marketingchina.agency/wp-content/uploads/2018/02/mobile-phone-industry-500x329.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Phone Companies" },
                    { 45, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7497), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://burdockgroup.com/wp-content/uploads/2018/05/dietary-supplements-wooden-spoons-copy.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nutritional & Dietary Supplements" },
                    { 46, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7510), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://cdn.pixabay.com/photo/2015/11/03/08/56/question-mark-1019820_1280.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Other" },
                    { 44, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7484), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://lh3.googleusercontent.com/proxy/nYMe910fGr1aP9-4Vvjhd61EQ1vOgqPcfEcl9LsLAE-7jVV6-eadYp0lnn0v4BBtuY8mmzU1tUMEQG9qBPAHgYarOEk7VnCP3VpDb_Yg8BcMGrTbDHQH97ffNdBH-5w4ee7T4KTUfYD3WF0dfJ7YPcdNu50Uy1XE4tt9MA", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Newspaper, Magazine & Book Publishing" },
                    { 43, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7471), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://www.tibco.com/blog/wp-content/uploads/2015/03/How-The-Music-Industry-is-Getting-Smarter.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Music & Production" },
                    { 42, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7458), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://blogs.3ds.com/delmia/wp-content/uploads/sites/24/2018/06/meat-disassembly-blog.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Meat Processing & Products" },
                    { 41, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7445), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://www.wallpaperup.com/uploads/wallpapers/2015/06/02/708905/8a78abe919157ab98171b9aa86e21eb7-700.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Liquor, Wine & Beer" },
                    { 47, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7523), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://www.digitalais.com/wp-content/uploads/2017/06/Farmaceutick%C3%BD-pr%C5%AFmysl.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pharmaceuticals" },
                    { 54, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7643), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://lh3.googleusercontent.com/proxy/V8LQ-lwAm3Ra4I_PvVE6U4WahtpFtpHwXd6BU77kyUPo0a-x0KXYBdRJSMuXxX85yaGNNIqczkOUCaq35M_4Qbh2YKUyJeMVn9yU01ywE9DuAi7mEZg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Railroads" },
                    { 57, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7682), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://bookingprotect.com/wp-content/uploads/2019/08/live_entertainment.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Recreation & Live Entertainment" },
                    { 56, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7669), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://lh3.googleusercontent.com/proxy/u9QC1f3VUFnFbVfG8l7YXkRA-f7a8MwWjt9pD7doK-Taz9IiHr0klERuKsJF9tqnIEwjBIGZMhC2Ki5T6m9Sda8HNbtLT9XPZmKxz0BY9ikRVuKuq6kcWwfs60Iur1SGORnHJYo", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Record Companies & Singers" },
                    { 58, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7694), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://w0.pngwave.com/png/303/783/select-sweets-retail-sales-industry-ecommerce-payment-system-png-clip-art.png", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Retail Sales" },
                    { 59, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7707), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://www.liberty.edu/champion/wp-content/uploads/2020/03/SPORTS-800x280.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sports, Arenas & Equipment" },
                    { 60, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7722), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://www.usnews.com/dims4/USNEWS/01e1de6/2147483647/thumbnail/640x420/quality/85/?url=http%3A%2F%2Fmedia.beam.usnews.com%2F00%2F58%2F5926109d42de9f633868cd479f65%2F190111-investingsectors-stock.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Stock Brokers & Investment" },
                    { 61, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7735), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://thehustle.co/wp-content/uploads/2019/02/share_me_now_brief_2019-02-27T021549.567Z-1.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Student Loans" },
                    { 62, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7747), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://plug-n-score.com/wp-content/files/2014/08/credit-in-the-telecom-industry.png", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Telecom Services & Equipment" },
                    { 63, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7760), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://www.see-industry.com/Files/statii/22017_02_Industry_Textile.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Textiles" },
                    { 64, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7772), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://daxueconsulting.com/wp-content/uploads/2016/01/Daxue-Consulting-China-Tobacco-Industry-Market-Research.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tobacco" },
                    { 65, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7786), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://www.odtap.com/wp-content/uploads/2019/03/5-Top-Technology-Trends-in-Transportation-and-Logistics-Industry.png", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Transportation" },
                    { 66, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7800), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://s29755.pcdn.co/wp-content/uploads/2019/08/2019_Top_Five_Class_5-Mack.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Truckery" },
                    { 67, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7843), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://lh3.googleusercontent.com/proxy/YZfiizLwa44Nzb58s3cKhWFgUbFevmGdK9QJQbaC7ASQtfVniCSHHZBL3RFv_e7XBowp4mSE-T6FyJvgTARAYSoJhFucvNE", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "TV Production" },
                    { 68, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7857), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://www.unionindustry.eu/assets/uploads/2010/10/jobs-ants.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Unions" },
                    { 69, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7870), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://www.vegan.io/blog/assets/10-healthiest-vegetables-to-include-in-your-vegan-diet-2018-04-16/healthiest-vegetables-df1cf550711076d052eaade12c38289a2637c38e546182d3c0136a90cb0bb0b3.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vegetables & Fruits" },
                    { 70, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7883), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://www.wyzowl.com/wp-content/uploads/2017/05/The-20-Best-Nonprofit-Explainer-Videos-Youll-Ever-See-ffb612.png", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Non-Profit" },
                    { 55, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7656), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://miro.medium.com/max/660/1*XaXQRJiI5BSFY3uFhTFCAg.jpeg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Real Estate" },
                    { 39, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7419), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://synchronium.io/wp-content/uploads/2019/02/blockchain-insurance-619x410.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Insurance" },
                    { 40, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7432), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://w7.pngwing.com/pngs/595/460/png-transparent-industry-businessperson-labor-industrail-workers-and-engineers-miscellaneous-people-employment.png", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Labor" },
                    { 37, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7394), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://miro.medium.com/max/960/1*VghRFv3Ejzs18nJ0MHzg9g.jpeg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hotels, Motels, Tourism" },
                    { 16, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7088), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://www.maritime-executive.com/media/images/article/Photos/Cruise_Ships/Diamond-Princess-sailing-in-Japan-courtesy-Princess-Cruises.78319a.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cruise Ships & Lines" },
                    { 15, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7075), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://www.netafim.com/48da28/globalassets/demo/products-and-solutions/open-fields/open_fields_headvisual-graded.jpg?height=620&width=1440&mode=crop&quality=80", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Crop Production & Processing" },
                    { 14, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7061), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://s27389.pcdn.co/wp-content/uploads/2019/07/digital-transformation-construction-industry-1024x440.jpeg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Construction Services" },
                    { 13, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7048), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://ctbhost.com/wp-content/uploads/2019/07/computer-internet-franchise-1.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Computers & Software" },
                    { 12, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7034), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://cfhsprowler.com/wp-content/uploads/2020/01/download.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Colleges, Universities & Schools" },
                    { 11, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7021), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://scstylecaster.files.wordpress.com/2018/08/nyfw-fi.png", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Clothing & Fashion" },
                    { 10, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7006), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d4/CH_cow_2_cropped.jpg/1200px-CH_cow_2_cropped.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cattle, Livestock & Ranching" },
                    { 9, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(6923), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://q-cf.bstatic.com/images/hotel/max1024x768/240/240066005.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Casinos & Gambling" },
                    { 8, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(6909), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://ftguidetobusinesstraining.com/wp-content/uploads/2018/08/Business-Services01.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Business Services" },
                    { 7, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(6896), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://rr-hk.com/Building_material/img/62614691_custom.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Building Materials & Equipment" },
                    { 6, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(6882), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://goguide.bg/upload/places/inner/1576236597Embassy.png", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bars & Restaurants" },
                    { 5, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(6866), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://studyabroad.bg/wp-content/uploads/2016/04/Bank.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Banking" },
                    { 4, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(6852), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://j2offshore.com/wp-content/uploads/2018/05/automative-2.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Automotive" },
                    { 3, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(6837), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://media.foxbusiness.com/BrightCove/854081161001/202005/2760/854081161001_6157982707001_6157986352001-vs.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Airlines (Air Transport)" },
                    { 2, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(6809), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://www.henkel.com/resource/image/946964/4x3/1120/840/e9b0d415e2ab94d019b20e32cf0f015/el/aerospace-industry-growth.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aerospace" },
                    { 38, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7406), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://multimedia.europarl.europa.eu/documents/20143/0/AP_97209887+%281%29.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Human Rights" },
                    { 18, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7115), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://idsb.tmgrup.com.tr/2015/10/05/GenelBuyuk/1443984579449_rs.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Defense" },
                    { 17, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7101), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://lh3.googleusercontent.com/proxy/B2Ha-PwtOsxqi3MXwm2SCOWM6CxKChDn4KM5xQ3G58LEULJkzZAS68_6Dz5k00fyK3WIca58WzfewwtRH5khlEgdF69EPlFKQ3QtiJF4KQGXKxv1v76dGKsixlpP", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dairy" },
                    { 20, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7140), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://miro.medium.com/max/3200/1*afTS3knLUSHCJDirSOlq1g.jpeg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Doctors & Health Professionals" },
                    { 36, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7381), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://cdn.aiidatapro.net/media/ec/a5/90/t780x490/eca590e038af4adfb46a726b3c0f9a98.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hospitals & Nursing Homes" },
                    { 35, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7368), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://geekculturepodcast.com/wp-content/uploads/2019/05/Healthcare-Industry.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Health Care" },
                    { 34, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7355), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://i.pinimg.com/originals/da/9b/60/da9b60f63d0957cc785967b309c7d775.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "General Contractors" },
                    { 33, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7339), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://plsadaptive.s3.amazonaws.com/eco/images/channel_content/images/offshore_platform.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gas & Oil" },
                    { 32, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7326), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://wiedemanfuneralhome.com/4640/Ultra/Woodbridge_Gardening.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Funeral Services" },
                    { 31, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7313), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://wallenius-sol.com/sites/wallenius-sol.com/files/the-forest-industry_photo-shutterstock_wallenius-sol_the-enabler_1920x1080px.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Forestry & Forest Products" },
                    { 19, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7128), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://www.orthodonticslimited.com/wp-content/uploads/2017/12/Depositphotos_dentisttool.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dentists" },
                    { 29, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7255), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://www.makeinethiopia.com/wp-content/uploads/2018/06/Food-Processing.png", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Food Processing & Sales" },
                    { 30, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7299), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://news.northeastern.edu/wp-content/uploads/2016/07/prison_money-800x0-c-default.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "For-Profit Prisons" },
                    { 27, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7229), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://www.newdma.org/wp-content/uploads/2015/01/Digital-marketing-for-the-Finance-industry.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Finance & Credit" },
                    { 26, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7216), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://upload.wikimedia.org/wikipedia/commons/7/7e/Farm_Kartoffelfeld_Schweden.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Farms" },
                    { 25, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7204), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://lh3.googleusercontent.com/proxy/Zm1otIkpcrSH9UHtVeqp3QWllwBRaPjVWkRHlKMDCT88mbJkkj7xzg1qO87ZdpMsFkhdfl8DoVSbQhbyitaQYqSDepQ_fpFWF0z8zXjWMz2AqN3jtOXcJhqYLBkV14SGJQGPDcLqIF-_OdlxCgoOlSG97VZdLubN", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Environment" },
                    { 24, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7191), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://www.marketingtochina.com/wp-content/uploads/2017/08/seo-marketing.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Entertainment" },
                    { 23, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7178), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://www.power-technology.com/wp-content/uploads/sites/7/2018/01/Renewable_Energy_on_the_Grid.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Energy & Natural Resources" },
                    { 22, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7166), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://www.e-spincorp.com/wp-content/uploads/2019/02/laser-micro-machining-electrical-industry-e1549935628710.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Electronics Manufacturing & Equipment" },
                    { 21, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7153), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://www.healthcaresalestraining.com/photo/5.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Education" },
                    { 28, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(7242), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://miro.medium.com/max/5120/1*YENSrU0nehgyGk6W-G8Rpg.jpeg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Food & Beverage" },
                    { 1, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(6239), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://help-investor.com/wp-content/uploads/2018/07/Accountant.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Accountants" }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(9739), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Space" },
                    { 2, new DateTime(2020, 5, 28, 14, 29, 16, 127, DateTimeKind.Utc).AddTicks(287), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Water" },
                    { 3, new DateTime(2020, 5, 28, 14, 29, 16, 127, DateTimeKind.Utc).AddTicks(315), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Money" }
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
                    { 1, 2, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(1806), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Real GDP contracted 4.8 percent at an annualized rate in the first quarter according to the advance estimate from the Bureau of Economic Analysis, worse than the consensus expectation for a 4.0 percent decline. The coronavirus pandemic and restrictions on movement led to big declines in consumer spending and business investment (down 7.6 percent and 8.6 percent annualized, respectively).", "https://www.agenda-bg.com/wp-content/uploads/2016/09/What-To-Expect-In-2015-An-Economic-Outlook-For-Small-And-Medium-Businesses-Making-It-TV.jpg", 27, false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "April 2020 National Economic Outlook by PINACLE", "National Economic Outlook" },
                    { 19, 2, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(3580), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This article is based on the IFCN Dairy Report 2019. This annual report summarises the work of IFCN Research Partners from over 100 countries. IFCN is a global network for dairy economic research and consultancy. In 2019, researchers from over 100 countries and more than 140 agribusiness companies are members of the network. IFCN has created a better understanding of the dairy world for 20 years. Key insights 2019 will be the year of lowest milk production growth since 2013. As this did not translate into milk price increases, IFCN identifies a structural drop in demand growth as one of the reasons. Milk production trends by regions are highly diverse and dynamic. The 3-5% rule indicates that strong regions grow and weak ones decline by this rate every year. Dairy farm structure dynamics drive milk supply and the speed of change is under-estimated. IFCN recommends using the annual growth of milk production per farm as an indicator. In the EU and the USA farms grew by 8% per year. The key driver for farm structure developments lies in dairy farm economics and the current structure of economies of scale. The Dairy Report analyses this in over 50 countries. The IFCN Dairy Report has been published annually since 2000 and has become a guideline publication for researchers and companies involved in the dairy chain. It enables to gain a global holistic view of the industry and serves as a solid fact base for discussions and strategic decisions.", "https://lh3.googleusercontent.com/proxy/B2Ha-PwtOsxqi3MXwm2SCOWM6CxKChDn4KM5xQ3G58LEULJkzZAS68_6Dz5k00fyK3WIca58WzfewwtRH5khlEgdF69EPlFKQ3QtiJF4KQGXKxv1v76dGKsixlpP", 17, false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This annual report summarises the work of IFCN Research Partners from over 100 countries.", "Global Dairy Trends and Drivers 2019" },
                    { 18, 2, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(3566), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cruise Lines International Association (CLIA), the world’s largest cruise industry trade association, has released the 2019 Cruise Trends and State of the Cruise Industry Outlook. The report offers a look at the trends impacting cruise travel in the coming year and beyond as well as the overall global economic impact. Cruise Lines International Association (CLIA) is the unified global organizationhelping members succeed by advocating, educating and promoting for the common interests of the cruise community.", "https://www.gannett-cdn.com/presto/2018/12/20/USAT/b3fa36d4-b0ed-48c5-a991-924c48685469-Costa_LNG__Side_Perspective.jpg?crop=2999,1687,x0,y299&width=2999&height=1687&format=pjpg&auto=webp", 16, false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Instagram photos are driving interest in travel around the world. With onboard connectivity, cruise passengers are filling Instagram feeds with diverse travel experiences both onboard and on land from several cruise destinations.", "2019 Cruise Trends & Industry Outlook" },
                    { 17, 2, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(3551), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This page intentionally left blank.", "https://media.nationalgeographic.org/assets/photos/120/983/091a0e2f-b93d-481b-9a60-db520c87ec33.jpg", 15, false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Corn Production Down 4 Percent from 2018. Soybean Production Down 19 Percent from 2018. Cotton Production Up 23 Percent from 2018. Winter Wheat Production Up 3 Percent from July Forecast", "USDA Crop Production Report 2019" },
                    { 16, 2, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(3538), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Currently, Construction is 99.9% complete. Commissioning Pre-functional test was completed by ARUP and draft report has been received. MEOR is scheduled to review and evaluate draft report from ARUP. Once MEOR reviews draft report, ARUP will issue final  Commissioning  report.  Substantial  Completion  is  estimated  to  be  issued  in  August,  after  the  issuance  of the final Commissioning report and Punch list items are issued to the Contractor.", "https://specials-images.forbesimg.com/imageserve/5c0077cc31358e5b43383ffc/960x0.jpg?fit=scale", 14, false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Los Angeles Mission College Gateway Science and Engineering Construction Management Monthly Progress Report", "Los Angeles College Monthly Construction Report" },
                    { 15, 2, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(3523), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "In this paper we quantify the usage of main resources (CPU, main memory, disk space and network bandwidth) of Windows 2000 machines from classroom laboratories. For that purpose, 169 machines of 11 classroom laboratories of an academic institution were monitored over 77 consecutive days. Samples were collected from all machines every 15 minutes for a total of 583653 samples. Besides evaluating availability of machines (uptime and downtime) and usage habits of users, the paper assesses usage of main resources, focusing on the impact of interactive login sessions over resource consumptions. Also, recurring to Self Monitoring Analysis and Reporting Technology (SMART) parameters of hard disks, the study estimates the average uptime per hard drive power cycle for the whole life of monitored computers. The paper also analyzes the potential of non-dedicated classroom Windows machines for distributed and parallel computing, evaluating the mean stability of group of machines. Our results show that resources idleness in classroom computers is very high, with an average CPU idleness of 97.93%, unused memory averaging 42.06% and unused disk space of the order of gigabytes per machine. Moreover, this study confirms the 2:1 equivalence rule found out by similar works, with N non-dedicated resources delivering an average CPU computing power roughly similar to N/2 dedicated machines. These results confirm the potentiality of these systems for resource harvesting, especially for grid desktop computing schemes. However, the efficient exploitation of the computational power of these environments requires adaptive fault-tolerance schemes to overcome the high volatility of resources.", "https://lh3.googleusercontent.com/proxy/ii7FxKnpaVHEW-pQ2JB5BikxCX8RvaXGzDlT41yX3mN9eDfvQjJoJhaCe0lc9kVmLFwkIAoqFF0y58zYnnYiSQwQBB_SVD9G_yn8KJNGls_2vL4bKA", 13, false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Studies focusing on Unix have shown that the vast majority of workstations and desktop computers remain idle for most of the time.", "Resources Usage of Windows Computer Laboratories" },
                    { 14, 2, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(3510), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This report discusses the changes that have occurred in the Australian workforce since the end of World War II (1945-2000). A review of some of the available literature provides insights into the changing role of women and migrants in the workforce, and the influence of new technologies and changing levels of unemployment have also been considered.", "https://upload.wikimedia.org/wikipedia/commons/1/1a/Chapin_Hall%2C_Williams_College_-_Williamstown%2C_Massachusetts.jpg", 12, false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The information presented in this report has been gathered from secondary sources, and from Australian Bureau of Statistics’ data.", "The Change in the Australian Work Force Since the End of World War 2" },
                    { 13, 2, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(3495), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The way we make, use and throwaway our clothes is unsustainable. Textile production contributes more to climate change than international aviation and shipping combined, consumes lake-sized volumes of fresh water and creates chemical and plastic pollution. Synthetic fibres are being found in the deep sea, in Arctic sea ice, in fish and shellfish. Our biggest retailers have ‘chased the cheap needle around the planet’, commissioning production  in  countries  with  low  pay,  little  trade  union  representation  and  weak  environmental protection. In many countries, poverty pay and conditions are standard for garment workers, most of whom are women. We are also concerned about the use of  child  labour,  prison  labour,  forced  labour  and  bonded  labour  in  factories  and  the  garment supply chain. Fast fashions’ overproduction and overconsumption of clothing is based on the globalisation of indifference towards these manual workers.", "https://dynaimage.cdn.cnn.com/cnn/c_fill,g_auto,w_1200,h_675,ar_16:9/https%3A%2F%2Fcdn.cnn.com%2Fcnnnext%2Fdam%2Fassets%2F190912152727-uglyleaddd.jpg", 11, false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fixing Fashion: Clothing Consumption and Sustainability", "Sustainability of the Fashion Industry" },
                    { 12, 2, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(3480), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Particularly in Central and South America, expansion of pastures for livestock production has been one of the driving forces behind this wholesale destruction.Deforestation causes incalculable environmental damage, releasing billions of tonnes of carbon dioxide into the atmosphere and driving thousands of species of life to extinction each year. Effective policies are urgently needed to discourage expansion of livestock production in forest areas and promote sustainable grazing systems that will halt the cycle of degradation and abandonment on cleared forest lands.", "https://fayranches.com/wp-content/uploads/2016/01/savvy-investors-cattle-ranches.jpg", 10, false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Over the past quarter century, forests have been cleared from an area the size of India.", "Cattle Ranching and Deforestation" },
                    { 11, 2, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(3428), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Casino Gambling has become a majorindustry in the United States over the past twodecades.  Nationwide, annual casino revenue tops $40billion. This report provides an analysis of casino gam-bling in the United States and discusses the economicissues surrounding casino gambling. The informationcontained in this report should prove useful to localofficials and policy-makers who may be considering theadoption of casino gambling or who already have casi-no gambling in their jurisdictions.", "https://royalepalmscasino-sofia.com/wp-content/uploads/2019/04/IMG_1710-1024x683.jpg", 9, false, true, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This report provides an analysis of casino gam - bling in the United States and discusses the economicissues surrounding casino gambling.", "Casino Gambling in America and Its Economic Impacts" },
                    { 8, 2, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(3383), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The report is divided into three sections. The first part reviews the supply side of the pub market, revealing numbers, trends and the contrasting fortunes of different sectors, and identifying some of the areas in which pubs are succeeding. The second section analyses the customer base: their demographics, habits and motivations. The final part takes a look at reasons for optimism, with insights into increasing appeal and sales and the emerging new breeds of pub in Britain. At a time of great challenges for both the out of home eating and drinking market and the UK economy as a whole, this report highlights the many positive trends and developments in the British pub market. We hope you enjoy reading it.", "https://imagesvc.meredithcorp.io/v3/mm/image?url=https%3A%2F%2Fstatic.onecms.io%2Fwp-content%2Fuploads%2Fsites%2F9%2F2019%2F12%2Fuk-pubs-growth-FT-BLOG1219.jpg&q=85", 6, false, true, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This report draws on CGA’s unrivalled suite of research services to provide a comprehensive picture of Great Britain’s pubs and their opportunities for growth", "The British Pub Market 2019" },
                    { 7, 2, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(3368), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This Report contains the conclusions from the comprehensive review of the Slovenian banking sector undertaken by the Bank of Sloveniain cooperation with the Slovenian Ministry of Finance over the period June to December 2013. This Asset Quality Review and Stress Test is a cornerstone in the broader initiative to restore the health in the Slovenian banking sector.", "https://www.assignmentpoint.com/wp-content/uploads/2013/04/E-Banking-in-Bangladesh.jpg", 5, false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Asset Quality Review and Stress Test were closely monitored by the international organisations (IOs), constituted of the European Commission, the European Central Bank, and the European Banking Authority.", "Full report on the comprehensive review of the banking system" },
                    { 6, 2, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(3353), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Since the introduction of the smartphone, it has become clear that customers are quick to adopt even highly complex and expensive technology if it makes their lives easier. In other words, users value convenience and ease. These core values turned the automobile into the defining technical cultural item of the 20th century. Now it is time to translate these properties into the context of today's – and tomorrow's –  technology and society. ", "https://eenews.cdnartwhere.eu/sites/default/files/styles/inner_article/public/sites/default/files/images/2019-10-17_automotive_cybersecurity_hacking_software_dark_web_intsights.jpg?itok=x-9upxnf", 4, false, true, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Welcome to the age of radical change in the automotive industry", "Five Trends Transforming the Automotive Industry" },
                    { 5, 2, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(3333), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Foreword Airline fare setting is a complex and dynamic process, varying by carrier, route and time. It lies at the heart of an airline’s commercial strategy as it aims to maximise the return on its assets employed, namely its aircraft and its people. In the following report, we have sought to describe clearly and comprehensively how the numerous factors an airline has to juggle play into the fare that passengers ultimately face. As a team who has an airline background, and advises both airlines and airports on matters of strategy, ICF has been uniquely well placed to do this.", "https://www.aljazeera.com/mritems/Images/2019/12/4/b604dd8d57c942a892cdc71f21d09973_18.jpg", 3, false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "An ICF Report prepared for ACI Europe", "Identifying the Drivers of Air Fares" },
                    { 4, 2, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(3302), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Aerospace Industries Association (AIA) is the most authoritative and influential trade association representing the aerospace and defense industry. We are the leading voice for the industry on Capitol Hill, within the administration, and internationally.", "https://cdn.canadianmetalworking.com/a/aerospace-sector-report-a-jetstream-of-trends-1489163413.jpg?size=1000x1000", 2, false, true, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aerospace Industry Report 3rd Edition by The Aerospace Industries Association (AIA)", "Aerospace Industry 2012 Report" },
                    { 3, 2, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(2872), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Despite pricing pressures and tariff concerns, measures of optimism are approaching the record-setting highs measured in spring 2018, according to the latest PNC semi-annual survey of small and mid-size business owners and executives.", "https://images.financialexpress.com/2019/06/ECONOMIC_SURVEY_660.jpg", 27, false, true, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Optimism Still Springs This Fall for Small and Mid-size Business Owners", "Fall Economic Outlook Survey" },
                    { 2, 2, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(2827), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "PNC's Market Expectation Survey lists the current consensus forecast for key economic data releases for the upcoming week, as well as PNC's own forecast for each item. A comprehensive calendar listing recent data for key economic indicators is provided on the second page.", "https://www.questionpro.com/blog/wp-content/uploads/2018/05/Market-Survey_Final-800x478.jpg", 27, false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A comprehensive calendar listing recent data for key economic indicators.", "Market Expectations Survey" },
                    { 10, 2, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(3414), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This report covers Audit and accounting; Business services; Professional services; and Legal services.", "https://www.antal.com/uploads/library/files/Business-Services.jpg", 8, false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This report covers: a description of the sector, the current EU regulatory regime, existing frameworks for how trade is facilitated between countries in this sector, and sector views. It does not contain commercially-, market-or negotiation-sensitive information.", "Professional and Business Services Sector Report" },
                    { 9, 2, new DateTime(2020, 5, 28, 14, 29, 16, 126, DateTimeKind.Utc).AddTicks(3398), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This report is intended to be of practical use in reducing the environmental impacts of construction. Nicole Lazarus will be glad to hear from any readers with feedback and examples of its application.", "https://lh3.googleusercontent.com/proxy/yv-Fqqhl26oZXf_TqO-irARoppooPkNe6DcoAoldvOSdkXl42fpaATpfcnqeuCs7qyZ_lv40MmzP2DikBzqshhJAhaXb9fi0gaDOCAEE0W6rtkbyUMYPDlYu8ixTEfPHorTtC8PB", 7, false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Toolkit for Carbon Neutral Developments", "Construction Materials Report" }
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

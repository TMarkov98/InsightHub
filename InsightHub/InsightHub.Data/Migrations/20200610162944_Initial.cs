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
                        onDelete: ReferentialAction.Restrict);
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    { 3, "3b4715fd-41fa-477f-9ea0-407056e41bfa", "Client", "CLIENT" },
                    { 2, "d6e71fa9-7694-48f7-8689-1e7bc3235f78", "Author", "AUTHOR" },
                    { 1, "4286bca7-43e5-43b5-8e21-4bbdb7b7805d", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BanReason", "ConcurrencyStamp", "CreatedOn", "Email", "EmailConfirmed", "FirstName", "IsBanned", "IsPending", "LastName", "LockoutEnabled", "LockoutEnd", "ModifiedOn", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RoleId", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 10, 0, null, "cad8d1f6-7e51-474c-b622-3e1d19cda60d", new DateTime(2020, 2, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "kiro.kirov@insighthub.com", false, "Kiro", false, false, "Kirov", true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "KIRO.KIROV@INSIGHTHUB.COM", "KIRO.KIROV@INSIGHTHUB.COM", "AQAAAAEAACcQAAAAENERqz8ULvF0fwCuVLsQKOf+xK+7A/yC3//YrW3phb8Z1gNFIeO44mmQQV4jpNaWjg==", null, false, null, "7I5VNHIJTSZNOT3KDWKNFUV3LSYBHGXF", false, "kiro.kirov@insighthub.com" },
                    { 8, 0, null, "0f911fb7-3631-401a-bc87-ef5a1b06ef2f", new DateTime(2020, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "preslav.mitev@insighthub.com", false, "Preslav", false, false, "Mitev", true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "PRESLAV.MITEV@INSIGHTHUB.COM", "PRESLAV.MITEV@INSIGHTHUB.COM", "AQAAAAEAACcQAAAAEJKSgZf7RbOvOlGOIWAanIPOMYPvE7SmdYysvXmlvaxIqDbCdoY0MVupao7bNfpU4Q==", null, false, null, "7I5VNHIJTSZNOT3KDWKNFUK3PVYBHGXF", false, "preslav.mitev@insighthub.com" },
                    { 7, 0, null, "ac68753a-3861-4037-95f4-9a2353d4bb55", new DateTime(2020, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "kaloyan.jekov@insighthub.com", false, "Kaloyan", false, false, "Jekov", true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "KALOYAN.JEKOV@INSIGHTHUB.COM", "KALOYAN.JEKOV@INSIGHTHUB.COM", "AQAAAAEAACcQAAAAEFBiv5Z/tpeT9YVoId1kzikZqDFiW+l22N/PlJllHHZDcAOKLvIRgcDASQSIjL0Udg==", null, false, null, "7I5VNHIJTSZNOT3KDWKNFUV5PPGBHGXF", false, "kaloyan.jekov@insighthub.com" },
                    { 6, 0, null, "d42626b6-9a38-4a3f-8ff2-9e13aed02b6f", new DateTime(2020, 4, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "valentina.dimitrova@insighthub.com", false, "Valentina", false, false, "Dimitrova", true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "VALENTINA.DIMITROVA@INSIGHTHUB.COM", "VALENTINA.DIMITROVA@INSIGHTHUB.COM", "AQAAAAEAACcQAAAAED9H7R3GeWFEc2e7RVLMSWvNXoKllH2EkHRukRBsa3aev+C/LdGLri9R1LD4l2XAXg==", null, false, null, "7I5VNHIJTSZNOT3KDWKNFUV5PVVBHGXF", false, "valentina.dimitrova@insighthub.com" },
                    { 5, 0, null, "419519a6-9794-4079-874b-d44130ba3b77", new DateTime(2020, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "petyr.petrov@insighthub.com", false, "Petyr", false, false, "Petrov", true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "PETYR.PETROV@INSIGHTHUB.COM", "PETYR.PETROV@INSIGHTHUB.COM", "AQAAAAEAACcQAAAAEB++oALLTm7H2Xh1QFaTLdBYHTg5m+dElTxKBV4K93XZT22/6kzyIStlMHGaqFHiOg==", null, false, null, "7I5VNHIJTSZNOT3KDWKNFUV5PVYBHPPG", false, "petyr.petrov@insighthub.com" },
                    { 4, 0, null, "e77de228-7d0c-4969-961d-f90702ee5c59", new DateTime(2020, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "atanas.velev@insighthub.com", false, "Atanas", false, false, "Velev", true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ATANAS.VELEV@INSIGHTHUB.COM", "ATANAS.VELEV@INSIGHTHUB.COM", "AQAAAAEAACcQAAAAELDD7vsyJWTNYMYWqHnhw4MRA+zxTOhoExvKiPSJBP1m2zoM5ZKZzLwc0Z5b3fDE0A==", null, false, null, "7I5VNHIJTSZNOT3KDWKNFUV5PVYBHGMN", false, "atanas.velev@insighthub.com" },
                    { 3, 0, null, "c32b4be4-2d3d-4a3b-9f86-9032f9e2d170", new DateTime(2020, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "miroslav.peev@insighthub.com", false, "Miroslav", false, false, "Peev", true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "MIROSLAV.PEEV@INSIGHTHUB.COM", "MIROSLAV.PEEV@INSIGHTHUB.COM", "AQAAAAEAACcQAAAAEPmHfXuBIh9bsC9DZeMg3f6f3Ja1CEsAQdX8NUOWLkzh01iTqIzBCL+Ai7vNkNAW7Q==", null, false, null, "7I5VNHIJTSZNOT3KDWKNFUV5PVYBHPPT", false, "miroslav.peev@insighthub.com" },
                    { 2, 0, null, "a7a46ea4-97b0-42e6-ba62-8fc348509639", new DateTime(2020, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "georgi.petrov@insighthub.com", false, "Georgi", false, false, "Petrov", true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "GEORGIPETROV@INSIGHTHUB.COM", "GEORGI.PETROV@INSIGHTHUB.COM", "AQAAAAEAACcQAAAAEKnRqQUK8YQAd+k3XX4rwb5juoznBHVpGRp6PvMJO0bV19s86qn/sw8wDgKawweLgA==", null, false, null, "7I5VNHIJTSZNOT3KDWKNFUV5PVYBHMHG", false, "georgi.petrov@insighthub.com" },
                    { 11, 0, null, "eee93f9d-7112-4ae2-93b7-fc412c328d1f", new DateTime(2020, 6, 10, 16, 29, 44, 150, DateTimeKind.Utc).AddTicks(5676), "admin2@insighthub.com", false, "Admin", false, false, "Second", true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ADMIN2@INSIGHTHUB.COM", "ADMIN2@INSIGHTHUB.COM", null, null, false, null, "7I5VNHIJTSZNOT3KDWKNFUV5PVYBHGXB", false, "admin2@insighthub.com" },
                    { 1, 0, null, "e19b212a-158c-4aeb-9c7d-e41f6dca5adb", new DateTime(2020, 6, 10, 16, 29, 44, 150, DateTimeKind.Utc).AddTicks(4371), "admin@insighthub.com", false, "Admincho", false, false, "Adminev", true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ADMIN@INSIGHTHUB.COM", "ADMIN@INSIGHTHUB.COM", "AQAAAAEAACcQAAAAED9o/LFfNKJTh6D3rIYVsMFqmAbYsiYTvMZS86jCQnJ0D8D8LAYS3qPs1MyWseFgGQ==", null, false, null, "7I5VNHIJTSZNOT3KDWKNFUV5PVYBHGXN", false, "admin@insighthub.com" },
                    { 9, 0, null, "b5c99e8c-42f9-416f-bf9b-e151bd22f3f8", new DateTime(2020, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "kristian.ivanov@insighthub.com", false, "Kristian", false, false, "Ivanov", true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "KRISTIAN.IVANOV@INSIGHTHUB.COM", "KRISTIAN.IVANOV@INSIGHTHUB.COM", "AQAAAAEAACcQAAAAENHxdRCZHxU+Bf36UlFtLKdDpIrumdIKb/RWROja8HB7JdqAZFCSK9Csd/FoAVwb1w==", null, false, null, "7I5VNHIJTSZNOT3KDWKNKSV5PVYBHGXF", false, "kristian.ivanov@insighthub.com" }
                });

            migrationBuilder.InsertData(
                table: "Industries",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "ImgUrl", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 17, new DateTime(2020, 6, 10, 16, 29, 44, 211, DateTimeKind.Utc).AddTicks(8830), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://i.imgur.com/fJ5s33U.png", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Restaurants" },
                    { 1, new DateTime(2020, 6, 10, 16, 29, 44, 211, DateTimeKind.Utc).AddTicks(7986), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://i.imgur.com/8Rkh6JW.png", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Finance" },
                    { 15, new DateTime(2020, 6, 10, 16, 29, 44, 211, DateTimeKind.Utc).AddTicks(8804), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://i.imgur.com/oWIukyA.png", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Food" },
                    { 14, new DateTime(2020, 6, 10, 16, 29, 44, 211, DateTimeKind.Utc).AddTicks(8791), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://i.imgur.com/5pZJcpA.png", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Energy" },
                    { 13, new DateTime(2020, 6, 10, 16, 29, 44, 211, DateTimeKind.Utc).AddTicks(8777), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://i.imgur.com/ApJqXTX.png", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Technology" },
                    { 12, new DateTime(2020, 6, 10, 16, 29, 44, 211, DateTimeKind.Utc).AddTicks(8765), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://i.imgur.com/WeTQtUK.png", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Education" },
                    { 11, new DateTime(2020, 6, 10, 16, 29, 44, 211, DateTimeKind.Utc).AddTicks(8752), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://i.imgur.com/ArAIUQ0.png", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Blockchain" },
                    { 2, new DateTime(2020, 6, 10, 16, 29, 44, 211, DateTimeKind.Utc).AddTicks(8567), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://i.imgur.com/dX9t5lS.png", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Airlines" },
                    { 3, new DateTime(2020, 6, 10, 16, 29, 44, 211, DateTimeKind.Utc).AddTicks(8597), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://i.imgur.com/TC7qZPP.png", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Healthcare" },
                    { 4, new DateTime(2020, 6, 10, 16, 29, 44, 211, DateTimeKind.Utc).AddTicks(8613), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://i.imgur.com/AO7gOGs.png", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Automobile" },
                    { 5, new DateTime(2020, 6, 10, 16, 29, 44, 211, DateTimeKind.Utc).AddTicks(8626), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://i.imgur.com/MWr58IA.png", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Business Services" },
                    { 6, new DateTime(2020, 6, 10, 16, 29, 44, 211, DateTimeKind.Utc).AddTicks(8678), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://i.imgur.com/evwFFDj.png", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Space" },
                    { 7, new DateTime(2020, 6, 10, 16, 29, 44, 211, DateTimeKind.Utc).AddTicks(8694), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://i.imgur.com/io9aGef.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Building" },
                    { 8, new DateTime(2020, 6, 10, 16, 29, 44, 211, DateTimeKind.Utc).AddTicks(8707), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://i.imgur.com/0aD3uZj.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Agriculture" },
                    { 16, new DateTime(2020, 6, 10, 16, 29, 44, 211, DateTimeKind.Utc).AddTicks(8817), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://i.imgur.com/GMNHXMs.png", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tourism" },
                    { 9, new DateTime(2020, 6, 10, 16, 29, 44, 211, DateTimeKind.Utc).AddTicks(8723), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://i.imgur.com/MIkU108.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Casino" },
                    { 10, new DateTime(2020, 6, 10, 16, 29, 44, 211, DateTimeKind.Utc).AddTicks(8738), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://i.imgur.com/TFcnTmD.png", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fashion" }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 9, new DateTime(2020, 6, 10, 16, 29, 44, 212, DateTimeKind.Utc).AddTicks(1283), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "sky" },
                    { 8, new DateTime(2020, 6, 10, 16, 29, 44, 212, DateTimeKind.Utc).AddTicks(1205), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "bank" },
                    { 7, new DateTime(2020, 6, 10, 16, 29, 44, 212, DateTimeKind.Utc).AddTicks(1191), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "building" },
                    { 6, new DateTime(2020, 6, 10, 16, 29, 44, 212, DateTimeKind.Utc).AddTicks(1177), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "food" },
                    { 5, new DateTime(2020, 6, 10, 16, 29, 44, 212, DateTimeKind.Utc).AddTicks(1162), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ship" },
                    { 4, new DateTime(2020, 6, 10, 16, 29, 44, 212, DateTimeKind.Utc).AddTicks(1146), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "future" },
                    { 3, new DateTime(2020, 6, 10, 16, 29, 44, 212, DateTimeKind.Utc).AddTicks(1131), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "money" },
                    { 2, new DateTime(2020, 6, 10, 16, 29, 44, 212, DateTimeKind.Utc).AddTicks(1102), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "beauty" },
                    { 10, new DateTime(2020, 6, 10, 16, 29, 44, 212, DateTimeKind.Utc).AddTicks(1301), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tech" },
                    { 1, new DateTime(2020, 6, 10, 16, 29, 44, 212, DateTimeKind.Utc).AddTicks(528), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "space" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 9, 3 },
                    { 8, 3 },
                    { 7, 3 },
                    { 6, 3 },
                    { 5, 2 },
                    { 4, 2 },
                    { 3, 2 },
                    { 10, 3 },
                    { 11, 1 },
                    { 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "IndustrySubscriptions",
                columns: new[] { "UserId", "IndustryId" },
                values: new object[,]
                {
                    { 6, 5 },
                    { 6, 2 },
                    { 6, 3 },
                    { 7, 1 },
                    { 7, 5 },
                    { 10, 2 },
                    { 7, 7 },
                    { 7, 9 },
                    { 8, 8 },
                    { 8, 2 },
                    { 8, 4 },
                    { 8, 9 },
                    { 9, 1 },
                    { 9, 3 },
                    { 9, 7 },
                    { 9, 6 },
                    { 9, 14 },
                    { 10, 11 },
                    { 10, 10 },
                    { 10, 1 },
                    { 10, 4 },
                    { 6, 9 }
                });

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "Id", "AuthorId", "CreatedOn", "DeletedOn", "Description", "ImgUrl", "IndustryId", "IsDeleted", "IsFeatured", "IsPending", "ModifiedOn", "Summary", "Title" },
                values: new object[,]
                {
                    { 15, 2, new DateTime(2020, 6, 7, 4, 22, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "In this paper we quantify the usage of main resources (CPU, main memory, disk space and network bandwidth) of Windows 2000 machines from classroom laboratories. For that purpose, 169 machines of 11 classroom laboratories of an academic institution were monitored over 77 consecutive days. Samples were collected from all machines every 15 minutes for a total of 583653 samples. Besides evaluating availability of machines (uptime and downtime) and usage habits of users, the paper assesses usage of main resources, focusing on the impact of interactive login sessions over resource consumptions. Also, recurring to Self Monitoring Analysis and Reporting Technology (SMART) parameters of hard disks, the study estimates the average uptime per hard drive power cycle for the whole life of monitored computers. The paper also analyzes the potential of non-dedicated classroom Windows machines for distributed and parallel computing, evaluating the mean stability of group of machines. Our results show that resources idleness in classroom computers is very high, with an average CPU idleness of 97.93%, unused memory averaging 42.06% and unused disk space of the order of gigabytes per machine. Moreover, this study confirms the 2:1 equivalence rule found out by similar works, with N non-dedicated resources delivering an average CPU computing power roughly similar to N/2 dedicated machines. These results confirm the potentiality of these systems for resource harvesting, especially for grid desktop computing schemes. However, the efficient exploitation of the computational power of these environments requires adaptive fault-tolerance schemes to overcome the high volatility of resources.", "https://i.imgur.com/CxJ0348.png", 13, false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Studies focusing on Unix have shown that the vast majority of workstations and desktop computers remain idle for most of the time.", "Resources Usage of Windows Computer Laboratories" },
                    { 3, 2, new DateTime(2020, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Despite pricing pressures and tariff concerns, measures of optimism are approaching the record-setting highs measured in spring 2018, according to the latest PNC semi-annual survey of small and mid-size business owners and executives.", "https://images.financialexpress.com/2019/06/ECONOMIC_SURVEY_660.jpg", 5, false, true, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Optimism Still Springs This Fall for Small and Mid-size Business Owners", "Fall Economic Outlook Survey" },
                    { 1, 3, new DateTime(2020, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Real GDP contracted 4.8 percent at an annualized rate in the first quarter according to the advance estimate from the Bureau of Economic Analysis, worse than the consensus expectation for a 4.0 percent decline. The coronavirus pandemic and restrictions on movement led to big declines in consumer spending and business investment (down 7.6 percent and 8.6 percent annualized, respectively).", "https://www.agenda-bg.com/wp-content/uploads/2016/09/What-To-Expect-In-2015-An-Economic-Outlook-For-Small-And-Medium-Businesses-Making-It-TV.jpg", 5, false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "April 2020 National Economic Outlook by PINACLE", "National Economic Outlook" },
                    { 19, 2, new DateTime(2019, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This article is based on the IFCN Dairy Report 2019. This annual report summarises the work of IFCN Research Partners from over 100 countries. IFCN is a global network for dairy economic research and consultancy. In 2019, researchers from over 100 countries and more than 140 agribusiness companies are members of the network. IFCN has created a better understanding of the dairy world for 20 years. Key insights 2019 will be the year of lowest milk production growth since 2013. As this did not translate into milk price increases, IFCN identifies a structural drop in demand growth as one of the reasons. Milk production trends by regions are highly diverse and dynamic. The 3-5% rule indicates that strong regions grow and weak ones decline by this rate every year. Dairy farm structure dynamics drive milk supply and the speed of change is under-estimated. IFCN recommends using the annual growth of milk production per farm as an indicator. In the EU and the USA farms grew by 8% per year. The key driver for farm structure developments lies in dairy farm economics and the current structure of economies of scale. The Dairy Report analyses this in over 50 countries. The IFCN Dairy Report has been published annually since 2000 and has become a guideline publication for researchers and companies involved in the dairy chain. It enables to gain a global holistic view of the industry and serves as a solid fact base for discussions and strategic decisions.", "https://i0.wp.com/edairynews.com/en/wp-content/uploads/2020/06/Global-Dairy-Commodity-Update-June-2020.jpg?fit=1024%2C683&ssl=1", 15, false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This annual report summarises the work of IFCN Research Partners from over 100 countries.", "Global Dairy Trends and Drivers 2019" },
                    { 5, 3, new DateTime(2020, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Foreword Airline fare setting is a complex and dynamic process, varying by carrier, route and time. It lies at the heart of an airline’s commercial strategy as it aims to maximise the return on its assets employed, namely its aircraft and its people. In the following report, we have sought to describe clearly and comprehensively how the numerous factors an airline has to juggle play into the fare that passengers ultimately face. As a team who has an airline background, and advises both airlines and airports on matters of strategy, ICF has been uniquely well placed to do this.", "https://www.aljazeera.com/mritems/Images/2019/12/4/b604dd8d57c942a892cdc71f21d09973_18.jpg", 2, false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "An ICF Report prepared for ACI Europe", "Identifying the Drivers of Air Fares" },
                    { 6, 3, new DateTime(2020, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Since the introduction of the smartphone, it has become clear that customers are quick to adopt even highly complex and expensive technology if it makes their lives easier. In other words, users value convenience and ease. These core values turned the automobile into the defining technical cultural item of the 20th century. Now it is time to translate these properties into the context of today's – and tomorrow's –  technology and society. ", "https://www.car-brand-names.com/wp-content/uploads/2016/04/Cars.jpg", 4, false, true, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Welcome to the age of radical change in the automotive industry", "Five Trends Transforming the Automotive Industry" },
                    { 7, 3, new DateTime(2020, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This Report contains the conclusions from the comprehensive review of the Slovenian banking sector undertaken by the Bank of Sloveniain cooperation with the Slovenian Ministry of Finance over the period June to December 2013. This Asset Quality Review and Stress Test is a cornerstone in the broader initiative to restore the health in the Slovenian banking sector.", "https://www.mindtree.com/sites/default/files/2018-08/Digital%20Banking%20Solution-Mindtree-H.jpg", 5, false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Asset Quality Review and Stress Test were closely monitored by the international organisations (IOs), constituted of the European Commission, the European Central Bank, and the European Banking Authority.", "Full report on the comprehensive review of the banking system" },
                    { 10, 3, new DateTime(2020, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This report covers Audit and accounting; Business services; Professional services; and Legal services.", "https://magneticonemobile.com/wp-content/uploads/2018/01/slide1-business.jpg", 1, false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This report covers: a description of the sector, the current EU regulatory regime, existing frameworks for how trade is facilitated between countries in this sector, and sector views. It does not contain commercially-, market-or negotiation-sensitive information.", "Professional and Business Services Sector Report" },
                    { 13, 3, new DateTime(2020, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Start-Up Space series examines space investment in the 21st century and analyzes investment trends, focusing on companies that began as angel- and venture capital-backed startups. The report tracks publicly-reported seed, venture, and private equity investment in start-up space ventures as they grow and mature, from 2000 through the end of 2019. The report includes debt financing for these companies where applicable to provide a complete picture of the capital available to them, and also highlights merger and acquisition (M&A) and initial public offering (IPO) activity.", "https://i.imgur.com/9ir2Qak.png", 6, false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Update on Investment in Commercial Space Ventures", "Start-Up Space Report 2020" },
                    { 16, 3, new DateTime(2020, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A graphic that provides foundational data on China’s orbital launch sites and launch vehicles, as well as on the general structure of China’s state-managed space industry. Includes operational vehicles and vehicles with a high probably of entering operations during the next 2 years. Partial failures counted as failures in reliability calculation.", "https://i.ytimg.com/vi/4wPSyl2Yb1M/maxresdefault.jpg", 6, false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A graphic that provides foundational data on China’s orbital launch sites and launch vehicles, as well as on the general structure of China’s state-managed space industry.", "China’s Orbital Launch Activity" },
                    { 18, 3, new DateTime(2020, 6, 7, 12, 40, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "WASHINGTON — A study that found that every small satellite launched commercially in the last five years suffered delays is evidence of the need of greater standardization in payload accommodations so that smallsats can easily switch vehicles, one company argues.", "https://i.imgur.com/028Yd8V.png", 6, false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Smallsat launch delays prompt push for greater standardization", "Smallsat Launch Delays" },
                    { 23, 3, new DateTime(2020, 6, 4, 10, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Currently, Construction is 99.9% complete. Commissioning Pre-functional test was completed by ARUP and draft report has been received. MEOR is scheduled to review and evaluate draft report from ARUP. Once MEOR reviews draft report, ARUP will issue final  Commissioning  report.  Substantial  Completion  is  estimated  to  be  issued  in  August,  after  the  issuance  of the final Commissioning report and Punch list items are issued to the Contractor.", "https://specials-images.forbesimg.com/imageserve/5c0077cc31358e5b43383ffc/960x0.jpg?fit=scale", 7, false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Los Angeles Mission College Gateway Science and Engineering Construction Management Monthly Progress Report", "Los Angeles College Monthly Construction Report" },
                    { 24, 3, new DateTime(2020, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A wide range of new and existing commercial technologies depend on reliable communications with spacecraft.  The cost, integrity, and reliability of these communications can be negatively affected by orbital debris, which presents an ever-increasing threat to operational spacecraft.  The environment in space continues to change and evolve in the New Space Age as increasing numbers of satellites are launched and new satellite technology is developed.  The regulations we adopt today are designed to ensure that the Commission’s actions concerning radio communications, including licensing U.S. spacecraft and granting access to the U.S. market for non-U.S. spacecraft, mitigate the growth of orbital debris, while at the same time not creating undue regulatory obstacles to new satellite ventures.  This action will help to ensure that Commission decisions are consistent with the public interest in space remaining viable for future satellites and systems and the many services that those systems provide to the public.", "https://aerospacedefenseforum.org/wp-content/uploads/2019/02/shutterstock_557310703_space_technology.jpg", 6, false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Report and Order and Further Notice of Proposed Rulemaking, IB Docket No. 18-313 ", "Mitigation of Orbital Debris in the New Space Age" },
                    { 22, 2, new DateTime(2020, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cruise Lines International Association (CLIA), the world’s largest cruise industry trade association, has released the 2019 Cruise Trends and State of the Cruise Industry Outlook. The report offers a look at the trends impacting cruise travel in the coming year and beyond as well as the overall global economic impact. Cruise Lines International Association (CLIA) is the unified global organizationhelping members succeed by advocating, educating and promoting for the common interests of the cruise community.", "https://www.gannett-cdn.com/presto/2018/12/20/USAT/b3fa36d4-b0ed-48c5-a991-924c48685469-Costa_LNG__Side_Perspective.jpg?crop=2999,1687,x0,y299&width=2999&height=1687&format=pjpg&auto=webp", 16, false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Instagram photos are driving interest in travel around the world. With onboard connectivity, cruise passengers are filling Instagram feeds with diverse travel experiences both onboard and on land from several cruise destinations.", "2019 Cruise Trends & Industry Outlook" },
                    { 2, 4, new DateTime(2020, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "PNC's Market Expectation Survey lists the current consensus forecast for key economic data releases for the upcoming week, as well as PNC's own forecast for each item. A comprehensive calendar listing recent data for key economic indicators is provided on the second page.", "https://www.questionpro.com/blog/wp-content/uploads/2018/05/Market-Survey_Final-800x478.jpg", 5, false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A comprehensive calendar listing recent data for key economic indicators.", "Market Expectations Survey" },
                    { 8, 4, new DateTime(2020, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Distributed Ledger Technology (DLT) (also known as blockchain technology or distributed database technology) has attracted significant interest and funding in the financial services industry in recent years. Several large financial institutions have established dedicated teams to explore the technology, and some market participants have formed consortia to create industry standards.2 According to a 2016 report by the World Economic Forum,3 over the past three years more than $1.4 billion has been invested in this technology to explore and implement uses in the financial services industry.", "https://www.jagranjosh.com/imported/images/E/Articles/What-is-Blockchain-Technology.png", 11, false, true, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Implicationsof Blockchain for the Securities Industry", "Distributed Ledger Technology" },
                    { 14, 4, new DateTime(2020, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This report discusses the changes that have occurred in the Australian workforce since the end of World War II (1945-2000). A review of some of the available literature provides insights into the changing role of women and migrants in the workforce, and the influence of new technologies and changing levels of unemployment have also been considered.", "https://upload.wikimedia.org/wikipedia/commons/1/1a/Chapin_Hall%2C_Williams_College_-_Williamstown%2C_Massachusetts.jpg", 7, false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The information presented in this report has been gathered from secondary sources, and from Australian Bureau of Statistics’ data.", "The Change in the Australian Work Force Since the End of World War 2" },
                    { 25, 4, new DateTime(2020, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The report is divided into three sections. The first part reviews the supply side of the pub market, revealing numbers, trends and the contrasting fortunes of different sectors, and identifying some of the areas in which pubs are succeeding. The second section analyses the customer base: their demographics, habits and motivations. The final part takes a look at reasons for optimism, with insights into increasing appeal and sales and the emerging new breeds of pub in Britain. At a time of great challenges for both the out of home eating and drinking market and the UK economy as a whole, this report highlights the many positive trends and developments in the British pub market. We hope you enjoy reading it.", "https://imagesvc.meredithcorp.io/v3/mm/image?url=https%3A%2F%2Fstatic.onecms.io%2Fwp-content%2Fuploads%2Fsites%2F9%2F2019%2F12%2Fuk-pubs-growth-FT-BLOG1219.jpg&q=85", 17, false, true, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This report draws on CGA’s unrivalled suite of research services to provide a comprehensive picture of Great Britain’s pubs and their opportunities for growth", "The British Pub Market 2019" },
                    { 21, 2, new DateTime(2020, 6, 1, 11, 15, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The way we make, use and throwaway our clothes is unsustainable. Textile production contributes more to climate change than international aviation and shipping combined, consumes lake-sized volumes of fresh water and creates chemical and plastic pollution. Synthetic fibres are being found in the deep sea, in Arctic sea ice, in fish and shellfish. Our biggest retailers have ‘chased the cheap needle around the planet’, commissioning production  in  countries  with  low  pay,  little  trade  union  representation  and  weak  environmental protection. In many countries, poverty pay and conditions are standard for garment workers, most of whom are women. We are also concerned about the use of  child  labour,  prison  labour,  forced  labour  and  bonded  labour  in  factories  and  the  garment supply chain. Fast fashions’ overproduction and overconsumption of clothing is based on the globalisation of indifference towards these manual workers.", "http://intrigue.ie/media/2014/04/fashion-industry-1.jpg", 10, false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fixing Fashion: Clothing Consumption and Sustainability", "Sustainability of the Fashion Industry" },
                    { 4, 5, new DateTime(2019, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "IN MEMORIAM - \"The future is not free: the story of all human progress is one of a struggle against all odds.We learned again that this America, which Abraham Lincoln called the last,best hope of man on Earth, was built on heroism and noble sacrifice.It was built by men and women like our seven star voyagers, who answered a call beyond duty, who gave more than was expected or required and who gave it little thought of worldly reward.\"- President Ronald Reagan January 31, 1986", "https://i.imgur.com/jEPMztX.png", 6, false, true, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Report to the President By the presidential commission - on the Space Shuttle Challenger Accident", "Report to the President by the presidential commission" },
                    { 9, 5, new DateTime(2020, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This report is intended to be of practical use in reducing the environmental impacts of construction. Nicole Lazarus will be glad to hear from any readers with feedback and examples of its application.", "https://5chat.com/wp-content/uploads/building-materials.jpg", 7, false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Toolkit for Carbon Neutral Developments", "Construction Materials Report" },
                    { 11, 5, new DateTime(2020, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Casino Gambling has become a majorindustry in the United States over the past twodecades.  Nationwide, annual casino revenue tops $40billion. This report provides an analysis of casino gam-bling in the United States and discusses the economicissues surrounding casino gambling. The informationcontained in this report should prove useful to localofficials and policy-makers who may be considering theadoption of casino gambling or who already have casi-no gambling in their jurisdictions.", "https://royalepalmscasino-sofia.com/wp-content/uploads/2019/04/IMG_1710-1024x683.jpg", 9, false, true, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This report provides an analysis of casino gam - bling in the United States and discusses the economicissues surrounding casino gambling.", "Casino Gambling in America and Its Economic Impacts" },
                    { 20, 5, new DateTime(2020, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Aerospace Industries Association (AIA) is the most authoritative and influential trade association representing the aerospace and defense industry. We are the leading voice for the industry on Capitol Hill, within the administration, and internationally.", "https://i.imgur.com/jXUg87d.png", 2, false, true, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aerospace Industry Report 3rd Edition by The Aerospace Industries Association (AIA)", "Aerospace Industry 2012 Report" },
                    { 17, 2, new DateTime(2020, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This page intentionally left blank.", "https://media.nationalgeographic.org/assets/photos/120/983/091a0e2f-b93d-481b-9a60-db520c87ec33.jpg", 8, false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Corn Production Down 4 Percent from 2018. Soybean Production Down 19 Percent from 2018. Cotton Production Up 23 Percent from 2018. Winter Wheat Production Up 3 Percent from July Forecast", "USDA Crop Production Report 2019" },
                    { 12, 5, new DateTime(2020, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Particularly in Central and South America, expansion of pastures for livestock production has been one of the driving forces behind this wholesale destruction.Deforestation causes incalculable environmental damage, releasing billions of tonnes of carbon dioxide into the atmosphere and driving thousands of species of life to extinction each year. Effective policies are urgently needed to discourage expansion of livestock production in forest areas and promote sustainable grazing systems that will halt the cycle of degradation and abandonment on cleared forest lands.", "https://fayranches.com/wp-content/uploads/2016/01/savvy-investors-cattle-ranches.jpg", 8, false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Over the past quarter century, forests have been cleared from an area the size of India.", "Cattle Ranching and Deforestation" }
                });

            migrationBuilder.InsertData(
                table: "DownloadedReports",
                columns: new[] { "UserId", "ReportId" },
                values: new object[,]
                {
                    { 8, 3 },
                    { 10, 9 },
                    { 6, 9 },
                    { 8, 4 },
                    { 8, 14 },
                    { 9, 8 },
                    { 7, 23 },
                    { 10, 18 },
                    { 9, 18 },
                    { 7, 18 },
                    { 6, 18 },
                    { 8, 18 },
                    { 7, 13 },
                    { 10, 19 },
                    { 6, 5 },
                    { 9, 13 },
                    { 7, 5 },
                    { 9, 6 },
                    { 6, 7 },
                    { 10, 5 }
                });

            migrationBuilder.InsertData(
                table: "ReportTags",
                columns: new[] { "ReportId", "TagId" },
                values: new object[,]
                {
                    { 18, 1 },
                    { 18, 9 },
                    { 18, 10 },
                    { 23, 7 },
                    { 2, 3 },
                    { 9, 7 },
                    { 1, 3 },
                    { 1, 8 },
                    { 21, 2 },
                    { 20, 9 }
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

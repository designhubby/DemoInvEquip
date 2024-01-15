using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InvEquip.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vendor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roles_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "date", nullable: true),
                    EndDate = table.Column<DateTime>(type: "date", nullable: true),
                    VendorId = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contract_Vendor_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HwModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HwModelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeviceTypeId = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    VendorId = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HwModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HwModels_DeviceType_DeviceTypeId",
                        column: x => x.DeviceTypeId,
                        principalTable: "DeviceType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HwModels_Vendor_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                    table.ForeignKey(
                        name: "FK_People_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HwModelId = table.Column<int>(type: "int", nullable: false),
                    ServiceTag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssetNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractId = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Devices_Contract_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Devices_HwModels_HwModelId",
                        column: x => x.HwModelId,
                        principalTable: "HwModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonDevices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    DeviceId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "date", nullable: true),
                    EndDate = table.Column<DateTime>(type: "date", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonDevices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonDevices_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonDevices_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Department",
                columns: new[] { "Id", "DepartmentName" },
                values: new object[,]
                {
                    { 1, "Unassigned" },
                    { 2, "TestDepartment02" },
                    { 3, "TestDepartment03" },
                    { 4, "TestDepartment04" },
                    { 5, "TestDepartment05" },
                    { 6, "TestDepartment06" }
                });

            migrationBuilder.InsertData(
                table: "DeviceType",
                columns: new[] { "Id", "DeviceTypeName" },
                values: new object[,]
                {
                    { 6, "TestDeviceType06" },
                    { 5, "TestDeviceType05" },
                    { 4, "TestDeviceType04" },
                    { 3, "TestDeviceType03" },
                    { 2, "TestDeviceType02" },
                    { 1, "Unassigned" }
                });

            migrationBuilder.InsertData(
                table: "Vendor",
                columns: new[] { "Id", "VendorName" },
                values: new object[,]
                {
                    { 1, "Unassigned" },
                    { 2, "TestVendor02" },
                    { 3, "TestVendor03" },
                    { 4, "TestVendor04" },
                    { 5, "TestVendor05" },
                    { 6, "TestVendor06" }
                });

            migrationBuilder.InsertData(
                table: "Contract",
                columns: new[] { "Id", "ContractName", "EndDate", "StartDate", "VendorId" },
                values: new object[,]
                {
                    { 11, "TestContract011", new DateTime(1980, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6 },
                    { 10, "TestContract010", new DateTime(1980, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 5, "TestContract05", new DateTime(1980, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 9, "TestContract09", new DateTime(1980, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 1, "Unassigned", new DateTime(1980, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 4, "TestContract04", new DateTime(1980, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 2, "TestContract02", new DateTime(1980, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 7, "TestContract07", new DateTime(1980, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 6, "TestContract06", new DateTime(1980, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6 },
                    { 3, "TestContract03", new DateTime(1980, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 8, "TestContract08", new DateTime(1980, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 }
                });

            migrationBuilder.InsertData(
                table: "HwModels",
                columns: new[] { "Id", "DeviceTypeId", "HwModelName", "VendorId" },
                values: new object[,]
                {
                    { 5, 5, "TestHwModel05", 5 },
                    { 4, 4, "TestHwModel04", 4 },
                    { 7, 3, "TestHwModel07", 3 },
                    { 6, 2, "TestHwModel06", 2 },
                    { 2, 2, "TestHwModel02", 2 },
                    { 1, 1, "Unassigned", 1 },
                    { 3, 3, "TestHwModel03", 3 }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "DepartmentId", "RoleName" },
                values: new object[,]
                {
                    { 6, 1, "TestRole06" },
                    { 5, 1, "TestRole05" },
                    { 4, 1, "TestRole04" },
                    { 3, 1, "TestRole03" },
                    { 2, 1, "TestRole02" },
                    { 1, 1, "Unassigned" }
                });

            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "Id", "AssetNumber", "ContractId", "DeviceName", "HwModelId", "Notes", "ServiceTag" },
                values: new object[,]
                {
                    { 23, "A0001023", 3, "TestDevice000023", 3, "TestNotes023", "A1A023" },
                    { 38, "A0001038", 3, "TestDevice000038", 3, "TestNotes038", "A1A038" },
                    { 4, "A000104", 4, "TestDevice00004", 4, "TestNotes04", "A1A04" },
                    { 9, "A000109", 4, "TestDevice00009", 4, "TestNotes09", "A1A09" },
                    { 14, "A0001014", 4, "TestDevice000014", 4, "TestNotes014", "A1A014" },
                    { 19, "A0001019", 4, "TestDevice000019", 4, "TestNotes019", "A1A019" },
                    { 24, "A0001024", 4, "TestDevice000024", 4, "TestNotes024", "A1A024" },
                    { 29, "A0001029", 4, "TestDevice000029", 4, "TestNotes029", "A1A029" },
                    { 34, "A0001034", 4, "TestDevice000034", 4, "TestNotes034", "A1A034" },
                    { 39, "A0001039", 4, "TestDevice000039", 4, "TestNotes039", "A1A039" },
                    { 5, "A000105", 5, "TestDevice00005", 5, "TestNotes05", "A1A05" },
                    { 10, "A0001010", 5, "TestDevice000010", 5, "TestNotes010", "A1A010" },
                    { 15, "A0001015", 5, "TestDevice000015", 5, "TestNotes015", "A1A015" },
                    { 20, "A0001020", 5, "TestDevice000020", 5, "TestNotes020", "A1A020" },
                    { 25, "A0001025", 5, "TestDevice000025", 5, "TestNotes025", "A1A025" },
                    { 30, "A0001030", 5, "TestDevice000030", 5, "TestNotes030", "A1A030" },
                    { 35, "A0001035", 5, "TestDevice000035", 5, "TestNotes035", "A1A035" },
                    { 40, "A0001040", 5, "TestDevice000040", 5, "TestNotes040", "A1A040" },
                    { 6, "A000106", 6, "TestDevice00006", 6, "TestNotes06", "A1A06" },
                    { 11, "A0001011", 6, "TestDevice000011", 6, "TestNotes011", "A1A011" },
                    { 16, "A0001016", 6, "TestDevice000016", 6, "TestNotes016", "A1A016" },
                    { 21, "A0001021", 6, "TestDevice000021", 6, "TestNotes021", "A1A021" },
                    { 26, "A0001026", 6, "TestDevice000026", 6, "TestNotes026", "A1A026" },
                    { 31, "A0001031", 6, "TestDevice000031", 6, "TestNotes031", "A1A031" },
                    { 33, "A0001033", 3, "TestDevice000033", 3, "TestNotes033", "A1A033" },
                    { 28, "A0001028", 3, "TestDevice000028", 3, "TestNotes028", "A1A028" },
                    { 41, "A0001041", 6, "TestDevice000041", 6, "TestNotes041", "A1A041" },
                    { 18, "A0001018", 3, "TestDevice000018", 3, "TestNotes018", "A1A018" },
                    { 36, "A0001036", 6, "TestDevice000036", 6, "TestNotes036", "A1A036" },
                    { 1, "A0001", 1, "Device0000", 1, "TestNotes", "A1A" },
                    { 2, "A000102", 2, "TestDevice00002", 2, "TestNotes02", "A1A02" },
                    { 7, "A000107", 2, "TestDevice00007", 2, "TestNotes07", "A1A07" },
                    { 17, "A0001017", 2, "TestDevice000017", 2, "TestNotes017", "A1A017" },
                    { 22, "A0001022", 2, "TestDevice000022", 2, "TestNotes022", "A1A022" },
                    { 12, "A0001012", 2, "TestDevice000012", 2, "TestNotes012", "A1A012" },
                    { 32, "A0001032", 2, "TestDevice000032", 2, "TestNotes032", "A1A032" },
                    { 37, "A0001037", 2, "TestDevice000037", 2, "TestNotes037", "A1A037" },
                    { 3, "A000103", 3, "TestDevice00003", 3, "TestNotes03", "A1A03" },
                    { 8, "A000108", 3, "TestDevice00008", 3, "TestNotes08", "A1A08" },
                    { 13, "A0001013", 3, "TestDevice000013", 3, "TestNotes013", "A1A013" },
                    { 27, "A0001027", 2, "TestDevice000027", 2, "TestNotes027", "A1A027" }
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "Fname", "Lname", "RoleId" },
                values: new object[] { 8, "FirstNameTest08", "LastNameTest08", 2 });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "Fname", "Lname", "RoleId" },
                values: new object[,]
                {
                    { 2, "FirstNameTest02", "LastNameTest02", 2 },
                    { 4, "FirstNameTest04", "LastNameTest04", 2 },
                    { 6, "FirstNameTest06", "LastNameTest06", 2 },
                    { 10, "FirstNameTest010", "LastNameTest010", 2 },
                    { 13, "FirstNameTest013", "LastNameTest013", 3 },
                    { 3, "FirstNameTest03", "LastNameTest03", 3 },
                    { 5, "FirstNameTest05", "LastNameTest05", 3 },
                    { 7, "FirstNameTest07", "LastNameTest07", 3 },
                    { 9, "FirstNameTest09", "LastNameTest09", 3 },
                    { 11, "FirstNameTest011", "LastNameTest011", 3 },
                    { 12, "FirstNameTest012", "LastNameTest012", 2 },
                    { 1, "FirstNameTest", "LastNameTest", 1 }
                });

            migrationBuilder.InsertData(
                table: "PersonDevices",
                columns: new[] { "Id", "DeviceId", "EndDate", "PersonId", "StartDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(1980, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(1980, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, new DateTime(2000, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2000, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 2, new DateTime(2000, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2000, 2, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 2, new DateTime(2000, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2000, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 2, new DateTime(2000, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2000, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 3, new DateTime(2000, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2000, 2, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 3, new DateTime(2000, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2000, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 3, new DateTime(2000, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2000, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
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
                name: "IX_Contract_VendorId",
                table: "Contract",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_ContractId",
                table: "Devices",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_HwModelId",
                table: "Devices",
                column: "HwModelId");

            migrationBuilder.CreateIndex(
                name: "IX_HwModels_DeviceTypeId",
                table: "HwModels",
                column: "DeviceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_HwModels_VendorId",
                table: "HwModels",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_People_RoleId",
                table: "People",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonDevices_DeviceId",
                table: "PersonDevices",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonDevices_PersonId",
                table: "PersonDevices",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_DepartmentId",
                table: "Roles",
                column: "DepartmentId");
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
                name: "PersonDevices");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropTable(
                name: "HwModels");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "DeviceType");

            migrationBuilder.DropTable(
                name: "Vendor");

            migrationBuilder.DropTable(
                name: "Department");
        }
    }
}

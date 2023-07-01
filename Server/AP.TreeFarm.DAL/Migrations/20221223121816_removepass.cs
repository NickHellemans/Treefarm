using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AP.MyTreeFarm.Infrastructure.Migrations
{
    public partial class removepass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Employee");

            migrationBuilder.EnsureSchema(
                name: "Site");

            migrationBuilder.EnsureSchema(
                name: "Tree");

            migrationBuilder.EnsureSchema(
                name: "TreeTask");

            migrationBuilder.EnsureSchema(
                name: "Zone");

            migrationBuilder.CreateTable(
                name: "tblEmployees",
                schema: "Employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Auth0Id = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblEmployees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblSites",
                schema: "Site",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    StreetNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MapPicturePath = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSites", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblTrees",
                schema: "Tree",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PictureUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    InstructionsUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    QrCodeUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTrees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblZones",
                schema: "Zone",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SurfaceArea = table.Column<double>(type: "float", maxLength: 255, nullable: false),
                    SiteId = table.Column<int>(type: "int", nullable: false),
                    TreeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblZones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblZones_tblSites_SiteId",
                        column: x => x.SiteId,
                        principalSchema: "Site",
                        principalTable: "tblSites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblZones_tblTrees_TreeId",
                        column: x => x.TreeId,
                        principalSchema: "Tree",
                        principalTable: "tblTrees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblTreeTasks",
                schema: "TreeTask",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: false),
                    DateStart = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateEnd = table.Column<DateTime>(type: "datetime", nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    ZoneId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    DatePlanned = table.Column<DateTime>(type: "datetime", nullable: false),
                    DatePaused = table.Column<DateTime>(type: "datetime", nullable: true),
                    TimePaused = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTreeTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblTreeTasks_tblEmployees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "Employee",
                        principalTable: "tblEmployees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblTreeTasks_tblZones_ZoneId",
                        column: x => x.ZoneId,
                        principalSchema: "Zone",
                        principalTable: "tblZones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Employee",
                table: "tblEmployees",
                columns: new[] { "Id", "Auth0Id", "Email", "EmployeeId", "FirstName", "IsActive", "IsAdmin", "LastName", "UserName" },
                values: new object[,]
                {
                    { 1, "auth0|63a21b713393043784814183", "redphorcys@hotmail.com", "W_123", "Bert", true, false, "Bibber", "BibberendeBert" },
                    { 2, "auth0|63a21af0e2c4faec70d45c72", "nick-hellemans@hotmail.com", "W_124", "Bart", true, false, "Bobber", "BobberendeBart" },
                    { 3, "auth0|63741b6967ad3e09030c7bfe", "s115990@ap.be", "A_1", "Nick", true, true, "Hellemans", "Hel_Nick" },
                    { 4, "auth0|63a42ea844641e0a186ca430", "s117923@ap.be", "A_1", "Chad", true, false, "Thunderglock", "Chad" }
                });

            migrationBuilder.InsertData(
                schema: "Site",
                table: "tblSites",
                columns: new[] { "Id", "MapPicturePath", "Name", "PostalCode", "Street", "StreetNumber" },
                values: new object[,]
                {
                    { 1, "farm_map.png", "Site_Ellerman", "2000", "Ellermanstraat", "61" },
                    { 2, "farm_map.png", "Site_Meir", "2000", "Lange Nieuwstraat", "35" }
                });

            migrationBuilder.InsertData(
                schema: "Tree",
                table: "tblTrees",
                columns: new[] { "Id", "InstructionsUrl", "Name", "PictureUrl", "QrCodeUrl" },
                values: new object[,]
                {
                    { 1, "appelboom.pdf", "Malus sylvestris", "appelboom.jpg", "appelboomQR.png" },
                    { 2, "pruimenboom.pdf", "Reine Claude d'Oullins", "pruimenboom.jpg", "pruimenboomQR.png" }
                });

            migrationBuilder.InsertData(
                schema: "Zone",
                table: "tblZones",
                columns: new[] { "Id", "Name", "SiteId", "SurfaceArea", "TreeId" },
                values: new object[,]
                {
                    { 1, "Eller_Zone1", 1, 0.5, 1 },
                    { 2, "Eller_Zone2", 1, 0.5, 2 },
                    { 3, "Meir_Zone1", 2, 0.25, 1 },
                    { 4, "Meir_Zone2", 2, 0.25, 2 }
                });

            migrationBuilder.InsertData(
                schema: "TreeTask",
                table: "tblTreeTasks",
                columns: new[] { "Id", "DateCreated", "DateEnd", "DatePaused", "DatePlanned", "DateStart", "Description", "Duration", "EmployeeId", "Name", "Priority", "Status", "TimePaused", "ZoneId" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 12, 23, 13, 18, 16, 2, DateTimeKind.Local).AddTicks(5611), new DateTime(2022, 12, 24, 14, 18, 16, 2, DateTimeKind.Local).AddTicks(5657), null, new DateTime(2022, 12, 24, 13, 18, 16, 2, DateTimeKind.Local).AddTicks(5661), new DateTime(2022, 12, 24, 13, 18, 16, 2, DateTimeKind.Local).AddTicks(5652), "Uitleg taak1", 50, 1, "Taak1", 1, 3, 0, 1 },
                    { 2, new DateTime(2022, 12, 23, 13, 18, 16, 2, DateTimeKind.Local).AddTicks(5664), new DateTime(2022, 12, 24, 15, 23, 16, 2, DateTimeKind.Local).AddTicks(5668), null, new DateTime(2022, 12, 24, 13, 18, 16, 2, DateTimeKind.Local).AddTicks(5670), new DateTime(2022, 12, 24, 14, 23, 16, 2, DateTimeKind.Local).AddTicks(5666), "Uitleg taak2", 60, 1, "Taak2", 2, 3, 0, 2 },
                    { 3, new DateTime(2022, 12, 23, 13, 18, 16, 2, DateTimeKind.Local).AddTicks(5672), new DateTime(2022, 12, 24, 17, 18, 16, 2, DateTimeKind.Local).AddTicks(5676), new DateTime(2022, 12, 24, 16, 18, 16, 2, DateTimeKind.Local).AddTicks(5680), new DateTime(2022, 12, 24, 13, 18, 16, 2, DateTimeKind.Local).AddTicks(5678), new DateTime(2022, 12, 24, 15, 33, 16, 2, DateTimeKind.Local).AddTicks(5674), "Uitleg taak3", 60, 1, "Taak3", 2, 0, 45, 2 },
                    { 4, new DateTime(2022, 12, 23, 13, 18, 16, 2, DateTimeKind.Local).AddTicks(5682), new DateTime(2022, 12, 24, 19, 23, 16, 2, DateTimeKind.Local).AddTicks(5685), null, new DateTime(2022, 12, 24, 13, 18, 16, 2, DateTimeKind.Local).AddTicks(5687), new DateTime(2022, 12, 24, 17, 23, 16, 2, DateTimeKind.Local).AddTicks(5683), "Uitleg taak4", 120, 1, "Taak4", 3, 3, 0, 2 },
                    { 5, new DateTime(2022, 12, 23, 13, 18, 16, 2, DateTimeKind.Local).AddTicks(5689), new DateTime(2022, 12, 25, 15, 18, 16, 2, DateTimeKind.Local).AddTicks(5692), null, new DateTime(2022, 12, 25, 13, 18, 16, 2, DateTimeKind.Local).AddTicks(5694), new DateTime(2022, 12, 25, 13, 18, 16, 2, DateTimeKind.Local).AddTicks(5691), "Uitleg taak5", 120, 1, "Taak5", 1, 3, 0, 1 },
                    { 6, new DateTime(2022, 12, 23, 13, 18, 16, 2, DateTimeKind.Local).AddTicks(5697), new DateTime(2022, 12, 24, 15, 18, 16, 2, DateTimeKind.Local).AddTicks(5700), null, new DateTime(2022, 12, 24, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 12, 24, 13, 18, 16, 2, DateTimeKind.Local).AddTicks(5698), "Uitleg taak6", 120, 2, "Taak6", 1, 3, 0, 3 },
                    { 7, new DateTime(2022, 12, 23, 13, 18, 16, 2, DateTimeKind.Local).AddTicks(5705), new DateTime(2022, 12, 24, 18, 8, 16, 2, DateTimeKind.Local).AddTicks(5708), new DateTime(2022, 12, 24, 16, 23, 16, 2, DateTimeKind.Local).AddTicks(5712), new DateTime(2022, 12, 24, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 12, 24, 15, 23, 16, 2, DateTimeKind.Local).AddTicks(5707), "Uitleg taak7", 120, 2, "Taak7", 2, 3, 45, 3 },
                    { 8, new DateTime(2022, 12, 23, 13, 18, 16, 2, DateTimeKind.Local).AddTicks(5715), new DateTime(2022, 12, 24, 19, 18, 16, 2, DateTimeKind.Local).AddTicks(5718), null, new DateTime(2022, 12, 24, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 12, 24, 18, 18, 16, 2, DateTimeKind.Local).AddTicks(5716), "Uitleg taak8", 60, 2, "Taak8", 3, 3, 0, 4 },
                    { 9, new DateTime(2022, 12, 23, 13, 18, 16, 2, DateTimeKind.Local).AddTicks(5722), new DateTime(2022, 12, 24, 20, 3, 16, 2, DateTimeKind.Local).AddTicks(5726), new DateTime(2022, 12, 24, 19, 43, 16, 2, DateTimeKind.Local).AddTicks(5729), new DateTime(2022, 12, 24, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 12, 24, 19, 23, 16, 2, DateTimeKind.Local).AddTicks(5724), "Uitleg taak9", 1, 2, "Taak9", 3, 0, 10, 4 },
                    { 10, new DateTime(2022, 12, 23, 13, 18, 16, 2, DateTimeKind.Local).AddTicks(5731), null, null, new DateTime(2022, 12, 23, 0, 0, 0, 0, DateTimeKind.Local), null, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse lacinia nisi ut urna ullamcorper, quis gravida ex venenatis. Sed elementum maximus eros at dignissim. Pellentesque vitae mi mi. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.", 120, 4, "Takken snoeien", 1, 0, 0, 1 },
                    { 11, new DateTime(2022, 12, 23, 13, 18, 16, 2, DateTimeKind.Local).AddTicks(5736), null, null, new DateTime(2022, 12, 23, 0, 0, 0, 0, DateTimeKind.Local), null, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse lacinia nisi ut urna ullamcorper, quis gravida ex venenatis. Sed elementum maximus eros at dignissim. Pellentesque vitae mi mi. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.", 60, 4, "Gezondheidscheck", 2, 0, 0, 1 },
                    { 12, new DateTime(2022, 12, 23, 13, 18, 16, 2, DateTimeKind.Local).AddTicks(5739), null, null, new DateTime(2022, 12, 23, 0, 0, 0, 0, DateTimeKind.Local), null, "Verwijderen van rotte appels, appels met wormen/rupsen, ... ", 60, 4, "Appelinspectie", 2, 0, 0, 1 },
                    { 13, new DateTime(2022, 12, 23, 13, 18, 16, 2, DateTimeKind.Local).AddTicks(5743), null, null, new DateTime(2022, 12, 23, 0, 0, 0, 0, DateTimeKind.Local), null, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse lacinia nisi ut urna ullamcorper, quis gravida ex venenatis. Sed elementum maximus eros at dignissim. Pellentesque vitae mi mi. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.", 120, 4, "Onkruidverdelging", 0, 0, 0, 1 },
                    { 14, new DateTime(2022, 12, 23, 13, 18, 16, 2, DateTimeKind.Local).AddTicks(5747), null, null, new DateTime(2022, 12, 24, 0, 0, 0, 0, DateTimeKind.Local), null, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse lacinia nisi ut urna ullamcorper, quis gravida ex venenatis. Sed elementum maximus eros at dignissim. Pellentesque vitae mi mi. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.", 300, 4, "Morgen 1", 1, 0, 0, 2 },
                    { 15, new DateTime(2022, 12, 23, 13, 18, 16, 2, DateTimeKind.Local).AddTicks(5751), null, null, new DateTime(2022, 12, 24, 0, 0, 0, 0, DateTimeKind.Local), null, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse lacinia nisi ut urna ullamcorper, quis gravida ex venenatis. Sed elementum maximus eros at dignissim. Pellentesque vitae mi mi. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.", 150, 4, "Morgen 2", 0, 0, 0, 2 },
                    { 16, new DateTime(2022, 12, 23, 13, 18, 16, 2, DateTimeKind.Local).AddTicks(5755), null, null, new DateTime(2022, 12, 24, 0, 0, 0, 0, DateTimeKind.Local), null, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse lacinia nisi ut urna ullamcorper, quis gravida ex venenatis. Sed elementum maximus eros at dignissim. Pellentesque vitae mi mi. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.", 30, 4, "Morgen 3", 0, 0, 0, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblEmployees_Id",
                schema: "Employee",
                table: "tblEmployees",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblSites_Id",
                schema: "Site",
                table: "tblSites",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblTrees_Id",
                schema: "Tree",
                table: "tblTrees",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblTreeTasks_EmployeeId",
                schema: "TreeTask",
                table: "tblTreeTasks",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_tblTreeTasks_Id",
                schema: "TreeTask",
                table: "tblTreeTasks",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblTreeTasks_ZoneId",
                schema: "TreeTask",
                table: "tblTreeTasks",
                column: "ZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_tblZones_Id",
                schema: "Zone",
                table: "tblZones",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblZones_SiteId",
                schema: "Zone",
                table: "tblZones",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_tblZones_TreeId",
                schema: "Zone",
                table: "tblZones",
                column: "TreeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblTreeTasks",
                schema: "TreeTask");

            migrationBuilder.DropTable(
                name: "tblEmployees",
                schema: "Employee");

            migrationBuilder.DropTable(
                name: "tblZones",
                schema: "Zone");

            migrationBuilder.DropTable(
                name: "tblSites",
                schema: "Site");

            migrationBuilder.DropTable(
                name: "tblTrees",
                schema: "Tree");
        }
    }
}

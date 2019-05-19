using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class AddUserModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecruitmentOffices",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: false),
                    ChiefFullName = table.Column<string>(nullable: false),
                    OfficeNo = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecruitmentOffices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TroopKinds",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TroopKinds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    MiddleName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DayOfWeek = table.Column<int>(nullable: false),
                    StartDate = table.Column<string>(nullable: false),
                    EndDate = table.Column<string>(nullable: false),
                    RecruitmentOfficeId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_RecruitmentOffices_RecruitmentOfficeId",
                        column: x => x.RecruitmentOfficeId,
                        principalTable: "RecruitmentOffices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TroopTypes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    TroopKindId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TroopTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TroopTypes_TroopKinds_TroopKindId",
                        column: x => x.TroopKindId,
                        principalTable: "TroopKinds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recruits",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FullName = table.Column<string>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    MaritalStatus = table.Column<string>(nullable: false),
                    RecruitmentOfficeId = table.Column<long>(nullable: true),
                    TroopTypeId = table.Column<long>(nullable: true),
                    PathToPhoto = table.Column<string>(nullable: false),
                    RecruitmentDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recruits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recruits_RecruitmentOffices_RecruitmentOfficeId",
                        column: x => x.RecruitmentOfficeId,
                        principalTable: "RecruitmentOffices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recruits_TroopTypes_TroopTypeId",
                        column: x => x.TroopTypeId,
                        principalTable: "TroopTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MedicalComissionResults",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Suitability = table.Column<byte>(nullable: false),
                    ComissionDate = table.Column<DateTime>(nullable: false),
                    RecruitId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalComissionResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalComissionResults_Recruits_RecruitId",
                        column: x => x.RecruitId,
                        principalTable: "Recruits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicalComissionResults_RecruitId",
                table: "MedicalComissionResults",
                column: "RecruitId");

            migrationBuilder.CreateIndex(
                name: "IX_Recruits_RecruitmentOfficeId",
                table: "Recruits",
                column: "RecruitmentOfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Recruits_TroopTypeId",
                table: "Recruits",
                column: "TroopTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_RecruitmentOfficeId",
                table: "Schedules",
                column: "RecruitmentOfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_TroopTypes_TroopKindId",
                table: "TroopTypes",
                column: "TroopKindId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicalComissionResults");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Recruits");

            migrationBuilder.DropTable(
                name: "RecruitmentOffices");

            migrationBuilder.DropTable(
                name: "TroopTypes");

            migrationBuilder.DropTable(
                name: "TroopKinds");
        }
    }
}

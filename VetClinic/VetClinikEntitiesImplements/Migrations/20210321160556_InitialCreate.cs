using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VetClinikModels.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DoctorVisits",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientFIO = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorVisits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicationName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medicines",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicineName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceName = table.Column<string>(nullable: false),
                    FIO = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedicationMedicines",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicineId = table.Column<int>(nullable: false),
                    MedicationId = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicationMedicines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicationMedicines_Medications_MedicationId",
                        column: x => x.MedicationId,
                        principalTable: "Medications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicationMedicines_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoctorVisitServices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceId = table.Column<int>(nullable: false),
                    DoctorVisitId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorVisitServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorVisitServices_DoctorVisits_DoctorVisitId",
                        column: x => x.DoctorVisitId,
                        principalTable: "DoctorVisits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorVisitServices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicationServices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceId = table.Column<int>(nullable: false),
                    MedicationId = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicationServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicationServices_Medications_MedicationId",
                        column: x => x.MedicationId,
                        principalTable: "Medications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicationServices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorVisitServices_DoctorVisitId",
                table: "DoctorVisitServices",
                column: "DoctorVisitId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorVisitServices_ServiceId",
                table: "DoctorVisitServices",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationMedicines_MedicationId",
                table: "MedicationMedicines",
                column: "MedicationId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationMedicines_MedicineId",
                table: "MedicationMedicines",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationServices_MedicationId",
                table: "MedicationServices",
                column: "MedicationId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationServices_ServiceId",
                table: "MedicationServices",
                column: "ServiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorVisitServices");

            migrationBuilder.DropTable(
                name: "MedicationMedicines");

            migrationBuilder.DropTable(
                name: "MedicationServices");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "DoctorVisits");

            migrationBuilder.DropTable(
                name: "Medicines");

            migrationBuilder.DropTable(
                name: "Medications");

            migrationBuilder.DropTable(
                name: "Services");
        }
    }
}

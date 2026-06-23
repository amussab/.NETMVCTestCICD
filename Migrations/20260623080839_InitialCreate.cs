using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    medicalNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    state = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    zipCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.medicalNumber);
                });

            migrationBuilder.CreateTable(
                name: "PatientInsurancePolicies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientMedicalNumber = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    providerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    policyNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientInsurancePolicies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientInsurancePolicies_Patients_PatientMedicalNumber",
                        column: x => x.PatientMedicalNumber,
                        principalTable: "Patients",
                        principalColumn: "medicalNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientInsurancePolicies_PatientMedicalNumber",
                table: "PatientInsurancePolicies",
                column: "PatientMedicalNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientInsurancePolicies");

            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}

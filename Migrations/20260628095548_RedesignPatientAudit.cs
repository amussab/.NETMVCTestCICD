using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class RedesignPatientAudit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "PatientAudits");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "PatientAudits");

            migrationBuilder.DropColumn(
                name: "InsuranceProvider",
                table: "PatientAudits");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "PatientAudits");

            migrationBuilder.DropColumn(
                name: "PolicyNumber",
                table: "PatientAudits");

            migrationBuilder.DropColumn(
                name: "State",
                table: "PatientAudits");

            migrationBuilder.RenameColumn(
                name: "ZipCode",
                table: "PatientAudits",
                newName: "FieldName");

            migrationBuilder.AddColumn<string>(
                name: "NewValue",
                table: "PatientAudits",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OldValue",
                table: "PatientAudits",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewValue",
                table: "PatientAudits");

            migrationBuilder.DropColumn(
                name: "OldValue",
                table: "PatientAudits");

            migrationBuilder.RenameColumn(
                name: "FieldName",
                table: "PatientAudits",
                newName: "ZipCode");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "PatientAudits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "PatientAudits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InsuranceProvider",
                table: "PatientAudits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "PatientAudits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PolicyNumber",
                table: "PatientAudits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "PatientAudits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

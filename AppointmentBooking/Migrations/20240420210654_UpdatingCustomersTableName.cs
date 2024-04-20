using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentBooking.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingCustomersTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerMeeting_AppointmentCustomers_CustomersId",
                table: "CustomerMeeting");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppointmentCustomers",
                table: "AppointmentCustomers");

            migrationBuilder.RenameTable(
                name: "AppointmentCustomers",
                newName: "Customers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerMeeting_Customers_CustomersId",
                table: "CustomerMeeting",
                column: "CustomersId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerMeeting_Customers_CustomersId",
                table: "CustomerMeeting");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "AppointmentCustomers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppointmentCustomers",
                table: "AppointmentCustomers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerMeeting_AppointmentCustomers_CustomersId",
                table: "CustomerMeeting",
                column: "CustomersId",
                principalTable: "AppointmentCustomers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finora.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRecurringIdempotencySupport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "RecurringOccurrenceDate",
                table: "Incomes",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RecurringTransactionId",
                table: "Incomes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "RecurringOccurrenceDate",
                table: "Expenses",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RecurringTransactionId",
                table: "Expenses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_RecurringTransactionId_RecurringOccurrenceDate",
                table: "Incomes",
                columns: new[] { "RecurringTransactionId", "RecurringOccurrenceDate" },
                unique: true,
                filter: "[RecurringTransactionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_RecurringTransactionId_RecurringOccurrenceDate",
                table: "Expenses",
                columns: new[] { "RecurringTransactionId", "RecurringOccurrenceDate" },
                unique: true,
                filter: "[RecurringTransactionId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Incomes_RecurringTransactionId_RecurringOccurrenceDate",
                table: "Incomes");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_RecurringTransactionId_RecurringOccurrenceDate",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "RecurringOccurrenceDate",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "RecurringTransactionId",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "RecurringOccurrenceDate",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "RecurringTransactionId",
                table: "Expenses");
        }
    }
}

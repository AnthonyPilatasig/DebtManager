using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DebtManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "AnnualInterestRate",
                table: "Debts",
                type: "numeric(10,4)",
                precision: 10,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(5,4)",
                oldPrecision: 5,
                oldScale: 4);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BaseCurrency", "CreatedAt", "Email", "IsDeleted", "LastModifiedAt" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), "USD", new DateTimeOffset(new DateTime(2026, 7, 23, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "test@debtmanager.com", false, new DateTimeOffset(new DateTime(2026, 7, 23, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.InsertData(
                table: "Debts",
                columns: new[] { "Id", "AnnualInterestRate", "CutoffDay", "DueDay", "IsCreditCard", "IsDeleted", "LastModifiedAt", "MinimumMonthlyPayment", "Name", "TotalBalance", "UserId" },
                values: new object[,]
                {
                    { new Guid("20000000-0000-0000-0000-000000000001"), 18.5m, 30, 15, true, false, new DateTimeOffset(new DateTime(2026, 7, 23, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 60.00m, "Tarjeta de Crédito Visa", 1200.00m, new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("20000000-0000-0000-0000-000000000002"), 12.0m, null, 5, false, false, new DateTimeOffset(new DateTime(2026, 7, 23, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 250.00m, "Préstamo Vehicular", 8500.00m, new Guid("00000000-0000-0000-0000-000000000001") }
                });

            migrationBuilder.InsertData(
                table: "Incomes",
                columns: new[] { "Id", "Amount", "Description", "IsActive", "IsDeleted", "LastModifiedAt", "Type", "UserId" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), 2500.00m, "Sueldo Fijo Mensual", true, false, new DateTimeOffset(new DateTime(2026, 7, 23, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1, new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("10000000-0000-0000-0000-000000000002"), 500.00m, "Trabajos Freelance", true, false, new DateTimeOffset(new DateTime(2026, 7, 23, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 2, new Guid("00000000-0000-0000-0000-000000000001") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Debts",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Debts",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.AlterColumn<decimal>(
                name: "AnnualInterestRate",
                table: "Debts",
                type: "numeric(5,4)",
                precision: 5,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(10,4)",
                oldPrecision: 10,
                oldScale: 4);
        }
    }
}

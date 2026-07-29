using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DebtManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveHardcodedSeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Debts",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001")
            );

            migrationBuilder.DeleteData(
                table: "FixedExpenses",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000001")
            );

            migrationBuilder.DeleteData(
                table: "FixedExpenses",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000002")
            );

            migrationBuilder.DeleteData(
                table: "Goals",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000001")
            );

            migrationBuilder.DeleteData(
                table: "Goals",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000002")
            );

            migrationBuilder.DeleteData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001")
            );

            migrationBuilder.DeleteData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002")
            );

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000001")
            );

            migrationBuilder.DeleteData(
                table: "Debts",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000002")
            );

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001")
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[]
                {
                    "Id",
                    "BaseCurrency",
                    "CreatedAt",
                    "Email",
                    "IsDeleted",
                    "LastModifiedAt"
                },
                values: new object[]
                {
                    new Guid("00000000-0000-0000-0000-000000000001"),
                    "USD",
                    new DateTimeOffset(
                        new DateTime(2026, 7, 23, 12, 0, 0, 0, DateTimeKind.Unspecified),
                        new TimeSpan(0, 0, 0, 0, 0)
                    ),
                    "test@debtmanager.com",
                    false,
                    new DateTimeOffset(
                        new DateTime(2026, 7, 23, 12, 0, 0, 0, DateTimeKind.Unspecified),
                        new TimeSpan(0, 0, 0, 0, 0)
                    )
                }
            );

            migrationBuilder.InsertData(
                table: "Debts",
                columns: new[]
                {
                    "Id",
                    "AnnualInterestRate",
                    "CutoffDay",
                    "DueDay",
                    "IsCreditCard",
                    "IsDeleted",
                    "LastModifiedAt",
                    "MinimumMonthlyPayment",
                    "Name",
                    "StartDate",
                    "TotalBalance",
                    "TotalQuotas",
                    "UserId"
                },
                values: new object[,]
                {
                    {
                        new Guid("20000000-0000-0000-0000-000000000001"),
                        18.5m,
                        30,
                        15,
                        true,
                        false,
                        new DateTimeOffset(
                            new DateTime(2026, 7, 23, 12, 0, 0, 0, DateTimeKind.Unspecified),
                            new TimeSpan(0, 0, 0, 0, 0)
                        ),
                        60.00m,
                        "Tarjeta de Crédito Visa",
                        new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                        1200.00m,
                        24,
                        new Guid("00000000-0000-0000-0000-000000000001")
                    },
                    {
                        new Guid("20000000-0000-0000-0000-000000000002"),
                        12.0m,
                        null,
                        5,
                        false,
                        false,
                        new DateTimeOffset(
                            new DateTime(2026, 7, 23, 12, 0, 0, 0, DateTimeKind.Unspecified),
                            new TimeSpan(0, 0, 0, 0, 0)
                        ),
                        250.00m,
                        "Préstamo Vehicular",
                        new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                        8500.00m,
                        60,
                        new Guid("00000000-0000-0000-0000-000000000001")
                    }
                }
            );

            migrationBuilder.InsertData(
                table: "FixedExpenses",
                columns: new[]
                {
                    "Id",
                    "Amount",
                    "DueDay",
                    "IsDeleted",
                    "LastModifiedAt",
                    "Name",
                    "UserId"
                },
                values: new object[,]
                {
                    {
                        new Guid("40000000-0000-0000-0000-000000000001"),
                        400.00m,
                        1,
                        false,
                        new DateTimeOffset(
                            new DateTime(2026, 7, 23, 12, 0, 0, 0, DateTimeKind.Unspecified),
                            new TimeSpan(0, 0, 0, 0, 0)
                        ),
                        "Arriendo",
                        new Guid("00000000-0000-0000-0000-000000000001")
                    },
                    {
                        new Guid("40000000-0000-0000-0000-000000000002"),
                        300.00m,
                        5,
                        false,
                        new DateTimeOffset(
                            new DateTime(2026, 7, 23, 12, 0, 0, 0, DateTimeKind.Unspecified),
                            new TimeSpan(0, 0, 0, 0, 0)
                        ),
                        "Comida (Supermercado)",
                        new Guid("00000000-0000-0000-0000-000000000001")
                    }
                }
            );

            migrationBuilder.InsertData(
                table: "Goals",
                columns: new[]
                {
                    "Id",
                    "EstimatedMonthlyPayment",
                    "IsDeleted",
                    "LastModifiedAt",
                    "Name",
                    "TargetAmount",
                    "TargetDate",
                    "Type",
                    "UserId"
                },
                values: new object[,]
                {
                    {
                        new Guid("50000000-0000-0000-0000-000000000001"),
                        null,
                        false,
                        new DateTimeOffset(
                            new DateTime(2026, 7, 23, 12, 0, 0, 0, DateTimeKind.Unspecified),
                            new TimeSpan(0, 0, 0, 0, 0)
                        ),
                        "Comprar PS5 (Contado)",
                        500.00m,
                        new DateTime(2026, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                        0,
                        new Guid("00000000-0000-0000-0000-000000000001")
                    },
                    {
                        new Guid("50000000-0000-0000-0000-000000000002"),
                        125.00m,
                        false,
                        new DateTimeOffset(
                            new DateTime(2026, 7, 23, 12, 0, 0, 0, DateTimeKind.Unspecified),
                            new TimeSpan(0, 0, 0, 0, 0)
                        ),
                        "Laptop Nueva (Crédito)",
                        1500.00m,
                        null,
                        1,
                        new Guid("00000000-0000-0000-0000-000000000001")
                    }
                }
            );

            migrationBuilder.InsertData(
                table: "Incomes",
                columns: new[]
                {
                    "Id",
                    "Amount",
                    "Description",
                    "IsActive",
                    "IsDeleted",
                    "LastModifiedAt",
                    "Type",
                    "UserId"
                },
                values: new object[,]
                {
                    {
                        new Guid("10000000-0000-0000-0000-000000000001"),
                        2500.00m,
                        "Sueldo Fijo Mensual",
                        true,
                        false,
                        new DateTimeOffset(
                            new DateTime(2026, 7, 23, 12, 0, 0, 0, DateTimeKind.Unspecified),
                            new TimeSpan(0, 0, 0, 0, 0)
                        ),
                        1,
                        new Guid("00000000-0000-0000-0000-000000000001")
                    },
                    {
                        new Guid("10000000-0000-0000-0000-000000000002"),
                        500.00m,
                        "Trabajos Freelance",
                        true,
                        false,
                        new DateTimeOffset(
                            new DateTime(2026, 7, 23, 12, 0, 0, 0, DateTimeKind.Unspecified),
                            new TimeSpan(0, 0, 0, 0, 0)
                        ),
                        2,
                        new Guid("00000000-0000-0000-0000-000000000001")
                    }
                }
            );

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[]
                {
                    "Id",
                    "Amount",
                    "DebtId",
                    "IsDeleted",
                    "IsExtraordinary",
                    "LastModifiedAt",
                    "PaymentDate"
                },
                values: new object[]
                {
                    new Guid("30000000-0000-0000-0000-000000000001"),
                    250.00m,
                    new Guid("20000000-0000-0000-0000-000000000002"),
                    false,
                    false,
                    new DateTimeOffset(
                        new DateTime(2026, 7, 23, 12, 0, 0, 0, DateTimeKind.Unspecified),
                        new TimeSpan(0, 0, 0, 0, 0)
                    ),
                    new DateTime(2026, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc)
                }
            );
        }
    }
}

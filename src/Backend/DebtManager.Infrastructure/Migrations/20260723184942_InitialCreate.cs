using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DebtManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(
                        type: "character varying(255)",
                        maxLength: 255,
                        nullable: false
                    ),
                    BaseCurrency = table.Column<string>(
                        type: "character varying(3)",
                        maxLength: 3,
                        nullable: false
                    ),
                    CreatedAt = table.Column<DateTimeOffset>(
                        type: "timestamp with time zone",
                        nullable: false
                    ),
                    LastModifiedAt = table.Column<DateTimeOffset>(
                        type: "timestamp with time zone",
                        nullable: false
                    ),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "Debts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(
                        type: "character varying(150)",
                        maxLength: 150,
                        nullable: false
                    ),
                    TotalBalance = table.Column<decimal>(
                        type: "numeric(18,4)",
                        precision: 18,
                        scale: 4,
                        nullable: false
                    ),
                    MinimumMonthlyPayment = table.Column<decimal>(
                        type: "numeric(18,4)",
                        precision: 18,
                        scale: 4,
                        nullable: false
                    ),
                    AnnualInterestRate = table.Column<decimal>(
                        type: "numeric(5,4)",
                        precision: 5,
                        scale: 4,
                        nullable: false
                    ),
                    DueDay = table.Column<int>(type: "integer", nullable: false),
                    IsCreditCard = table.Column<bool>(type: "boolean", nullable: false),
                    CutoffDay = table.Column<int>(type: "integer", nullable: true),
                    LastModifiedAt = table.Column<DateTimeOffset>(
                        type: "timestamp with time zone",
                        nullable: false
                    ),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Debts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Debts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "Incomes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(
                        type: "character varying(150)",
                        maxLength: 150,
                        nullable: false
                    ),
                    Amount = table.Column<decimal>(
                        type: "numeric(18,4)",
                        precision: 18,
                        scale: 4,
                        nullable: false
                    ),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    LastModifiedAt = table.Column<DateTimeOffset>(
                        type: "timestamp with time zone",
                        nullable: false
                    ),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incomes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Incomes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(name: "IX_Debts_UserId", table: "Debts", column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_UserId",
                table: "Incomes",
                column: "UserId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Debts");

            migrationBuilder.DropTable(name: "Incomes");

            migrationBuilder.DropTable(name: "Users");
        }
    }
}

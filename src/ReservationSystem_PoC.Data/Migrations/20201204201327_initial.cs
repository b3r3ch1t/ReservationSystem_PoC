﻿using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Text;

namespace ReservationSystem_PoC.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Valid = table.Column<bool>(type: "bit", nullable: false),
                    DateOfChange = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfCreation = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContactTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Valid = table.Column<bool>(type: "bit", nullable: false),
                    DateOfChange = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfCreation = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_ContactType_ContactTypeId",
                        column: x => x.ContactTypeId,
                        principalTable: "ContactType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Ranking = table.Column<int>(type: "int", nullable: false),
                    Favorited = table.Column<bool>(type: "bit", nullable: false),
                    Valid = table.Column<bool>(type: "bit", nullable: false),
                    DateOfChange = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfCreation = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_BirthDate",
                table: "Contacts",
                column: "BirthDate");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_ContactTypeId",
                table: "Contacts",
                column: "ContactTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_DateOfChange",
                table: "Contacts",
                column: "DateOfChange");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_DateOfCreation",
                table: "Contacts",
                column: "DateOfCreation");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_Name",
                table: "Contacts",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_PhoneNumber",
                table: "Contacts",
                column: "PhoneNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_Valid",
                table: "Contacts",
                column: "Valid");

            migrationBuilder.CreateIndex(
                name: "IX_ContactType_DateOfChange",
                table: "ContactType",
                column: "DateOfChange");

            migrationBuilder.CreateIndex(
                name: "IX_ContactType_DateOfCreation",
                table: "ContactType",
                column: "DateOfCreation");

            migrationBuilder.CreateIndex(
                name: "IX_ContactType_Description",
                table: "ContactType",
                column: "Description");

            migrationBuilder.CreateIndex(
                name: "IX_ContactType_Valid",
                table: "ContactType",
                column: "Valid");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ContactId",
                table: "Reservations",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_DateOfChange",
                table: "Reservations",
                column: "DateOfChange");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_DateOfCreation",
                table: "Reservations",
                column: "DateOfCreation");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_Favorited",
                table: "Reservations",
                column: "Favorited");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_Ranking",
                table: "Reservations",
                column: "Ranking");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_Valid",
                table: "Reservations",
                column: "Valid");

            AddStoreProcedure(migrationBuilder);
        }

        private void AddStoreProcedure(MigrationBuilder migrationBuilder)
        {

            var sb = new StringBuilder();

            sb.AppendLine(" CREATE PROCEDURE UpdateContactType @Description nvarchar(512), @Valid BIT,@DateOfChange DATETIME2 (7), @DateOfCreation  DATETIME2 (7), @Id UNIQUEIDENTIFIER ");
            sb.AppendLine(" AS ");
            sb.AppendLine(" BEGIN ");
            sb.AppendLine("	SET NOCOUNT ON; ");
            sb.AppendLine(" 	UPDATE [dbo].[ContactType] ");
            sb.AppendLine("                SET ");
            sb.AppendLine(" 					[Description] = @Description,");
            sb.AppendLine("					    [Valid] = @Valid,  ");
            sb.AppendLine("					    [DateOfChange] = @DateOfChange , ");
            sb.AppendLine("					    [DateOfCreation] = @DateOfCreation");
            sb.AppendLine("                WHERE (Id = @Id)");
            sb.AppendLine("END");
            sb.AppendLine("GO");

            migrationBuilder.Sql(sb.ToString());

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "ContactType");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetAdoption.Repository.Migrations
{
    /// <inheritdoc />
    public partial class models4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionForms_Animals_AnimalId",
                table: "AdoptionForms");

            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionForms_AspNetUsers_ApplicantId",
                table: "AdoptionForms");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SubmittedOn",
                table: "AdoptionForms",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicantId",
                table: "AdoptionForms",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<Guid>(
                name: "AnimalId",
                table: "AdoptionForms",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionForms_Animals_AnimalId",
                table: "AdoptionForms",
                column: "AnimalId",
                principalTable: "Animals",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionForms_AspNetUsers_ApplicantId",
                table: "AdoptionForms",
                column: "ApplicantId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionForms_Animals_AnimalId",
                table: "AdoptionForms");

            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionForms_AspNetUsers_ApplicantId",
                table: "AdoptionForms");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SubmittedOn",
                table: "AdoptionForms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ApplicantId",
                table: "AdoptionForms",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AnimalId",
                table: "AdoptionForms",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionForms_Animals_AnimalId",
                table: "AdoptionForms",
                column: "AnimalId",
                principalTable: "Animals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionForms_AspNetUsers_ApplicantId",
                table: "AdoptionForms",
                column: "ApplicantId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCoreDemo.Migrations;

public partial class DogConfiguration : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "Name",
            table: "Dogs",
            newName: "DogName");

        migrationBuilder.AlterColumn<DateTime>(
            name: "BirthDate",
            table: "Dogs",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "DogName",
            table: "Dogs",
            maxLength: 32,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)",
            oldNullable: true);

        migrationBuilder.AddColumn<byte[]>(
            name: "RowVersion",
            table: "Dogs",
            rowVersion: true,
            nullable: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "RowVersion",
            table: "Dogs");

        migrationBuilder.RenameColumn(
            name: "DogName",
            table: "Dogs",
            newName: "Name");

        migrationBuilder.AlterColumn<DateTime>(
            name: "BirthDate",
            table: "Dogs",
            type: "datetime2",
            nullable: true,
            oldClrType: typeof(DateTime));

        migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "Dogs",
            type: "nvarchar(max)",
            nullable: true,
            oldClrType: typeof(string),
            oldMaxLength: 32,
            oldNullable: true);
    }
}

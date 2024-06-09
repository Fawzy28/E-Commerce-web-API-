using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Context.Migrations
{
    /// <inheritdoc />
    public partial class finalll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "newOrder",
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "status",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "newOrder");
        }
    }
}

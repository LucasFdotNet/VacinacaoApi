using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VacinacaoApi.Migrations
{
    /// <inheritdoc />
    public partial class AddSenhaNaPessoa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Senha",
                table: "Pessoas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Senha",
                table: "Pessoas");
        }
    }
}

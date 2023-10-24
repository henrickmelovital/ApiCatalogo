using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiCatalogo.Migrations
{
    /// <inheritdoc />
    public partial class PopulaCategorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO CTL_Categoria(Nome, ImagemUrl)" +
                "Values ('Bebidas', 'bebidas.jpg')");

            migrationBuilder.Sql("INSERT INTO CTL_Categoria(Nome, ImagemUrl)" +
                "Values ('Lanches', 'lanches.jpg')");

            migrationBuilder.Sql("INSERT INTO CTL_Categoria(Nome, ImagemUrl)" +
                "Values ('Sobremesas', 'sobremesas.jpg')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM CTL_Categoria");
        }
    }
}

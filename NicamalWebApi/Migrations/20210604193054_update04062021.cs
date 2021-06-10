using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NicamalWebApi.Migrations
{
    public partial class update04062021 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Provinces",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Álava" },
                    { 29, "Madrid" },
                    { 30, "Málaga" },
                    { 31, "Murcia" },
                    { 32, "Navarra" },
                    { 33, "Ourense" },
                    { 34, "Palencia" },
                    { 35, "Las Palmas" },
                    { 36, "Pontevedra" },
                    { 37, "La Rioja" },
                    { 38, "Salamanca" },
                    { 28, "Lugo" },
                    { 39, "Segovia" },
                    { 41, "Soria" },
                    { 42, "Tarragona" },
                    { 43, "Santa Crus de Tenerife" },
                    { 44, "Teruel" },
                    { 45, "Toledo" },
                    { 46, "Valencia" },
                    { 47, "Valladolid" },
                    { 48, "Vizcaya" },
                    { 49, "Zamora" },
                    { 50, "Zaragoza" },
                    { 40, "Sevilla" },
                    { 27, "Lleida" },
                    { 26, "León" },
                    { 25, "Jaén" },
                    { 2, "Albacete" },
                    { 3, "Alicante" },
                    { 4, "Almería" },
                    { 5, "Asturias" },
                    { 6, "Ávila" },
                    { 7, "Badajoz" },
                    { 8, "Barcelona" },
                    { 9, "Burgos" },
                    { 10, "Cáceres" },
                    { 11, "Cádiz" },
                    { 12, "Cantabria" },
                    { 13, "Castellón" },
                    { 14, "Ciudad Real" },
                    { 15, "Córdoba" },
                    { 16, "A Coruña" },
                    { 17, "Cuenca" },
                    { 18, "Girona" },
                    { 19, "Granada" },
                    { 20, "Guadalajara" },
                    { 21, "Gipuzkoa" },
                    { 22, "Huelva" },
                    { 23, "Huesca" },
                    { 24, "Baleares" },
                    { 51, "Ceuta" },
                    { 52, "Melilla" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Provinces");
        }
    }
}

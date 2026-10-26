using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiInicial.Migrations
{
    public partial class DatosIniciales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insertar categorías
            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Nombre", "Descripcion" },
                values: new object[,]
                {
                    { 1, "Bebidas", "Gaseosas, jugos y agua" },
                    { 2, "Snacks", "Papas fritas, galletas y golosinas" },
                    { 3, "Lácteos", "Leche, yogur y queso" },
                    { 4, "Limpieza", "Detergentes y artículos de aseo" },
                }
            );

            // Insertar productos
            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "Id", "Nombre", "Precio", "Stock", "Estado", "CategoriaId" },
                values: new object[,]
                {
                    { 2, "Jugo de naranja 1L", 12.00m, 30, true, 1 },
                    { 3, "Papas fritas clásicas", 8.00m, 60, true, 2 },
                    { 4, "Galletas de chocolate", 6.00m, 80, true, 2 },
                    { 5, "Maní salado", 4.00m, 45, true, 2 },
                    { 6, "Yogur de frutilla", 7.00m, 35, true, 3 },
                    { 7, "Queso fresco 500g", 25.00m, 20, true, 3 },
                    { 8, "Lavandina 1L", 10.00m, 40, true, 4 },
                    { 9, "Jabón de tocador", 3.00m, 90, true, 4 },
                }
            );

            // Reiniciar las secuencias de los IDs autoincrementales
            migrationBuilder.Sql(
                """
                SELECT setval(
                    pg_get_serial_sequence('"Categorias"', 'Id'),
                    COALESCE((SELECT MAX("Id") FROM "Categorias"), 1),
                    EXISTS (SELECT 1 FROM "Categorias")
                );

                SELECT setval(
                    pg_get_serial_sequence('"Productos"', 'Id'),
                    COALESCE((SELECT MAX("Id") FROM "Productos"), 1),
                    EXISTS (SELECT 1 FROM "Productos")
                );
                """
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Eliminar primero los productos por su relación con las categorías
            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValues: new object[] { 2, 3, 4, 5, 6, 7, 8, 9 }
            );

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3, 4 }
            );
        }
    }
}

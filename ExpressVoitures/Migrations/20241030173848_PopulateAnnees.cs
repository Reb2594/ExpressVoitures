using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpressVoitures.Migrations
{
    /// <inheritdoc />
    public partial class PopulateAnnees : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            int startYear = 1990;
            int currentYear = DateTime.Now.Year;

            for (int year = startYear; year <= currentYear; year++)
            {
                migrationBuilder.InsertData(
                    table: "Annees",
                    columns: new[] { "Id", "Valeur" },
                    values: new object[] { year - startYear + 1, year });
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            int startYear = 1990;
            int currentYear = DateTime.Now.Year;

            for (int year = startYear; year <= currentYear; year++)
            {
                migrationBuilder.DeleteData(
                    table: "Annees",
                    keyColumn: "Id",
                    keyValue: year - startYear + 1);
            }
        }
    }
}

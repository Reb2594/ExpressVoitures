using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpressVoitures.Migrations
{
    /// <inheritdoc />
    public partial class PopulateAnnees : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insertion des années de 1990 à 2024
            for (int year = 1990; year <= 2024; year++)
            {
                migrationBuilder.Sql($"INSERT INTO Annees (Valeur) VALUES ({year})");
            }

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Suppression des années ajoutées
            for (int year = 1990; year <= 2024; year++)
            {
                migrationBuilder.Sql($"DELETE FROM Annees WHERE Valeur = {year}");
            }
        }
    }
}

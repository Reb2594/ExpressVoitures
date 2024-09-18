using ExpressVoitures.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExpressVoitures.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Vehicule> Vehicules { get; set; }
        public DbSet<Marque> Marques { get; set; }
        public DbSet<Modele> Modeles { get; set; }
        public DbSet<ModeleMarque> ModeleMarques { get; set; }
        public DbSet<Annee> Annees { get; set; }
        public DbSet<Finition> Finitions { get; set; }
        public DbSet<Reparation> Reparations { get; set; }
        public DbSet<Image> Images { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuration des clés primaires
            modelBuilder.Entity<ModeleMarque>()
                .HasKey(mm => mm.Id);

            modelBuilder.Entity<Vehicule>()
                .HasKey(mm => mm.Id);

            modelBuilder.Entity<Annee>()
                .HasKey(mm => mm.Id);

            modelBuilder.Entity<Finition>()
                .HasKey(mm => mm.Id);

            modelBuilder.Entity<Reparation>()
                .HasKey(mm => mm.Id);

            modelBuilder.Entity<Image>()
                .HasKey(mm => mm.Id);

            modelBuilder.Entity<Modele>()
                .HasKey(mm => mm.Id);

            modelBuilder.Entity<Marque>()
                .HasKey(mm => mm.Id);

            // Configuration de la relation entre Vehicule et ModeleMarque
            modelBuilder.Entity<Vehicule>()
                .HasOne(v => v.ModeleMarque)
                .WithMany(mm => mm.Vehicules)
                .HasForeignKey(v => v.ModeleMarqueId);

            // Configuration de la relation entre Modele et ModeleMarque
            modelBuilder.Entity<ModeleMarque>()
                .HasOne(v => v.Modele)
                .WithMany(m => m.ModeleMarques)
                .HasForeignKey(t => t.ModeleId);

            // Configuration de la relation entre Marque et ModeleMarque
            modelBuilder.Entity<ModeleMarque>()
                .HasOne(v => v.Marque)
                .WithMany(s => s.ModeleMarques)
                .HasForeignKey(t => t.MarqueId);

            // Configuration de la relation entre Vehicule et Annee
            modelBuilder.Entity<Vehicule>()
                .HasOne(v => v.Annee)
                .WithMany(a => a.Vehicules)
                .HasForeignKey(v => v.AnneeId);

            // Configuration de la relation entre Vehicule et Finition
            modelBuilder.Entity<Vehicule>()
                .HasOne(v => v.Finition)
                .WithOne(f => f.Vehicules)
                .HasForeignKey<Vehicule>(v => v.FinitionId);

            // Configuration de la relation entre Vehicule et Reparation
            modelBuilder.Entity<Reparation>()
                .HasOne(r => r.Vehicule)
                .WithOne(v => v.Reparation)
                .HasForeignKey<Reparation>(r => r.VehiculeId);

            modelBuilder.Entity<Vehicule>()
                .HasOne(v => v.Reparation)
                .WithOne(e => e.Vehicule)
                .HasForeignKey<Vehicule>(r => r.ReparationId);

            // Configuration de la relation entre Image et Véhicule
            modelBuilder.Entity<Image>()
                .HasOne(r => r.Vehicule)
                .WithMany(i => i.ListImage)
                .HasForeignKey(i => i.VehiculeId);
         
        }

    }
}

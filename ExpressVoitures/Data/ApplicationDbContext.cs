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
        public DbSet<Annee> Annees { get; set; }
        public DbSet<Finition> Finitions { get; set; }
        public DbSet<Reparation> Reparations { get; set; }
        public DbSet<Image> Images { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuration des clés primaires

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

            // Configuration de la relation entre Vehicule et Marque
            modelBuilder.Entity<Vehicule>()
                .HasOne(v => v.Marque)
                .WithMany(mm => mm.Vehicules)
                .HasForeignKey(v => v.MarqueId);

            // Configuration de la relation entre Véhicule et Modele
            modelBuilder.Entity<Vehicule>()
                .HasOne(v => v.Modele)
                .WithMany(m => m.Vehicules)
                .HasForeignKey(t => t.ModeleId);

            // Configuration de la relation entre Marque et Modele
            modelBuilder.Entity<Modele>()
               .HasOne(m => m.Marque)
               .WithMany(mq => mq.Modeles)
               .HasForeignKey(m => m.MarqueId)
               .OnDelete(DeleteBehavior.Restrict);

            // Configuration de la relation entre Vehicule et Annee
            modelBuilder.Entity<Vehicule>()
                .HasOne(v => v.Annee)
                .WithMany(a => a.Vehicules)
                .HasForeignKey(v => v.AnneeId);

            // Configuration de la relation entre Vehicule et Finition
            modelBuilder.Entity<Vehicule>()
                .HasOne(v => v.Finition)
                .WithMany(f => f.Vehicules)
                .HasForeignKey(v => v.FinitionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuration de la relation entre Vehicule et Reparation
            modelBuilder.Entity<Reparation>()
                .HasOne(r => r.Vehicule)
                .WithOne(v => v.Reparation)
                .HasForeignKey<Reparation>(r => r.VehiculeId);
            

            // Configuration de la relation entre Image et Véhicule
            modelBuilder.Entity<Vehicule>()
               .HasMany(v => v.Images)
               .WithOne(img => img.Vehicule) // Aucune navigation vers Vehicule depuis Image
               .HasForeignKey(img => img.VehiculeId)
               .OnDelete(DeleteBehavior.Cascade); // Suppression en cascade si un Vehicule est supprimé

            // Ignorer la propriété calculée ImageUrls pour qu'elle ne soit pas mappée dans la base de données
            modelBuilder.Entity<Vehicule>()
                .Ignore(v => v.ImageUrls);

        }

    }
}

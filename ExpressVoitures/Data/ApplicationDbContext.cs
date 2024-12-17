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

            // Ajout des données statiques pour les Marques
            modelBuilder.Entity<Marque>().HasData(
                new Marque { Id = 1, Nom = "Mazda" },
                new Marque { Id = 2, Nom = "Jeep" },
                new Marque { Id = 3, Nom = "Renault" },
                new Marque { Id = 4, Nom = "Ford" },
                new Marque { Id = 5, Nom = "Honda" },
                new Marque { Id = 6, Nom = "Volkswagen" },
                new Marque { Id = 7, Nom = "Peugeot" },
                new Marque { Id = 8, Nom = "Volvo" }
            );

            // Ajout des données statiques pour les Modèles
            modelBuilder.Entity<Modele>().HasData(
                new Modele { Id = 1, Nom = "Miata", MarqueId = 1 },
                new Modele { Id = 2, Nom = "Liberty", MarqueId = 2 },
                new Modele { Id = 3, Nom = "Scénic", MarqueId = 3 },
                new Modele { Id = 4, Nom = "Explorer", MarqueId = 4 },
                new Modele { Id = 5, Nom = "Civic", MarqueId = 5 },
                new Modele { Id = 6, Nom = "GTI", MarqueId = 6 },
                new Modele { Id = 7, Nom = "Edge", MarqueId = 4 },
                new Modele { Id = 8, Nom = "3008", MarqueId = 7 },
                new Modele { Id = 9, Nom = "206", MarqueId = 7 },
                new Modele { Id = 10, Nom = "XC60", MarqueId = 8 },
                new Modele { Id = 11, Nom = "XC90", MarqueId = 8 }
            );

            // Ajout des données statiques pour les Finitions
            modelBuilder.Entity<Finition>().HasData(
                new Finition { Id = 1, Nom = "LE" },
                new Finition { Id = 2, Nom = "Sport" },
                new Finition { Id = 3, Nom = "TCe" },
                new Finition { Id = 4, Nom = "XLT" },
                new Finition { Id = 5, Nom = "LX" },
                new Finition { Id = 6, Nom = "S" },
                new Finition { Id = 7, Nom = "SEL" }
            );

        }

    }
}

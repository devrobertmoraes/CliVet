using CliVet.Model;
using Microsoft.EntityFrameworkCore;

namespace CliVet.Data.Context
{
    public class CliVetContext : DbContext
    {
        public CliVetContext(DbContextOptions<CliVetContext> options) : base(options)
        {
        }

        public DbSet<TutorModel> Tutores { get; set; }
        public DbSet<PetModel> Pets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PetModel>()
                .HasOne(pet => pet.Tutor)
                .WithMany(tutor => tutor.Pets)
                .HasForeignKey(pet => pet.TutorId);

            // CPF do Tutor deve ser único
            modelBuilder.Entity<TutorModel>()
                .HasIndex(tutor => tutor.Cpf)
                .IsUnique();

            // Nome do Pet deve ser único
            modelBuilder.Entity<PetModel>()
                .HasIndex(pet => pet.Nome)
                .IsUnique();
        }
    }
}

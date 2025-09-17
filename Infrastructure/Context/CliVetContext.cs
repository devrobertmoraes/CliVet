using CliVet.Domain;
using Microsoft.EntityFrameworkCore;

namespace CliVet.Infrastructure.Context
{
    public class CliVetContext : DbContext
    {
        public CliVetContext(DbContextOptions<CliVetContext> options) : base(options)
        {
        }

        public DbSet<Tutor> Tutores { get; set; }
        public DbSet<Pet> Pets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pet>()
                .HasOne(pet => pet.Tutor)
                .WithMany(tutor => tutor.Pets)
                .HasForeignKey(pet => pet.TutorId);

            // CPF do Tutor deve ser único
            modelBuilder.Entity<Tutor>()
                .HasIndex(tutor => tutor.Cpf)
                .IsUnique();

            // Nome do Pet deve ser único
            modelBuilder.Entity<Pet>()
                .HasIndex(pet => pet.Nome)
                .IsUnique();
        }
    }
}

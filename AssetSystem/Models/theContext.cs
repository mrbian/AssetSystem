using System.Data.Entity;

namespace AssetSystem.Models
{
    public class TheContext : DbContext
    {
        public TheContext() : base("name=TheContext")
        {
//            Database.SetInitializer(new DbInitializer());
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<EquipmentType> EquipmentTypes { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<History> Histories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Admin>()
                .HasMany(a => a.Equipments)
                .WithRequired(e => e.Admin);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Equipments)
                .WithOptional(e => e.User);

            modelBuilder.Entity<EquipmentType>()
                .HasMany<EquipmentType>(t => t.SmallEquipmentTypes)
                .WithOptional(t => t.BigEquipmentType);

            modelBuilder.Entity<EquipmentType>()
                .HasMany(t => t.Equipments)
                .WithRequired(e => e.EquipmentType);
        }
    }
}
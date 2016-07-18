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
                .WithRequired(e => e.Admin); //配置管理员和设备的one To Many关系

            modelBuilder.Entity<User>()
                .HasMany(u => u.Equipments)
                .WithOptional(e => e.User); //配置用户和设备的one or zero To Many 关系

            modelBuilder.Entity<EquipmentType>()
                .HasMany<EquipmentType>(t => t.SmallEquipmentTypes)
                .WithOptional(t => t.BigEquipmentType);  //配置大类和小类的 one To Many 关系

            modelBuilder.Entity<EquipmentType>()
                .HasMany(t => t.Equipments)
                .WithRequired(e => e.EquipmentType); //配置设备种类和设备的one To Many关系
        }

    }
}
using AssetSystem.Models;

namespace AssetSystem.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AssetSystem.Models.TheContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AssetSystem.Models.TheContext context)
        {
            DbInitializer dbInitializer = new DbInitializer();
            dbInitializer.Seed();
        }
    }
}

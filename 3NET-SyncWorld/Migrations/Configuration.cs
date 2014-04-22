namespace _3NET_SyncWorld.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<_3NET_SyncWorld.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "_3NET_SyncWorld.Models.ApplicationDbContext";
        }
    }
}

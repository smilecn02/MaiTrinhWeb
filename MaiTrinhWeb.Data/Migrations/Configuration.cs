namespace MaiTrinhWeb.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<MaiTrinhWebContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(MaiTrinhWebContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Colors.AddOrUpdate(p => p.Name, new Color() { Id = new Guid("97B13B39-5DA5-4796-ABDB-1AEDBBD966F2"), Name = "Đỏ" });
            context.Colors.AddOrUpdate(p => p.Name, new Color() { Id = new Guid("DCA78443-3451-4AA2-99E4-B10A5C159199"), Name = "Cam" });
            context.Colors.AddOrUpdate(p => p.Name, new Color() { Id = new Guid("60166A05-32C8-4CD6-8EC2-117431B0DED2"), Name = "Vàng" });
            context.Colors.AddOrUpdate(p => p.Name, new Color() { Id = new Guid("670E50B9-E2D8-4492-954D-8F656F7EAF47"), Name = "Xanh lá cây" });
            context.Colors.AddOrUpdate(p => p.Name, new Color() { Id = new Guid("6F5A2A71-9339-4D0D-B8C9-7616E78D5FB2"), Name = "Xanh nước biển" });
            context.Colors.AddOrUpdate(p => p.Name, new Color() { Id = new Guid("7DC0FCA6-1D1B-4E99-A957-35612C3F6C56"), Name = "Tím" });
            context.Colors.AddOrUpdate(p => p.Name, new Color() { Id = new Guid("EF9923FB-FFBE-4F75-A114-83714DF71B04"), Name = "Hồng" });
            context.Colors.AddOrUpdate(p => p.Name, new Color() { Id = new Guid("5B90AFF1-FC46-4F05-9B9F-23824599D518"), Name = "Trắng" });
            context.Colors.AddOrUpdate(p => p.Name, new Color() { Id = new Guid("42E5667A-4D04-4D00-9CEF-AD970F6E038F"), Name = "Đen" });
            context.Colors.AddOrUpdate(p => p.Name, new Color() { Id = new Guid("716A233F-F581-4CDE-B4F8-D516BCCDE098"), Name = "Xám" });

        }
    }
}

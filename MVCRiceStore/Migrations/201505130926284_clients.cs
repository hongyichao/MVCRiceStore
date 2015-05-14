namespace MVCRiceStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class clients : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.StoreRices", newName: "RiceStores");
            DropPrimaryKey("dbo.RiceStores");
            AddPrimaryKey("dbo.RiceStores", new[] { "Rice_Id", "Store_Id" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.RiceStores");
            AddPrimaryKey("dbo.RiceStores", new[] { "Store_Id", "Rice_Id" });
            RenameTable(name: "dbo.RiceStores", newName: "StoreRices");
        }
    }
}

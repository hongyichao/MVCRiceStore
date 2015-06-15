namespace MVCRiceStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_clientOders : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.RiceStores", newName: "StoreRices");
            DropForeignKey("dbo.Stores", "client_Id", "dbo.Clients");
            DropIndex("dbo.Stores", new[] { "client_Id" });
            DropPrimaryKey("dbo.StoreRices");
            CreateTable(
                "dbo.ClientOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        RiceId = c.Int(nullable: false),
                        Kilogram = c.Int(nullable: false),
                        Client_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.Client_Id)
                .Index(t => t.Client_Id);
            
            AddPrimaryKey("dbo.StoreRices", new[] { "Store_Id", "Rice_Id" });
            DropColumn("dbo.Stores", "client_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Stores", "client_Id", c => c.Int());
            DropForeignKey("dbo.ClientOrders", "Client_Id", "dbo.Clients");
            DropIndex("dbo.ClientOrders", new[] { "Client_Id" });
            DropPrimaryKey("dbo.StoreRices");
            DropTable("dbo.ClientOrders");
            AddPrimaryKey("dbo.StoreRices", new[] { "Rice_Id", "Store_Id" });
            CreateIndex("dbo.Stores", "client_Id");
            AddForeignKey("dbo.Stores", "client_Id", "dbo.Clients", "Id");
            RenameTable(name: "dbo.StoreRices", newName: "RiceStores");
        }
    }
}

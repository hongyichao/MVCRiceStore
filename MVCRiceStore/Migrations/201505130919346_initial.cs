namespace MVCRiceStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Stores", "client_Id", c => c.Int());
            CreateIndex("dbo.Stores", "client_Id");
            AddForeignKey("dbo.Stores", "client_Id", "dbo.Clients", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stores", "client_Id", "dbo.Clients");
            DropIndex("dbo.Stores", new[] { "client_Id" });
            DropColumn("dbo.Stores", "client_Id");
            DropTable("dbo.Clients");
        }
    }
}

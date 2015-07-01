namespace MVCRiceStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClientNameIsRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Clients", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clients", "Name", c => c.String());
        }
    }
}

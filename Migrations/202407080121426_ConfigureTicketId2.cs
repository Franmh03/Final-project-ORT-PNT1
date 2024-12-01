namespace CineOrt.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConfigureTicketId2 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Tickets");
            AddColumn("dbo.Tickets", "TicketId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Tickets", "TicketId");
            DropColumn("dbo.Tickets", "IdTicket");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tickets", "IdTicket", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Tickets");
            DropColumn("dbo.Tickets", "TicketId");
            AddPrimaryKey("dbo.Tickets", "IdTicket");
        }
    }
}

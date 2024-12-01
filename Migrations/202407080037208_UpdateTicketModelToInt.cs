namespace CineOrt.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTicketModelToInt : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Tickets");
            AlterColumn("dbo.Tickets", "IdTicket", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Tickets", "IdTicket");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Tickets");
            AlterColumn("dbo.Tickets", "IdTicket", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Tickets", "IdTicket");
        }
    }
}

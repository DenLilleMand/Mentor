namespace Mentor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MentorMenteeBooleans : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Mentor", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "Mentee", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Mentee");
            DropColumn("dbo.AspNetUsers", "Mentor");
        }
    }
}

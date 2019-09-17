namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Comment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Yorum = c.String(),
                        YorumTime = c.DateTime(nullable: false),
                        UserYorum_Id = c.Int(),
                        OrganizationYorum_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserYorum_Id)
                .ForeignKey("dbo.Organizations", t => t.OrganizationYorum_Id)
                .Index(t => t.UserYorum_Id)
                .Index(t => t.OrganizationYorum_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "OrganizationYorum_Id", "dbo.Organizations");
            DropForeignKey("dbo.Comments", "UserYorum_Id", "dbo.Users");
            DropIndex("dbo.Comments", new[] { "OrganizationYorum_Id" });
            DropIndex("dbo.Comments", new[] { "UserYorum_Id" });
            DropTable("dbo.Comments");
        }
    }
}

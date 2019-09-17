namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrganizationMembers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Organization_Id = c.Int(),
                        Users_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organizations", t => t.Organization_Id)
                .ForeignKey("dbo.Users", t => t.Users_Id)
                .Index(t => t.Organization_Id)
                .Index(t => t.Users_Id);
            
            CreateTable(
                "dbo.Organizations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        OrgDate = c.DateTime(nullable: false),
                        Place = c.String(),
                        ImageUrl = c.String(),
                        Organizer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Organizer_Id)
                .Index(t => t.Organizer_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        Age = c.Int(nullable: false),
                        UserType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Organizations", "Organizer_Id", "dbo.Users");
            DropForeignKey("dbo.OrganizationMembers", "Users_Id", "dbo.Users");
            DropForeignKey("dbo.OrganizationMembers", "Organization_Id", "dbo.Organizations");
            DropIndex("dbo.Organizations", new[] { "Organizer_Id" });
            DropIndex("dbo.OrganizationMembers", new[] { "Users_Id" });
            DropIndex("dbo.OrganizationMembers", new[] { "Organization_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Organizations");
            DropTable("dbo.OrganizationMembers");
        }
    }
}

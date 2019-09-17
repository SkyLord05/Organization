namespace DataAccessLayer
{
    using DataAccessLayer.Entities;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class DbOrganization : DbContext
    {
        // Your context has been configured to use a 'DbOrganization' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'DataAccessLayer.DbOrganization' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'DbOrganization' 
        // connection string in the application configuration file.
        public DbOrganization()
            : base("name=DbOrganization")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<OrganizationMember> OrganizationMembers { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}
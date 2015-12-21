namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class securityChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.User", "UserTypeId", "dbo.UserType");
            DropIndex("dbo.User", new[] { "UserTypeId" });
            CreateTable(
                "dbo.UserTypeUsers",
                c => new
                    {
                        UserType_UserTypeId = c.Guid(nullable: false),
                        User_UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserType_UserTypeId, t.User_UserId })
                .ForeignKey("dbo.UserType", t => t.UserType_UserTypeId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.User_UserId, cascadeDelete: true)
                .Index(t => t.UserType_UserTypeId)
                .Index(t => t.User_UserId);
            
            AddColumn("dbo.UserType", "RolesCollectionId", c => c.Guid());
            CreateIndex("dbo.UserType", "RolesCollectionId");
            AddForeignKey("dbo.UserType", "RolesCollectionId", "dbo.RolesCollection", "RolesCollectionId");
            DropColumn("dbo.User", "UserTypeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "UserTypeId", c => c.Guid(nullable: false));
            DropForeignKey("dbo.UserTypeUsers", "User_UserId", "dbo.User");
            DropForeignKey("dbo.UserTypeUsers", "UserType_UserTypeId", "dbo.UserType");
            DropForeignKey("dbo.UserType", "RolesCollectionId", "dbo.RolesCollection");
            DropIndex("dbo.UserTypeUsers", new[] { "User_UserId" });
            DropIndex("dbo.UserTypeUsers", new[] { "UserType_UserTypeId" });
            DropIndex("dbo.UserType", new[] { "RolesCollectionId" });
            DropColumn("dbo.UserType", "RolesCollectionId");
            DropTable("dbo.UserTypeUsers");
            CreateIndex("dbo.User", "UserTypeId");
            AddForeignKey("dbo.User", "UserTypeId", "dbo.UserType", "UserTypeId", cascadeDelete: true);
        }
    }
}

namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class securitychangesv2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.User", "RolesCollectionId", "dbo.RolesCollection");
            DropForeignKey("dbo.User", "UserTypeId", "dbo.UserType");
            DropIndex("dbo.User", new[] { "RolesCollectionId" });
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
            
            AddColumn("dbo.UserType", "UserTypeIconId", c => c.Int(nullable: false,defaultValueSql:"0"));
           // AddColumn("dbo.UserType", "RolesCollectionId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Adresse", "CodePosatal", c => c.String());
            CreateIndex("dbo.UserType", "RolesCollectionId");
            AddForeignKey("dbo.UserType", "RolesCollectionId", "dbo.RolesCollection", "RolesCollectionId", cascadeDelete: true);
            DropColumn("dbo.User", "RolesCollectionId");
            DropColumn("dbo.User", "UserTypeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "UserTypeId", c => c.Guid(nullable: false));
            AddColumn("dbo.User", "RolesCollectionId", c => c.Guid());
            DropForeignKey("dbo.UserTypeUsers", "User_UserId", "dbo.User");
            DropForeignKey("dbo.UserTypeUsers", "UserType_UserTypeId", "dbo.UserType");
            DropForeignKey("dbo.UserType", "RolesCollectionId", "dbo.RolesCollection");
            DropIndex("dbo.UserTypeUsers", new[] { "User_UserId" });
            DropIndex("dbo.UserTypeUsers", new[] { "UserType_UserTypeId" });
            DropIndex("dbo.UserType", new[] { "RolesCollectionId" });
            AlterColumn("dbo.Adresse", "CodePosatal", c => c.String(nullable: false));
            DropColumn("dbo.UserType", "RolesCollectionId");
            DropColumn("dbo.UserType", "UserTypeIconId");
            DropTable("dbo.UserTypeUsers");
            CreateIndex("dbo.User", "UserTypeId");
            CreateIndex("dbo.User", "RolesCollectionId");
            AddForeignKey("dbo.User", "UserTypeId", "dbo.UserType", "UserTypeId", cascadeDelete: true);
            AddForeignKey("dbo.User", "RolesCollectionId", "dbo.RolesCollection", "RolesCollectionId");
        }
    }
}

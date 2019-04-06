namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CentrageChnges : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RCP",
                c => new
                    {
                        RcpId = c.Guid(nullable: false, identity: true),
                        Description = c.String(),
                        RcpTitle = c.String(),
                        DateTimeRcp = c.DateTime(nullable: false),
                        Error = c.String(),
                    })
                .PrimaryKey(t => t.RcpId);
            
            AddColumn("dbo.Patient", "RCP_RcpId", c => c.Guid());
            AddColumn("dbo.RendezVous", "CentrageDateTime", c => c.DateTime());
            AddColumn("dbo.RendezVous", "ArriveeEnPhysiqueDateTime", c => c.DateTime());
            AddColumn("dbo.RendezVous", "SortieDePhysiqueDateTime", c => c.DateTime());
            AddColumn("dbo.RendezVous", "Urgent", c => c.Boolean(nullable: false));
            AddColumn("dbo.User", "RCP_RcpId", c => c.Guid());
            AddColumn("dbo.RolesCollection", "RcpViewAllow", c => c.Boolean(nullable: false));
            AddColumn("dbo.RolesCollection", "RcpEditAllow", c => c.Boolean(nullable: false));
            AddColumn("dbo.RolesCollection", "CentrageViewAllow", c => c.Boolean(nullable: false));
            AddColumn("dbo.RolesCollection", "CentrageEditAllow", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Patient", "RCP_RcpId");
            CreateIndex("dbo.User", "RCP_RcpId");
            AddForeignKey("dbo.User", "RCP_RcpId", "dbo.RCP", "RcpId");
            AddForeignKey("dbo.Patient", "RCP_RcpId", "dbo.RCP", "RcpId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Patient", "RCP_RcpId", "dbo.RCP");
            DropForeignKey("dbo.User", "RCP_RcpId", "dbo.RCP");
            DropIndex("dbo.User", new[] { "RCP_RcpId" });
            DropIndex("dbo.Patient", new[] { "RCP_RcpId" });
            DropColumn("dbo.RolesCollection", "CentrageEditAllow");
            DropColumn("dbo.RolesCollection", "CentrageViewAllow");
            DropColumn("dbo.RolesCollection", "RcpEditAllow");
            DropColumn("dbo.RolesCollection", "RcpViewAllow");
            DropColumn("dbo.User", "RCP_RcpId");
            DropColumn("dbo.RendezVous", "Urgent");
            DropColumn("dbo.RendezVous", "SortieDePhysiqueDateTime");
            DropColumn("dbo.RendezVous", "ArriveeEnPhysiqueDateTime");
            DropColumn("dbo.RendezVous", "CentrageDateTime");
            DropColumn("dbo.Patient", "RCP_RcpId");
            DropTable("dbo.RCP");
        }
    }
}

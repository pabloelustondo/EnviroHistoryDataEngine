namespace JassWeather.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usrinfo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JassUserInfoes",
                c => new
                    {
                        JassUserInfoID = c.Int(nullable: false, identity: true),
                        JassUserInfoUserName = c.String(),
                        JassVariableGroupID = c.Int(nullable: false),
                        JassLatLonGroupID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.JassUserInfoID)
                .ForeignKey("dbo.JassVariableGroups", t => t.JassVariableGroupID, cascadeDelete: true)
                .ForeignKey("dbo.JassLatLonGroups", t => t.JassLatLonGroupID, cascadeDelete: true)
                .Index(t => t.JassVariableGroupID)
                .Index(t => t.JassLatLonGroupID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.JassUserInfoes", new[] { "JassLatLonGroupID" });
            DropIndex("dbo.JassUserInfoes", new[] { "JassVariableGroupID" });
            DropForeignKey("dbo.JassUserInfoes", "JassLatLonGroupID", "dbo.JassLatLonGroups");
            DropForeignKey("dbo.JassUserInfoes", "JassVariableGroupID", "dbo.JassVariableGroups");
            DropTable("dbo.JassUserInfoes");
        }
    }
}

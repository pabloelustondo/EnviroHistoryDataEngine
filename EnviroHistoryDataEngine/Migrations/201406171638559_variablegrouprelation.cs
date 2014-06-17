namespace JassWeather.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class variablegrouprelation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JassVariableGroupRels",
                c => new
                    {
                        JassVariableGroupRelID = c.Int(nullable: false, identity: true),
                        JassVariableGroupID = c.Int(nullable: false),
                        JassVariableID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.JassVariableGroupRelID)
                .ForeignKey("dbo.JassVariableGroups", t => t.JassVariableGroupID, cascadeDelete: true)
                .ForeignKey("dbo.JassVariables", t => t.JassVariableID, cascadeDelete: true)
                .Index(t => t.JassVariableGroupID)
                .Index(t => t.JassVariableID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.JassVariableGroupRels", new[] { "JassVariableID" });
            DropIndex("dbo.JassVariableGroupRels", new[] { "JassVariableGroupID" });
            DropForeignKey("dbo.JassVariableGroupRels", "JassVariableID", "dbo.JassVariables");
            DropForeignKey("dbo.JassVariableGroupRels", "JassVariableGroupID", "dbo.JassVariableGroups");
            DropTable("dbo.JassVariableGroupRels");
        }
    }
}

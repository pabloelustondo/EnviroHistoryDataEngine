namespace JassWeather.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class variablgroups : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JassVariableGroups",
                c => new
                    {
                        JassVariableGroupID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.JassVariableGroupID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.JassVariableGroups");
        }
    }
}

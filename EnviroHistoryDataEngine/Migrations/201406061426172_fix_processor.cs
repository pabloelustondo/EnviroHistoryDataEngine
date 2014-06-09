namespace JassWeather.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix_processor : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.JassProcessors", "startTime", c => c.DateTime());
            AlterColumn("dbo.JassProcessors", "endTime", c => c.DateTime());
            AlterColumn("dbo.JassProcessors", "spanTime", c => c.Time());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.JassProcessors", "spanTime", c => c.Time(nullable: false));
            AlterColumn("dbo.JassProcessors", "endTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.JassProcessors", "startTime", c => c.DateTime(nullable: false));
        }
    }
}

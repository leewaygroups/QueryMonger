namespace QueryMonger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCanExeQueryColumnToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "CanExecuteQuery", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "CanExecuteQuery");
        }
    }
}

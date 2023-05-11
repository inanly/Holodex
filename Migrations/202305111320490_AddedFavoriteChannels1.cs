namespace Holodex.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFavoriteChannels1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FavoriteChannels", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.FavoriteChannels", "ApplicationUser_Id");
            AddForeignKey("dbo.FavoriteChannels", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FavoriteChannels", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.FavoriteChannels", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.FavoriteChannels", "ApplicationUser_Id");
        }
    }
}

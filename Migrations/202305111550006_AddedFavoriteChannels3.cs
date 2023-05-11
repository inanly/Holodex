namespace Holodex.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFavoriteChannels3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Videos", "Channel_Id", "dbo.Channels");
            DropForeignKey("dbo.FavoriteChannels", "Video_Id", "dbo.Videos");
            DropIndex("dbo.FavoriteChannels", new[] { "Video_Id" });
            DropIndex("dbo.Videos", new[] { "Channel_Id" });
            AddColumn("dbo.FavoriteChannels", "ChannelId", c => c.String());
            AddColumn("dbo.FavoriteChannels", "ChannelName", c => c.String());
            AddColumn("dbo.FavoriteChannels", "ChannelEnglishName", c => c.String());
            DropColumn("dbo.FavoriteChannels", "Video_Id");
            DropTable("dbo.Videos");
            DropTable("dbo.Channels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Channels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        EnglishName = c.String(),
                        Name = c.String(),
                        Photo = c.String(),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Videos",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        AvailableAt = c.String(),
                        Duration = c.Int(nullable: false),
                        LiveViewers = c.Int(nullable: false),
                        PublishedAt = c.String(),
                        StartActual = c.String(),
                        StartScheduled = c.String(),
                        Status = c.String(),
                        Title = c.String(),
                        TopicId = c.String(),
                        Type = c.String(),
                        Channel_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.FavoriteChannels", "Video_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.FavoriteChannels", "ChannelEnglishName");
            DropColumn("dbo.FavoriteChannels", "ChannelName");
            DropColumn("dbo.FavoriteChannels", "ChannelId");
            CreateIndex("dbo.Videos", "Channel_Id");
            CreateIndex("dbo.FavoriteChannels", "Video_Id");
            AddForeignKey("dbo.FavoriteChannels", "Video_Id", "dbo.Videos", "Id");
            AddForeignKey("dbo.Videos", "Channel_Id", "dbo.Channels", "Id");
        }
    }
}

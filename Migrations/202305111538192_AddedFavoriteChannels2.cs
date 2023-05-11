namespace Holodex.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFavoriteChannels2 : DbMigration
    {
        public override void Up()
        {
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Channels", t => t.Channel_Id)
                .Index(t => t.Channel_Id);
            
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
            
            AddColumn("dbo.FavoriteChannels", "Video_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.FavoriteChannels", "Video_Id");
            AddForeignKey("dbo.FavoriteChannels", "Video_Id", "dbo.Videos", "Id");
            DropColumn("dbo.FavoriteChannels", "ChannelId");
            DropColumn("dbo.FavoriteChannels", "ChannelName");
            DropColumn("dbo.FavoriteChannels", "ChannelEnglishName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FavoriteChannels", "ChannelEnglishName", c => c.String());
            AddColumn("dbo.FavoriteChannels", "ChannelName", c => c.String());
            AddColumn("dbo.FavoriteChannels", "ChannelId", c => c.String());
            DropForeignKey("dbo.FavoriteChannels", "Video_Id", "dbo.Videos");
            DropForeignKey("dbo.Videos", "Channel_Id", "dbo.Channels");
            DropIndex("dbo.Videos", new[] { "Channel_Id" });
            DropIndex("dbo.FavoriteChannels", new[] { "Video_Id" });
            DropColumn("dbo.FavoriteChannels", "Video_Id");
            DropTable("dbo.Channels");
            DropTable("dbo.Videos");
        }
    }
}

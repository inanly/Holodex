﻿namespace Holodex.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFavoriteChannels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FavoriteChannels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ChannelId = c.String(),
                        ChannelName = c.String(),
                        ChannelEnglishName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FavoriteChannels");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Holodex.Models
{
    public class FavoriteChannel
    {
        
        
        public int Id { get; set; }

        public string UserId { get; set; }

        // Holodex 频道的信息
        public string ChannelId { get; set; }
        public string ChannelName { get; set; }
        public string ChannelEnglishName { get; set; }
        
        
    }

}
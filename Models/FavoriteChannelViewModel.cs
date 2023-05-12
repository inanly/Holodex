using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Holodex.Models
{
    public class FavoriteChannelViewModel
    {
        public FavoriteChannel FavoriteChannel { get; set; }
        public List<Video> Videos { get; set; }
    }
}
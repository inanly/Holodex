using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Holodex.Models
{
    public class HolodexApiResponse : List<Video>
    {

        public List<Video> Data { get; set; }

        public HolodexApiResponse() { }

        public HolodexApiResponse(IEnumerable<Video> videos) : base(videos)
        {
        }
    }
}

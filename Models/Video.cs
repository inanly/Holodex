using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Holodex.Models
{
    public class Video
    {
        public string AvailableAt { get; set; }
        public Channel Channel { get; set; }
        public int Duration { get; set; }
        public string Id { get; set; }
        [Display(Name = "Live Viewers")]
        public int LiveViewers { get; set; }
        public string PublishedAt { get; set; }
        public string StartActual { get; set; }
        public string StartScheduled { get; set; }
        public string Status { get; set; }
        public string Title { get; set; }
        public string TopicId { get; set; }
        public string Type { get; set; }
        
    }
}
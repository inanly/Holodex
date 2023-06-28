using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Holodex.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace YourNamespace.Controllers
{
    public class VideosController : Controller
    {
        private static readonly HttpClient client = new HttpClient();
        private readonly ApplicationDbContext _context;

        public VideosController()
        {
            _context = new ApplicationDbContext();
        }
        public VideosController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ActionResult> AddFavoriteChannel(string channelId, string channelName, string channelEnglishName)
        {
            var userId = User.Identity.GetUserId();
            var favoriteChannel = new FavoriteChannel
            {
                UserId = userId,
                ChannelId = channelId,
                ChannelName = channelName,
                ChannelEnglishName = channelEnglishName,

            };
            _context.FavoriteChannels.Add(favoriteChannel);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> RemoveFavoriteChannel(int id)
        {
            var favoriteChannel = await _context.FavoriteChannels.FindAsync(id);
            if (favoriteChannel != null && favoriteChannel.UserId == User.Identity.GetUserId())
            {
                _context.FavoriteChannels.Remove(favoriteChannel);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Favorites");
        }

        public async Task<ActionResult> Favorites()
        {
            string userId = User.Identity.GetUserId();
            var favoriteChannels = _context.FavoriteChannels.Where(fc => fc.UserId == userId).ToList();

            var favoriteChannelViewModels = new List<FavoriteChannelViewModel>();

            foreach (var favoriteChannel in favoriteChannels)
            {
                var videos = await GetVideosByChannelAsync(favoriteChannel.ChannelId);

                var favoriteChannelViewModel = new FavoriteChannelViewModel
                {
                    FavoriteChannel = favoriteChannel,
                    Videos = videos
                };

                favoriteChannelViewModels.Add(favoriteChannelViewModel);
            }

            return View(favoriteChannelViewModels);
        }



        public async Task<ActionResult> Index(string search = "")
        {
            var result = await GetVideosAsync(search);

            if (!string.IsNullOrWhiteSpace(search))
            {
                result = result.Where(v => v.Channel.Name.ToLower().Contains(search.ToLower())).ToList();
            }

            return View(result);
        }
        public async Task<ActionResult> Living(string search = "")
        {
            var result = await GetVideosAsync(search);

            if (!string.IsNullOrWhiteSpace(search))
            {
                result = result.Where(v => v.Channel.Name.ToLower().Contains(search.ToLower())).ToList();
            }

            return View(result);
        }
        public async Task<ActionResult> Upcoming(string search = "")
        {
            var result = await GetVideosAsync(search);

            if (!string.IsNullOrWhiteSpace(search))
            {
                result = result.Where(v => v.Channel.Name.ToLower().Contains(search.ToLower())).ToList();
            }

            return View(result);
        }

        

        private async Task<List<Video>> GetVideosByChannelAsync(string channelId)
        {
            List<Video> result;
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://holodex.net/api/v2/live?channel_id={channelId}&type=stream&status=live,upcoming&max_upcoming_hours=24&org=Hololive&sort=start_scheduled"),
                Headers =
        {
            { "Accept", "application/json" },
            { "X-APIKEY", "a69df232-609b-466f-8772-10939f05e8b4" }, 
        },
            };

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<Video>>(body);
            }

            return result;
        }




        private async Task<List<Video>> GetVideosAsync(string search)
        {
            List<Video> result;
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://holodex.net/api/v2/live?search={search}&type=stream&status=live,upcoming&max_upcoming_hours=24&org=Hololive&sort=start_scheduled"),
                Headers =
                {
                    { "Accept", "application/json" },
                    { "X-APIKEY", "a69df232-609b-466f-8772-10939f05e8b4" },
                },
            };

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<Video>>(body);
            }

            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Holodex.Models;
using Newtonsoft.Json;

namespace YourNamespace.Controllers
{
    public class VideosController : Controller
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<ActionResult> Index(string search = "")
        {
            var result = await GetVideosAsync(search);

            if (!string.IsNullOrWhiteSpace(search))
            {
                result = new HolodexApiResponse(result.Where(v => v.Channel.Name.ToLower().Contains(search.ToLower())).ToList());
            }

            return View(result);
        }
        public async Task<ActionResult> Living(string search = "")
        {
            var result = await GetVideosAsync(search);

            if (!string.IsNullOrWhiteSpace(search))
            {
                result = new HolodexApiResponse(result.Where(v => v.Channel.Name.ToLower().Contains(search.ToLower())).ToList());
            }

            return View(result);
        }
        public async Task<ActionResult> Upcoming(string search = "")
        {
            var result = await GetVideosAsync(search);

            if (!string.IsNullOrWhiteSpace(search))
            {
                result = new HolodexApiResponse(result.Where(v => v.Channel.Name.ToLower().Contains(search.ToLower())).ToList());
            }

            return View(result);
        }
        

        private async Task<List<Video>> GetVideosAsync(string search)
        {
            var result = new HolodexApiResponse();
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
                result = JsonConvert.DeserializeObject<HolodexApiResponse>(body);
            }

            return result;
        }

    }
}

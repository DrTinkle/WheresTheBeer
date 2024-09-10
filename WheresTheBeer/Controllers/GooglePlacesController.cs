using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using WheresTheBeer.Shared.Models;

namespace WheresTheBeer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GooglePlacesController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public GooglePlacesController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["GooglePlacesApiKey"] ?? throw new ArgumentNullException("GooglePlacesApiKey not found in configuration.");
        }

        [HttpGet("nearby")]
        public async Task<IActionResult> GetNearbyPlaces([FromQuery] string location, [FromQuery] int radius = 200)
        {
            var googlePlacesUrl = $"https://maps.googleapis.com/maps/api/place/nearbysearch/json" +
                $"?location={location}" +
                $"&radius={radius}" +
                $"&type=bar" +
                $"&key={_apiKey}";

            var response = await _httpClient.GetAsync(googlePlacesUrl);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, "Failed to retrieve data from Google Places.");
            }

            var content = await response.Content.ReadAsStringAsync();

            var placesResponse = JsonSerializer.Deserialize<GooglePlacesResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (placesResponse?.Results == null)
            {
                return StatusCode(500, "Failed to parse Google Places response.");
            }

            return Ok(placesResponse.Results);

        }
    }
}

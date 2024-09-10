﻿using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using WheresTheBeer.Shared.Models;
using System.Globalization;

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

            // Fetch raw JSON data from Google Places API
            var response = await _httpClient.GetAsync(googlePlacesUrl);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, "Failed to retrieve data from Google Places.");
            }

            // Get the raw JSON response
            var rawContent = await response.Content.ReadAsStringAsync();

            // Log the exact raw JSON response
            Console.WriteLine($"Raw JSON Response: {rawContent}");

            // (Optional) Deserialize the raw content for further processing, but AFTER logging the raw data
            var placesResponse = JsonSerializer.Deserialize<GooglePlacesResponse>(rawContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return Ok(placesResponse.Results);
        }

        [HttpGet("keywordsearch")]
        public async Task<IActionResult> GetBarsNearKeyword([FromQuery] string keyword, [FromQuery] int radius = 200)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return BadRequest("Keyword is required.");
            }

            // Step 1: Use the Geocoding API to get coordinates from the keyword
            var geocodeUrl = $"https://maps.googleapis.com/maps/api/geocode/json" +
                $"?address={keyword}" +
                $"&key={_apiKey}";

            var geocodeResponse = await _httpClient.GetAsync(geocodeUrl);
            if (!geocodeResponse.IsSuccessStatusCode)
            {
                return StatusCode((int)geocodeResponse.StatusCode, "Failed to retrieve location from keyword.");
            }

            var geocodeContent = await geocodeResponse.Content.ReadAsStringAsync();
            var geocodeData = JsonSerializer.Deserialize<GeocodeResponse>(geocodeContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (geocodeData?.Results?.Count == 0)
            {
                return NotFound("No location found for the given keyword.");
            }

            // Extract the coordinates from the geocoding response
            var location = geocodeData.Results[0].Geometry.Location;

            // Ensure the coordinates are formatted with periods using InvariantCulture
            var locationCoordinates = string.Format(CultureInfo.InvariantCulture, "{0},{1}", location.Lat, location.Lng);

            // Step 2: Use the Nearby Search API with the derived coordinates
            var nearbySearchUrl = $"https://maps.googleapis.com/maps/api/place/nearbysearch/json" +
                $"?location={locationCoordinates}" +
                $"&radius={radius}" +
                $"&type=bar" +
                $"&key={_apiKey}";

            var nearbyResponse = await _httpClient.GetAsync(nearbySearchUrl);
            if (!nearbyResponse.IsSuccessStatusCode)
            {
                return StatusCode((int)nearbyResponse.StatusCode, "Failed to retrieve nearby places.");
            }

            var nearbyContent = await nearbyResponse.Content.ReadAsStringAsync();
            var placesResponse = JsonSerializer.Deserialize<GooglePlacesResponse>(nearbyContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return Ok(placesResponse.Results);
        }

        [HttpGet("photo")]
        public IActionResult GetPhotoUrl([FromQuery] string photoReference)
        {
            if (string.IsNullOrEmpty(photoReference))
            {
                return BadRequest("Photo reference is required.");
            }

            var photoUrl = $"https://maps.googleapis.com/maps/api/place/photo?maxwidth=400&photoreference={photoReference}&key={_apiKey}";
            return Ok(photoUrl);  // Return the constructed photo URL
        }

    }
}

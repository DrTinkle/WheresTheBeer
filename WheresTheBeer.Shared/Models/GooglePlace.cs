using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WheresTheBeer.Shared.Models
{
    public class GooglePlace
    {
        public string Name { get; set; }
        public string Vicinity { get; set; }
        public double Rating { get; set; }

        [JsonPropertyName("user_ratings_total")]
        public int UserRatingsTotal { get; set; }

        public PlaceGeometry Geometry { get; set; }

        [JsonPropertyName("opening_hours")]
        public PlaceOpeningHours OpeningHours { get; set; }

        public string Icon { get; set; }

        [JsonPropertyName("place_id")]
        public string PlaceId { get; set; }

        [JsonPropertyName("price_level")]
        public int? PriceLevel { get; set; }

        public List<PlacePhoto> Photos { get; set; }
        public string PhotoUrl { get; set; }
    }

    public class PlaceGeometry
    {
        public PlaceLocation Location { get; set; }
    }

    public class PlaceLocation
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }

    public class PlaceOpeningHours
    {
        [JsonPropertyName("open_now")]
        public bool OpenNow { get; set; }
    }

    public class PlacePhoto
    {
        public int Height { get; set; }
        public int Width { get; set; }

        [JsonPropertyName("photo_reference")]
        public string PhotoReference { get; set; }
    }

    public class GooglePlacesResponse
    {
        public List<GooglePlace> Results { get; set; } = new List<GooglePlace>();

        [JsonPropertyName("next_page_token")]
        public string NextPageToken { get; set; }
    }

    public class GeocodeResponse
    {
        [JsonPropertyName("results")]
        public List<GeocodeResult> Results { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }
    }

    public class GeocodeResult
    {
        [JsonPropertyName("geometry")]
        public GeocodeGeometry Geometry { get; set; }
    }

    public class GeocodeGeometry
    {
        [JsonPropertyName("location")]
        public GeocodeLocation Location { get; set; }
    }

    public class GeocodeLocation
    {
        [JsonPropertyName("lat")]
        public double Lat { get; set; }

        [JsonPropertyName("lng")]
        public double Lng { get; set; }
    }
}

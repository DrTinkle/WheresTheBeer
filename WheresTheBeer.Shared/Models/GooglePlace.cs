using System.Collections.Generic;

namespace WheresTheBeer.Shared.Models
{
    public class GooglePlace
    {
        public string Name { get; set; } = string.Empty;
        public string Vicinity { get; set; } = string.Empty;
        public double Rating { get; set; }
        public int UserRatingsTotal { get; set; }
        public Geometry Geometry { get; set; } = new Geometry();
        public OpeningHours OpeningHours { get; set; } = new OpeningHours();
    }

    public class Geometry
    {
        public Location Location { get; set; } = new Location();
    }

    public class Location
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }

    public class OpeningHours
    {
        public bool OpenNow { get; set; }
    }

    public class GooglePlacesResponse
    {
        public List<GooglePlace> Results { get; set; } = new List<GooglePlace>();
        public string NextPageToken { get; set; } = string.Empty;
    }
}

namespace Ambev.DeveloperEvaluation.Domain.ValueObjects
{
    public class Geolocation
    {
        public string Lat { get; private set; }
        public string Long { get; private set; }

        private Geolocation() { }

        public Geolocation(string lat, string lon)
        {
            if (string.IsNullOrWhiteSpace(lat) || string.IsNullOrWhiteSpace(lon))
                throw new ArgumentException("Latitude and longitude are required.");
            Lat = lat;
            Long = lon;
        }
    }
}

namespace Ambev.DeveloperEvaluation.Domain.ValueObjects
{
    public class Address
    {
        public string City { get; private set; }
        public string Street { get; private set; }
        public int Number { get; private set; }
        public string Zipcode { get; private set; }
        public Geolocation Geolocation { get; private set; }

        private Address() { }

        public Address(string city, string street, int number, string zipcode, Geolocation geo)
        {
            if (string.IsNullOrWhiteSpace(city) || string.IsNullOrWhiteSpace(street) || string.IsNullOrWhiteSpace(zipcode))
                throw new ArgumentException("City, street and zipcode are required.");
            City = city;
            Street = street;
            Number = number;
            Zipcode = zipcode;
            Geolocation = geo ?? throw new ArgumentNullException(nameof(geo));
        }
    }
}

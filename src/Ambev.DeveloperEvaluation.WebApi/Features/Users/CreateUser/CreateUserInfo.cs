namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser
{
    public class UserName
    {
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
    }

    public class UserAddress
    {
        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public int Number { get; set; }
        public string ZipCode { get; set; } = string.Empty;
        public Geo? GeoLocation { get; set; }

        public class Geo
        {
            public string Lat { get; set; } = string.Empty;
            public string Long { get; set; } = string.Empty;
        }
    }
}

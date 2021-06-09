using SimpleSocialNetworkApp.Domain.Interfaces;
using System;

namespace SimpleSocialNetworkApp.Domain.Models
{
    public class AddressInformation : IAddressInformation
    {
        public string Street { get; set; }
        public int StreetNumber { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public AddressInformation(string street, int streetNo, string city, string country)
        {
            Street = street;
            StreetNumber = streetNo;
            City = city;
            Country = country;
        }
        public string PrintAddressInfo()
        {
            return $"\nAddress information:\nStreet {Street}\nStreet Number: {StreetNumber}\nCity: {City}\nCountry{Country}\n";
        }
    }
}

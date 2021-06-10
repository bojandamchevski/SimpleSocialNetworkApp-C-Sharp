using SimpleSocialNetworkApp.Domain.Enums;
using System;
using System.Collections.Generic;

namespace SimpleSocialNetworkApp.Domain.Models
{
    public class User : BaseEntity
    {
        public PersonalInformation PersonalInfo { get; set; }
        public AddressInformation AddressInfo { get; set; }
        public List<Inbox> MyMessages { get; set; }
        public Friends MyFriends { get; set; }
        public User(string firstName, string lastName, int age, string username, string password, UserGender gender, string street, int streetNo, string city, string country)
        {
            PersonalInfo = new PersonalInformation(firstName, lastName, age, username, password, gender);
            AddressInfo = new AddressInformation(street, streetNo, city, country);
            MyMessages = new List<Inbox>();
            MyFriends = new Friends();
            IsAccountActive = true;
        }
        public override string PrintInfo()
        {
            return $"ID: {Id} {PersonalInfo.PrintPersonalInfo()} {AddressInfo.PrintAddressInfo()}";
        }
    }
}

using SimpleSocialNetworkApp.Domain.Enums;
using SimpleSocialNetworkApp.Domain.Interfaces;

namespace SimpleSocialNetworkApp.Domain.Models
{
    public class PersonalInformation : IPersonalInformation
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserGender Gender { get; set; }
        public PersonalInformation(string firstName, string lastName, int age, string username, string password, UserGender gender)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Username = username;
            Password = password;
            Gender = gender;
        }
        public string PrintPersonalInfo()
        {
            return $"\nPersonal information:\nFirst name {FirstName}\nLast name: {LastName}\nAge: {Age}\nEmail: {Username}\nPassword: {Password}\nGender {Gender}";
        }
    }
}

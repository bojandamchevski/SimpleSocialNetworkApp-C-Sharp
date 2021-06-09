using SimpleSocialNetworkApp.Domain.Models;
using SimpleSocialNetworkApp.Services.Interfaces;
using System;
using System.Linq;

namespace SimpleSocialNetworkApp.Services.Helpers
{
    public class ValidationHelper : IValidationHelper
    {
        private TextHelper _textHelper;
        public ValidationHelper()
        {
            _textHelper = new TextHelper();
        }
        public bool ValidateUsername(User user)
        {

            if (user.PersonalInfo.Username.Length < 5)
            {
                _textHelper.GenerateText("The username can not be less than 5 characters.", ConsoleColor.Red);
                return false;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Username approved !");
                return true;
            }
        }
        public bool ValidatePassword(User user)
        {
            if (user.PersonalInfo.Password.Length < 5)
            {
                _textHelper.GenerateText("The password can not be less than 6 characters.", ConsoleColor.Red);
                return false;
            }
            else
            {
                if (user.PersonalInfo.Password.Any(char.IsUpper) && user.PersonalInfo.Password.Any(char.IsDigit))
                {
                    _textHelper.GenerateText("Password approved.", ConsoleColor.Green);
                    return true;
                }
                else
                {
                    _textHelper.GenerateText("Password must contain at least one capital letter and at least one number.", ConsoleColor.Red);
                    return false;
                }
            }
        }
        public bool ValidateFirstNameLastName(User user)
        {
            if (user.PersonalInfo.FirstName.Length < 2 && user.PersonalInfo.LastName.Length < 2)
            {
                _textHelper.GenerateText("First and last name can not be shorter than 2 characters.", ConsoleColor.Red);
                return false;
            }
            else
            {
                bool firstNameContainsNumber = user.PersonalInfo.FirstName.Any(char.IsDigit);
                bool lastNameContainsNumber = user.PersonalInfo.LastName.Any(char.IsDigit);
                bool firstNameContainsSymbol = user.PersonalInfo.FirstName.Any(char.IsSymbol);
                bool lastNameContainsSymbol = user.PersonalInfo.LastName.Any(char.IsSymbol);
                if (firstNameContainsNumber || lastNameContainsNumber || lastNameContainsSymbol || firstNameContainsSymbol)
                {
                    _textHelper.GenerateText("First and last name must not contain numbers or symbols.", ConsoleColor.Red);
                    return false;
                }
                else
                {
                    _textHelper.GenerateText("First and last name approved !", ConsoleColor.Green);
                    return true;
                }
            }
        }
        public bool ValidateAge(User user)
        {
            {
                if (user.PersonalInfo.Age < 18 || user.PersonalInfo.Age > 120)
                {
                    _textHelper.GenerateText("Age must be from 18 to 120.", ConsoleColor.Red);
                    return false;
                }
                else
                {
                    _textHelper.GenerateText("Age approved !", ConsoleColor.Green);
                    return true;
                }

            }

        }

        public bool OtherValidations(User user)
        {
            if (user.AddressInfo.City == "" && user.AddressInfo.Country == "" && user.AddressInfo.Street == "")
            {
                _textHelper.GenerateText("Field can not be empty", ConsoleColor.Red);
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}

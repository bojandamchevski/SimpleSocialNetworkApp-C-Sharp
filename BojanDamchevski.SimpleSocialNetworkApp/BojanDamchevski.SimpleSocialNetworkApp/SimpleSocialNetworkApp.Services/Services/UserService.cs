using SimpleSocialNetworkApp.Domain.Database;
using SimpleSocialNetworkApp.Domain.Enums;
using SimpleSocialNetworkApp.Domain.Models;
using SimpleSocialNetworkApp.Services.Helpers;
using SimpleSocialNetworkApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SimpleSocialNetworkApp.Services.Services
{
    public class UserService : IUserService
    {
        private Database _database;
        private TextHelper _textHelper;
        private ValidationHelper _validationHelper;
        public UserService()
        {
            _database = new Database();
            _textHelper = new TextHelper();
            _validationHelper = new ValidationHelper();
        }
        public User LogIn()
        {
            while (true)
            {
                Console.Clear();
                _textHelper.GenerateText("Welcome to the log in menu.\nEnter the required information bellow.", ConsoleColor.Cyan);
                _textHelper.GenerateText("\nEnter username:\n", ConsoleColor.Cyan);
                string usernameInput = Console.ReadLine();
                _textHelper.GenerateText("\nEnter password:\n", ConsoleColor.Cyan);
                string passwordInput = Console.ReadLine();
                User user = _database.GetAll().FirstOrDefault(x => x.PersonalInfo.Username == usernameInput && x.PersonalInfo.Password == passwordInput);
                if (user == null)
                {
                    Console.Clear();
                    _textHelper.GenerateText("Incorrect username or password.", ConsoleColor.Red);
                    Thread.Sleep(2000);
                }
                else if (!user.IsAccountActive)
                {
                    while (true)
                    {

                        Console.Clear();
                        _textHelper.GenerateText("Account is deactivated at the moment.\nWould you like to activate it again ?\n", ConsoleColor.Red);
                        _textHelper.GenerateText("1.) Yes I want to activate it again. 2.) No i will log in with a different account.\n", ConsoleColor.Yellow);
                        bool activateChoiceValidation = int.TryParse(Console.ReadLine(), out int activateChoice);
                        if (!activateChoiceValidation)
                        {
                            Console.Clear();
                            _textHelper.GenerateText("Invalid input.\nTry again.", ConsoleColor.Red);
                            Thread.Sleep(2000);
                        }
                        else
                        {
                            if (activateChoice == 1)
                            {
                                user.IsAccountActive = true;
                                _database.Update(user);
                                Console.Clear();
                                _textHelper.GenerateText("Account activated.", ConsoleColor.Green);
                                Thread.Sleep(2000);
                            }
                            else
                            {
                                break;
                            }
                            break;
                        }
                        Thread.Sleep(2000);
                    }
                }
                else
                {
                    Console.Clear();
                    _textHelper.GenerateText("Success!", ConsoleColor.Green);
                    Thread.Sleep(2000);
                    return user;
                }
            }
        }

        public void Register()
        {
            while (true)
            {

                Console.Clear();
                _textHelper.GenerateText("Welcome to the register menu.\nType the required information bellow.\n", ConsoleColor.Cyan);
                _textHelper.GenerateText("\nEnter first name:\n", ConsoleColor.DarkCyan);
                string firstName = Console.ReadLine();
                _textHelper.GenerateText("\nEnter last name:\n", ConsoleColor.DarkCyan);
                string lastName = Console.ReadLine();
                _textHelper.GenerateText("\nEnter age:\n", ConsoleColor.DarkCyan);
                int age;
                while (true)
                {
                    bool ageValidation = int.TryParse(Console.ReadLine(), out int ageInput);
                    if (!ageValidation)
                    {
                        _textHelper.GenerateText("\nInvalid input.\n", ConsoleColor.Red);
                    }
                    else
                    {
                        age = ageInput;
                        break;
                    }
                }
                _textHelper.GenerateText("\nEnter username:\n", ConsoleColor.DarkCyan);
                string username = Console.ReadLine();
                _textHelper.GenerateText("\nEnter password:\n", ConsoleColor.DarkCyan);
                string password = Console.ReadLine();
                UserGender gender;
                while (true)
                {
                    UserGender genderInput;
                    _textHelper.GenerateText("\nChoose gender:\n", ConsoleColor.DarkCyan);
                    _textHelper.GenerateText("\n1.) Male   2.) Female   3.) Other\n", ConsoleColor.DarkCyan);
                    bool genderValidation = int.TryParse(Console.ReadLine(), out int genderChoice);
                    if (!genderValidation)
                    {
                        _textHelper.GenerateText("\nInvalid input.\n", ConsoleColor.Red);
                    }
                    else
                    {
                        if (genderChoice == 1)
                        {
                            genderInput = UserGender.Male;
                            gender = genderInput;
                            break;
                        }
                        else if (genderChoice == 2)
                        {
                            genderInput = UserGender.Female;
                            gender = genderInput;
                            break;
                        }
                        else if (genderChoice == 3)
                        {
                            genderInput = UserGender.Other;
                            gender = genderInput;
                            break;
                        }
                        else
                        {
                            _textHelper.GenerateText("\nChoose one of the given gender options.\n", ConsoleColor.Red);
                        }
                    }
                }
                _textHelper.GenerateText("\nEnter street:\n", ConsoleColor.DarkCyan);
                string street = Console.ReadLine();
                int streetNumber;
                while (true)
                {
                    _textHelper.GenerateText("\nEnter street number:\n", ConsoleColor.DarkCyan);
                    bool streetNumValidation = int.TryParse(Console.ReadLine(), out int streetNumInput);
                    if (!streetNumValidation)
                    {
                        _textHelper.GenerateText("\nInvalid input.\n", ConsoleColor.Red);
                    }
                    else
                    {
                        streetNumber = streetNumInput;
                        break;
                    }
                }
                _textHelper.GenerateText("\nEnter city:\n", ConsoleColor.DarkCyan);
                string city = Console.ReadLine();
                _textHelper.GenerateText("\nEnter country:\n", ConsoleColor.DarkCyan);
                string country = Console.ReadLine();
                User user = new User(firstName, lastName, age, username, password, gender, street, streetNumber, city, country);
                Console.Clear();
                if (_validationHelper.ValidateFirstNameLastName(user) && _validationHelper.ValidateAge(user) && _validationHelper.ValidatePassword(user) && _validationHelper.ValidateUsername(user) && _validationHelper.OtherValidations(user))
                {
                    Thread.Sleep(2000);
                    Console.Clear();
                    _textHelper.GenerateText("Success!\nPlease log in.", ConsoleColor.Green);
                    _database.Insert(user);
                    Thread.Sleep(2000);
                    break;
                }
                else
                {
                    _textHelper.GenerateText("Try again.\n", ConsoleColor.Red);
                    Thread.Sleep(3000);
                }
            }
        }

        public void AddFriend(User user)
        {
            while (true)
            {
                Console.Clear();
                _textHelper.GenerateText("\nEnter users ID to add them to your friend list.\n", ConsoleColor.Cyan);
                List<User> friends = _database.GetAll();
                foreach (User friend in friends)
                {
                    if (user.Id == friend.Id)
                    {
                        Console.WriteLine();
                    }
                    else
                    {
                        _textHelper.GenerateText($"\n{friend.PersonalInfo.FirstName} {friend.PersonalInfo.LastName} {friend.Id}\n", ConsoleColor.Cyan);
                    }
                }
                _textHelper.GenerateText("\nEnter users ID:\n", ConsoleColor.Cyan);
                bool friendIdValidation = int.TryParse(Console.ReadLine(), out int friendId);
                User myNewFriend = friends.FirstOrDefault(x => x.Id == friendId);
                if (myNewFriend == null)
                {
                    Console.Clear();
                    _textHelper.GenerateText("No user with that ID exists.", ConsoleColor.Cyan);
                    Thread.Sleep(2000);
                }
                else
                {
                    Console.Clear();
                    User existingFriend = user.MyFriends.FriendsList.FirstOrDefault(x => x.Id == friendId);
                    if (existingFriend != null)
                    {
                        Console.Clear();
                        _textHelper.GenerateText("You alredy have that friend!\nEnter another ID", ConsoleColor.Red);
                        Thread.Sleep(2000);
                    }
                    else
                    {
                        user.MyFriends.FriendsList.Add(myNewFriend);
                        myNewFriend.MyFriends.FriendsList.Add(user);
                        UpdateFriend(myNewFriend, user);
                        _textHelper.GenerateText("Friend added", ConsoleColor.Green);
                        Thread.Sleep(2000);
                        break;
                    }
                }
            }
        }
        public void UpdateFriend(User friend, User user)
        {
            _database.Update(user);
            _database.Update(friend);
        }
        public void SendMessage(User user)
        {
            while (true)
            {
                Console.Clear();
                _textHelper.GenerateText("Send a message.\n", ConsoleColor.Cyan);
                List<User> friends = user.MyFriends.FriendsList;
                foreach (User friend in friends)
                {
                    _textHelper.GenerateText($"\n{friend.PersonalInfo.FirstName} {friend.PersonalInfo.LastName} {friend.Id}\n", ConsoleColor.Cyan);
                }
                _textHelper.GenerateText("\nEnter friend's ID to send them a message.\n", ConsoleColor.Cyan);
                bool idValidation = int.TryParse(Console.ReadLine(), out int id);
                if (!idValidation)
                {
                    Console.Clear();
                    _textHelper.GenerateText("Invalid input.", ConsoleColor.Red);
                    Thread.Sleep(2000);
                }
                else
                {
                    User myFriend = friends.FirstOrDefault(x => x.Id == id);
                    if (myFriend == null)
                    {
                        Console.Clear();
                        _textHelper.GenerateText("You don't have a friend with that ID.", ConsoleColor.Red);
                        Thread.Sleep(2000);
                    }
                    else
                    {
                        Console.Clear();
                        _textHelper.GenerateText($"Type message to {myFriend.PersonalInfo.FirstName} {myFriend.PersonalInfo.LastName}:", ConsoleColor.Green);
                        string message = Console.ReadLine();
                        if (String.IsNullOrEmpty(message))
                        {
                            Console.Clear();
                            _textHelper.GenerateText("Can't send empty message!", ConsoleColor.Red);
                            Thread.Sleep(2000);
                        }
                        else
                        {
                            Console.Clear();
                            DateTime currentDateAndTime = DateTime.Now;
                            Inbox newMessage = new Inbox(message, user, currentDateAndTime);
                            myFriend.MyMessages.Add(newMessage);
                            _database.Update(myFriend);
                            _textHelper.GenerateText($"Message sent to {myFriend.PersonalInfo.FirstName}.", ConsoleColor.Green);
                            Thread.Sleep(2000);
                            break;
                        }
                    }
                }
            }
        }

        public void ViewFriends(User user)
        {
            List<User> friends = user.MyFriends.FriendsList;
            foreach (User friend in friends)
            {
                _textHelper.GenerateText($"\n{friend.PersonalInfo.FirstName} {friend.PersonalInfo.LastName}\n", ConsoleColor.Cyan);
            }
        }

        public void ViewMessages(User user)
        {
            Console.Clear();
            if (user.MyMessages.Count == 0)
            {
                Console.Clear();
                _textHelper.GenerateText("You have no messages.", ConsoleColor.Red);
                Thread.Sleep(2000);
            }
            else
            {
                foreach (Inbox message in user.MyMessages)
                {
                    _textHelper.GenerateText($"{message.PrintMessages()}\n____________________________", ConsoleColor.Cyan);
                }
                _textHelper.GenerateText("\n\n\nPress any key to continue.", ConsoleColor.Cyan);
                Console.ReadKey();
            }
        }

        public void ChangePassword(User user)
        {
            while (true)
            {
                Console.Clear();
                _textHelper.GenerateText($"Welcome {user.PersonalInfo.FirstName} to the change your password menu.", ConsoleColor.Cyan);
                _textHelper.GenerateText("\nIf you don't want to change your password just hit ENTER to go back to the profile menu.\n", ConsoleColor.Cyan);
                _textHelper.GenerateText("\nEnter new password:\n", ConsoleColor.Cyan);
                string newPassword = Console.ReadLine();
                if (newPassword == "")
                {
                    break;
                }
                user.PersonalInfo.Password = newPassword;
                bool newPasswordValidation = _validationHelper.ValidatePassword(user);
                if (newPasswordValidation)
                {
                    _database.Update(user);
                    Console.Clear();
                    _textHelper.GenerateText("New password approved.\n", ConsoleColor.Green);
                    Thread.Sleep(2000);
                    break;
                }
                else
                {
                    Console.Clear();
                    _textHelper.GenerateText("New password not approved!", ConsoleColor.Red);
                    Thread.Sleep(2000);
                }
            }
        }

        public void ChangeUsername(User user)
        {
            while (true)
            {
                Console.Clear();
                _textHelper.GenerateText($"Welcome {user.PersonalInfo.FirstName} to the change your username menu.", ConsoleColor.Cyan);
                _textHelper.GenerateText("\nIf you don't want to change your username just hit ENTER to go back to the profile menu.\n", ConsoleColor.Cyan);
                _textHelper.GenerateText("\nEnter new username:\n", ConsoleColor.Cyan);
                string newUsername = Console.ReadLine();
                if (newUsername == "")
                {
                    break;
                }
                user.PersonalInfo.Username = newUsername;
                bool newUsernameValidation = _validationHelper.ValidateUsername(user);
                if (newUsernameValidation)
                {
                    _database.Update(user);
                    Console.Clear();
                    _textHelper.GenerateText("New username approved.\n", ConsoleColor.Green);
                    Thread.Sleep(2000);
                    break;
                }
                else
                {
                    Console.Clear();
                    _textHelper.GenerateText("New username not approved!", ConsoleColor.Red);
                    Thread.Sleep(2000);
                }
            }
        }

        public void ChangeFirstName(User user)
        {
            while (true)
            {
                Console.Clear();
                _textHelper.GenerateText($"Welcome {user.PersonalInfo.FirstName} to the change your first name menu.", ConsoleColor.Cyan);
                _textHelper.GenerateText("\nIf you don't want to change your first name just hit ENTER to go back to the profile menu.\n", ConsoleColor.Cyan);
                _textHelper.GenerateText("\nEnter new first name:\n", ConsoleColor.Cyan);
                string newFirstName = Console.ReadLine();
                if (newFirstName == "")
                {
                    break;
                }
                user.PersonalInfo.FirstName = newFirstName;
                bool newFirstNameValidation = _validationHelper.ValidateFirstNameLastName(user);
                if (newFirstNameValidation)
                {
                    _database.Update(user);
                    Console.Clear();
                    _textHelper.GenerateText("New first name approved.\n", ConsoleColor.Green);
                    Thread.Sleep(2000);
                    break;
                }
                else
                {
                    Console.Clear();
                    _textHelper.GenerateText("New first name not approved!", ConsoleColor.Red);
                    Thread.Sleep(2000);
                }
            }
        }

        public void ChangeLastName(User user)
        {
            while (true)
            {
                Console.Clear();
                _textHelper.GenerateText($"Welcome {user.PersonalInfo.FirstName} to the change your last name menu.", ConsoleColor.Cyan);
                _textHelper.GenerateText("\nIf you don't want to change your last name just hit ENTER to go back to the profile menu.\n", ConsoleColor.Cyan);
                _textHelper.GenerateText("\nEnter new last name:\n", ConsoleColor.Cyan);
                string newLastName = Console.ReadLine();
                if (newLastName == "")
                {
                    break;
                }
                user.PersonalInfo.LastName = newLastName;
                bool newLastNameValidation = _validationHelper.ValidateFirstNameLastName(user);
                if (newLastNameValidation)
                {
                    _database.Update(user);
                    Console.Clear();
                    _textHelper.GenerateText("New last name approved.\n", ConsoleColor.Green);
                    Thread.Sleep(2000);
                    break;
                }
                else
                {
                    Console.Clear();
                    _textHelper.GenerateText("New last name not approved!", ConsoleColor.Red);
                    Thread.Sleep(2000);
                }
            }
        }

        public void DeactivateAccount(User user)
        {
            user.IsAccountActive = false;
            _database.Update(user);
            Console.Clear();
            _textHelper.GenerateText("Account has been deactivated.\n", ConsoleColor.Green);
            _textHelper.GenerateText("You have to log in again!\n", ConsoleColor.Green);
            Thread.Sleep(2000);
        }

        public void ChangeAddressInfo(User user)
        {
            while (true)
            {
                Console.Clear();
                _textHelper.GenerateText($"Welcome {user.PersonalInfo.FirstName} to the change your address information menu.", ConsoleColor.Cyan);
                _textHelper.GenerateText("\nIf you don't want to change your address information just hit ENTER to go back to the profile menu.\n", ConsoleColor.Cyan);
                _textHelper.GenerateText("\nEnter new street name:\n", ConsoleColor.Cyan);
                string newStreet = Console.ReadLine();
                if (newStreet == "")
                {
                    break;
                }
                _textHelper.GenerateText("\nEnter new street number name:\n", ConsoleColor.Cyan);
                bool streetNoValidation = int.TryParse(Console.ReadLine(), out int newStreetNumberInput);
                int newStreetNumber = newStreetNumberInput;
                _textHelper.GenerateText("\nEnter new city name:\n", ConsoleColor.Cyan);
                string newCity = Console.ReadLine();
                _textHelper.GenerateText("\nEnter new country name:\n", ConsoleColor.Cyan);
                string newCountry = Console.ReadLine();
                if (!streetNoValidation)
                {
                    Console.Clear();
                    _textHelper.GenerateText("Invalid number input,please try again.!", ConsoleColor.Red);
                    Thread.Sleep(2000);
                }
                else
                {

                    user.AddressInfo.Street = newStreet;
                    user.AddressInfo.StreetNumber = newStreetNumber;
                    user.AddressInfo.City = newCity;
                    user.AddressInfo.Country = newCountry;
                    bool newAddressInfoValidation = _validationHelper.OtherValidations(user);
                    if (newAddressInfoValidation)
                    {
                        _database.Update(user);
                        Console.Clear();
                        _textHelper.GenerateText("New address approved.\n", ConsoleColor.Green);
                        Thread.Sleep(2000);
                        break;
                    }
                    else
                    {
                        Console.Clear();
                        _textHelper.GenerateText("New address not approved!", ConsoleColor.Red);
                        Thread.Sleep(2000);
                    }
                }
            }
        }
    }
}

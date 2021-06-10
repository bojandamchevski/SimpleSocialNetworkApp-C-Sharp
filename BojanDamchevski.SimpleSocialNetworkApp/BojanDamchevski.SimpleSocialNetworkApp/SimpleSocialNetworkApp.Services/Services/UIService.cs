using SimpleSocialNetworkApp.Domain.Database;
using SimpleSocialNetworkApp.Domain.Models;
using SimpleSocialNetworkApp.Services.Helpers;
using SimpleSocialNetworkApp.Services.Interfaces;
using System;
using System.Threading;

namespace SimpleSocialNetworkApp.Services.Services
{
    public class UIService : IUIService
    {
        private TextHelper _textHelper;
        private UserService _userService;
        public UIService()
        {
            _textHelper = new TextHelper();
            _userService = new UserService();
        }
        public void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                _textHelper.GenerateText("Welcome to the main menu.", ConsoleColor.Yellow);
                _textHelper.GenerateText("Please choose one of the following options:", ConsoleColor.Yellow);
                _textHelper.GenerateText("1.) Log in     2.) Register     3.) Exit", ConsoleColor.Yellow);
                _textHelper.GenerateText("Type one of the following numbers and press ENTER to continue.", ConsoleColor.DarkYellow);
                bool menuChoiceValidation = int.TryParse(Console.ReadLine(), out int menuChoice);
                if (!menuChoiceValidation)
                {

                    Console.Clear();
                    _textHelper.GenerateText("Invalid input, please try again.", ConsoleColor.Red);
                    Thread.Sleep(2000);
                }
                else
                {
                    if (menuChoice == 1)
                    {
                        User user = _userService.LogIn();
                        while (true)
                        {
                            Console.Clear();
                            _textHelper.GenerateText($"Welcome to your profile page.\nHello {user.PersonalInfo.FirstName}!\n\n\n", ConsoleColor.Green);
                            _textHelper.GenerateText(user.PrintInfo(), ConsoleColor.Cyan);
                            _textHelper.GenerateText($"1.) Add a friend", ConsoleColor.Yellow);
                            _textHelper.GenerateText($"2.) View friends", ConsoleColor.Yellow);
                            _textHelper.GenerateText($"3.) Send a message", ConsoleColor.Yellow);
                            _textHelper.GenerateText($"4.) View messages", ConsoleColor.Yellow);
                            _textHelper.GenerateText($"5.) Change password", ConsoleColor.Yellow);
                            _textHelper.GenerateText($"6.) Change username", ConsoleColor.Yellow);
                            _textHelper.GenerateText($"7.) Change first name", ConsoleColor.Yellow);
                            _textHelper.GenerateText($"8.) Change last name", ConsoleColor.Yellow);
                            _textHelper.GenerateText($"9.) Change address information", ConsoleColor.Yellow);
                            _textHelper.GenerateText($"10.) Deactivate account", ConsoleColor.Yellow);
                            _textHelper.GenerateText($"11.) Log out", ConsoleColor.Yellow);
                            bool profileChoiceValidation = int.TryParse(Console.ReadLine(), out int profileChoice);
                            if (!profileChoiceValidation)
                            {
                                Console.Clear();
                                _textHelper.GenerateText("Invalid input, please try again.", ConsoleColor.Red);
                                Thread.Sleep(2000);
                            }
                            else
                            {
                                if (profileChoice == 1)
                                {
                                    _userService.AddFriend(user);
                                }
                                else if (profileChoice == 2)
                                {
                                    Console.Clear();
                                    _userService.ViewFriends(user);
                                    _textHelper.GenerateText("\nPress any key to go back to your profile menu.", ConsoleColor.Green);
                                    Console.ReadKey();
                                }
                                else if (profileChoice == 3)
                                {
                                    _userService.SendMessage(user);
                                }
                                else if (profileChoice == 4)
                                {
                                    _userService.ViewMessages(user);
                                }
                                else if (profileChoice == 5)
                                {
                                    _userService.ChangePassword(user);
                                }
                                else if (profileChoice == 6)
                                {
                                    _userService.ChangeUsername(user);
                                }
                                else if (profileChoice == 7)
                                {
                                    _userService.ChangeFirstName(user);
                                }
                                else if (profileChoice == 8)
                                {
                                    _userService.ChangeLastName(user);
                                }
                                else if (profileChoice == 9)
                                {
                                    _userService.ChangeAddressInfo(user);
                                }
                                else if (profileChoice == 10)
                                {
                                    _userService.DeactivateAccount(user);
                                    break;
                                }
                                else if (profileChoice == 11)
                                {
                                    Console.Clear();
                                    _textHelper.GenerateText("Logging out. Goodbye.", ConsoleColor.Green);
                                    Thread.Sleep(2000);
                                    break;
                                }
                                else
                                {
                                    Console.Clear();
                                    _textHelper.GenerateText("Invalid input, please try again.", ConsoleColor.Red);
                                    Thread.Sleep(2000);
                                }
                            }
                        }
                    }
                    else if (menuChoice == 2)
                    {
                        _userService.Register();
                    }
                    else if (menuChoice == 3)
                    {
                        Console.Clear();
                        _textHelper.GenerateText("Goodbye!", ConsoleColor.Green);
                        Thread.Sleep(2000);
                        break;
                    }
                    else
                    {
                        Console.Clear();
                        _textHelper.GenerateText("Invalid input, please try again.", ConsoleColor.Red);
                        Thread.Sleep(2000);
                    }
                }
            }
        }
    }
}

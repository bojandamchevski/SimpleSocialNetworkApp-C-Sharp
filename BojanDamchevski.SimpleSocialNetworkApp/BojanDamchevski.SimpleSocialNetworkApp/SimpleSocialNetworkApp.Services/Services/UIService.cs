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
                            _textHelper.GenerateText($"5.) Log out", ConsoleColor.Yellow);
                            bool profileChoiceValidation = int.TryParse(Console.ReadLine(), out int profileChoice);
                            if (!profileChoiceValidation)
                            {
                                Console.Clear();
                                _textHelper.GenerateText("Invalid input, please try again.", ConsoleColor.Red);
                                Thread.Sleep(2000);
                            }
                            else
                            {
                                if (profileChoice == 5)
                                {
                                    Console.Clear();
                                    _textHelper.GenerateText("Logging out. Goodbye.", ConsoleColor.Green);
                                    Thread.Sleep(2000);
                                    break;
                                }
                                else if (profileChoice == 1)
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
                                else if(profileChoice == 3)
                                {
                                    _userService.SendMessage(user);
                                }
                                else if (profileChoice == 4)
                                {
                                    _userService.ViewMessages(user);
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

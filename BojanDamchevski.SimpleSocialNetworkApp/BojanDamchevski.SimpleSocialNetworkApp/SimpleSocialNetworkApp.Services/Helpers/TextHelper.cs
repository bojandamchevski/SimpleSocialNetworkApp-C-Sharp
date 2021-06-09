using SimpleSocialNetworkApp.Services.Interfaces;
using System;

namespace SimpleSocialNetworkApp.Services.Helpers
{
    public class TextHelper : ITextHelper
    {
        public void GenerateText(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}

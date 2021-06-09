using SimpleSocialNetworkApp.Services.Services;

namespace SimpleSocialNetworkApp.App
{
    class Program
    {
        public static UIService uiService = new UIService();
        static void Main(string[] args)
        {
            uiService.MainMenu();
        }
    }
}

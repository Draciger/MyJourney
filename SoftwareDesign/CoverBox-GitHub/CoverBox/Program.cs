using System;
using CoverBox.Business_Logic_Layer;
using CoverBox.Data_Access_Layer;
using CoverBox.UserInterface;

namespace CoverBox
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Make sure the SQLite DB + tables exist
            DatabaseInitializer.EnsureCreated();

            // Create DAL instances
            var gameLibrary = new GameLibrary();
            var userGameLibrary = new UserGameLibrary();
            var userLibrary = new UserLibrary();

            // Create BLL instances
            var gameManager = new GameManager(gameLibrary, userGameLibrary);
            var userManager = new UserManager(userLibrary, userGameLibrary, gameLibrary);

            // Start UI
            var menu = new Menu(gameManager, userManager);
            menu.Run();
    
        }
    }
}

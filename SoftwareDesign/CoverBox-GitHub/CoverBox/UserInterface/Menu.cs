using System;
using System.Linq;
using CoverBox.Business_Logic_Layer;
using CoverBox.Models;

namespace CoverBox.UserInterface
{
    public class Menu
    {
        private readonly GameManager _gameManager;
        private readonly UserManager _userManager;

        public Menu(GameManager gameManager, UserManager userManager)
        {
            _gameManager = gameManager;
            _userManager = userManager;
        }

        // Runs the menu
        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome to CoverBox! Track games you've played. Save those you want to play later!");
                Console.WriteLine("1) List all games");
                Console.WriteLine("2) Add new game");
                Console.WriteLine("3) Create user");
                Console.WriteLine("4) List users");
                Console.WriteLine("5) Show user's lists (Played / ToBePlayed)");
                Console.WriteLine("6) Update or delete user");
                Console.WriteLine("7) Add game to your ToBePlayed list");
                Console.WriteLine("8) Mark game as Played and rate");
                Console.WriteLine("9) Update or delete game");
                Console.WriteLine("0) Exit");
                Console.Write("Choice: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": ShowAllGames(); break;
                    case "2": AddGame(); break;
                    case "3": CreateUser(); break;
                    case "4": ListUsers(); break;
                    case "5": ShowUserLists(); break;
                    case "6": ManageUser(); break;
                    case "7": AddGameToList(ListStatus.ToBePlayed); break;
                    case "8": AddGameToList(ListStatus.Played, alsoRate: true); break;
                    case "9": ManageGame(); break;
                    case "0": return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        Pause();
                        break;
                }
            }
        }

        // Helpers - To improve user input handling and flow control

        private static void Pause()
        {
            Console.WriteLine();
            Console.Write("Press Enter to continue...");
            Console.ReadLine();
        }
        private static int ReadInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var txt = Console.ReadLine();
                if (int.TryParse(txt, out var value))
                    return value;

                Console.WriteLine("Please enter a valid number.");
            }
        }

        private static string ReadRequired(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var txt = Console.ReadLine() ?? "";
                if (!string.IsNullOrWhiteSpace(txt))
                    return txt.Trim();

                Console.WriteLine("This field is required.");
            }
        }

        private static string ReadOptional(string prompt, string current)
        {
            Console.Write($"{prompt} ({current}): ");
            var txt = Console.ReadLine();
            return string.IsNullOrWhiteSpace(txt) ? current : txt.Trim();
        }

        // Games - basic operations
        private void ShowAllGames()
        {
            Console.Clear();
            Console.WriteLine("** All Games **");

            var games = _gameManager.GetAllGames().ToList();
            if (!games.Any())
            {
                Console.WriteLine("No games in the database.");
                Pause();
                return;
            }

            foreach (var g in games)
            {
                Console.WriteLine(
                    $"{g.Id}: {g.Name} [{g.System}] ({g.YearReleased}) - Avg: {g.AverageRating:0.0}");
            }

            Pause();
        }

        private void AddGame()
        {
            Console.Clear();
            Console.WriteLine("** Add Game **");

            var name = ReadRequired("Name: ");
            var system = ReadRequired("System (e.g., PC, PS5, Switch): ");
            var genre = ReadRequired("Genre: ");
            var year = ReadInt("Year Released: ");

            var game = new Game
            {
                Name = name,
                System = system,
                Genre = genre,
                YearReleased = year,
                AverageRating = 0
            };

            var newId = _gameManager.AddGame(game);

            Console.WriteLine(newId > 0
                ? $"Game added with Id {newId}."
                : "Failed to add game.");

            Pause();
        }

        private void ManageGame()
        {
            Console.Clear();
            Console.WriteLine("** Update or Delete Game **");

            var games = _gameManager.GetAllGames().ToList();
            if (!games.Any())
            {
                Console.WriteLine("No games in the database.");
                Pause();
                return;
            }

            foreach (var g in games)
            {
                Console.WriteLine(
                    $"{g.Id}: {g.Name} [{g.System}] ({g.YearReleased}) - Avg: {g.AverageRating:0.0}");
            }

            var gameId = ReadInt("Game Id: ");
            var game = games.FirstOrDefault(g => g.Id == gameId);

            if (game == null)
            {
                Console.WriteLine("Game not found.");
                Pause();
                return;
            }

            Console.WriteLine();
            Console.WriteLine("1) Update this game");
            Console.WriteLine("2) Delete this game");
            Console.WriteLine("0) Cancel");
            Console.Write("Choice: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    UpdateGameInteractive(game);
                    break;
                case "2":
                    DeleteGameInteractive(game);
                    break;
                default:
                    break;
            }
        }

        private void UpdateGameInteractive(Game game)
        {
            Console.WriteLine();
            Console.WriteLine("** Update Game **");

            var name = ReadOptional("Name", game.Name);
            var system = ReadOptional("System", game.System);
            var genre = ReadOptional("Genre", game.Genre);

            int year;
            while (true)
            {
                Console.Write($"Year Released ({game.YearReleased}): ");
                var txt = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(txt))
                {
                    year = game.YearReleased;
                    break;
                }

                if (int.TryParse(txt, out year)) break;

                Console.WriteLine("Please enter a valid year.");
            }

            var updated = new Game
            {
                Id = game.Id,
                Name = name,
                System = system,
                Genre = genre,
                YearReleased = year,
                AverageRating = game.AverageRating
            };

            var ok = _gameManager.UpdateGame(updated);
            Console.WriteLine(ok ? "Game updated." : "Failed to update game.");
            Pause();
        }

        private void DeleteGameInteractive(Game game)
        {
            Console.WriteLine();
            Console.Write($"Are you sure you want to delete \"{game.Name}\"? (y/n): ");
            var ans = Console.ReadLine();
            if (!string.Equals(ans, "y", StringComparison.OrdinalIgnoreCase))
                return;

            var ok = _gameManager.DeleteGame(game.Id);
            Console.WriteLine(ok ? "Game deleted." : "Failed to delete game.");
            Pause();
        }

        // Users - basic operations

        private void CreateUser()
        {
            Console.Clear();
            Console.WriteLine("** Create User **");

            var name = ReadRequired("Name: ");
            var age = ReadInt("Age: ");
            var gender = ReadRequired("Gender: ");
            var country = ReadRequired("Country: ");

            var newId = _userManager.CreateUser(name, age, gender, country);

            Console.WriteLine(newId > 0
                ? $"User created with Id {newId}."
                : "Failed to create user.");

            Pause();
        }

        private void ListUsers()
        {
            Console.Clear();
            Console.WriteLine("** Users **");

            var users = _userManager.GetAllUsers().ToList();
            if (!users.Any())
            {
                Console.WriteLine("No users in the database.");
                Pause();
                return;
            }

            foreach (var u in users)
            {
                Console.WriteLine(
                    $"{u.Id}: {u.Name} ({u.Age}, {u.Gender}, {u.Country}) - Games played: {u.GamesPlayed}");
            }

            Pause();
        }

        private void ManageUser()
        {
            Console.Clear();
            Console.WriteLine("** Update or Delete User **");

            var users = _userManager.GetAllUsers().ToList();
            if (!users.Any())
            {
                Console.WriteLine("No users in the database.");
                Pause();
                return;
            }

            foreach (var u in users)
            {
                Console.WriteLine(
                    $"{u.Id}: {u.Name} ({u.Age}, {u.Gender}, {u.Country}) - Games played: {u.GamesPlayed}");
            }

            var userId = ReadInt("User Id: ");
            var user = users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                Console.WriteLine("User not found.");
                Pause();
                return;
            }

            Console.WriteLine();
            Console.WriteLine("1) Update this user");
            Console.WriteLine("2) Delete this user");
            Console.WriteLine("0) Cancel");
            Console.Write("Choice: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    UpdateUserInteractive(user);
                    break;
                case "2":
                    DeleteUserInteractive(user);
                    break;
                default:
                    break;
            }
        }

        private void UpdateUserInteractive(User user)
        {
            Console.WriteLine();
            Console.WriteLine("** Update User **");

            var name = ReadOptional("Name", user.Name);
            var gender = ReadOptional("Gender", user.Gender);
            var country = ReadOptional("Country", user.Country);

            int age;
            while (true)
            {
                Console.Write($"Age ({user.Age}): ");
                var txt = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(txt))
                {
                    age = user.Age;
                    break;
                }

                if (int.TryParse(txt, out age)) break;

                Console.WriteLine("Please enter a valid age.");
            }

            var ok = _userManager.UpdateUser(user.Id, name, age, gender, country);
            Console.WriteLine(ok ? "User updated." : "Failed to update user.");
            Pause();
        }

        private void DeleteUserInteractive(User user)
        {
            Console.WriteLine();
            Console.Write($"Are you sure you want to delete user \"{user.Name}\"? (y/n): ");
            var ans = Console.ReadLine();
            if (!string.Equals(ans, "y", StringComparison.OrdinalIgnoreCase))
                return;

            var ok = _userManager.DeleteUser(user.Id);
            Console.WriteLine(ok ? "User deleted." : "Failed to delete user.");
            Pause();
        }

        private void ShowUserLists()
        {
            Console.Clear();
            Console.WriteLine("** Show User Lists **");

            var users = _userManager.GetAllUsers().ToList();
            if (!users.Any())
            {
                Console.WriteLine("No users in the database.");
                Pause();
                return;
            }

            foreach (var u in users)
            {
                Console.WriteLine(
                    $"{u.Id}: {u.Name} ({u.Age}, {u.Gender}, {u.Country}) - Games played: {u.GamesPlayed}");
            }

            var userId = ReadInt("User Id: ");

            var (user, played, toPlay) = _userManager.GetUserLists(userId);
            if (user == null)
            {
                Console.WriteLine("User not found.");
                Pause();
                return;
            }

            Console.WriteLine();
            Console.WriteLine($"User: {user.Name}");
            Console.WriteLine($"Games played: {user.GamesPlayed}");
            Console.WriteLine();

            Console.WriteLine("Played games:");
            var playedList = played.ToList();
            if (!playedList.Any())
                Console.WriteLine("  (none)");
            else
                foreach (var g in playedList)
                    Console.WriteLine($"  {g.Id}: {g.Name} [{g.System}] ({g.YearReleased})");

            Console.WriteLine();
            Console.WriteLine("ToBePlayed list:");
            var toPlayList = toPlay.ToList();
            if (!toPlayList.Any())
                Console.WriteLine("  (none)");
            else
                foreach (var g in toPlayList)
                    Console.WriteLine($"  {g.Id}: {g.Name} [{g.System}] ({g.YearReleased})");

            Pause();
        }

        // User + Game actions (lists and rating)

        private void AddGameToList(ListStatus status, bool alsoRate = false)
        {
            Console.Clear();
            Console.WriteLine(status == ListStatus.ToBePlayed
                ? "** Add Game To User's ToBePlayed List **"
                : "** Mark Game as Played (and optionally rate) **");

            var users = _userManager.GetAllUsers().ToList();
            if (!users.Any())
            {
                Console.WriteLine("No users in the database.");
                Pause();
                return;
            }

            Console.WriteLine("** Users **");
            foreach (var u in users)
                Console.WriteLine($"{u.Id}: {u.Name} ({u.Age}, {u.Gender}, {u.Country})");

            var userId = ReadInt("User Id: ");

            var games = _gameManager.GetAllGames().ToList();
            if (!games.Any())
            {
                Console.WriteLine("No games in the database.");
                Pause();
                return;
            }

            Console.WriteLine();
            Console.WriteLine("** Games **");
            foreach (var g in games)
                Console.WriteLine($"{g.Id}: {g.Name} [{g.System}] ({g.YearReleased})");

            var gameId = ReadInt("Game Id: ");

            _userManager.AddGameToList(userId, gameId, status);

            if (alsoRate && status == ListStatus.Played)
            {
                var rating = ReadInt("Rating (1–10): ");
                _gameManager.RateGameForUser(userId, gameId, rating);
            }

            Console.WriteLine("Done.");
            Pause();
        }
    }
}

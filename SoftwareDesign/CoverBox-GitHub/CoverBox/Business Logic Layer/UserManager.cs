using System;
using System.Collections.Generic;
using System.Linq;
using CoverBox.Data_Access_Layer;
using CoverBox.Models;

namespace CoverBox.Business_Logic_Layer
{
    public class UserManager
    {
        private readonly UserLibrary _users;
        private readonly UserGameLibrary _userGames;
        private readonly GameLibrary _games;

        public UserManager(UserLibrary users, UserGameLibrary userGames, GameLibrary games)
        {
            _users = users;
            _userGames = userGames;
            _games = games;
        }

        // Basic User operations

        public IEnumerable<User> GetAllUsers()
        {
            return _users.GetAll();
        }

        public int CreateUser(string name, int age, string gender, string country)
        {
            var user = new User
            {
                Name = name,
                Age = age,
                Gender = gender,
                Country = country,
                GamesPlayed = 0
            };

            return _users.Add(user);
        }

        /// <summary>
        /// Update a user’s basic info.
        /// </summary>
        public bool UpdateUser(int id, string name, int age, string gender, string country)
        {
            var user = _users.GetById(id);
            if (user == null) return false;

            user.Name = name;
            user.Age = age;
            user.Gender = gender;
            user.Country = country;

            return _users.Update(user);
        }

        /// <summary>
        /// Delete a user
        /// </summary>
        public bool DeleteUser(int id)
        {
            return _users.Delete(id);
        }

        // User <-> Game list handling

        /// <summary>
        /// Add or move a game to the specified list (Played / ToBePlayed).
        /// </summary>
        public void AddGameToList(int userId, int gameId, ListStatus status)
        {
            _userGames.AddOrUpdateStatus(userId, gameId, status);

            // If Played, recompute GamesPlayed for the user
            if (status == ListStatus.Played)
            {
                var allLinks = _userGames.GetForUser(userId);
                int playedCount = allLinks.Count(l => l.Status == ListStatus.Played);
                _users.UpdateGamesPlayed(userId, playedCount);
            }
        }

        /// <summary>
        /// Show basic stats for a user (used in the menu).
        /// </summary>
        public (User? user, IEnumerable<Game> played, IEnumerable<Game> toPlay) GetUserLists(int userId)
        {
            var user = _users.GetById(userId);
            if (user == null)
                return (null, Enumerable.Empty<Game>(), Enumerable.Empty<Game>());

            var links = _userGames.GetForUser(userId).ToList();

            var playedIds = links.Where(l => l.Status == ListStatus.Played).Select(l => l.GameId).ToHashSet();
            var toPlayIds = links.Where(l => l.Status == ListStatus.ToBePlayed).Select(l => l.GameId).ToHashSet();

            var allGames = _games.GetAll().ToList();

            var played = allGames.Where(g => playedIds.Contains(g.Id));
            var toPlay = allGames.Where(g => toPlayIds.Contains(g.Id));

            return (user, played, toPlay);
        }
    }
}

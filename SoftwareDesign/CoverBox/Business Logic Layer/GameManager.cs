using System;
using System.Collections.Generic;
using System.Linq;
using CoverBox.Data_Access_Layer;
using CoverBox.Models;

namespace CoverBox.Business_Logic_Layer
{
    public class GameManager
    {
        private readonly GameLibrary _games;
        private readonly UserGameLibrary _userGames;

        public GameManager(GameLibrary games, UserGameLibrary userGames)
        {
            _games = games;
            _userGames = userGames;
        }

        // Basic Game CRUD
        public IEnumerable<Game> GetAllGames()
        {
            return _games.GetAll();
        }

        public int AddGame(Game g)
        {
            return _games.Add(g);
        }

        public bool UpdateGame(Game g)
        {
            return _games.Update(g);
        }

        public bool DeleteGame(int gameId)
        {
            return _games.Delete(gameId);
        }

        // Rating logic - mark Played + update average rating

        /// <summary>
        /// Mark a game as Played for a user and store their rating.
        /// Also calculates the game's AverageRating.
        /// </summary>
        public void RateGameForUser(int userId, int gameId, int rating)
        {
            if (rating < 1 || rating > 10)
                throw new ArgumentOutOfRangeException(nameof(rating), "Rating must be between 1 and 10.");

            // Update the status to Played
            _userGames.AddOrUpdateStatus(userId, gameId, ListStatus.Played);

            // Store rating
            _userGames.Rate(userId, gameId, rating);

            // Get all valid ratings for the game
            var ratings = _userGames.GetRatingsForGame(gameId)
                                    .Where(ug => ug.UserRating.HasValue)
                                    .Select(ug => ug.UserRating!.Value)
                                    .ToList();

            double avg = ratings.Count > 0 ? ratings.Average() : 0.0;

            _games.UpdateAverageRating(gameId, avg);
        }
    }
}

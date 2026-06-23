using System;

namespace CoverBox.Models
{
    public enum ListStatus
    {
        ToBePlayed = 0,
        Played = 1
    }

    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string System { get; set; } = "";
        public string Genre { get; set; } = "";
        public int YearReleased { get; set; }
        public double AverageRating { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int Age { get; set; }
        public string Gender { get; set; } = "";
        public string Country { get; set; } = "";
        public int GamesPlayed { get; set; }
    }

    public class UserGame
    {
        public int UserId { get; set; }
        public int GameId { get; set; }
        public ListStatus Status { get; set; }
        public int? UserRating { get; set; }
    }
}

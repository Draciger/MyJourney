using System;
using System.IO;
using Microsoft.Data.Sqlite;

namespace CoverBox.Data_Access_Layer
{
    public static class DatabaseInitializer
    {
        public static string GetConnectionString()
        {
            // Make sure /Database folder exists in the output directory
            var dbFolder = Path.Combine(AppContext.BaseDirectory, "Database");
            Directory.CreateDirectory(dbFolder);

            var dbPath = Path.Combine(dbFolder, "CoverBoxDatabase.db");
            return $"Data Source={dbPath}";
        }

        /// <summary>
        /// Creates the database file and all tables if they do not already exist.
        /// </summary>
        public static void EnsureCreated()
        {
            using var con = new SqliteConnection(GetConnectionString());
            con.Open();

            var sql = @"
            CREATE TABLE IF NOT EXISTS Game (
                Id            INTEGER PRIMARY KEY AUTOINCREMENT,
                Name          TEXT    NOT NULL,
                ""System""     TEXT    NOT NULL,
                Genre         TEXT    NOT NULL,
                YearReleased  INTEGER NOT NULL,
                AverageRating REAL    NOT NULL DEFAULT 0
            );

            CREATE TABLE IF NOT EXISTS ""User"" (
                Id           INTEGER PRIMARY KEY AUTOINCREMENT,
                Name         TEXT    NOT NULL,
                Age          INTEGER NOT NULL,
                Gender       TEXT    NOT NULL,
                Country      TEXT    NOT NULL,
                GamesPlayed  INTEGER NOT NULL DEFAULT 0
            );

            CREATE TABLE IF NOT EXISTS UserGameLink (
                Id        INTEGER PRIMARY KEY AUTOINCREMENT,
                UserId    INTEGER NOT NULL,
                GameId    INTEGER NOT NULL,
                Status    INTEGER NOT NULL,   -- e.g. 0 = ToBePlayed, 1 = Played
                UserRating REAL NOT NULL DEFAULT 0,
                FOREIGN KEY (UserId) REFERENCES ""User""(Id) ON DELETE CASCADE,
                FOREIGN KEY (GameId) REFERENCES Game(Id)   ON DELETE CASCADE
            );
            ";

            using var cmd = new SqliteCommand(sql, con);
            cmd.ExecuteNonQuery();
        }
    }
}

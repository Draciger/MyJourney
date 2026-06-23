using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using CoverBox.Models;

namespace CoverBox.Data_Access_Layer
{
    public class GameLibrary
    {
        private string ConnectionString => DatabaseInitializer.GetConnectionString();


        /// <summary>
        /// Retrieves all game records from the database.
        /// </summary>
        /// <remarks>
        /// Executes a SELECT query on the Game table and maps each row to a Game object.
        /// </remarks>
        /// <returns>
        /// A list of Game objects containing Id, Name, System, Genre, YearReleased, and AverageRating.
        /// </returns>
        public IEnumerable<Game> GetAll()
        {
            var list = new List<Game>();

            using SqliteConnection? con = new SqliteConnection(ConnectionString);
            con.Open();

            var sql = @"SELECT Id, Name, ""System"", Genre, YearReleased, AverageRating 
                        FROM Game;";
            using var cmd = new SqliteCommand(sql, con);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Game
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    System = reader.GetString(2),
                    Genre = reader.GetString(3),
                    YearReleased = reader.GetInt32(4),
                    AverageRating = reader.GetDouble(5)
                });
            }

            return list;
        }


        // Retrieves a single game record from the database by its unique ID.
        public Game? GetById(int id)
        {
            using var con = new SqliteConnection(ConnectionString);
            con.Open();

            var sql = @"SELECT Id, Name, ""System"", Genre, YearReleased, AverageRating 
                        FROM Game
                        WHERE Id = @Id;";
            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@Id", id);

            using var reader = cmd.ExecuteReader();
            if (!reader.Read()) return null;

            return new Game
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                System = reader.GetString(2),
                Genre = reader.GetString(3),
                YearReleased = reader.GetInt32(4),
                AverageRating = reader.GetDouble(5)
            };
        }

        // Create a game
        public int Add(Game g)
        {
            using var con = new SqliteConnection(ConnectionString);
            con.Open();

            var sql = @"
            INSERT INTO Game (Name, ""System"", Genre, YearReleased, AverageRating)
            VALUES (@Name, @System, @Genre, @YearReleased, @AverageRating);
            SELECT last_insert_rowid();";

            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@Name", g.Name);
            cmd.Parameters.AddWithValue("@System", g.System);
            cmd.Parameters.AddWithValue("@Genre", g.Genre);
            cmd.Parameters.AddWithValue("@YearReleased", g.YearReleased);
            cmd.Parameters.AddWithValue("@AverageRating", g.AverageRating);

            var result = cmd.ExecuteScalar();
            return Convert.ToInt32(result);
        }

        // Updating a game
        public bool Update(Game g)
        {
            using var con = new SqliteConnection(ConnectionString);
            con.Open();

            var sql = @"
            UPDATE Game
            SET Name = @Name,
                ""System"" = @System,
                Genre = @Genre,
                YearReleased = @YearReleased,
                AverageRating = @AverageRating
            WHERE Id = @Id;";

            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@Name", g.Name);
            cmd.Parameters.AddWithValue("@System", g.System);
            cmd.Parameters.AddWithValue("@Genre", g.Genre);
            cmd.Parameters.AddWithValue("@YearReleased", g.YearReleased);
            cmd.Parameters.AddWithValue("@AverageRating", g.AverageRating);
            cmd.Parameters.AddWithValue("@Id", g.Id);

            return cmd.ExecuteNonQuery() == 1;
        }

        // Delete a game
        public bool Delete(int gameId)
        {
            using var con = new SqliteConnection(ConnectionString);
            con.Open();

            var sql = @"DELETE FROM Game WHERE Id = @Id;";
            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@Id", gameId);

            return cmd.ExecuteNonQuery() == 1;
        }

        // Update the average rating
        public bool UpdateAverageRating(int gameId, double avg)
        {
            using var con = new SqliteConnection(ConnectionString);
            con.Open();

            var sql = @"UPDATE Game SET AverageRating = @AverageRating WHERE Id = @Id;";
            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@AverageRating", avg);
            cmd.Parameters.AddWithValue("@Id", gameId);

            return cmd.ExecuteNonQuery() == 1;
        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using CoverBox.Models;

namespace CoverBox.Data_Access_Layer
{
    public class UserGameLibrary
    {
        private string ConnectionString => DatabaseInitializer.GetConnectionString();

        private static UserGame ReadUserGame(SqliteDataReader reader)
        {
            return new UserGame
            {
                UserId = reader.GetInt32(0),
                GameId = reader.GetInt32(1),
                Status = (ListStatus)reader.GetInt32(2),
                UserRating = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3)
            };
        }

        // All links for a user
        public IEnumerable<UserGame> GetForUser(int userId)
        {
            var list = new List<UserGame>();

            using var con = new SqliteConnection(ConnectionString);
            con.Open();

            var sql = @"SELECT UserId, GameId, Status, UserRating
                        FROM UserGame
                        WHERE UserId = @UserId;";
            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@UserId", userId);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(ReadUserGame(reader));
            }

            return list;
        }

        // Links for a user with a specific status (ToBePlayed / Played)
        public IEnumerable<UserGame> GetForUserAndStatus(int userId, ListStatus status)
        {
            var list = new List<UserGame>();

            using var con = new SqliteConnection(ConnectionString);
            con.Open();

            var sql = @"SELECT UserId, GameId, Status, UserRating
                        FROM UserGame
                        WHERE UserId = @UserId AND Status = @Status;";
            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@Status", (int)status);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(ReadUserGame(reader));
            }

            return list;
        }

        // All ratings for a given game (used for average)
        public IEnumerable<UserGame> GetRatingsForGame(int gameId)
        {
            var list = new List<UserGame>();

            using var con = new SqliteConnection(ConnectionString);
            con.Open();

            var sql = @"SELECT UserId, GameId, Status, UserRating
                        FROM UserGame
                        WHERE GameId = @GameId;";
            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@GameId", gameId);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(ReadUserGame(reader));
            }

            return list;
        }

        // Insert or update a link with a given status (no rating change)
        public void AddOrUpdateStatus(int userId, int gameId, ListStatus status)
        {
            using var con = new SqliteConnection(ConnectionString);
            con.Open();

            var sqlUpdate = @"UPDATE UserGame
                              SET Status = @Status
                              WHERE UserId = @UserId AND GameId = @GameId;";
            using (var update = new SqliteCommand(sqlUpdate, con))
            {
                update.Parameters.AddWithValue("@Status", (int)status);
                update.Parameters.AddWithValue("@UserId", userId);
                update.Parameters.AddWithValue("@GameId", gameId);

                var rows = update.ExecuteNonQuery();
                if (rows > 0) return;
            }

            // If nothing updated -> insert
            var sqlInsert = @"INSERT INTO UserGame (UserId, GameId, Status, UserRating)
                              VALUES (@UserId, @GameId, @Status, NULL);";
            using var insert = new SqliteCommand(sqlInsert, con);
            insert.Parameters.AddWithValue("@UserId", userId);
            insert.Parameters.AddWithValue("@GameId", gameId);
            insert.Parameters.AddWithValue("@Status", (int)status);
            insert.ExecuteNonQuery();
        }

        // Set rating for a user–game link
        public void Rate(int userId, int gameId, int rating)
        {
            using var con = new SqliteConnection(ConnectionString);
            con.Open();

            var sql = @"UPDATE UserGame
                        SET UserRating = @Rating
                        WHERE UserId = @UserId AND GameId = @GameId;";
            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@Rating", rating);
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@GameId", gameId);

            var rows = cmd.ExecuteNonQuery();
            if (rows == 0)
            {
                var insert = @"INSERT INTO UserGame (UserId, GameId, Status, UserRating)
                               VALUES (@UserId, @GameId, @Status, @Rating);";
                using var cmd2 = new SqliteCommand(insert, con);
                cmd2.Parameters.AddWithValue("@UserId", userId);
                cmd2.Parameters.AddWithValue("@GameId", gameId);
                cmd2.Parameters.AddWithValue("@Status", (int)ListStatus.Played);
                cmd2.Parameters.AddWithValue("@Rating", rating);
                cmd2.ExecuteNonQuery();
            }
        }
    }
}

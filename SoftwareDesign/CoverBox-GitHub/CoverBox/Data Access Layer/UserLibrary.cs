using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using CoverBox.Models;

namespace CoverBox.Data_Access_Layer
{
    public class UserLibrary
    {
        private string ConnectionString => DatabaseInitializer.GetConnectionString();

        // Fetches all the users stored in the DB
        public IEnumerable<User> GetAll()
        {
            var list = new List<User>();

            using var con = new SqliteConnection(ConnectionString);
            con.Open();

            var sql = @"SELECT Id, Name, Age, Gender, Country, GamesPlayed FROM ""User"";";
            using var cmd = new SqliteCommand(sql, con);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new User
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Age = reader.GetInt32(2),
                    Gender = reader.GetString(3),
                    Country = reader.GetString(4),
                    GamesPlayed = reader.GetInt32(5)
                });
            }

            return list;
        }

        // Gets a single user by its ID
        public User? GetById(int id)
        {
            using var con = new SqliteConnection(ConnectionString);
            con.Open();

            var sql = @"SELECT Id, Name, Age, Gender, Country, GamesPlayed 
                        FROM ""User"" WHERE Id = @Id;";
            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@Id", id);

            using var reader = cmd.ExecuteReader();
            if (!reader.Read()) return null;

            return new User
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Age = reader.GetInt32(2),
                Gender = reader.GetString(3),
                Country = reader.GetString(4),
                GamesPlayed = reader.GetInt32(5)
            };
        }
        
        // Basic operations adds, updates or deletes users
        public int Add(User u)
        {
            using var con = new SqliteConnection(ConnectionString);
            con.Open();

            var sql = @"
            INSERT INTO ""User"" (Name, Age, Gender, Country, GamesPlayed)
            VALUES (@Name, @Age, @Gender, @Country, @GamesPlayed);
            SELECT last_insert_rowid();";

            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@Name", u.Name);
            cmd.Parameters.AddWithValue("@Age", u.Age);
            cmd.Parameters.AddWithValue("@Gender", u.Gender);
            cmd.Parameters.AddWithValue("@Country", u.Country);
            cmd.Parameters.AddWithValue("@GamesPlayed", u.GamesPlayed);

            var result = cmd.ExecuteScalar();
            return Convert.ToInt32(result);
        }

        public bool Update(User u)
        {
            using var con = new SqliteConnection(ConnectionString);
            con.Open();

            var sql = @"
            UPDATE ""User""
            SET Name = @Name,
                Age = @Age,
                Gender = @Gender,
                Country = @Country,
                GamesPlayed = @GamesPlayed
            WHERE Id = @Id;";

            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@Name", u.Name);
            cmd.Parameters.AddWithValue("@Age", u.Age);
            cmd.Parameters.AddWithValue("@Gender", u.Gender);
            cmd.Parameters.AddWithValue("@Country", u.Country);
            cmd.Parameters.AddWithValue("@GamesPlayed", u.GamesPlayed);
            cmd.Parameters.AddWithValue("@Id", u.Id);

            return cmd.ExecuteNonQuery() == 1;
        }

        public bool Delete(int userId)
        {
            using var con = new SqliteConnection(ConnectionString);
            con.Open();

            var sql = @"DELETE FROM ""User"" WHERE Id = @Id;";
            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@Id", userId);

            return cmd.ExecuteNonQuery() == 1;
        }

        public bool UpdateGamesPlayed(int userId, int newValue)
        {
            using var con = new SqliteConnection(ConnectionString);
            con.Open();

            var sql = @"UPDATE ""User"" SET GamesPlayed = @GamesPlayed WHERE Id = @Id;";
            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@GamesPlayed", newValue);
            cmd.Parameters.AddWithValue("@Id", userId);

            return cmd.ExecuteNonQuery() == 1;
        }
    }
}

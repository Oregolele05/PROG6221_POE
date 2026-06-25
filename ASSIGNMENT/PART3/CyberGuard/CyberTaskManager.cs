using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace CyberGuard
{
    public class CyberTaskManager
    {
        // Update password if you set one during MySQL installation
        private const string ConnectionString = "Server=localhost;Database=cyberguard_db;Uid=root;Pwd=*Om270805@";

        public void Initialise()
        {
            try
            {
                // Use backticks (`) or no quotes for database name – never single quotes.
                string createDb = "CREATE DATABASE IF NOT EXISTS cyberguard_db;";
                using (var conn = new MySqlConnection("Server=localhost;Uid=root;Pwd=*Om270805@"))
                {
                    conn.Open();
                    new MySqlCommand(createDb, conn).ExecuteNonQuery();
                }

                string createTable = @"
                    CREATE TABLE IF NOT EXISTS tasks (
                        id           INT AUTO_INCREMENT PRIMARY KEY,
                        title        VARCHAR(255)  NOT NULL,
                        description  TEXT,
                        reminder_date DATE,
                        is_completed  TINYINT(1)   DEFAULT 0,
                        created_at   DATETIME      DEFAULT CURRENT_TIMESTAMP
                    );";

                using (var conn = new MySqlConnection(ConnectionString))
                {
                    conn.Open();
                    new MySqlCommand(createTable, conn).ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Database initialisation failed: " + ex.Message);
            }
        }

        public int AddTask(string title, string description, DateTime? reminderDate = null)
        {
            string sql = @"INSERT INTO tasks (title, description, reminder_date)
                           VALUES (@title, @desc, @reminder);
                           SELECT LAST_INSERT_ID();";
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@desc", description ?? "");
                cmd.Parameters.AddWithValue("@reminder", reminderDate.HasValue ? (object)reminderDate.Value : DBNull.Value);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public List<CyberTask> GetAllTasks()
        {
            var tasks = new List<CyberTask>();
            string sql = "SELECT id, title, description, reminder_date, is_completed, created_at FROM tasks ORDER BY id;";
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                using (var reader = new MySqlCommand(sql, conn).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tasks.Add(new CyberTask
                        {
                            Id = reader.GetInt32("id"),
                            Title = reader.GetString("title"),
                            Description = reader.IsDBNull(reader.GetOrdinal("description")) ? "" : reader.GetString("description"),
                            ReminderDate = reader.IsDBNull(reader.GetOrdinal("reminder_date")) ? (DateTime?)null : reader.GetDateTime("reminder_date"),
                            IsCompleted = reader.GetBoolean("is_completed"),
                            CreatedAt = reader.GetDateTime("created_at")
                        });
                    }
                }
            }
            return tasks;
        }

        public bool CompleteTask(int id)
        {
            string sql = "UPDATE tasks SET is_completed = 1 WHERE id = @id;";
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool DeleteTask(int id)
        {
            string sql = "DELETE FROM tasks WHERE id = @id;";
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool SetReminder(int id, DateTime reminderDate)
        {
            string sql = "UPDATE tasks SET reminder_date = @date WHERE id = @id;";
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@date", reminderDate);
                cmd.Parameters.AddWithValue("@id", id);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
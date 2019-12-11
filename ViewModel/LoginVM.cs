using System.Data.SQLite;
using System;

namespace MyNotes
{
    public class LoginVM
    {
        string dbConnectionString = @"Data Source=..\..\Data\mynotesDB.db";

        public LoginVM() { }
        /// <summary>
        /// Realization of logging process,connection with data base
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="Password"></param>
        public void Action1(string Email, string Password)
        {

            SQLiteConnection sqliteCon = new SQLiteConnection(dbConnectionString);
            try
            {
                sqliteCon.Open();
                string Query = "select * from Users where Email='" + Email + "' and Password='" + Password + "' ";
                SQLiteCommand createCommand = new SQLiteCommand(Query, sqliteCon);

                createCommand.ExecuteNonQuery();
                SQLiteDataReader dr = createCommand.ExecuteReader();
                int id = 0;
                int count = 0;
                while (dr.Read())
                {
                    count++;
                    id = dr.GetInt32(0);
                }
                if (count == 1)
                {
                    App.currentUser = new User { UserId = id };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
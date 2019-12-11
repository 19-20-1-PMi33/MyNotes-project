using System.Data.SQLite;
using System.ComponentModel;
using System;
using System.Runtime.CompilerServices;
using MyNotes.Binding;
namespace MyNotes
{
    class UserPageVM
    {

        string dbConnectionString = @"Data Source=..\..\Data\mynotesDB.db";
        public bool exist { get; set; }
        /// <summary>
        /// Updating User-name in database
        /// </summary>
        /// <param name="UserName"></param>
        public void Action1(string UserName)
        {            
            SQLiteConnection sqliteCon = new SQLiteConnection(dbConnectionString);
            //var dbConnection = db.Database.Connection as SQLiteConnection;
            try
            {
                sqliteCon.Open();
                string Query = "update Users set Username='" + UserName + "' where UserId='" + App.currentUser.UserId + "'";
                SQLiteCommand createCommand = new SQLiteCommand(Query, sqliteCon);
                createCommand.ExecuteNonQuery();                                        
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
        /// <summary>
        /// Updating User-password in database
        /// </summary>
        /// <param name="Password"></param>
        public void Action2( string Password)
        {
            SQLiteConnection sqliteCon = new SQLiteConnection(dbConnectionString);
            //var dbConnection = db.Database.Connection as SQLiteConnection;
            try
            {
                sqliteCon.Open();
                string Query = "update Users set Password='" + Password + "' where UserId='" + App.currentUser.UserId + "'";
                SQLiteCommand createCommand = new SQLiteCommand(Query, sqliteCon);
                createCommand.ExecuteNonQuery();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
        /// <summary>
        /// Updating User-email in data base
        /// </summary>
        /// <param name="Email"></param>
        public void Action3(string Email)
        {
            SQLiteConnection sqliteCon = new SQLiteConnection(dbConnectionString);
            //var dbConnection = db.Database.Connection as SQLiteConnection;
            try
            {
                sqliteCon.Open();
                string Query3 = "select * from Users where Email='" + Email + "' ";
                SQLiteCommand createCommand3 = new SQLiteCommand(Query3, sqliteCon);
                createCommand3.ExecuteNonQuery();
                SQLiteDataReader dr = createCommand3.ExecuteReader();
                int count = 0;
                while (dr.Read())
                {
                    count++;
                }
                if (count != 1)
                {
                    exist = true;
                    sqliteCon.Open();
                    string Query = "update Users set Email='" + Email + "' where UserId='" + App.currentUser.UserId + "'";
                    SQLiteCommand createCommand = new SQLiteCommand(Query, sqliteCon);
                    createCommand.ExecuteNonQuery();
                }
                else
                {
                    exist = false;
                }

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
    }
}

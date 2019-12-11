using System.Data.SQLite;
using System.ComponentModel;
using System;
using System.Runtime.CompilerServices;
using MyNotes.Binding;
namespace MyNotes
{
    public class RegisterVM : INotifyPropertyChanged
    {
        public RegisterVM() { }

        string dbConnectionString = @"Data Source=..\..\Data\mynotesDB.db";
        /// <summary>
        /// Realization of registering functionality and importing user-information to data base
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Email"></param>
        /// <param name="Password"></param>
        
        public void Action1(string UserName,string Email,string Password)
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
                if(count!=1)
                {

                    string Query = "insert into Users(Username,Email,Password) values('" + UserName + "','" + Email + "','" + Password + "')";
                    SQLiteCommand createCommand = new SQLiteCommand(Query, sqliteCon);
                    createCommand.ExecuteNonQuery();

                    string Query2 = "select * from Users where Email='" + Email + "' and Password='" + Password + "' ";
                    SQLiteCommand createCommand2 = new SQLiteCommand(Query2, sqliteCon);
                    createCommand2.ExecuteNonQuery();
                    SQLiteDataReader dr2 = createCommand2.ExecuteReader();
                    int id = 0;
                    int count2 = 0;
                    while (dr2.Read())
                    {
                        count2++;
                        id = dr2.GetInt32(0);
                    }
                    if (count2 == 1)
                    {
                        App.currentUser = new User { UserId = id };
                    }
                }
                else { App.currentUser = null; }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

    }
}
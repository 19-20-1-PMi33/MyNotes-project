using System.Data.SQLite;
using System.ComponentModel;
using System;
using System.Runtime.CompilerServices;
using MyNotes.Binding;
namespace MyNotes
{
    
    public class LoginVM:INotifyPropertyChanged
    {
        //public string Password { private get; set; }
        
        string dbConnectionString = @"Data Source=..\..\Data\mynotesDB.db";

        public LoginVM() {  }

        public void Action1(string Email,string Password) {

            SQLiteConnection sqliteCon = new SQLiteConnection(dbConnectionString);
            // var dbConnection = db.Database.Connection as SQLiteConnection;            
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
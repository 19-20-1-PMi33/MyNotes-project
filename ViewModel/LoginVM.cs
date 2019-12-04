using System.Data.SQLite;
using System.ComponentModel;
using System;
using System.Runtime.CompilerServices;
using MyNotes.Binding;
namespace MyNotes
{

    public class LoginVM : INotifyPropertyChanged
    {
        public LoginButton ButtonClick { get; set; }
        public string Text { get; set; }

        AppDbContext db;

        private string email;
        public string Email
        {
            get { return this.email; }
            set
            {
                if (!string.Equals(this.email, value))
                {
                    this.email = value;
                }
            }
        }
        private string password;
        public string Password
        {
            get { return this.password; }
            set
            {
                if (!string.Equals(this.password, value))
                {
                    this.password = value;
                }
            }
        }


        //string dbConnectionString = @"Data Source=D:\Users\Notes\MyNotes-project\Data\mynotesDB.db";

        public LoginVM()
        {
            ButtonClick = new LoginButton(Action1);
            ButtonClick.IsEnabled = true;
            db = new AppDbContext();

        }


        private void Action1()
        {

            //SQLiteConnection sqliteCon = new SQLiteConnection(dbConnectionString);
            var dbConnection = db.Database.Connection as SQLiteConnection;
            try
            {
                dbConnection.Open();
                string Query = "select * from Users where Email='" + Email.ToString() + "' and Password='" + Password.ToString() + "' ";
                SQLiteCommand createCommand = new SQLiteCommand(Query, dbConnection);

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
                if (count > 1)
                {
                    //DB unique component modify
                }
                if (count < 1)
                {
                    Text = "Please sign up!";
                    NotifyPropertyChanged("Text");
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
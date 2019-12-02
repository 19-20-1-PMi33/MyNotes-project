using System.Data.SQLite;
using System.ComponentModel;
using System;
using System.Runtime.CompilerServices;
using MyNotes.Binding;
namespace MyNotes
{
    
    public class User1:INotifyPropertyChanged
    {
        private string _email;
        private string _password;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged("Title");
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Title");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
    public class LoginVM
    {
        public LoginButton ButtonClick { get; set; }
        User1 us1 = new User1();
        string dbConnectionString = @"Data Source=D:\Users\Notes\MyNotes-project\Data\mynotesDB.db";
        
        public LoginVM()
        {
            ButtonClick = new LoginButton(Action1);
            ButtonClick.IsEnabled = true;
           
        }

        private void Action1() {
            SQLiteConnection sqliteCon = new SQLiteConnection(dbConnectionString);
            try
            {
                sqliteCon.Open();
                string Query = "select * from Users where Email='1@ukr'and Password='1111' ";
                SQLiteCommand createCommand = new SQLiteCommand(Query, sqliteCon);

                createCommand.ExecuteNonQuery();
                SQLiteDataReader dr = createCommand.ExecuteReader();

                int count = 0;
                while (dr.Read())
                {
                    count++;
                }
                if (count == 1)
                {
                    
                    
                    //login.NavigationService.Navigate(new Uri("View/HomePage.xaml", UriKind.Relative));
                }
                if (count > 1)
                {
                    Console.WriteLine("Duplicate Email and password,try again!");
                }
                if (count < 1)
                {
                    ButtonClick.IsEnabled = false;
                    Console.WriteLine("Email and password is not correct!");
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
    }
}
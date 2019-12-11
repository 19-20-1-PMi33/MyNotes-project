using System;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace MyNotes
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            InitializeComponent();
            this.DataContext = new RegisterVM();
        }
        /// <summary>
        /// Checking email validation
        /// </summary>
        public static class ValidatorExtensions
        {
            public static bool IsValidEmailAddress(string s)
            {
                Regex regex = new Regex(@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$");
                return regex.IsMatch(s);
            }
        }
        /// <summary>
        /// Realization of registering process
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterVM register = new RegisterVM();
            bool result = ValidatorExtensions.IsValidEmailAddress(txt2.Text);
            if (txt1.Text == null || txt2.Text == null ||
                pass2.Password.ToString() == null || string.IsNullOrWhiteSpace(txt1.Text) ||
                string.IsNullOrWhiteSpace(txt2.Text) || string.IsNullOrWhiteSpace(pass2.Password.ToString()))
            {
                MessageBox.Show("All rows must be fill in!");
            }
            else
            {
                if (result == false) { MessageBox.Show("Incorrect Email validation!"); }
                else
                {
                    register.Action1(txt1.Text, txt2.Text, pass2.Password.ToString());
                    if (App.currentUser != null)
                    {
                        this.NavigationService.Navigate(new Uri("View/LoadingPage.xaml", UriKind.Relative));
                    }
                    else
                    {
                        MessageBox.Show("Account already exists!");
                    }
                }
            }
        }
    }
}

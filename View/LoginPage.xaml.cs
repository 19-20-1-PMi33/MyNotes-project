using System;
using System.Collections.Generic;
using System.Xaml;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;
namespace MyNotes
{

    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
            this.DataContext = new LoginVM();
            
        }



        void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            
            LoginVM login = new LoginVM();
            login.Action1(txt_box1.Text, pass.Password.ToString());
            if (txt_box1.Text == null || pass.Password.ToString() == null ||
                 string.IsNullOrWhiteSpace(txt_box1.Text) ||
                string.IsNullOrWhiteSpace(pass.Password.ToString()))
            {
                MessageBox.Show("All rows must be fill in!");
            }
            else
            {
                if (App.currentUser != null)
                {
                    this.NavigationService.Navigate(new Uri("View/LoadingPage.xaml", UriKind.Relative));
                }
                else
                {
                    MessageBox.Show("Account doesn`t exist!");
                }
            }
            // if (signed in)
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)this.DataContext).Password = ((PasswordBox)sender).Password;
            }
        }

    }

}

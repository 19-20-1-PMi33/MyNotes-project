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

        void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterVM register = new RegisterVM();
            register.Action1(txt1.Text, txt2.Text, pass2.Password.ToString());
            // if (signed up)
            if (txt1.Text == null|| txt2.Text == null||
                pass2.Password.ToString() == null|| string.IsNullOrWhiteSpace(txt1.Text)||
                string.IsNullOrWhiteSpace(txt2.Text)|| string.IsNullOrWhiteSpace(pass2.Password.ToString()))
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
                    MessageBox.Show("Account already exists!");
                }
            }
        }
    }
}

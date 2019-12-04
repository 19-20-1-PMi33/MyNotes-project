using System;
using System.Collections.Generic;
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
            if (App.currentUser != null)
            {
                this.NavigationService.Navigate(new Uri("View/HomePage.xaml", UriKind.Relative));
            }
            // if (signed in)
        }
       

    }

}

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
    /// Interaction logic for UserPage.xaml
    /// </summary>
    public partial class UserPage :Page
    {
        public UserPage()
        {
            InitializeComponent();
            this.DataContext = new UserPageVM();
        }
        /// <summary>
        /// Updating user data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            UserPageVM user = new UserPageVM();
            if (txt1.Text == null && txt2.Text == null &&
                   pass3.Password.ToString() == null || string.IsNullOrWhiteSpace(txt1.Text) &&
                   string.IsNullOrWhiteSpace(txt2.Text) && string.IsNullOrWhiteSpace(pass3.Password.ToString()))
               {
                   MessageBox.Show("Any of row must be fill in!");
               }
               else
               {
                if (txt1.Text != null && !string.IsNullOrWhiteSpace(txt1.Text))
                {
                    user.Action1(txt1.Text);
                }
                if (txt2.Text != null && !string.IsNullOrWhiteSpace(txt2.Text))
                {
                    user.Action3(txt2.Text);
                    if (user.exist == false)
                    {
                        MessageBox.Show("Email already exist!");
                    }
                }
                if (pass3.Password.ToString() != null && !string.IsNullOrWhiteSpace(pass3.Password.ToString()))
                {
                    user.Action2(pass3.Password.ToString());
                }
                MessageBox.Show("Updated!");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("View/HomePage.xaml", UriKind.Relative));
        }
    }
}

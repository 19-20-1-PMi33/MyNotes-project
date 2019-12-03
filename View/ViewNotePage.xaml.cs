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
    /// Interaction logic for ViewNotePage.xaml
    /// </summary>
    public partial class ViewNotePage : Page
    {
        public ViewNotePage()
        {
            InitializeComponent();
            this.DataContext = new ViewNotePageVM();
        }

        void btnBack_Click(object sender, RoutedEventArgs e)
        {
            // if (signed in)
            this.NavigationService.Navigate(new Uri("View/HomePage.xaml", UriKind.Relative));
        }


        //private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    TextBox textBox = (TextBox)sender;
        //    MessageBox.Show(textBox.Text);
        //}
    }
}

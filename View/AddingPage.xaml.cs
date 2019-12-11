using System;
using System.Windows;
using System.Windows.Controls;

namespace MyNotes
{
    /// <summary>
    /// Interaction logic for AddingPage.xaml
    /// </summary>
    public partial class AddingPage : Page
    {
        public AddingPage()
        {
            InitializeComponent();
            this.DataContext = new AddingPageVM();
        }

        /// <summary>
        /// Save botton click event,that adds new note to DataBase
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">RoutedEventArgs</param>
        void btnSave_Click(object sender, RoutedEventArgs e)
        {
            HomePageVM vm1 = new HomePageVM();
            AddingPageVM vm = this.DataContext as AddingPageVM;
            string[] SaveString = new string[2];
            SaveString[0] = txt1.Text;
            SaveString[1] = txt2.Text;
            if ((vm != null) && (vm.SaveCommand.CanExecute(null)))
                vm.SaveCommand.Execute(SaveString);
            this.NavigationService.Navigate(new Uri("View/HomePage.xaml", UriKind.Relative));
        }

        void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("View/HomePage.xaml", UriKind.Relative));
        }
    }
}

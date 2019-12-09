using System;
using System.Xaml;
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
    /// Interaction logic for AddingPage.xaml
    /// </summary>
    public partial class AddingPage : Page
    {
        public AddingPage()
        {
            InitializeComponent();
            this.DataContext = new AddingPageVM();
        }
        void btnSave_Click(object sender, RoutedEventArgs e)
        {
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

       
        //private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        //{
        //    MessageBox.Show("Справка по приложению");
        //}


        //CommandBinding commandBinding = new CommandBinding();
        //commandBinding.Command = ApplicationCommands.Save;
        //    commandBinding.Executed += CommandBinding_Executed;
        //    saveButton.CommandBindings.Add(commandBinding);

    }
}

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
        public Note currnote;
        public ViewNotePage(Note SelectedNote)
        {
            InitializeComponent();
            txt1.Text = SelectedNote.Title;
            txt2.Text = SelectedNote.Description;
            currnote = SelectedNote;
            this.DataContext = new ViewNotePageVM();
        }
         /// <summary>
         /// Save botton click event,that update information of the chosen note in DataBase
         /// </summary>
         /// <param name="sender">Event sender</param>
         /// <param name="e">RoutedEventArgs</param>
        void btnSave_Click(object sender, RoutedEventArgs e)
        {
            HomePageVM a = new HomePageVM();
            ViewNotePageVM vm = this.DataContext as ViewNotePageVM;
            object[] SaveString = new object[3];
            SaveString[0] = txt1.Text;
            SaveString[1] = txt2.Text;
            SaveString[2] = currnote.NoteId;
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

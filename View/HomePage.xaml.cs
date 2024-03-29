﻿using System;
using System.Windows;
using System.Windows.Controls;

namespace MyNotes
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
            this.DataContext = new HomePageVM();
        }

        /// <summary>
        /// LogOut functionality
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("View/LoginPage.xaml", UriKind.Relative));
            App.currentUser = null;
        }

        /// <summary>
        /// Handler of SelectionChanged event in sorting ComboBox 
        /// </summary>
        /// <param name="sender">Event sender (sorting ComboBox)</param>
        /// <param name="e">RoutedEventArgs</param>
        void sortRuleChanged(object sender, RoutedEventArgs e)
        {
            HomePageVM vm = this.DataContext as HomePageVM;
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;

            string sortRule = selectedItem.Content?.ToString();
            if (sortRule != null)
            {
                if ((vm != null) && (vm.SortCommand.CanExecute(null)))
                    vm.SortCommand.Execute(sortRule);
            }
        }

        /// <summary>
        /// Handler of TextChanged event in search TextBox
        /// </summary>
        /// <param name="sender">Event sender (search TextBox)</param>
        /// <param name="e">RoutedEventArgs</param>
        void searchStringChanged(object sender, RoutedEventArgs e)
        {
            HomePageVM vm = this.DataContext as HomePageVM;
            TextBox textBox = (TextBox)sender;

            string searchString = textBox.Text;

            if ((vm != null) && (vm.SearchCommand.CanExecute(null)))
                vm.SearchCommand.Execute(searchString);

            sortRuleChanged(sortBox, e);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("View/UserPage.xaml", UriKind.Relative));
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("View/AddingPage.xaml", UriKind.Relative));
        }

        /// <summary>
        /// Send SelectedNote object to View,and open ViewPage
        /// </summary>
        private void ViewNoteButton_Click(object sender, RoutedEventArgs e)
        {
            HomePageVM home = this.DataContext as HomePageVM;
            this.NavigationService.Navigate(new ViewNotePage(home.SelectedNote));
        }
    }
}

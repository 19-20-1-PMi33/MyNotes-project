﻿using System;
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
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
            this.DataContext = new HomePageVM();
        }

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
    }
}

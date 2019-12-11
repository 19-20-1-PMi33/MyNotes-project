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
using System.ComponentModel;
namespace MyNotes
{
    /// <summary>
    /// Interaction logic for LoadingPage.xaml
    /// </summary>
    public partial class LoadingPage : Page,INotifyPropertyChanged
    {
        BackgroundWorker backgroundWorker = new BackgroundWorker();
        private int worker;
        public int WorkerState
        {
            get { return worker; }
            set
            {
                worker = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("WorkerState"));
                }
            }
        }
        private void worker_RunWorkerCompleted(object sender,
                                           RunWorkerCompletedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("View/HomePage.xaml", UriKind.Relative));
        }
        /// <summary>
        /// Implements animation and functionality of loading progress bar
        /// </summary>
        public LoadingPage()
        {
            InitializeComponent();
            DataContext = this;
            backgroundWorker.DoWork += (s, e) =>
            {
                
                for (int i=0;i<100; i++)
                {
                    System.Threading.Thread.Sleep(20);
                    WorkerState = i;
                }
            };
            backgroundWorker.RunWorkerCompleted+= worker_RunWorkerCompleted;
            backgroundWorker.RunWorkerAsync();
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}

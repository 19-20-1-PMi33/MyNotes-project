using System;
using System.Windows.Controls;
using System.ComponentModel;

namespace MyNotes
{
    /// <summary>
    /// Interaction logic for LoadingPage.xaml
    /// </summary>
    public partial class LoadingPage : Page, INotifyPropertyChanged
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

                for (int i = 0; i < 100; i++)
                {
                    System.Threading.Thread.Sleep(20);
                    WorkerState = i;
                }
            };
            backgroundWorker.RunWorkerCompleted += worker_RunWorkerCompleted;
            backgroundWorker.RunWorkerAsync();
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}

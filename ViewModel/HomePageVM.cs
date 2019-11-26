using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace MyNotes
{
    public class HomePageVM : INotifyPropertyChanged
    {
        Note selectedNote;
        public Note SelectedNote
        {
            get { return selectedNote; }
            set
            {
                selectedNote = value;
                OnPropertyChanged("SelectedNote");
            }
        }
        public ObservableCollection<Note> Notes { get; set; }

        public HomePageVM()
        {
            Notes = new ObservableCollection<Note>
            {
                new Note { Title="Test title 1", Description="Test descr 1", TimeModified="26.11.19 22:00" },
                new Note { Title="Test title 2", Description="Test descr 2", TimeModified="26.11.19 23:00" }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
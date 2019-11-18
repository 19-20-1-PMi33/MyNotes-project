using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyNotes
{
    public class Note : INotifyPropertyChanged
    {
        string title;
        string description;
        string timeModified;

        public int NoteId { get; set; }
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }
        public string TimeModified
        {
            get { return timeModified; }
            set
            {
                timeModified = value;
                OnPropertyChanged("TimeModified");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
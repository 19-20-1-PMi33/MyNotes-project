using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Data.Entity;
using System.Collections.Generic;

namespace MyNotes
{
    public class HomePageVM : INotifyPropertyChanged
    {
        AppDbContext db;
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
        public IEnumerable<Note> Notes { get; set; }

        public HomePageVM()
        {
            db = new AppDbContext();
            db.Notes.Load();
            Notes = db.Notes.Local.ToBindingList();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
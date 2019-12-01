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
        public List<Note> Notes { get; set; }

        public HomePageVM()
        {
            db = new AppDbContext();
            App.currentUser = new User { UserId = 1}; //mock
            loadNotes();
        }

        async void loadNotes()
        {
            Notes = await db.Database.SqlQuery<Note>("SELECT Notes.NoteId, Title, Description, TimeModified " +
                                                     "FROM Notes JOIN UserNotes ON Notes.NoteId = UserNotes.NoteId " +
                                                     $"WHERE UserNotes.UserId = {App.currentUser.UserId}")
                                                     .ToListAsync();            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
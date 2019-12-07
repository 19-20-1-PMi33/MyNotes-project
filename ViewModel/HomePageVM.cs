using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;

namespace MyNotes
{
    public class HomePageVM : INotifyPropertyChanged
    {
        AppDbContext db;
        Note selectedNote;

        BindingList<Note> notes;
        public BindingList<Note> Notes
        {
            get { return notes; }
            set
            {
                notes = value;
                OnPropertyChanged("Notes");
            }
        }

        RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                    (removeCommand = new RelayCommand(selectedItem =>
                    {
                        if (selectedItem == null) return;
                        Note note = selectedItem as Note;
                        db.Database.ExecuteSqlCommand($"DELETE FROM Notes WHERE NoteId={note.NoteId};" +
                                                      $"DELETE FROM UserNotes WHERE NoteId={note.NoteId};");
                        Notes.Remove(note);
                        db.SaveChanges();
                    },
                    (obj) => selectedNote != null));
            }
        }

        RelayCommand sortCommand;
        public RelayCommand SortCommand
        {
            get
            {
                return sortCommand ??
                    (sortCommand = new RelayCommand(sortRule =>
                    {
                        switch (sortRule.ToString())
                        {
                            case "Sort notes":
                                Notes = new BindingList<Note>(Notes.OrderBy(note => note.NoteId).ToList());
                                break;
                            case "By title":
                                Notes = new BindingList<Note>(Notes.OrderBy(note => note.Title).ToList());
                                break;
                            case "By time":
                                Notes = new BindingList<Note>(Notes.OrderByDescending(note => note.TimeModified).ToList());
                                break;
                        }
                    }));
            }
        }

        RelayCommand searchCommand;
        public RelayCommand SearchCommand
        {
            get
            {
                return searchCommand ??
                    (searchCommand = new RelayCommand(search =>
                    {                        
                        string searchString = search.ToString().ToLower();

                        if (string.IsNullOrWhiteSpace(searchString))
                        {
                            loadNotes();
                        }
                        else
                        {
                            loadNotes();
                            List<Note> data = new List<Note>(Notes);
                            Notes = new BindingList<Note>();

                            foreach (Note note in data)
                            {
                                if (note.Title.ToLower().Contains(searchString) || note.Description.ToLower().Contains(searchString))
                                    Notes.Add(note);
                            }
                        }
                    }));
            }
        }

        public Note SelectedNote
        {
            get { return selectedNote; }
            set
            {
                selectedNote = value;
                OnPropertyChanged("SelectedNote");
            }
        }

        public HomePageVM()
        {
            db = new AppDbContext();
            //App.currentUser = new User { UserId = 1 }; //mock
            loadNotes();
        }

        async void loadNotes()
        {
            List<Note> query = await db.Database.SqlQuery<Note>("SELECT Notes.NoteId, Title, Description, TimeModified " +
                                                     "FROM Notes JOIN UserNotes ON Notes.NoteId = UserNotes.NoteId " +
                                                     $"WHERE UserNotes.UserId = {App.currentUser.UserId}")
                                                     .ToListAsync();

            Notes = new BindingList<Note>(query);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
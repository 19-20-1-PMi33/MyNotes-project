using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;

namespace MyNotes
{
    public class AddingPageVM : INotifyPropertyChanged
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

        RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                    (saveCommand = new RelayCommand(selectedItem =>
                    {
                        if (selectedItem == null) return;
                        Note note = selectedItem as Note;
                        db.Database.ExecuteSqlCommand($"UPDATE Notes WHERE NoteId={note.NoteId};" +
                                                      $"UPDATE UserNotes WHERE NoteId={note.NoteId};");
                        Notes.Save(note);
                        db.SaveChanges();
                    },
                    (obj) => selectedNote != null));
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}

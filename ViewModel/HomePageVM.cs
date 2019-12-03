﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Data.Entity;
using System.Collections.Generic;

namespace MyNotes
{
    public class HomePageVM : INotifyPropertyChanged
    {
        AppDbContext db;
        Note selectedNote;

        public BindingList<Note> Notes { get; set; }

        private RelayCommand removeCommand;
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
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Data.Entity;
using System.Collections.Generic;
using System;
using System.Windows;

namespace MyNotes
{
    public class ViewNotePageVM : INotifyPropertyChanged
    {

        AppDbContext db;
        Note selectedNote;
        RelayCommand saveCommand;

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
        /// <summary>
        /// Gets command, that change information of the chosen note
        /// </summary>
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                    (saveCommand = new RelayCommand(obj =>
                    {
                        object[] saveString = obj as object[];
                        db.Database.ExecuteSqlCommand($"update Notes set Title = '{saveString[0]}' where NoteId={saveString[2]}");
                        db.Database.ExecuteSqlCommand($"update Notes set Description = '{saveString[1]}' where NoteId={saveString[2]}");
                        db.Database.ExecuteSqlCommand($"update Notes set TimeModified = '{DateTime.Now}' where NoteId={saveString[2]}");
                        db.SaveChanges();
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
        public ViewNotePageVM()
        {
            db = new AppDbContext();
            //App.currentUser = new User { UserId = 1 }; //mock
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
   
         
}
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System;

namespace MyNotes
{
    public class AddingPageVM : INotifyPropertyChanged
    {
        AppDbContext db;
        Note selectedNote;
        int lastNoteId;

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
                    (saveCommand = new RelayCommand(obj =>
                    {
                        string[] saveString = obj as string[];
                        //db.Database.ExecuteSqlCommand($"insert into Notes(Title,Description,TimeModified) values(" + saveString[0] + "," + saveString[1] + "," + DateTime.Now.ToString() + ")");
                        db.Database.ExecuteSqlCommand($"insert into Notes(Title, Description, TimeModified) values('{saveString[0]}', '{saveString[1]}', '{DateTime.Now}')");
                        db.SaveChanges();
                        //db.Database.ExecuteSqlCommand($"insert into UserNotes(UserId,NoteId) values(" + App.currentUser.UserId + "," + ("Select MAX(NoteId) from Notes") + "))");
                        getLastNoteId();
                        db.Database.ExecuteSqlCommand($"insert into UserNotes values ('{App.currentUser.UserId}', '{lastNoteId}')");
                        //Notes.Add(obj);
                        db.SaveChanges();
                    }));

            }
        }
        async void getLastNoteId()
        {
            List<int> query = await db.Database.SqlQuery<int>("select max(NoteId) from Notes").ToListAsync();
            lastNoteId = query[0];
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
        public AddingPageVM()
        {
            db = new AppDbContext();
            //App.currentUser = new User { UserId = 1 }; //mock
            //loadNotes();
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
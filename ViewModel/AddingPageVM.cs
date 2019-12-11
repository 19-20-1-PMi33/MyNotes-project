using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System;

namespace MyNotes
{
    public class AddingPageVM : INotifyPropertyChanged
    {
        AppDbContext db;
        RelayCommand saveCommand;
        int lastNoteId;

        /// <summary>
        /// Gets command, that adds new note to database
        /// </summary> 
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                    (saveCommand = new RelayCommand(obj =>
                    {
                        string[] saveString = obj as string[];
                        db.Database.ExecuteSqlCommand($"insert into Notes(Title, Description, TimeModified) values('{saveString[0]}', '{saveString[1]}', '{DateTime.Now}')");
                        db.SaveChanges();
                        getLastNoteId();
                        db.Database.ExecuteSqlCommand($"insert into UserNotes values ('{App.currentUser.UserId}', '{lastNoteId}')");
                        db.SaveChanges();
                    }));

            }
        }
        /// <summary>
        /// Select note with max ID from Notes
        /// </summary>
        async void getLastNoteId()
        {
            List<int> query = await db.Database.SqlQuery<int>("select max(NoteId) from Notes").ToListAsync();
            lastNoteId = query[0];
        }
        public AddingPageVM()
        {
            db = new AppDbContext();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
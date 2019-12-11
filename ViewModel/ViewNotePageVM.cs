using System;

namespace MyNotes
{
    public class ViewNotePageVM
    {
        AppDbContext db;
        RelayCommand saveCommand;

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

        public ViewNotePageVM()
        {
            db = new AppDbContext();
        }
    }

}
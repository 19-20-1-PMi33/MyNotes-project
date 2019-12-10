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
                    object[] saveString = obj as object[];
                    HomePageVM a = new HomePageVM();
                    //MessageBox.Show(int.Parse(saveString[2] as string).ToString());
                    //db.Database.ExecuteSqlCommand($"update Notes set Title='" + saveString[0] as string + " where NoteId=" +   int.Parse(saveString[2] as string) ");
                    db.Database.ExecuteSqlCommand($"update Notes set Title = '{saveString[0]}' where NoteId={saveString[2]}");
                    db.Database.ExecuteSqlCommand($"update Notes set Description = '{saveString[1]}' where NoteId={saveString[2]}");
                    db.Database.ExecuteSqlCommand($"update Notes set TimeModified = '{DateTime.Now}' where NoteId={saveString[2]}");
                    //db.Database.ExecuteSqlCommand($"update Notes set Description='" + saveString[1] as string + "' where NoteId='" + int.Parse(saveString[2] as string) + "'");
                    //db.Database.ExecuteSqlCommand($"update Notes set TimeModified='" + DateTime.Now + "' where NoteId='" + int.Parse(saveString[2] as string) + "'");
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
    public ViewNotePageVM()
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
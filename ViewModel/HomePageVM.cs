using System;
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
        bool noteIsSelected;
        RelayCommand removeCommand;
        RelayCommand sortCommand;
        RelayCommand searchCommand;

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
        /// Gets command, that removes selected note
        /// </summary>
        /// 
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

        /// <summary>
        /// Gets command, that sorts notes on home page
        /// </summary>
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
                                Notes = new BindingList<Note>(Notes.OrderByDescending(note => dateTimeParser(note.TimeModified)).ToList());
                                break;
                        }
                    }));
            }
        }

        DateTime dateTimeParser(string dateString)
        {
            string[] splitDateString = dateString.Split();
            string[] dayMonthYear = splitDateString[0].Split('.');
            string[] hourMinSec = splitDateString[1].Split(':');

            return new DateTime(int.Parse(dayMonthYear[2]), int.Parse(dayMonthYear[1]), int.Parse(dayMonthYear[0]),
                                int.Parse(hourMinSec[0]), int.Parse(hourMinSec[1]), int.Parse(hourMinSec[2]));
        }

        /// <summary>
        /// Gets command, that performs searching note on home page
        /// </summary>
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
                NoteIsSelected = selectedNote != null ? true : false;
                OnPropertyChanged("SelectedNote");
            }
        }

        public bool NoteIsSelected
        {
            get { return noteIsSelected; }
            set
            {
                noteIsSelected = value;
                OnPropertyChanged("NoteIsSelected");
            }
        }

        public HomePageVM()
        {
            db = new AppDbContext();
            loadNotes();
        }

        /// <summary>
        /// Loads user notes from database
        /// </summary>
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
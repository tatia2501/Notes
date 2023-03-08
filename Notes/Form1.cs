using System;
using System.Drawing;
using System.Windows.Forms;
using Notes.Services;

namespace Notes
{
    public partial class Form1 : Form
    {
        private readonly INoteService _service;

        // the height of the notes window in the main menu
        private const int NoteHeight = 60;
        // the width of the notes window in the main menu
        private const int NoteWidth = 200;
        // x-axis position of the first column with notes on the main menu
        private const int FirstColumnPositionX = 20;
        // x-axis position of the second column with notes on the main menu
        private const int SecondColumnPositionX = 250;
        // offset of the coordinates of the note windows along the y-axis
        private const int NotePositionYChange = 80;
        // the x-axis coordinate of the last added note
        private int _notePositionX = 20;
        // the y-axis coordinate of the last added note
        private int _notePositionY = 70;

        public Form1()
        {
            InitializeComponent();
            _service = new NoteService();
        }

        // an auxiliary method that creates a window with a note and places it in the right place
        private void CreateAndPlaceItem(string text, Guid id)
        {
            var newBtn = new Button();
            newBtn.Text = text;
            newBtn.Size = new Size(NoteWidth, NoteHeight);
            newBtn.Tag = id;
            newBtn.Click += new EventHandler(button2_Click);
            
            if (_notePositionX == FirstColumnPositionX)
            {
                newBtn.Location = new Point(SecondColumnPositionX, _notePositionY);
                _notePositionX = SecondColumnPositionX;
            }
            else
            {
                newBtn.Location = new Point(FirstColumnPositionX, _notePositionY + NotePositionYChange);
                _notePositionX = FirstColumnPositionX;
                _notePositionY += NotePositionYChange;
            }
            Controls.Add(newBtn);
        }

        // the method executed when loading the form, outputs all existing notes
        private void Form1_Load(object sender, EventArgs e)
        {
            _notePositionX = SecondColumnPositionX;
            _notePositionY -= NotePositionYChange;
            
            var initialNotes = _service.GetInitialNotes();
            foreach (var initialNote in initialNotes)
            {
                CreateAndPlaceItem(initialNote.Title, initialNote.Id);
            } 
        }
        
        // the "Добавить" button method, creates a new note
        private void button1_Click(object sender, EventArgs e)
        {
            var newId = _service.InsertNewNote("Новая заметка", "Текст заметки");
            CreateAndPlaceItem("Новая заметка", newId);

            var newForm = new Editor(this);
            newForm.InboxData = newId;
            newForm.Show();
        }

        // the method that opens the Editor form when you click on a note
        private void button2_Click(object sender, EventArgs e)
        {
            var newForm = new Editor(this);
            newForm.InboxData = (Guid) ((Control)sender).Tag;
            newForm.Show();
        }

        // the method that updates the form, applied after editing or deleting a note
        public void UpdateChanges()
        {
            Application.Restart();
        }
    }
} 
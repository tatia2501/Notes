using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Notes.Services;

namespace Notes
{
    public partial class Form1 : Form
    {
        private readonly INoteService _service;

        private const int NoteHeight = 60;
        private const int NoteWidth = 200;
        private int _notePositionX = 20;
        private int _notePositionY = 70;
        private const int FirstColumnPositionX = 20;
        private const int SecondColumnPositionX = 250;
        private const int NotePositionYChange = 80;

        public Form1()
        {
            InitializeComponent();
            _service = new NoteService();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _notePositionX = SecondColumnPositionX;
            _notePositionY -= NotePositionYChange;
            
            var initialNotes = _service.GetInitialNotes();
            foreach (var initialNote in initialNotes)
            {
                Button newBtn = new Button();
                newBtn.Text = initialNote.Title;
                newBtn.Size = new Size(NoteWidth, NoteHeight);
                newBtn.Tag = initialNote.Id;
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
                
                this.Controls.Add(newBtn);
            } 
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            var newId = _service.InsertNewNote("Новая заметка", "Текст заметки");
            Button newBtn = new Button();
            newBtn.Text = "Новая заметка";
            newBtn.Size = new Size(NoteWidth, NoteHeight);
            newBtn.Tag = newId;
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
            this.Controls.Add(newBtn);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Editor newForm = new Editor(this);
            newForm.InboxData = (int) ((Control)sender).Tag;
            newForm.Show();
        }
    }
} 
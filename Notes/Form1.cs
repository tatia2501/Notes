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

        private int _noteHeight = 60;
        private int _noteWidth = 200;
        private int _notePositionX = 20;
        private int _notePositionY = 70;
        private int _firstColumnPositionX = 20;
        private int _secondColumnPositionX = 250;
        private int _notePositionYChange = 80;
        
        public Form1()
        {
            InitializeComponent();
            _service = new NoteService();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _notePositionX = _secondColumnPositionX;
            _notePositionY -= _notePositionYChange;
            
            var initialNotes = _service.GetInitialNotes();
            foreach (var initialNote in initialNotes)
            {
                Button newBtn = new Button();
                newBtn.Text = initialNote.Title;
                newBtn.Size = new Size(_noteWidth, _noteHeight);
                newBtn.Click += new EventHandler(button2_Click);
                
                if (_notePositionX == _firstColumnPositionX)
                {
                    newBtn.Location = new Point(_secondColumnPositionX, _notePositionY);
                    _notePositionX = _secondColumnPositionX;
                }
                else
                {
                    newBtn.Location = new Point(_firstColumnPositionX, _notePositionY + _notePositionYChange);
                    _notePositionX = _firstColumnPositionX;
                    _notePositionY += _notePositionYChange;
                }
                
                this.Controls.Add(newBtn);
            } 
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            Button newBtn = new Button();
            newBtn.Text = "Новая заметка";
            newBtn.Size = new Size(_noteWidth, _noteHeight);
            newBtn.Click += new EventHandler(button2_Click);

            if (_notePositionX == _firstColumnPositionX)
            {
                newBtn.Location = new Point(_secondColumnPositionX, _notePositionY);
                _notePositionX = _secondColumnPositionX;
            }
            else
            {
                newBtn.Location = new Point(_firstColumnPositionX, _notePositionY + _notePositionYChange);
                _notePositionX = _firstColumnPositionX;
                _notePositionY += _notePositionYChange;
            }
            this.Controls.Add(newBtn);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Editor newForm = new Editor(this);
            newForm.Show();
        }
        
    }
} 
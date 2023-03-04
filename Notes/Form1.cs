using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notes
{
    public partial class Form1 : Form
    {
        private int _noteHeight = 120;
        private int _noteWidth = 150;
        private int _notePositionX = 20;
        private int _notePositionY = 70;
        private int _notePositionXChange = 180;
        private int _notePositionYChange = 150;
        private int _formWidth = 1000;
        private int _initialXLocation = 20;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Button newBtn = new Button();
            newBtn.Text = "Новая заметка";
            newBtn.Size = new Size(_noteWidth, _noteHeight);
            newBtn.Location = new Point(_notePositionX, _notePositionY);
            newBtn.Click += new EventHandler(button2_Click);
            newBtn.UseVisualStyleBackColor = false;
            this.Controls.Add(newBtn);
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            Button newBtn = new Button();
            newBtn.Text = "Новая заметка";
            newBtn.Size = new Size(_noteWidth, _noteHeight);
            if (_notePositionX + _notePositionXChange < _formWidth)
            {
                newBtn.Location = new Point(_notePositionX + _notePositionXChange, _notePositionY);
                _notePositionX += _notePositionXChange;
            }
            else
            {
                newBtn.Location = new Point(_initialXLocation, _notePositionY + _notePositionYChange);
                _notePositionX = _initialXLocation;
                _notePositionY += _notePositionYChange;
            }
            newBtn.Click += new EventHandler(button2_Click);
            newBtn.UseVisualStyleBackColor = false;
            this.Controls.Add(newBtn);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Editor newForm = new Editor(this);
            newForm.Show();
        }
        
    }
}
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

namespace Notes
{
    public partial class Form1 : Form
    {
        SqlConnection connection;
        SqlCommand command;
        
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
            
            connection = new SqlConnection();
            connection.ConnectionString = @"Data Source=LAPTOP-02135ROM\SQLEXPRESS;Initial Catalog=Notes;Integrated Security=SSPI;";
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Button newBtn = new Button();
            string title="55";

            command.CommandText = "select * from NotesText where ip=0;";
            connection.Open();
            SqlDataReader reader= command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    title = reader.GetString(1);
                    var text = reader.GetString(2);
                }
            }
            connection.Close();

            newBtn.Text = title;
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
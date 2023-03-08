using System;
using System.Windows.Forms;
using Notes.Services;

namespace Notes
{
    public partial class Editor : Form
    {
        private readonly INoteService _service;
        
        public int InboxData;
        private int _id;
        public Editor()
        {
            InitializeComponent();
            _service = new NoteService();
        }
        
        public Editor(Form1 f)
        {
            InitializeComponent();
            _service = new NoteService();
        }

        private void Editor_Load(object sender, EventArgs e)
        {
            _id = InboxData;
            var note = _service.GetNote(_id);

            this.textBox1.Text = note.Title;
            this.textBox2.Text = note.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _service.DeleteNote(_id);
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }
    }
}
using System;
using System.Windows.Forms;
using Notes.Services;

namespace Notes
{
    public partial class Editor : Form
    {
        private readonly INoteService _service;
        
        public Guid InboxData;
        private Guid _id;
        private readonly Form1 _form;
        public Editor()
        {
            InitializeComponent();
            _service = new NoteService();
        }
        
        public Editor(Form1 f)
        {
            InitializeComponent();
            _service = new NoteService();
            _form = f;
        }

        // the method executed when loading the form, outputs the title and text of the note
        private void Editor_Load(object sender, EventArgs e)
        {
            _id = InboxData;
            var note = _service.GetNote(_id);

            textBox1.Text = note.Title;
            textBox2.Text = note.Text;
        }

        // the method of the "Удалить" button, deletes the note and closes the form
        private void button1_Click(object sender, EventArgs e)
        {
            _service.DeleteNote(_id);
            _form.UpdateChanges();
            Close();
        }

        // the method of the "Сохранить" button, saves the changes in the note and closes the form
        private void button2_Click(object sender, EventArgs e)
        {
            _service.UpdateNote(_id, textBox1.Text, textBox2.Text);
            _form.UpdateChanges();
        }
    }
}
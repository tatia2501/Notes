using System;

namespace Notes.Entities
{
    public class NoteModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Text  { get; set; }
        
        public NoteModel(){}

        public NoteModel(string title, string text)
        {
            Title = title;
            Text = text;
        }

    }
}
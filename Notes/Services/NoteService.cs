using System.Collections.Generic;
using Notes.Entities;
using Notes.Repositories;

namespace Notes.Services
{
    public class NoteService : INoteService
    {
        private NoteReporitory _reporitory;
        
        public NoteService()
        {
            _reporitory = new NoteReporitory();
        }

        public List<NoteModel> GetInitialNotes()
        {
            return _reporitory.GetInitialNotes();
        }

        public NoteModel GetNote(int id)
        {
            return _reporitory.GetNote(id);

        }

        public int InsertNewNote(string title, string text)
        {
            return _reporitory.InsertNewNote(title, text);
        }

        public void DeleteNote(int id)
        {
            _reporitory.DeleteNote(id);
        }

        public void UpdateNote(int id, string title, string text)
        {
            _reporitory.UpdateNote(id, title, text);
        }
    }
}
using System;
using System.Collections.Generic;
using Notes.Entities;

namespace Notes.Services
{
    public interface INoteService
    {
        List<NoteModel> GetInitialNotes();
        NoteModel GetNote(Guid id);
        Guid InsertNewNote(string title, string text);
        void DeleteNote(Guid id);
        void UpdateNote(Guid id, string title, string text);
    }
}
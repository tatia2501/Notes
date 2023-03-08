using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Notes.Entities;

namespace Notes.Repositories
{
    public class NoteReporitory
    {
        private readonly SqlConnection _connection;
        private readonly SqlCommand _command;
        public NoteReporitory()
        {
            // connecting to a local database
            _connection = new SqlConnection();
            _connection.ConnectionString = @"Data Source=LAPTOP-02135ROM\SQLEXPRESS;Initial Catalog=NotesApp;Integrated Security=SSPI;";
            _command = new SqlCommand();
            _command.Connection = _connection;
            _command.CommandType = CommandType.Text;
        }

        // the method gets and returns all existing notes in the database
        public List<NoteModel> GetInitialNotes()
        {
            var initialNotes = new List<NoteModel>();
            _command.CommandText = "select * from NotesContent";
            _connection.Open();
            var reader= _command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var newNote = new NoteModel
                    {
                        Id = reader.GetGuid(0),
                        Title = reader.GetString(1),
                        Text = reader.GetString(2)
                    };
                    initialNotes.Add(newNote);
                }
            }
            _connection.Close();
            return initialNotes;
        }

        // the method gets and returns a note by its id
        public NoteModel GetNote(Guid id)
        {
            var newNote = new NoteModel();
            _command.CommandText = $"select * from NotesContent where id='{id}';";
            _connection.Open();
            var reader= _command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    newNote.Id = reader.GetGuid(0);
                    newNote.Title = reader.GetString(1);
                    newNote.Text = reader.GetString(2);;
                }
            }
            _connection.Close();
            return newNote;
        }
        
        // the method adds a new note to the database
        // the name and text are passed as arguments, the id is generated automatically and its value is returned
        public Guid InsertNewNote(string title, string text)
        {
            var num = Guid.NewGuid();
            _command.CommandText = $"insert into NotesContent (id, Title, Text) values ('{num}', '{title}', '{text}');";
            _connection.Open();
            _command.ExecuteNonQuery();
            _connection.Close();
            
            return num;
        }
        
        // the method deletes a note by its id
        public void DeleteNote(Guid id)
        {
            _command.CommandText = $"delete from NotesContent where id='{id}';";
            _connection.Open();
            _command.ExecuteNonQuery();
            _connection.Close();
        }

        // the method searches for a note by id and updates the information in it
        public void UpdateNote(Guid id, string title, string text)
        {
            _command.CommandText = $"update NotesContent set Title='{title}', Text='{text}' where id='{id}'";
            _connection.Open();
            _command.ExecuteNonQuery();
            _connection.Close();
        }
    }
}
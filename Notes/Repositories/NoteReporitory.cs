using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Notes.Entities;

namespace Notes.Repositories
{
    public class NoteReporitory
    {
        SqlConnection connection;
        SqlCommand command;
        public NoteReporitory()
        {
            connection = new SqlConnection();
            connection.ConnectionString = @"Data Source=LAPTOP-02135ROM\SQLEXPRESS;Initial Catalog=NotesApp;Integrated Security=SSPI;";
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
        }

        public List<NoteModel> GetInitialNotes()
        {
            var _initialNotes = new List<NoteModel>();
            command.CommandText = "select * from NotesContent";
            connection.Open();
            SqlDataReader reader= command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var newNote = new NoteModel();
                    newNote.Id = reader.GetGuid(0);
                    newNote.Title = reader.GetString(1);
                    newNote.Text = reader.GetString(2);
                    _initialNotes.Add(newNote);
                }
            }
            connection.Close();
            return _initialNotes;
        }

        public NoteModel GetNote(Guid id)
        {
            var newNote = new NoteModel();
            command.CommandText = $"select * from NotesContent where id='{id}';";
            connection.Open();
            SqlDataReader reader= command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    newNote.Id = reader.GetGuid(0);
                    newNote.Title = reader.GetString(1);
                    newNote.Text = reader.GetString(2);;
                }
            }
            connection.Close();
            return newNote;
        }
        
        public Guid InsertNewNote(string title, string text)
        {
            var num = Guid.NewGuid();
            command.CommandText = $"insert into NotesContent (id, Title, Text) values ('{num}', '{title}', '{text}');";
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            
            return num;
        }
        
        public void DeleteNote(Guid id)
        {
            command.CommandText = $"delete from NotesContent where id='{id}';";
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void UpdateNote(Guid id, string title, string text)
        {
            command.CommandText = $"update NotesContent set Title='{title}', Text='{text}' where id='{id}'";
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
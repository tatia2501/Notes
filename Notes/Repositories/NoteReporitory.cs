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
            connection.ConnectionString = @"Data Source=LAPTOP-02135ROM\SQLEXPRESS;Initial Catalog=Notes;Integrated Security=SSPI;";
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
        }

        public List<NoteModel> GetInitialNotes()
        {
            var _initialNotes = new List<NoteModel>();
            command.CommandText = "select * from NotesText";
            connection.Open();
            SqlDataReader reader= command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var newNote = new NoteModel();
                    newNote.Id = reader.GetInt32(0);
                    newNote.Title = reader.GetString(1);
                    newNote.Text = reader.GetString(2);
                    _initialNotes.Add(newNote);
                }
            }
            connection.Close();
            return _initialNotes;
        }

        public NoteModel GetNote(int id)
        {
            var newNote = new NoteModel();
            command.CommandText = $"select * from NotesText where id={id};";
            connection.Open();
            SqlDataReader reader= command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    newNote.Id = reader.GetInt32(0);
                    newNote.Title = reader.GetString(1);
                    newNote.Text = reader.GetString(2);;
                }
            }
            connection.Close();
            return newNote;
        }
        
        public int InsertNewNote(string title, string text)
        {
            command.CommandText = "select count(1) from NotesText";
            connection.Open();
            var num = (int) command.ExecuteScalar();
            connection.Close();
            
            command.CommandText = $"insert into NotesText(id, Title, Text) values ({num}, '{title}', '{text}');";
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            return num;
        }
    }
}
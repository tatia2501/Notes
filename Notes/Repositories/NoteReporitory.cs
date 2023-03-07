﻿using System.Collections.Generic;
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
                    newNote.Title = reader.GetString(1);
                    newNote.Text = reader.GetString(2);
                    _initialNotes.Add(newNote);
                }
            }
            connection.Close();
            return _initialNotes;
        }
    }
}
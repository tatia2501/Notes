using System.Data;
using System.Data.SqlClient;

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

        public string FirstOneTitle()
        {
            string title = "";
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
            return title;
        }
    }
}
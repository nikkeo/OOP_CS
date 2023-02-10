using MySqlConnector;

namespace Data_Access_layer;

public class DB : IDisposable
{
    private MySqlConnection _connection;
    public DB(string server, string port, string username, string password, string database)
    {
        string name = "server=" + server + ";port=" + port + ";username=" + username + ";password=" + password +
                      ";database=" + database + ";";
        Database = database;
        _connection = new MySqlConnection(name);
    }

    public MySqlConnection Connection { get => _connection; }
    public string Database { get; }

    public void openConnection()
    {
        if (_connection.State == System.Data.ConnectionState.Closed)
            _connection.Open();
    }
    
    public void closeConnection()
    {
        if (_connection.State == System.Data.ConnectionState.Open)
            _connection.Close();
    }

    public void WriteEmployeeAccounts(string user, string password)
    {
        if (_connection.State == System.Data.ConnectionState.Closed)
            _connection.Open();
        string sql = "insert into users (user,password) values(" + user + "," + password + ")";
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataAdapter adapter = new MySqlDataAdapter();
        adapter.InsertCommand = command;
        adapter.InsertCommand.ExecuteNonQuery();
        command.Dispose();
        _connection.Close();
    }
    
    public void WriteSourseMessageAccountAccounts(string user, string password, string soursemessage)
    {
        if (_connection.State == System.Data.ConnectionState.Closed)
            _connection.Open();
        string sql = "insert into SourceMessages (user,password,soursemessage) values(" + user + "," + password + "," + soursemessage + ")";
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataAdapter adapter = new MySqlDataAdapter();
        adapter.InsertCommand = command;
        adapter.InsertCommand.ExecuteNonQuery();
        command.Dispose();
        _connection.Close();
    }

    public List<string> SyncEmployeeAccounts()
    {
        if (_connection.State == System.Data.ConnectionState.Closed)
            _connection.Open();
        List<string> output = new List<string>();
        string sql = "select user,password from users";
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader dataReader = command.ExecuteReader();
        while (dataReader.Read())
        {
            output.Add((string)dataReader.GetValue(0));
            output.Add((string)dataReader.GetValue(1));
        }

        return output;
    }
    
    public List<string> SyncSourseMessageAccounts()
    {
        if (_connection.State == System.Data.ConnectionState.Closed)
            _connection.Open();
        List<string> output = new List<string>();
        string sql = "select user,password,soursemessage from SourseMessages";
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader dataReader = command.ExecuteReader();
        while (dataReader.Read())
        {
            output.Add((string)dataReader.GetValue(0));
            output.Add((string)dataReader.GetValue(1));
            output.Add((string)dataReader.GetValue(2));
        }

        return output;
    }

    public void Dispose()
    {
        _connection.Dispose();
    }
}
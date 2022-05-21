public class CheckButton
{
    private MessageBox _outputMessageBox;
    private MessageBox _passportTextBox;
    private DatabaseController _databaseController;

    public void OnClick()
    {
        string result;

        if (_passportTextBox.Text.Trim() == "")
        {
            _outputMessageBox.Show("Введите серию и номер паспорта");

            return;
        }
        string userInput = GetUserInput();

        if (ValidatePassport(userInput) == false)
        {
            _outputMessageBox.Text = "Неверный формат серии или номера паспорта";
            
            return;
        }
        string sqlRequest = GenerateSQLRequest(userInput);
        _databaseController.TryCreateSQLiteConnection("db.sqlite");
        DataTable dataTable = _databaseController.GetDataTableAndCloseConnection(sqlRequest);

        if (dataTable == null)
        {
            _outputMessageBox.Text = $"Паспорт «{userInput}» в списке участников дистанционного голосования НЕ НАЙДЕН";

            return;
        }
        if (CheckAccessByPassport(dataTable))
            result = "ПРЕДОСТАВЛЕН";
        else
            result = "НЕ ПРЕДОСТАВЛЕН";

        _outputMessageBox.Text = $"По паспорту «{userInput}» доступ к биллютеню на дистанционном электронном голосовании {result}"
    }

    private bool ValidatePassport(string userInput)
    {
        int minLength = 10;

        return userInput.Length < minLength ? false : true;
    }

    private string GetUserInput() => _passportTextbox.Text.Trim().Replace(" ", string.Empty);

    private bool CheckAccessByPassport(DataTable dataTable) => Convert.ToBoolean(dataTable.Rows[0].ItemArray[1]) ? true : false;

    private string GenerateSQLRequest(string passportNum) => string.Format($"select * from passports where num='{passportNum}' limit 1;", (object)Form1.ComputeSha256Hash(rawData));
}

public class DatabaseController
{
    private SQLiteConnection _activeConnection;

    public bool TryCreateSQLiteConnection(string dbName)
    {
        try
        {
            string connectionString = GenerateSQLiteConnectionString(dbName);
            var connection = new SQLiteConnection(connectionString);
            var sqLiteDataAdapter = new SQLiteDataAdapter(new SQLiteCommand(connectionString, connection));

            connection.Open();
            _activeConnection = connection;

            return true;
        }
        catch (SQLiteException exception)
        {
            if (exception.ErrorCode != 1)
                return false;

            MessageBox.Show($"Файл {dbName} не найден. Положите файл в папку вместе с exe.");

            return false;
        }
    }

    public DataTable GetDataTableAndCloseConnection(string sqLiteCommand)
    {
        var dataTable = new DataTable();
        var sqLiteDataAdapter = new SQLiteDataAdapter(new SQLiteCommand(sqLiteCommand, connection));

        sqLiteDataAdapter.Fill(dataTable);
        _activeConnection.Close();

        if (dataTable.Columns.Count == 0)
            return null;

        return dataTable;
    }

    private string GenerateSQLiteConnectionString(string dbName) => string.Format($"Data Source={Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\{dbName}");
}
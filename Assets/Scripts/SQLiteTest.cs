using System.Data;
using Mono.Data.SqliteClient;
using UnityEngine;
using UnityEngine.UI;

public class SQLiteTest : MonoBehaviour
{
    public Text debugText;
    private readonly string path = $"URI=file:{Application.streamingAssetsPath}/Database/Pecados.db";
    private IDbConnection connection;
    private IDbCommand command;
    private IDataReader reader;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Ran");
        connection = new SqliteConnection(path);
        connection.Open();

        Debug.Log("Connection open");
        command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM pecados";

        Debug.Log("Executing command");
        reader = command.ExecuteReader();

        while (reader.Read())
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                var result = reader.GetString(i);
                debugText.text += result + ", ";
                Debug.Log(result);
            }
        }
    }
}

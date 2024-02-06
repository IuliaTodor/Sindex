using System.Data;
using Mono.Data.SqliteClient;
using UnityEngine;

public class SQLiteTest : MonoBehaviour
{
    private readonly string path = "URI=file:Assets/Database/Pecados.db";
    private IDbConnection connection;
    private IDbCommand command;
    private IDataReader reader;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Ran");
        connection = new SqliteConnection(path);
        connection.Open();

        command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM sqlite_master";

        reader = command.ExecuteReader();

        while (reader.Read())
        {
            Debug.LogWarning(reader.GetString(0));
        }
    }
}

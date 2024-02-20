using System.Data;
using Mono.Data.SqliteClient;
using UnityEngine;
using UnityEngine.UI;

public class SQLManager : MonoBehaviour
{
    public Text debugText;
    [HideInInspector] public SQLManager Instance;
    private readonly string path = $"URI=file:{Application.streamingAssetsPath}/Database/Pecados.db";
    private IDbConnection connection;
    private IDbCommand command;
    private IDataReader reader;

    void Start()
    {
        // Only one SQLManager
        if (!Instance) { Instance = this; }
        else { Destroy(gameObject); return; }
        DontDestroyOnLoad(gameObject);

        // Default setup
        connection = new SqliteConnection(path);
        connection.Open();

        Query(
            "SELECT P.Pecado_ID, P.Nombre, P.Pecado, P.Descripcion, E.Nombre, E2.Nombre, P.Area, P.Fortaleza, P.Debilidad" +
            " FROM Pecados P JOIN Elementos E ON P.Elemento1 = E.Elemento_ID" +
            " JOIN Elementos E2 ON P.Elemento2 = E2.Elemento_ID" +
            " JOIN Areas A ON P.Area = A.Area_ID;"
        );
    }

    // Hacer una consulta
    public void Query(string query = "SELECT * FROM pecados")
    {
        // Create the command
        command = connection.CreateCommand();
        command.CommandText = query;

        // Close and open new reader
        reader?.Close();
        reader = command.ExecuteReader();

        // Read the contents
        while (reader.Read())
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                var result = reader.GetString(i);
                debugText.text += result + ", ";
            }
        }
    }
}

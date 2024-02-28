using System;
using System.Data;
using Mono.Data.SqliteClient;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SQLManager : MonoBehaviour
{
    [HideInInspector] public SQLManager Instance;
    
    private readonly string path = $"URI=file:{Application.streamingAssetsPath}/Database/Pecados.db";
    private IDbConnection connection;
    private IDbCommand command;
    private IDataReader reader;
    private Text outputText;
    private Toggle useIDToggle;

    void Start()
    {
        // Only one SQLManager
        if (!Instance) { Instance = this; }
        else { Destroy(gameObject); return; }
        DontDestroyOnLoad(gameObject);

        // Try get new references on scene changes
        SceneManager.sceneLoaded += OnSceneLoaded;
        TryGetNewReferences();

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

    private void TryGetNewReferences()
    {
        outputText = GameObject.Find("Output Text")?.GetComponent<Text>();
        useIDToggle = GameObject.Find("ID Toggle")?.GetComponent<Toggle>();
        Debug.LogWarning($"{outputText.name}, {useIDToggle.name}");
    }

    // Hacer una consulta
    public void Query(string query = "SELECT * FROM Pecados")
    {
        // Create the command
        command = connection.CreateCommand();
        command.CommandText = query;

        // Close and open new reader
        reader?.Close();
        reader = command.ExecuteReader();

        // Read the contents
        outputText.text = string.Empty;
        while (reader.Read())
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                var result = reader.GetString(i);
                outputText.text += result + ", ";
            }
        }
    }

    // Input field
    public void SearchInput(Text input = default)
    {
        Debug.Log(useIDToggle.isOn);
        if (input.text.Trim() == string.Empty) return;
        Query($"SELECT * FROM Pecados P WHERE (P.Pecado_ID = '{input.text}' AND {useIDToggle.isOn} IS TRUE) OR P.Nombre = '{input.text}' OR (P.Area = '{input.text}' AND {useIDToggle.isOn} IS FALSE)");
    }

    // Gets new references once you change scenes
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        TryGetNewReferences();
    }
}

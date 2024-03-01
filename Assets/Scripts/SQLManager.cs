using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Mono.Data.SqliteClient;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SQLManager : MonoBehaviour
{
    [HideInInspector] public static SQLManager Instance;
    [HideInInspector] public int sinSelectedFromMenu;
    
    private string path = $"URI=file:{Application.streamingAssetsPath}/Database/Pecados.db";
    private IDbConnection connection;
    private IDbCommand command;
    private IDataReader reader;
    private Text outputText;

    void Start()
    {
        // Only one SQLManager
        if (!Instance) { Instance = this; }
        else { Destroy(gameObject); return; }
        DontDestroyOnLoad(gameObject);

        // Try get new references on scene changes
        SceneManager.sceneLoaded += OnSceneLoaded;
        sinSelectedFromMenu = 1;
        TryGetNewReferences();

        try
        {
            // Default setup
            connection = new SqliteConnection(path);
            connection.Open();
            
        } catch (Exception e)
        {
            outputText.text = e.Message;
            try
            {
                // Default setup
                path = $"URI=file:{Application.persistentDataPath}/Database/Pecados.db";

                Directory.CreateDirectory($"{Application.persistentDataPath}/Database");
                File.Copy($"URI=file:{Application.streamingAssetsPath}/Database/Pecados.db", path);

                connection = new SqliteConnection($"URI=file:{Application.persistentDataPath}/Database/Pecados.db".Replace("/", "\\"));
                connection.Open();

            } catch (Exception e2) { outputText.text = e2.Message; }
        }

        Query(
            "SELECT P.Pecado_ID, P.Nombre, P.Pecado, P.Descripcion, E.Nombre, E2.Nombre, P.Area, P.Fortaleza, P.Debilidad" +
            " FROM Pecados P JOIN Elementos E ON P.Elemento1 = E.Elemento_ID" +
            " JOIN Elementos E2 ON P.Elemento2 = E2.Elemento_ID" +
            " JOIN Areas A ON P.Area = A.Area_ID;"
        );
    }

    private void TryGetNewReferences()
    {
        outputText = GameObject.Find("Output Text").GetComponent<Text>();
    }

    // Hacer una consulta
    public List<string> Query(string query = "SELECT * FROM Pecados")
    {
        // Create the command
        command = connection.CreateCommand();
        command.CommandText = query;

        // Close and open new reader
        reader?.Close();
        reader = command.ExecuteReader();

        // Read the contents
        List<string> data = new();
        outputText.text = string.Empty;

        while (reader.Read())
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                var result = reader.GetString(i);
                outputText.text += result + ", ";
                data.Add(result);
            }
        }

        return data;
    }

    // Input field
    public List<string> SearchInput(string input, bool useID = true)
    {
        if (input.Trim() == string.Empty) return null; 
        return Query($"SELECT * FROM Pecados P WHERE (P.Pecado_ID = '{input}' AND {useID} IS TRUE) OR P.Nombre LIKE  '%{input}%' OR (P.Area = '{input}' AND {useID} IS FALSE)");
    }

    // Gets new references once you change scenes
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        TryGetNewReferences();
    }
}

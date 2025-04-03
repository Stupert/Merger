using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JsonHandler : MonoBehaviour
{
    // The file path where the JSON file will be stored.
    private string filePath;
    public Board board;
    public TimeController timeController;
    public EnergyManager energyManager;

    private void Start()
    {
        // Set the file path in the application's persistent data path
        filePath = Application.persistentDataPath + "/playerData.json";

        // Example of creating a new player and saving it to JSON
        PlayerData player = new PlayerData(board.UIDData, timeController.GetTime(), energyManager.energy);

        // Optionally, load the data from the JSON file
        
    }

    // Save data to a JSON file
    private void SaveToJSON(PlayerData playerData)
    {
        board.UpdateCellUIDData();
        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(filePath, json);
        Debug.Log("Player data saved");
    }

    // Load data from a JSON file
    private PlayerData LoadFromJSON()
    {
        if (File.Exists(filePath))
        {
            Debug.Log("Game Loading");
            string json = File.ReadAllText(filePath);
            PlayerData loadedPlayer = JsonUtility.FromJson<PlayerData>(json);
            return loadedPlayer;
        }
        else
        {
            Debug.LogError("File not found: " + filePath);
            return null;
        }
    }

    #region DebugKeybinds
    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.B)) //load 
        {
            //LoadFromJSON();
            PlayerData loadedPlayer = LoadFromJSON();
            if (loadedPlayer != null)
            {
                board.LoadData(loadedPlayer); //load board
                timeController.UpdateTime(loadedPlayer.epochTime, loadedPlayer.energy); //load time
            }
        }

        if (Input.GetKeyDown(KeyCode.V)) //save
        {
            PlayerData player = new PlayerData(board.UIDData, timeController.GetTime(), energyManager.energy);
            SaveToJSON(player);
        }
        
    }
    #endregion
}

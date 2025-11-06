using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomController : MonoBehaviour
{
    public CheckpointTrigger checkpoint;
    public WaveController wave;

    // --- CHANGE 1: Make this a public property ---
    // Change this line:
    // private bool isCleared = false;
    // To this:
    public bool IsCleared { get; private set; } = false;


    [Header("Level Exit Settings")]
    [Tooltip("Check this ONLY for the final room (e.g., Room6)")]
    public bool isFinalRoom = false;

    [Tooltip("The name of the scene to load if this is the final room")]
    public string nextLevelName;

    private void Start()
    {
        checkpoint.room = this;
        wave.room = this;
    }

    public void OnPlayerEnter()
    {
        // --- CHANGE 2: Use the new property ---
        if (IsCleared) return;
        wave.StartWave();
    }

    public void OnWaveCleared()
    {
        // --- CHANGE 3: Use the property and REMOVE the loading logic ---
        IsCleared = true;
        Debug.Log("Room has been cleared!");

        // --- DELETE THIS WHOLE BLOCK ---
        // if (isFinalRoom)
        // {
        //     LoadNextScene();
        // }
        // ---------------------------------

        // (Optional: You could play a "portal open" sound here)
    }

    // --- CHANGE 4: Add this new public function ---
    // The new portal script will call this function.
    public void AttemptToExit()
    {
        // Only load the scene if this IS the final room AND it has been cleared.
        if (isFinalRoom && IsCleared)
        {
            LoadNextScene();
        }
        else if (isFinalRoom && !IsCleared)
        {
            // Optional: Show a "defeat all enemies" message
            Debug.Log("You must defeat all enemies first!");
        }
    }

    // This function stays exactly the same.
    private void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextLevelName))
        {
            Debug.Log("Final room cleared! Loading level: " + nextLevelName);
            SceneManager.LoadSceneAsync(nextLevelName);
        }
        else
        {
            Debug.LogError("isFinalRoom is checked, but no nextLevelName was set!", this);
        }
    }
}
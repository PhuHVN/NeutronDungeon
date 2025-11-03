using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // <-- 1. ADD THIS LINE

public class RoomController : MonoBehaviour
{
    public CheckpointTrigger checkpoint;
    public WaveController wave;

    private bool isCleared = false;

    [Header("Level Exit Settings")]
    [Tooltip("Check this ONLY for the final room (e.g., Room6)")]
    public bool isFinalRoom = false;

    // 2. ADD THIS LINE to tell it *which* level to load
    [Tooltip("The name of the scene to load if this is the final room")]
    public string nextLevelName;

    private void Start()
    {
        checkpoint.room = this;
        wave.room = this;
    }

    public void OnPlayerEnter()
    {
        if (isCleared) return;
        wave.StartWave();
    }

    public void OnWaveCleared()
    {
        isCleared = true;
        Debug.Log("Room has been cleared!"); // Good for testing

        // --- 3. ADD THIS LOGIC ---
        // Check if this specific room is the final one
        if (isFinalRoom)
        {
            LoadNextScene();
        }
    }

    // --- 4. ADD THIS NEW FUNCTION ---
    private void LoadNextScene()
    {
        // Check if the name field isn't empty
        if (!string.IsNullOrEmpty(nextLevelName))
        {
            Debug.Log("Final room cleared! Loading level: " + nextLevelName);
            SceneManager.LoadScene(nextLevelName);
        }
        else
        {
            // Error message if you forgot to set the name in the Inspector
            Debug.LogError("isFinalRoom is checked, but no nextLevelName was set!", this);
        }
    }
}
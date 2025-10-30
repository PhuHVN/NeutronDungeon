using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public CheckpointTrigger checkpoint;
    public WaveController wave;

    private bool isCleared = false;

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
    }
}

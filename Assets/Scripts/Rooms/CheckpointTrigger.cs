using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    [HideInInspector] public RoomController room;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            room.OnPlayerEnter();
            gameObject.SetActive(false);
        }
    }
}

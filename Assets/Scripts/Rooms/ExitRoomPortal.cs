using UnityEngine;

public class ExitRoomPortal : MonoBehaviour
{
    [Tooltip("Drag the Room object that this portal belongs to (e.g., Room6)")]
    public RoomController roomController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player entered the portal
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player entered the exit portal.");
            roomController.AttemptToExit();

            // Ask the RoomController if we are allowed to leave
            if (roomController != null)
            {
                roomController.AttemptToExit();
            }
            else
            {
                Debug.LogError("Portal is missing its RoomController!", this);
            }
        }
    }
}

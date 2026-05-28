using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public string playerTag = "Player";
    bool alreadyTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (alreadyTriggered) return;
        if (!other.CompareTag(playerTag)) return;

        alreadyTriggered = true;

        if (PuzzleManager.Instance != null)
        {
            Debug.Log("DoorTrigger: Player walked through door, closing door.");
            PuzzleManager.Instance.SetPlayerEnteredRoom();
        }
    }
}
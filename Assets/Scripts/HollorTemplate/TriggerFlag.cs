using UnityEngine;

public class TriggerFlag : MonoBehaviour
{
    // This flag will be true when the player is inside the trigger, false otherwise
    public bool isPlayerInside = false;

    void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true; // Set the flag to true
            Debug.Log("Player entered the trigger area.");
            EntranceUIManager.OnEntranceUI.Invoke();
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the object exiting the trigger is the player
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false; // Set the flag to false
            Debug.Log("Player exited the trigger area.");
        }
    }
}

using UnityEngine;

public class Coin : Pickup
{
    UIManager uiManager;

    void Start()
    {
        uiManager = Object.FindFirstObjectByType<UIManager>(); // Updated to use FindFirstObjectByType
    }

    protected override void PickupItem()
    {
        if(uiManager.gameOver) return; // If the game is over, do not allow pickup
        Destroy(this.gameObject); // Destroys the pickup object
        if (uiManager != null)
        {
            uiManager.UpdateScore(100); // Update the score in the UI
            //Debug.Log("Add 100 coins!"); // Log message for debugging
        }
        else
        {
            Debug.LogWarning("UIManager not found!"); // Log a warning if UIManager is not found
        }
    }
}

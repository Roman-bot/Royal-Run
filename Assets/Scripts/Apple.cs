using UnityEngine;

public class Apple:Pickup
{
    LevelGenerator levelGenerator;

    void Start()
    {
        levelGenerator = GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>();
    }

    protected override void PickupItem()
    {
        //Debug.Log("Power up!"); // Log message for debugging
        levelGenerator.ChunkSpeedUP(); // Calls the method to speed up the level generation
        Destroy(this.gameObject); // Destroys the pickup object
    }
}

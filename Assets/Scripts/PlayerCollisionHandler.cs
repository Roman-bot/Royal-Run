using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    LevelGenerator levelGenerator;

    [SerializeField]
    UIManager uiManager;


    private void Start()
    {
        levelGenerator = GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);
        if (collision.gameObject.tag == "Obstacle")
        {
            animator.SetTrigger("Stumble");
            levelGenerator.ChunkSlowDawn();
            // Additional logic for handling player collision with obstacles can be added here
        }
        
        if (collision.gameObject.tag == "Checkpoint")
        {
           Debug.Log("Checkpoint reached: " + collision.gameObject.name);
            uiManager.timer +=5f; // Increase the timer by 5 seconds when a checkpoint is reached
            // Additional logic for handling player collision with checkpoints can be added here
        }

    }
}

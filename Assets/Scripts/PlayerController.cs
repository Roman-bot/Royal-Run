using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 move;
    Rigidbody rb;
    float moveSpeed = 10f;
    
    float xRange = 3f;
    float yRange = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(InputAction.CallbackContext inputAction)
    {
        move = inputAction.ReadValue<Vector2>();
        //Debug.Log("Movement " + move);
      
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        Vector3 currentPosition = rb.position;
        Vector3 directionMovement = new Vector3 (move.x, 0.0f, move.y);
        Vector3 newPosition = currentPosition + directionMovement * moveSpeed * Time.fixedDeltaTime;


        // Clamp the x position to the specified range
        newPosition.x = Mathf.Clamp(newPosition.x, -xRange, xRange);
        newPosition.y = Mathf.Clamp(newPosition.y, -yRange, yRange);

        // Debug.Log("New Position: " + newPosition);

        rb.MovePosition(newPosition);
    }
   
}

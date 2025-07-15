using UnityEngine;
using UnityEngine.UIElements;

public abstract class Pickup : MonoBehaviour
{
    protected abstract void PickupItem();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Pickup collected by: " + other.gameObject.name);
            PickupItem();
        }
       
    }

    private void Update()
    {
        this.transform.Rotate(0, 100 * Time.deltaTime, 0); // Rotates the pickup object around the Y-axis
    }
}

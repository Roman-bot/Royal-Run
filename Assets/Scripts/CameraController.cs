using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    ParticleSystem ParticleSystem;

    Unity.Cinemachine.CinemachineCamera vcam;

    private void Start()
    {
        // Updated to use the recommended method for finding objects  
        vcam = Object.FindFirstObjectByType<Unity.Cinemachine.CinemachineCamera>();
    }

    public void ChangeCameraFOV(float speedAmount)
    {
        StartCoroutine(ChangeCameraRoutine(speedAmount));

        if (ParticleSystem != null && speedAmount > 0)
        {
            ParticleSystem.Play(); // Play the particle system when changing camera FOV  
        }
        else
        {
            Debug.LogWarning("ParticleSystem is not assigned in the inspector.");
        }
    }

    IEnumerator ChangeCameraRoutine(float speedAmount)
    {
        float duration = 1f; // Duration of the transition  
        float startFOV = vcam.Lens.FieldOfView;

        float targetFOV = Mathf.Clamp(startFOV + speedAmount * 5, 30f, 90f); // Adjust FOV limits as needed  

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            vcam.Lens.FieldOfView = Mathf.Lerp(startFOV, targetFOV, t);
            yield return null;
        }

        vcam.Lens.FieldOfView = targetFOV; // Ensure final value is set  
        //Debug.Log("Camera FOV changed to: " + vcam.Lens.FieldOfView);
    }
}

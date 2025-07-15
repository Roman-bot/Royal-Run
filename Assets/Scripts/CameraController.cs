using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    ParticleSystem ParticleSystem;

    Cinemachine.CinemachineVirtualCamera vcam;


    private void Start()
    {
        // Updated to use the recommended method for finding objects  
        vcam = Object.FindFirstObjectByType<Cinemachine.CinemachineVirtualCamera>();
    }

    public void ChangeCameraFOV(float speedAmount)
    {
        StartCoroutine(ChangeCameraRoutine(speedAmount));

        if(ParticleSystem != null && speedAmount >0)
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
        float startFOV = vcam.m_Lens.FieldOfView;

        float targetFOV = Mathf.Clamp(startFOV + speedAmount * 5, 30f, 90f); // Adjust FOV limits as needed  

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            vcam.m_Lens.FieldOfView = Mathf.Lerp(startFOV, targetFOV, t);
            yield return null;
        }

        vcam.m_Lens.FieldOfView = targetFOV; // Ensure final value is set  
        //Debug.Log("Camera FOV changed to: " + vcam.m_Lens.FieldOfView);
    }
}

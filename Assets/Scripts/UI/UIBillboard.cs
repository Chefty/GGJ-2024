using UnityEngine;

public class UIBillboard : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        // Cache the reference to the main camera to improve performance
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Check if the main camera is set
        if (mainCamera == null) return;

        // Make the text face the camera
        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
    }
}

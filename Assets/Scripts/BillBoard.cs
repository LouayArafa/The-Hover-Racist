using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] private Camera Camera;

    private void Start()
    {

        if (Camera == null)
        {
            Debug.LogError(" camera not found.");
        }
    }

    private void Update()
    {
        if (Camera != null)
        {
            // Make the canvas face the camera
            transform.LookAt(transform.position + Camera.transform.rotation * Vector3.forward,
                Camera.transform.rotation * Vector3.up);
        }
    }
}

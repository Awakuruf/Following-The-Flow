using UnityEngine;
using UnityEngine.InputSystem; 
public class PlayerCam : MonoBehaviour
{
    public float sensX = 100f;
    public float sensY = 100f;

    public Transform orientation;

    float xRotation;
    float yRotation;

    private Vector2 lookInput;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Only update if there's input (so it doesn't jitter at zero)
        lookInput = Mouse.current.delta.ReadValue(); // new input system
        float mouseX = lookInput.x * Time.deltaTime * sensX;
        float mouseY = lookInput.y * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}

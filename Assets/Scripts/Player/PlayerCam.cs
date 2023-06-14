using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform mainCamera;
    [SerializeField] private float sensitivity;

    private float yRotation, xRotation;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (UIManager.Instance.foodInatorUIActive) return;
        GetInput();
        MoveCam();
    }

    private void GetInput()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * sensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensitivity;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -89, 89);
    }

    private void MoveCam()
    {
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

}


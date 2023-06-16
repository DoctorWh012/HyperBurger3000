using UnityEngine;
using System.IO;

public class PlayerCam : MonoBehaviour
{
    public static PlayerCam Instance;

    [Header("Components")]
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform mainCamera;
    private float sensitivity;

    private float yRotation, xRotation;

    private void Awake()
    {
        Instance = this;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Start()
    {
        GetSensitivity();
    }

    private void Update()
    {
        if (RigidBodyPlayerMovement.Instance.movementFreeze) return;
        GetInput();
        MoveCam();
    }

    public void GetSensitivity()
    {
        string json = File.ReadAllText($"{Application.dataPath}/PlayerPrefs.json");
        PlayerPreferences playerPrefs = JsonUtility.FromJson<PlayerPreferences>(json);
        sensitivity = playerPrefs.sensitivity / 10;
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


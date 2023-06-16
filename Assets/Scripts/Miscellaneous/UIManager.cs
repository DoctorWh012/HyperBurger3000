using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Components")]
    [SerializeField] private GameObject foodInatorUI;
    [SerializeField] private GameObject endGameUi;
    [SerializeField] private GameObject settingsUI;
    [SerializeField] private GameObject crosshair;
    [SerializeField] public TextMeshProUGUI uiBottomMessage;
    [SerializeField] public TextMeshProUGUI endingPlatesDone;
    [SerializeField] public TextMeshProUGUI endingScore;
    [SerializeField] private Button[] orderButtons;


    public bool foodInatorUIActive = false;


    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EnableDisableSettingsMenu();
        }

    }

    private void EnableDisableSettingsMenu()
    {
        settingsUI.SetActive(!settingsUI.activeSelf);
        RigidBodyPlayerMovement.Instance.FreezePlayerMovement(settingsUI.activeSelf);
        if (settingsUI.activeSelf)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void EnableDisableFoodInatorUI(bool buttonsState)
    {
        foodInatorUIActive = !foodInatorUIActive;
        crosshair.SetActive(!foodInatorUIActive);
        foodInatorUI.SetActive(foodInatorUIActive);
        Cursor.visible = foodInatorUIActive;
        if (foodInatorUIActive)
        {
            RigidBodyPlayerMovement.Instance.FreezePlayerMovement(true);
            UIManager.Instance.uiBottomMessage.SetText("");
            Cursor.lockState = CursorLockMode.None;
            DisableEnableOrderButtons(buttonsState);
        }
        else
        {
            RigidBodyPlayerMovement.Instance.FreezePlayerMovement(false);
            UIManager.Instance.uiBottomMessage.SetText("Press [E] To Access The FoodInator3000 Menu");
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void DisableEnableOrderButtons(bool state)
    {
        for (int i = 0; i < orderButtons.Length; i++)
        {
            orderButtons[i].enabled = state;
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ActivateEndGameUi()
    {
        foodInatorUI.SetActive(false);
        endGameUi.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}

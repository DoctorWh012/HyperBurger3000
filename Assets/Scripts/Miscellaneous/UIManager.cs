using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Components")]
    [SerializeField] private GameObject foodInatorUI;
    [SerializeField] private GameObject crosshair;
    [SerializeField] public TextMeshProUGUI uiBottomMessage;
    [SerializeField] private Button[] orderButtons;


    public bool foodInatorUIActive = false;

    private void Awake()
    {
        Instance = this;
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
}
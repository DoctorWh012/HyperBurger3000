using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject[] otherMenus;

    private void Start()
    {
        ActivateMenu(mainMenu);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Kitchen");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ActivateMenu(GameObject menu)
    {
        DisableAllMenus();
        menu.SetActive(true);
    }

    public void DisableAllMenus()
    {
        mainMenu.SetActive(false);
        for (int i = 0; i < otherMenus.Length; i++) { otherMenus[i].SetActive(false); }
    }
}

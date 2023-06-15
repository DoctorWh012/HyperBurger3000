using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Components")]
    [SerializeField] private DeliverVerifier deliverVerifier;
    [SerializeField] private ScriptableHamburgers[] hamburgers;

    [Header("Settings")]
    [SerializeField] int correctDeliveryScoreIncrease;
    [SerializeField] int wrongDeliveryScoreDecrease;

    private int hamburgersDone;
    private int score;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GetNewRecipe();
    }

    private void GetNewRecipe()
    {
        ScriptableHamburgers newHamburger = hamburgers[Random.Range(0, hamburgers.Length)];
        TVDisplay.Instance.UpdateDisplayRecipe(newHamburger);

        deliverVerifier.requestedRecipe = newHamburger.hamburgerRecipe;
    }

    public void DeliveredCorrect()
    {
        hamburgersDone++;
        score += correctDeliveryScoreIncrease;
        TVDisplay.Instance.preparedQntTxt.SetText(hamburgersDone.ToString());
        TVDisplay.Instance.scoreValTxt.SetText(score.ToString());
        GetNewRecipe();
    }

    public void DeliveredWrong()
    {
        score -= wrongDeliveryScoreDecrease;
        TVDisplay.Instance.scoreValTxt.SetText(score.ToString());
    }
}

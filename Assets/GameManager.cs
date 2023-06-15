using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Components")]
    [SerializeField] private DeliverVerifier deliverVerifier;
    [SerializeField] private ScriptableHamburgers[] hamburgers;

    [Header("Settings")]
    [SerializeField] float correctDeliveryScoreIncrease;
    [SerializeField] float wrongDeliveryScoreDecrease;

    private int hamburgersDone;
    private float score
    {
        get { return score; }
        set
        {
            score = value;

        }
    }

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
        TVDisplay.Instance.preparedQntTxt.SetText(hamburgersDone.ToString());
        GetNewRecipe();
    }

    public void DeliveredWrong()
    {

    }
}

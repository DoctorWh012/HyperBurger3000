using UnityEngine;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Components")]
    [SerializeField] private DeliverVerifier deliverVerifier;
    [SerializeField] private ScriptableHamburgers[] hamburgers;
    [SerializeField] private PlayableDirector playableDirector;

    [Header("Settings")]
    [SerializeField] float totalCountdownTime;
    [SerializeField] int correctDeliveryScoreIncrease;
    [SerializeField] int wrongDeliveryScoreDecrease;

    public bool gameOngoing;

    private float _remainingTime;
    private float remainingTime
    {
        get { return _remainingTime; }
        set
        {
            _remainingTime = value;
            TVDisplay.Instance.remainingTimeValTxt.SetText(_remainingTime.ToString("0"));
            if (_remainingTime <= 0) EndGame();
        }
    }
    private int hamburgersDone;
    private int score;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        playableDirector.stopped += DirectorFinishedCutscene;
        remainingTime = totalCountdownTime;
        GetNewRecipe();
    }

    private void Update()
    {
        if (!gameOngoing)
        {
            if (playableDirector.state == PlayState.Playing && Input.GetKeyDown(KeyCode.Space)) { playableDirector.Stop(); }
            return;
        }
        remainingTime -= Time.deltaTime;
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

    private void DirectorFinishedCutscene(PlayableDirector director)
    {
        if (!director == playableDirector) return;
        StartGame();
    }

    private void EndGame()
    {
        gameOngoing = false;
        UIManager.Instance.ActivateEndGameUi();
        UIManager.Instance.endingPlatesDone.SetText(hamburgersDone.ToString());
        UIManager.Instance.endingScore.SetText(score.ToString());
        RigidBodyPlayerMovement.Instance.FreezePlayerMovement(true);
    }

    private void StartGame()
    {
        gameOngoing = true;
        RigidBodyPlayerMovement.Instance.FreezePlayerMovement(false);
    }
}

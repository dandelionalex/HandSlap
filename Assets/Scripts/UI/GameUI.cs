using DG.Tweening;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text score;
    
    [SerializeField]
    private GameObject multipliers;
    
    [SerializeField]
    private GameObject[] multiplierIcons;

    private ScoreManager scoreManager;

    private Sequence animateMultiplier;
    private Sequence animateScore;

    private void Awake()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }
    
    private void OnEnable()
    {
        score.SetText("1");
        ResetMultiplier();
        scoreManager.OnMultiplyerUpdated += OnMultiplierUpdated;
    }

    private void OnDisable()
    {
        scoreManager.OnMultiplyerUpdated -= OnMultiplierUpdated;
    }

    private int cachedScore = 0;

    private void Update()
    {
        if (cachedScore == scoreManager.Score)
            return;
        
        cachedScore = scoreManager.Score;
        score.SetText(scoreManager.Score.ToString());
    }

    private void OnMultiplierUpdated(int multiplierID)
    {
        multiplierID--; // original myltiplier starts with 1
        if(multiplierID >=multiplierIcons.Length || multiplierID <0 )
            return;
        
        foreach (GameObject m in multiplierIcons)
        {
            m.SetActive(false);
        }
        
        multiplierIcons[multiplierID].SetActive(true);
        animateMultiplier.SetLoops(1, LoopType.Restart);
        animateMultiplier.Restart();
    }

    public void StartGame()
    {
        animateMultiplier = DOTween.Sequence();
        animateMultiplier.Append(multipliers.transform.DOScale(1.3f, 0.1f));
        animateMultiplier.Append(multipliers.transform.DOScale(1, 0.1f));
        animateMultiplier.SetAutoKill(false);
    }
    
    private void ResetMultiplier()
    {
        foreach (GameObject m in multiplierIcons)
        {
            m.SetActive(false);
        }
        multiplierIcons[0].SetActive(true);
    }
}

using System;
using System.Collections;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] 
    private AudioSource increaseMultiplierSound;// Move to soundmanager and add to id pitch functionality

    private GameController gameController;

    public int Score { get; private set; }
    private int multiplier = 1;
    public Action<int> OnMultiplyerUpdated;
    
    private int maxMultiplier = 5; // from config
    private int scoreIncreaseRate = 1;//from config
    
    private static ScoreManager inventory;
    private bool isGameStarted;
    
    private void Awake()
    {
        if (inventory != null)
        {
            Destroy(this.gameObject);
            return;
        }
        
        DontDestroyOnLoad(this);
        inventory = this;
    }
    
    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        gameController.OnGameStarted += OnGameStart;
        gameController.OnGameFinished += OnGameFinished;
        gameController.OnGamePaused += OnGamePaused;
    }

    public void FixLeakage(bool isPerfect)
    {
        if (multiplier >= maxMultiplier)
            return;
            
        multiplier += 1;
        if(OnMultiplyerUpdated != null)
            OnMultiplyerUpdated.Invoke(multiplier);

        var p = 1f + multiplier * 0.1f;
        increaseMultiplierSound.pitch = p;
        increaseMultiplierSound.Play();
    }

    public void MissHit()
    {
        multiplier = 1;
        if(OnMultiplyerUpdated != null)
            OnMultiplyerUpdated.Invoke(multiplier);
    }

    public void ResetLevel()
    {
        Score = 0;
    }
    
    private void OnGameStart()
    {
        isGameStarted = true;
        multiplier = 1;
        StartCoroutine(IncreaseScore());
    }

    private void OnGameFinished()
    {
        isGameStarted = false;
    }

    private void OnGamePaused()
    {
        isGameStarted = false;
    }
    
    private IEnumerator IncreaseScore()
    {
        while (isGameStarted )
        {
            Score += multiplier;
            yield return new WaitForSeconds(1 / (scoreIncreaseRate * multiplier + 1));
        }
    }
}

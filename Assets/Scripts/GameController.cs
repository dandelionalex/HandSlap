using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    
    public Action OnGameStarted;
    public Action OnGameFinished;
    public Action OnGamePaused;
    
    private Inventory inventory;
    private ScoreManager scoreManager;

    private static GameController instanse;

    private void Awake()
    {
        if (instanse != null)
        {
            Destroy(this.gameObject);
            return;
        }

        instanse = this;
        DontDestroyOnLoad(this);
    }

    private void Start() 
    {
        inventory = FindObjectOfType<Inventory>();
        scoreManager = FindObjectOfType<ScoreManager>();
        StartLevel();
    }

    public void StartLevel()
    {
        inventory.GamesCount++;
        
        ShowLevel();
    }

    private void ShowRoom()
    {
        StartCoroutine(ShowRoomCoroutine());
    }

    private IEnumerator ShowRoomCoroutine()
    {
        if(SceneManager.GetActiveScene().name != GameSettings.ROOM_SCENE)
            yield return SceneManager.LoadSceneAsync(GameSettings.ROOM_SCENE, LoadSceneMode.Single);

        var room = FindObjectOfType<BaseRoom>();
        room.Init(() =>
        {
            ShowLevel();
        });
    }

    private void ShowWallet()
    {
        StartCoroutine(ShowWalletCoroutine());
    }

    private IEnumerator ShowWalletCoroutine()
    {
        if(SceneManager.GetActiveScene().name != GameSettings.WALLET_SCENE)
            yield return SceneManager.LoadSceneAsync(GameSettings.WALLET_SCENE, LoadSceneMode.Single);

        var coinsGenerator = FindObjectOfType<CoinsGenerator>();
        var totalCoins = scoreManager.Score;
        scoreManager.ResetLevel();
        coinsGenerator.Init(totalCoins, () =>
        {
            ShowRoom();
        });
    }
    
    private void ShowLevel()
    {
        StartCoroutine(ShowLevelCoroutine());
    }

    private IEnumerator ShowLevelCoroutine()
    {
        if(SceneManager.GetActiveScene().name != GameSettings.LEVEL_SCENE)
            yield return SceneManager.LoadSceneAsync(GameSettings.LEVEL_SCENE, LoadSceneMode.Single);
        
        var levelBehaviour = FindObjectOfType<LevelBehaviour>();
        levelBehaviour.Init(OnLevelStateChanged);
        levelBehaviour.StartLevel();
    }

    private void OnLevelStateChanged(LevelState levelState)
    {
        switch (levelState)
        {
            case LevelState.Started:
                if(OnGameStarted != null)
                    OnGameStarted.Invoke();
                break;
            case LevelState.Finished:
                if(OnGameFinished != null)
                    OnGameFinished.Invoke();
                ShowWallet();
                break;
            case LevelState.TutorialPaused:
                if(OnGamePaused != null)
                    OnGamePaused.Invoke();
                break;
        }
    }
}

using System;
using System.Collections;
using UnityEngine;

public class LevelBehaviour : BaseLevelBehaviour
{
    public override float TimePassed { get; protected set; }
    public override LevelState LevelState { get; protected set; }

    [SerializeField]    
    private GameObject environment;
    [SerializeField]
    private GameUI gameUi;//Move
    [SerializeField] 
    private GameObject endLevelConfetti;
    
    private Action<LevelState> onLevelStateChanged;
    
    private EffectsManager effectsManager;
    private GameSettings gameSettings;
    private Inventory inventory;
    private FirebaseController firebaseController;
    
    private void Awake()
    {
        effectsManager = FindObjectOfType<EffectsManager>();
        gameSettings = FindObjectOfType<GameSettings>();
        inventory = FindObjectOfType<Inventory>();
        firebaseController = FindObjectOfType<FirebaseController>();
    }

    public void StartLevel()
    {
        firebaseController.LevelStartedEvent(inventory.GamesCount);
        StartCoroutine(StartLevelCoroutine());
    }

    public override void SetTutorialPauseLevel(bool value)
    {
        if (!value)
        {
            ChangeLevelState(LevelState.Started);
            return;
        }
        
        if (gameSettings.InAquariumTutorialMode)
        {
            effectsManager.ShowEffect( new EffectData(EffectType.TapToFix,3) );
        }
        
        gameSettings.InAquariumTutorialMode = false;
        ChangeLevelState(LevelState.TutorialPaused);
    }

    private IEnumerator StartLevelCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        TimePassed = 0;
        environment.SetActive(true);
        environment.transform.position = Vector3.zero;
        gameUi.gameObject.SetActive(true);
        effectsManager.ShowEffect(new EffectData(EffectType.StartGame, 3, inventory.GamesCount));
        yield return new WaitForSeconds(2);
        gameUi.StartGame();
        ChangeLevelState(LevelState.Started);
    }
    
    private void FixedUpdate()
    {
        if(LevelState != LevelState.Started)
            return;
        
        TimePassed += Time.fixedDeltaTime;
        
        if (TimePassed >= gameSettings.LevelLength)
            EndLevel();
    }
    
    private void EndLevel()
    {
        firebaseController.LevelCompletedEvent(inventory.GamesCount);
        ChangeLevelState(LevelState.Finished);
    }
    
    public override void Init(Action<LevelState> onLevelStateChanged)
    {
        this.onLevelStateChanged = onLevelStateChanged;
        gameObject.SetActive(true);
    }

    private void ChangeLevelState(LevelState state)
    {
        LevelState = state;

        if(onLevelStateChanged != null)
            onLevelStateChanged.Invoke(state);  
    }
}

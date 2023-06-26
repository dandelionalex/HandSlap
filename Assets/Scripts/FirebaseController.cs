using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using UnityEngine;

public class FirebaseController : MonoBehaviour
{
    private const string TUTORIAL_FINISHED = "tutorial_finished";
    private const string LEVEL_COMPLETED = "level_completed";
    private const string LEVEL_STARTED = "level_started";
    private const string ROOM_UPGRADED = "room_upgraded";
    
    private DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;
    protected bool firebaseInitialized = false;

    private static FirebaseController instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void Start() 
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available) 
            {
                FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
                firebaseInitialized = true;
            } 
            else 
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    public void LevelStartedEvent(int level)
    {
        if(firebaseInitialized)
            FirebaseAnalytics.LogEvent(LEVEL_STARTED, "level", level);
    }

    public void LevelCompletedEvent(int level)
    {
        if(firebaseInitialized)
            FirebaseAnalytics.LogEvent(LEVEL_COMPLETED, "level", level);
    }

    public void RoomUpgradedEvent( int roomLevel)
    {
        if(firebaseInitialized)
            FirebaseAnalytics.LogEvent(ROOM_UPGRADED, "roomLevel", roomLevel);
    }

    public void TutorialFinishedEvent()
    {
        if(firebaseInitialized)
            FirebaseAnalytics.LogEvent(TUTORIAL_FINISHED);
    }
}
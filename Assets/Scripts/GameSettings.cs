using UnityEngine;

public class GameSettings : MonoBehaviour
{

    public static string ROOM_SCENE = "Room";
    public static string LEVEL_SCENE = "WaterLevel";
    public static string WALLET_SCENE = "Wallet";
    private static string IN_AQUARIUM_TUTORIAL_MODE = "InAquariumTutorialMode";
    private static string IN_ROOM_TUTORIAL_MODE = "InRoomTutorialMode";
    public float Difficulty { get; set; }
    public float Speed { get; set; }
    public float LevelLength { get; set; }
    public float HitDistance { get; set; }
    public float PerfectHitDistance { get; set; }
    public int FurnitureCostMultiplier { get; set; }
    public int ScoreChangeRate { get; set; }
    
    #region tutorial setting

    private bool inAquariumTutorialMode;
    public bool InAquariumTutorialMode
    {
        get
        {
            return inAquariumTutorialMode;
        }
        set
        {
            inAquariumTutorialMode = value;
            PlayerPrefs.SetInt(IN_AQUARIUM_TUTORIAL_MODE, inAquariumTutorialMode ? 1 : 0 );
        }
    }

    public float FirstTutorialLeakage { get; set; }
    public float FirstTutorialLeakagePausePos { get; set; }
    private bool inRoomTutorialMode;

    public bool InRoomTutorialMode
    {
        get { return inRoomTutorialMode; }
        set
        {
            inRoomTutorialMode = value;
            PlayerPrefs.SetInt(IN_ROOM_TUTORIAL_MODE, inRoomTutorialMode ? 1 : 0 );
        }
    }
    
    #endregion

    public int MaxProgressForRoom(int roomId)
    {
        return 7;
    }
    
    private static GameSettings instanse; //TODO: remove before production
    private void Awake()
    {
        if (instanse != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Difficulty = 0.5f;
        Speed = -0.05f;
        LevelLength = 30;
        HitDistance = 1f;
        PerfectHitDistance = 0.3f;
        ScoreChangeRate = 100;
        FurnitureCostMultiplier = 30;
        
        inRoomTutorialMode = PlayerPrefs.GetInt(IN_ROOM_TUTORIAL_MODE) == 1;
        inAquariumTutorialMode = PlayerPrefs.GetInt(IN_AQUARIUM_TUTORIAL_MODE) == 1;
        
        #region tutorial setting
        
        InAquariumTutorialMode = true;
        FirstTutorialLeakage = 12;
        FirstTutorialLeakagePausePos = 6.5f;
        InRoomTutorialMode = true;
        
        #endregion
        instanse = this;
        DontDestroyOnLoad(this);
    }
}

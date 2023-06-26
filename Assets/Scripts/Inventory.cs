using System;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private const string USER_COINS_KEY = "UserCoins";
    private const string ROOM_KEY = "RoomID";
    private const string PROGRESS_KEY = "Progress";
    private const string GAMES_COUNT = "GamesCount";

    private int userCoins = -1;

    private static Inventory inventory;

    private void Awake()
    {
        if (inventory != null)
        {
            Destroy(this.gameObject);
            return;
        }
        
        DontDestroyOnLoad(this.gameObject);
        inventory = this;
    }

    public int UserCoins
    {
        get
        {
            if (userCoins != -1)
                return userCoins;
            
            userCoins = PlayerPrefs.GetInt(USER_COINS_KEY, 0);
            return userCoins;
        }
        set
        {
            userCoins = value;
            PlayerPrefs.SetInt(USER_COINS_KEY, userCoins);
        }
    }

    private int gamesCount = -1;


    public int GamesCount
    {
        get
        {
            if (gamesCount != -1)
                return gamesCount;

            gamesCount = PlayerPrefs.GetInt(GAMES_COUNT, 0);
            return gamesCount;
        }
        set
        {
            gamesCount = value;
            PlayerPrefs.SetInt(GAMES_COUNT, gamesCount);
        }
        
    }

    private int room = -1;

    public int Room
    {
        get
        {
            if (room != -1)
                return room;
            
            room = PlayerPrefs.GetInt(ROOM_KEY, 0);
            return room;
        }
        set
        {
            room = value;
            PlayerPrefs.SetInt(ROOM_KEY, room);
        }
    }

    private int progress = -1;

    public int Progress
    {
        get
        {
            if (progress != -1)
                return progress;
            progress = PlayerPrefs.GetInt(PROGRESS_KEY, 0);
            return progress;
        }
        set 
        {  
            progress = value;
            PlayerPrefs.SetInt(PROGRESS_KEY, progress);
        }
    }
}

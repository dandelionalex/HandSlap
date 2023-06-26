using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomUI : MonoBehaviour
{
    [SerializeField] 
    private Button goToWorkButton;

    [SerializeField]
    private Button upgradeButton;

    [SerializeField] 
    private TMP_Text upgradeCost;

    [SerializeField] 
    private TMP_Text roomProgressText;

    [SerializeField] 
    private TMP_Text coinsText;
    
    [SerializeField] 
    private SlicedFilledImage roomProgressImage;
   
    [SerializeField] 
    private GameObject tutorialArrows;

    public Action<int> OnItemBought;
    
    private GameController gameController;
    private Inventory inventory;
    private GameSettings gameSettings;
    
    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        inventory = FindObjectOfType<Inventory>();
        gameSettings = FindObjectOfType<GameSettings>();
    }
    
    private void OnEnable()
    {
        UpdateUI();
        
        goToWorkButton.onClick.AddListener(() =>
        {
            gameController.StartLevel();
            gameObject.SetActive(false);
        } );
        
        var currentUpgradeCost = (inventory.Progress + 1) * gameSettings.FurnitureCostMultiplier;
        
        upgradeButton.onClick.AddListener(() =>
        {
            if (currentUpgradeCost < inventory.UserCoins)
            {
                inventory.UserCoins -= currentUpgradeCost;
                inventory.Progress++;
                UpdateUI();
                
                if(OnItemBought != null)
                   OnItemBought.Invoke(inventory.Progress);
                
                gameSettings.InRoomTutorialMode = false;
                tutorialArrows.SetActive(false);
            }
        });
    }

    private void UpdateUI()
    {
        var curentRooom = inventory.Room;
        var roomProgress = inventory.Progress;
        var maxProgress = gameSettings.MaxProgressForRoom(curentRooom);
        float progress = (float) roomProgress / maxProgress;
        roomProgressText.text = $"{(int)(progress*100)}%";
        roomProgressImage.fillAmount = progress;
        coinsText.text = $"<sprite=0>{inventory.UserCoins}";
        var currentUpgradeCost = (inventory.Progress + 1) * gameSettings.FurnitureCostMultiplier;
        upgradeCost.text = $"<sprite=0>{currentUpgradeCost}";
        if (currentUpgradeCost < inventory.UserCoins)
        {
            upgradeButton.gameObject.SetActive(true);
        } else
        {
            upgradeButton.gameObject.SetActive(false);
        }
        //upgradeButton.interactable = currentUpgradeCost < inventory.UserCoins;
        
        if (gameSettings.InRoomTutorialMode && currentUpgradeCost < inventory.UserCoins)
        {
            //tutorialArrows.SetActive(true);
        }
    }

    private void OnDisable()
    {
        goToWorkButton.onClick.RemoveAllListeners();
        upgradeButton.onClick.RemoveAllListeners();
    }
}
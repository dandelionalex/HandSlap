using System;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class Room : BaseRoom
{
    [SerializeField]
    private DOTweenAnimation doorAnimation;

    [SerializeField] 
    private RoomUI roomUi;

    [SerializeField] 
    private Furniture[] furnitures;
    
    [SerializeField] private ICustomFurnitureReveal reveal;
    
    private GameSettings gameSettings;
    private Inventory inventory;
    private Action onRoomClosed;
    private FirebaseController firebaseController;
    
    private void Start()
    {
        gameSettings = FindObjectOfType<GameSettings>();
        inventory = FindObjectOfType<Inventory>();
        firebaseController = FindObjectOfType<FirebaseController>();
        
        Show();
    }

    private void OnEnable()
    {
        roomUi.OnItemBought += OnItemBought;
    }
    
    private void OnDisable()
    {
        roomUi.OnItemBought -= OnItemBought;
    }
    
    public override void Init(Action onRoomClosed)
    {
        this.onRoomClosed = onRoomClosed;
    }
    
    public override void Show()
    {
        for (int i = 0; i < inventory.Progress; i++)
        {
            if( i >= furnitures.Length)
                break;
            
            furnitures[i].RevealItem(true);
        }
        
        doorAnimation.GetTweens().FirstOrDefault().Play().OnComplete(() =>
        {
            roomUi.gameObject.SetActive(true);
        });
    }

    public override void Hide()
    {
        doorAnimation.DORewind();
        if(onRoomClosed != null)
            onRoomClosed.Invoke();
    }

    private void OnItemBought(int furnitureId)
    {
        furnitureId--;
        firebaseController.RoomUpgradedEvent(furnitureId);
        
        if(furnitureId >= furnitures.Length)
            return;
        
        furnitures[furnitureId].RevealItem();
    }
}
using DG.Tweening;
using TMPro;
using UnityEngine;

public class WalletUI : MonoBehaviour
{
    private ScoreManager scoreManager;
    private CoinsGenerator coinsGenerator;
    private Inventory inventory;
    
    private Sequence animateAddCoins;

    [SerializeField]
    private TMP_Text addCoins;
    [SerializeField]
    private TMP_Text coins;
    [SerializeField] 
    private TMP_Text scores;
    
    [SerializeField]
    private GameObject addCoinsGO;

    [SerializeField] 
    private GameObject coinsGO;
    
    private int cachedWallet = 0;
    private int cachedBalance = 0;
    private int cachedTotal = 0;
    
    private void Awake()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        inventory = FindObjectOfType<Inventory>();
        coinsGenerator = FindObjectOfType<CoinsGenerator>();
        cachedBalance = inventory.UserCoins;
    }

    private void Start()
    {
        animateAddCoins = DOTween.Sequence();
        animateAddCoins.Append(addCoinsGO.transform.DOScale(1.3f, 0.1f));
        animateAddCoins.Append(addCoinsGO.transform.DOScale(1f, 0.1f));
        animateAddCoins.SetAutoKill(false);
        
        coins.text = $"<sprite=0>{inventory.UserCoins}";
        addCoins.SetText("+0");
    }
    
    private void Update()
    {
        if (coinsGenerator.TotalCoins != cachedTotal)
        {
            scores.SetText(coinsGenerator.TotalCoins.ToString() );
            cachedTotal = coinsGenerator.TotalCoins;
        }
        
        if (coinsGenerator.CoinsInWallet != 0 && coinsGenerator.CoinsInWallet != cachedWallet)
        {
            if(!addCoinsGO.activeSelf)
                addCoinsGO.SetActive(true);
            
            addCoins.SetText($"+{coinsGenerator.CoinsInWallet}" );
            if (coinsGenerator.CoinsInWallet > cachedWallet)
            {
                animateAddCoins.SetLoops(1, LoopType.Restart);
                animateAddCoins.Restart();
            }

            cachedWallet = coinsGenerator.CoinsInWallet;
        }
        else if (coinsGenerator.CoinsInWallet == 0 && addCoinsGO.activeSelf)
        {
            addCoinsGO.SetActive(false);
            cachedWallet = 0;
        }
        
        if (inventory.UserCoins > cachedBalance)
        {
            if(!coinsGO.gameObject.activeSelf)
                coinsGO.gameObject.SetActive(true);
            
            coins.text = $"<sprite=0>{inventory.UserCoins}";
            cachedBalance = inventory.UserCoins;
        }
    }
}

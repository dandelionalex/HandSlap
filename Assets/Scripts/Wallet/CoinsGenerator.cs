using System;
using System.Collections;
using UnityEngine;

public class CoinsGenerator : MonoBehaviour
{
    private Action walletAnimationFinished;
    [SerializeField]
    private GameObject endLevelConfetti;
    [SerializeField]
    private Wallet wallet;
    public int CoinsInWallet { get; private set; }
    private SoundManager soundManager;
    private Inventory inventory;
    private GameSettings gameSettings;

    public int TotalCoins { get; private set; }

    [SerializeField]
    private GameObject coinPrefab;
    
    private void Awake()
    {
        soundManager = FindObjectOfType<SoundManager>();
        gameSettings = FindObjectOfType<GameSettings>();
        inventory = FindObjectOfType<Inventory>();
    }
    
    public void Init(int totalCoins, Action walletAnimationFinished )
    {
        this.TotalCoins = totalCoins;
        this.walletAnimationFinished = walletAnimationFinished;
        CoinsInWallet = 0;
        var confetti = Instantiate(endLevelConfetti);
        Destroy(confetti, 3);
    }

    public void OnWalletReady()
    {
        StartCoroutine(GenerateCoins());
    }

    private IEnumerator GenerateCoins()
    {
        var coinsGenerated = 0;
        var delay = 0.8f;
        while ( TotalCoins > gameSettings.ScoreChangeRate)
        {
            var newCoin = Instantiate(coinPrefab, transform);
            Destroy(newCoin, 2);

            coinsGenerated += 1;
            TotalCoins -= gameSettings.ScoreChangeRate;
            
            if (delay >= 0.1f)
            {
                delay = delay / 2f;
            }

            yield return new WaitForSeconds(delay);
        }
        
        yield return new WaitForSeconds(1f);
        wallet.AllCoinsCollected();
        StartCoroutine(MoveCoinsFromWallet());
        
    }
    
    public void AddCoinToWallet()
    {
        CoinsInWallet += 1;
        soundManager.PlaySound(SoundType.CoinDropToWallet);
        // Score -= gameSettings.ScoreChangeRate;
    }

    private IEnumerator MoveCoinsFromWallet()
    {
        while (CoinsInWallet > 0)
        {
            inventory.UserCoins++;
            CoinsInWallet--;

            yield return new WaitForSeconds(0.02f);
        }

        if(walletAnimationFinished != null)
            walletAnimationFinished.Invoke();
    }
}

using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    private CoinsGenerator coinsGenerator;
    private Animator anim;
    private List<Tween> tweens;

    private void Start()
    {
        coinsGenerator = FindObjectOfType<CoinsGenerator>();
        anim = GetComponent<Animator>();
        tweens = GetComponent<DOTweenAnimation>().GetTweens();
    }

    //called from animation
    public void ReadyToCollectCoins()
    {
        coinsGenerator.OnWalletReady();
    }

    public void AllCoinsCollected()
    {
        anim.SetTrigger("Close");
        tweens[0].PlayBackwards();
        Destroy(gameObject, 2);
    }
}

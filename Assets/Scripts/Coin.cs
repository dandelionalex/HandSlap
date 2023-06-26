using UnityEngine;

public class Coin : MonoBehaviour
{
    private CoinsGenerator scoreManager;

    private void Start()
    {
        scoreManager = FindObjectOfType<CoinsGenerator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        scoreManager.AddCoinToWallet();
        Destroy(transform.gameObject);
    }
}

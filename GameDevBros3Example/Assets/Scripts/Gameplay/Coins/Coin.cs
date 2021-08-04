using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] GameObject gameManager;

    void OnTriggerEnter2D(Collider2D collision)
    {
        gameManager.GetComponent<CoinCounter>().AddCoin(1);
        Destroy(gameObject);
    }
}

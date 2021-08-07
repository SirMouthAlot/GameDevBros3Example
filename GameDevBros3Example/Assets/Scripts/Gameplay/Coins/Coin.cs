using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] GameObject gameManager;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameManager != null)
        {
            gameManager.GetComponent<CoinCounter>().AddCoin(1);
            gameManager.GetComponent<ScoreCounter>().AddScore(100);
            Destroy(gameObject);

            FindObjectOfType<AudioManager>().Play("Coin");
        }
    }
}

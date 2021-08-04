using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockHit : MonoBehaviour
{
    [SerializeField] GameObject blockItem;
    [SerializeField] GameObject gameManager;

    [SerializeField] Sprite usedBlock;
    [SerializeField] Sprite unusedBlock;
    SpriteRenderer spriteRenderer;

    bool blockHit = false;
    bool blockHitActionPerformed = false;

    GameObject item = null;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!blockHit)
        {
            spriteRenderer.sprite = unusedBlock;
        }
        else
        {
            spriteRenderer.sprite = usedBlock;

            if (!blockHitActionPerformed)
            {
                BlockHitAction();
            }
        }
    }

    void BlockHitAction()
    {
        if (blockItem.CompareTag("Coin"))
        {
            item = Instantiate(blockItem, transform.position + new Vector3(0, 1, 0), Quaternion.Euler(0, 0, 0));
            Destroy(item, 0.5f);
            item.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 5, ForceMode2D.Impulse);

            gameManager.GetComponent<CoinCounter>().AddCoin(1);
        }
        else if (blockItem.CompareTag("Powerup"))
        {
            item = Instantiate(blockItem, transform.position + new Vector3(0, 1.01f, 0), Quaternion.Euler(0, 0, 0));
            item.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        }

        blockHitActionPerformed = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.contacts[0].normal.y > 0.5f)
            {
                blockHit = true;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedKoopa : MonoBehaviour
{
    [SerializeField] float kickForce = 2.5f;
    [SerializeField] float speed = 1.5f;

    bool isSquashed = false;
    bool isKicked = false;
    bool isMoving = true;
    bool movingLeft = false;

    // Update is called once per frame
    void Update()
    {
        if (!isSquashed)
        {
            Move();
        }
    }

    void Move()
    {
        if (movingLeft)
        {
            transform.position += Vector3.left * Time.deltaTime * speed;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.position += Vector3.right * Time.deltaTime * speed;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        isMoving = true;
    }

    public void ApplyKickForce(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().AddForce(direction * kickForce, ForceMode2D.Impulse);
        isKicked = true;
        isMoving = true;

        FindObjectOfType<AudioManager>().Play("Kick");
    }

    public bool GetIsSquashed()
    {
        return isSquashed;
    }

    public void SetIsSquashed(bool squashed)
    {
        isSquashed = squashed;
    }

    public bool GetIsKicked()
    {
        return isKicked;
    }

    public void SetIsKicked(bool kicked)
    {
        isKicked = kicked;
    }

    public bool GetIsMoving()
    {
        return isMoving;
    }

    public void SetIsMoving(bool moving)
    {
        isMoving = moving;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isSquashed)
        {
            movingLeft = !movingLeft;
        }

        if (isKicked)
        {
            if (collision.contacts[0].normal.x > 0)
            {
                ApplyKickForce(new Vector2(1, 0));
            }
            if (collision.contacts[0].normal.x < 0)
            {
                ApplyKickForce(new Vector2(-1, 0));
            }

            if (collision.gameObject.tag == "Goomba")
            {
                collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 3;

                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 8, ForceMode2D.Impulse);

                collision.gameObject.GetComponent<Collider2D>().enabled = false;

                Destroy(collision.gameObject, 2);

                FindObjectOfType<AudioManager>().Play("Bump");
            }

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioController : MonoBehaviour
{
    [SerializeField] float runForce;
    [SerializeField] float jumpForce;
    [SerializeField] float maxSpeed;
    [SerializeField] GameObject bigMarioPrefab;
    [SerializeField] GameObject smallMarioPrefab;
    [SerializeField] GameObject gameManager;

    Transform trans;
    Rigidbody2D body;

    bool isRunning;
    bool isGrounded;
    bool isDead;
    bool isBig;

    float runInput;
    bool jumpInput;

    bool deathStarted = false;

    float deathPauseTimer;

    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<Transform>();
        body = GetComponent<Rigidbody2D>();

        FindObjectOfType<AudioManager>().Play("Music");
    }

    // Update is called once per frame
    void Update()
    {
        runInput = Input.GetAxis("Horizontal");

        if (runInput == 0)
        {
            isRunning = false;
        }


        if (Input.GetKey(KeyCode.W))
        {
            jumpInput = true;
        }
        else
        {
            jumpInput = false;
        }

        if (runInput == 0 && body.velocity.y == 0)
        {
            body.drag = 3;
        }
        else
        {
            body.drag = 1;
        }

        if (trans.position.y <= -5 && !deathStarted)
        {
            StartDeath();
        }

        if (trans.position.y <= -7)
        {
            Die();
        }
    }

    void FixedUpdate()
    {
        if (runInput != 0 && !deathStarted)
        {
            Run();
        }

        if (jumpInput && isGrounded && !deathStarted)
        {
            Jump();
        }
    }

    void Run()
    {
        isRunning = true;

        if (Mathf.Abs(body.velocity.x) >= maxSpeed)
        {
            return;
        }
        if (runInput > 0)
        {
            body.AddForce(Vector2.right * runForce, ForceMode2D.Force);
            trans.rotation = Quaternion.Euler(0, 180, 0);
        }
        if (runInput < 0)
        {
            body.AddForce(Vector2.left * runForce, ForceMode2D.Force);
            trans.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void StartDeath()
    {
        FindObjectOfType<Lives>().LoseLife();

        FindObjectOfType<AudioManager>().Stop("Music");

        if (FindObjectOfType<Lives>().GetCurrentLives() < 1)
        {
            FindObjectOfType<AudioManager>().Play("GameOver");
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("LifeLost");
        }

        isDead = true;

        body.velocity = Vector2.zero;

        //deathPauseTimer = Time.realtimeSinceStartup + 0.5f;

        //while(deathPauseTimer > Time.realtimeSinceStartup)
        //{

        //}

        body.gravityScale = 3;

        body.AddForce(Vector3.up * jumpForce / 2, ForceMode2D.Impulse);

        GetComponent<Collider2D>().enabled = false;

        deathStarted = true;
    }

    void Die()
    {
        if (FindObjectOfType<Lives>().GetCurrentLives() < 1)
        {
            gameManager.GetComponent<LevelStatus>().SetGameOver(true);
        }
        else
        {
            gameManager.GetComponent<LevelStatus>().SetLevelFailed(true);
        }
    }

    void Jump()
    {
        body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isGrounded = false;

        FindObjectOfType<AudioManager>().Play("Jump");
    }

    void EnemyBounce()
    {
        body.AddForce(Vector2.up * jumpForce / 1.5f, ForceMode2D.Impulse);
        isGrounded = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        for (int i = 0; i < collision.contacts.Length; i++)
        {
            if (collision.contacts[i].normal.y > 0.5)
            {
                isGrounded = true;
            }
        }

        if (collision.gameObject.tag == "Goomba")
        {
            if (collision.contacts[0].normal.y > 0.5)
            {
                EnemyBounce();
                collision.gameObject.GetComponent<Goomba>().SetIsSquashed(true);
                gameManager.GetComponent<ScoreCounter>().AddScore(1000);

                FindObjectOfType<AudioManager>().Play("Bump");
            }
            else
            {
                if (!collision.gameObject.GetComponent<Goomba>().GetIsSquashed())
                {
                    if (isBig)
                    {
                        GetComponent<BoxCollider2D>().size = smallMarioPrefab.GetComponent<BoxCollider2D>().size;
                        isBig = false;

                        FindObjectOfType<AudioManager>().Play("PowerDown");
                    }
                    else
                    {
                        StartDeath();
                    }
                }
            }
        }

        if (collision.gameObject.tag == "Koopa")
        {
            if (collision.contacts[0].normal.y > 0.5 && !collision.gameObject.GetComponent<RedKoopa>().GetIsSquashed())
            {
                EnemyBounce();
                collision.gameObject.GetComponent<RedKoopa>().SetIsSquashed(true);
                collision.gameObject.GetComponent<RedKoopa>().SetIsMoving(false);

                collision.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(1, 1);

                gameManager.GetComponent<ScoreCounter>().AddScore(1000);

                FindObjectOfType<AudioManager>().Play("Bump");
            }
            else if (collision.gameObject.GetComponent<RedKoopa>().GetIsSquashed() && !collision.gameObject.GetComponent<RedKoopa>().GetIsKicked())
            {
                if (collision.gameObject.transform.position.x > transform.position.x)
                {
                    collision.gameObject.GetComponent<RedKoopa>().ApplyKickForce(new Vector2(1, 0));
                }
                if (collision.gameObject.transform.position.x < transform.position.x)
                {
                    collision.gameObject.GetComponent<RedKoopa>().ApplyKickForce(new Vector2(-1, 0));
                }
            }
            else if (collision.contacts[0].normal.y > 0.5 && collision.gameObject.GetComponent<RedKoopa>().GetIsKicked())
            {
                EnemyBounce();
                collision.gameObject.GetComponent<RedKoopa>().SetIsKicked(false);
                collision.gameObject.GetComponent<RedKoopa>().SetIsMoving(false);

                collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, collision.gameObject.GetComponent<Rigidbody2D>().velocity.y);

                gameManager.GetComponent<ScoreCounter>().AddScore(100);
            }
            else
            {
                if (collision.gameObject.GetComponent<RedKoopa>().GetIsMoving())
                {
                    if (isBig)
                    {
                        GetComponent<BoxCollider2D>().size = smallMarioPrefab.GetComponent<BoxCollider2D>().size;
                        isBig = false;

                        FindObjectOfType<AudioManager>().Play("PowerDown");
                    }
                    else
                    {
                        StartDeath();
                    }
                }
            }
        }

        if (collision.gameObject.name.Contains("Mushroom"))
        {
            FindObjectOfType<AudioManager>().Play("Powerup");

            if (!isBig)
            {
                Destroy(collision.gameObject);

                isBig = true;

                GetComponent<BoxCollider2D>().size = bigMarioPrefab.GetComponent<BoxCollider2D>().size;

            }
            else
            {
                Destroy(collision.gameObject);

                gameManager.GetComponent<ScoreCounter>().AddScore(500);
            }
        }
    }

    public bool GetIsRunning()
    {
        return isRunning;
    }

    public bool GetIsGrounded()
    {
        return isGrounded;
    }

    public bool GetIsDead()
    {
        return isDead;
    }

    public bool GetIsBig()
    {
        return isBig;
    }
}

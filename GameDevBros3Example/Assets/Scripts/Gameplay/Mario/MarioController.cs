using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioController : MonoBehaviour
{
    [SerializeField] float runForce;
    [SerializeField] float jumpForce;
    [SerializeField] float maxSpeed;

    Transform trans;
    Rigidbody2D body;

    bool isRunning;
    bool isGrounded;

    float runInput;
    bool jumpInput;

    bool deathStarted = false;

    float deathPauseTimer;

    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<Transform>();
        body = GetComponent<Rigidbody2D>();
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
        Destroy(gameObject);
    }

    void Jump()
    {
        body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isGrounded = false;
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
            }
            else
            {
                if (!collision.gameObject.GetComponent<Goomba>().GetIsSquashed())
                {
                    StartDeath();
                }
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
}

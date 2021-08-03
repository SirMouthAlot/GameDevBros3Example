using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    Animator anim;

    MarioController marioMovement;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        marioMovement = GetComponent<MarioController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (marioMovement.GetIsRunning())
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        if (marioMovement.GetIsGrounded())
        {
            anim.SetBool("isJumping", false);
        }
        else
        {
            anim.SetBool("isJumping", true);
        }
    }
}

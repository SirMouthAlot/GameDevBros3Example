using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    [SerializeField] GameObject mario;
    [SerializeField] List<GameObject> goombas;

    Animator marioAnimator;
    MarioController marioMovement;


    // Start is called before the first frame update
    void Start()
    {
        marioAnimator = mario.GetComponent<Animator>();
        marioMovement = mario.GetComponent<MarioController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (marioMovement.GetIsRunning())
        {
            marioAnimator.SetBool("isWalking", true);
        }
        else
        {
            marioAnimator.SetBool("isWalking", false);
        }

        if (marioMovement.GetIsGrounded())
        {
            marioAnimator.SetBool("isJumping", false);
        }
        else
        {
            marioAnimator.SetBool("isJumping", true);
        }

        if (marioMovement.GetIsDead())
        {
            marioAnimator.SetBool("isDead", true);
        }
        else
        {
            marioAnimator.SetBool("isDead", false);
        }

        if (marioMovement.GetIsBig())
        {
            marioAnimator.SetBool("isBig", true);
        }
        else
        {
            marioAnimator.SetBool("isBig", false);
        }

        for (int i = 0; i < goombas.Count; i++)
        {
            if (goombas[i] != null)
            {
                if (goombas[i].GetComponent<Goomba>().GetIsSquashed())
                {
                    goombas[i].GetComponent<Animator>().SetBool("Squashed", true);
                }
                else
                {
                    goombas[i].GetComponent<Animator>().SetBool("Squashed", false);
                }
            }
        }
    }
}

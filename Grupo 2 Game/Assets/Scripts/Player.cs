using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public bool player1;
    public float Velocity = 5f;
    public float JumpVelocity = 5f;
    public int JumpCountMax = 2;
    public Main main;

    private int jumpCount;
    private Vector2 totalVel;

    // Use this for initialization
    void Start ()
    {
        jumpCount = JumpCountMax;
	}
	
	// Update is called once per frame
	void Update ()
    {
        totalVel = Vector2.zero;

        if (player1)
        {
            //ir para a esquerda
            if(Input.GetKey(KeyCode.A))
            {
                RunLeft();
            }
            //ir para a direita
            else if (Input.GetKey(KeyCode.D))
            {
                RunRight();
            }
            //parar
            else 
            {
                Stop();
            }

            //Pula
            if (Input.GetKeyDown(KeyCode.W) && jumpCount > 0)
            {
                Jump();
            }

            //Ataca
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
            }
        }
        else
        {
            //ir para a esquerda
            if(Input.GetKey(KeyCode.LeftArrow))
            {
                RunLeft();
            }
            //ir para a direita
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                RunRight();
            }
            //parar
            else
            {
                Stop();
            }

            //Pula
            if (Input.GetKeyDown(KeyCode.UpArrow) && jumpCount > 0)
            {
                Jump();
            }

            //Ataca
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Attack();
            }
        }

        Vector2 vel = new Vector2(totalVel.x, GetComponent<Rigidbody2D>().velocity.y);
        GetComponent<Rigidbody2D>().velocity = vel;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("platform"))
        {
            jumpCount = JumpCountMax;
            GetComponent<Animator>().SetBool("Jumping", false);
        }
    }
    public void OnTriggerEnter2D(Collider2D collider2D){
        if(collider2D.gameObject.CompareTag("chao"))
        {
            main.EndGame(player1);
            Destroy(gameObject);
        }
    }

    public void Attack()
    {
        GetComponent<Animator>().SetBool("Attack", true);
    }

    public void EndAttack()
    {
        GetComponent<Animator>().SetBool("Attack", false);
    }

    public void Jump()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * JumpVelocity, ForceMode2D.Impulse);
        jumpCount--;
        GetComponent<Animator>().SetBool("Jumping", true);
    }

    public void RunLeft()
    {
        totalVel += Vector2.left * Velocity;
        GetComponent<Animator>().SetBool("Running", true);
        GetComponent<Animator>().SetBool("Right", false);
    }

    public void RunRight()
    {
        totalVel += Vector2.right * Velocity;
        GetComponent<Animator>().SetBool("Running", true);
        GetComponent<Animator>().SetBool("Right", true);
    }

    public void Stop()
    {
        Vector2 vela = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
        GetComponent<Rigidbody2D>().velocity = vela;
        GetComponent<Animator>().SetBool("Running", false);
    }
}

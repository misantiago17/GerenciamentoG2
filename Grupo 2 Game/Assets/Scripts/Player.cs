using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public bool player1;
    public float Velocity = 5f;
    public float JumpVelocity = 5f;
    public int JumpCountMax = 2;
    public Main main;

    public GameObject Player1;
    public GameObject Player2;

    public PhysicsMaterial2D nofriction;

    private int jumpCount;
    private Vector2 totalVel;
    private bool right;

    private bool knocked;
    private bool canAttack;

    // Use this for initialization
    void Start ()
    {
        jumpCount = JumpCountMax;
        canAttack = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(knocked)
        {
            return;
        }

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
            GetComponent<BoxCollider2D>().sharedMaterial = null;
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
        if(canAttack)
        {
            GetComponent<Animator>().SetBool("Attack", true);
            if (player1)
            {
                AddExplosionForce(Player2, right ? Vector2.right : Vector2.left);
            }
            else
            {
                AddExplosionForce(Player1, right ? Vector2.right : Vector2.left);
            }
            canAttack = false;
            StartCoroutine(ResetAttack());
        }
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
        right = false;
    }

    public void RunRight()
    {
        totalVel += Vector2.right * Velocity;
        GetComponent<Animator>().SetBool("Running", true);
        GetComponent<Animator>().SetBool("Right", true);
        right = true;
    }

    public void Stop()
    {
        Vector2 vela = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
        GetComponent<Rigidbody2D>().velocity = vela;
        GetComponent<Animator>().SetBool("Running", false);
    }

    public void AddExplosionForce(GameObject enemy, Vector2 dir)// float explosionForce, Vector3 explosionPosition, float explosionRadius, float upliftModifier)
    {
        if (enemy.transform.position.y >= transform.position.y - 30)
        {
            if((dir == Vector2.right && enemy.transform.position.x > transform.position.x) ||
               (dir == Vector2.left && enemy.transform.position.x < transform.position.x))
            {
                if (Vector2.Distance(enemy.transform.position, transform.position) < 2)
                {
                    enemy.GetComponent<Player>().ReceiveAttack(dir); 
                }
            }
        }        
    }

    public void ReceiveAttack(Vector2 dir)
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        knocked = true;
        Vector2 force = Vector2.up * 70 + dir * 50;
        GetComponent<BoxCollider2D>().sharedMaterial = nofriction;
        GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);

        StartCoroutine(ResetKnock());
    }

    IEnumerator ResetKnock()
    {
        yield return new WaitForSeconds(0.5f);
        knocked = false;
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(0.5f);
        canAttack = true;
    }
}

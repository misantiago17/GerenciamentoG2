using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float Velocity = 5f;
    public float JumpVelocity = 5f;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector2 totalVel = Vector2.zero;
        //ir para a esquerda
		if(Input.GetKey(KeyCode.A))
        {
            totalVel += Vector2.left * Velocity;
        }
        //ir para a direita
        else if (Input.GetKey(KeyCode.D))
        {
            totalVel += Vector2.right * Velocity;
        }
        //parar
        else 
        {
            Vector2 vela = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
            GetComponent<Rigidbody2D>().velocity = vela;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * JumpVelocity, ForceMode2D.Impulse);
        }

        Vector2 vel = new Vector2(totalVel.x, GetComponent<Rigidbody2D>().velocity.y);
        GetComponent<Rigidbody2D>().velocity = vel;
    }
}

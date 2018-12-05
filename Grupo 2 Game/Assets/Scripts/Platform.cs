﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

	public GameObject mainScriptsObject;
	//public float fallSpeed = 5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// transform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.World);
	}

	public void OnTriggerEnter2D(Collider2D collider2D){

        if (collider2D.gameObject.tag == "platform")
        {
            mainScriptsObject.GetComponent<PlataformaAleatoria>().removePlataformFromGO(collider2D.gameObject);
            Destroy(collider2D.gameObject);
		}
	}
}

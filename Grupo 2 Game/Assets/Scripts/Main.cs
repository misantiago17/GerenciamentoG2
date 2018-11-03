using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour {

	public Text endText;
	// Use this for initialization
	void Start () {
		endText.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void EndGame(bool player1){
		endText.text = player1?"Player 1 ganhou":"Player 2 ganhou";
		endText.gameObject.SetActive(true);
		StartCoroutine(End());

	}

	private IEnumerator End()
    {
		yield return new WaitForSeconds(2f);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

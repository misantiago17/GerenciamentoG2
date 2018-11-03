using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaAleatoria : MonoBehaviour {

    public GameObject Plataform;

    private float _screenWidth;

	// Use this for initialization
	void Start () {
        _screenWidth = Screen.width;

    }
	
	// Update is called once per frame
	void Update () {
		
	}


    void randomizePlataformPlace()
    {
        float plataformWidth = Plataform.GetComponent<RectTransform>().rect.width;

        Vector3 spawnPos = transform.TransformPoint(Random.Range(-1 * _screenWidth/2,_screenWidth/2), Screen.height + 10, 0);
        Instantiate(Plataform, spawnPos, Plataform.transform.rotation);
    }
}

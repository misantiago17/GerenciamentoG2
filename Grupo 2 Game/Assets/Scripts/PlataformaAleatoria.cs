using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaAleatoria : MonoBehaviour {

    public GameObject Plataform;

    private float _screenWidth;

    private List<GameObject> _presentPlataforms = new List<GameObject>();

	// Use this for initialization
	void Start () {
        
        _screenWidth = Camera.main.rect.width;
        StartCoroutine(MovingPlataform());
    }
	
	// Update is called once per frame
	void Update () {

        if (_presentPlataforms.Count > 0)
        {
            foreach (GameObject plat in _presentPlataforms)
            {
                if (plat.transform.position.y < -1*Camera.main.rect.height - 150)
                {
                    _presentPlataforms.Remove(plat);
                    Destroy(plat);
                }

            }
        }
        
	}


    void randomizePlataformPlace()
    {
        float plataformWidth = Plataform.GetComponent<RectTransform>().rect.width;

        Vector3 spawnPos = transform.TransformPoint(Random.Range(Camera.main.rect.xMin, _screenWidth ), Camera.main.rect.height + 10, 0);
        GameObject newPlataform = Instantiate(Plataform, spawnPos, Plataform.transform.rotation);

        _presentPlataforms.Add(newPlataform);
        movePlataformDowards(newPlataform);
    }

    void movePlataformDowards(GameObject plataform)
    {
        plataform.transform.position = Vector3.Slerp(plataform.transform.position,new Vector3(plataform.transform.position.x, plataform.transform.position.y - 10, 0), 0);
    }

    IEnumerator MovingPlataform()
    {
        while (true)
        {
            randomizePlataformPlace();

            yield return new WaitForSeconds(1f);
        }
    }
}

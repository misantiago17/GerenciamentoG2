using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaAleatoria : MonoBehaviour {

    public GameObject PlataformPrefab;
    public float MaxVelocity = 10f;
    public float MinVelocity = 4f;

    private float _screenWidth;

    [HideInInspector]
    public List<Plataform> _presentPlataforms = new List<Plataform>();

    public struct Plataform
    {
        public GameObject plataforma;
        public float velocity;

        public Plataform(GameObject gm,float speed)
        {
            this.plataforma = gm;
            this.velocity = speed;
        }
    }

	// Use this for initialization
	void Start () {
        
        _screenWidth = Camera.main.rect.width;
        StartCoroutine(MovingPlataform());
    }
	
	// Update is called once per frame
	void Update () {

        if (_presentPlataforms.Count > 0)
        {
            foreach (Plataform plat in _presentPlataforms)
            {
               movePlataformDowards(plat);

                /*if (plat.plataforma.transform.position.y < -1*Camera.main.rect.height - 20)
                {
                    Destroy(plat.plataforma);
                    _presentPlataforms.Remove(plat);
                }*/

            }
        }
        
	}

    public void removePlataformFromGO(GameObject go)
    {
        foreach (Plataform plat in _presentPlataforms)
        {
            if(plat.plataforma == go)
            {
                _presentPlataforms.Remove(plat);
                break;
            }
        }

    }


    void randomizePlataformPlace()
    {
        float plataformWidth = PlataformPrefab.GetComponent<RectTransform>().rect.width;
        Vector3 spawnPos = transform.TransformPoint(Random.Range(Camera.main.transform.position.x - 12, Camera.main.transform.position.x + 12), Camera.main.rect.height + 10, 0);
        GameObject newPlataformGO = Instantiate(PlataformPrefab, spawnPos, PlataformPrefab.transform.rotation);

        Plataform newPlataform = new Plataform(newPlataformGO, Random.Range(MinVelocity, MaxVelocity));
        _presentPlataforms.Add(newPlataform);
    }

    void movePlataformDowards(Plataform plataform)
    {
        plataform.plataforma.GetComponent<Rigidbody2D>().velocity = -1 * PlataformPrefab.transform.up * (plataform.velocity * Time.fixedDeltaTime);
    }

    IEnumerator MovingPlataform()
    {
        while (true)
        {
            randomizePlataformPlace();

            yield return new WaitForSeconds(0.5f);
        }
    }
}

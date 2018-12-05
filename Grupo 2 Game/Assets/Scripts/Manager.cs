using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {

    public GameObject Tutorial;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ActivateTutorial(false);
        }
    }

    public void changeScene(string scene)
    {
		SceneManager.LoadScene(scene);
	}

    public void ActivateTutorial(bool active)
    {
        Tutorial.SetActive(active);
    }
}

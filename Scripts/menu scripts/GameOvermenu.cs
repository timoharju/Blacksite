using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOvermenu : MonoBehaviour {

    Button retryButton;
    Button quitButton;

	// Use this for initialization
	void Start () {

        retryButton = GameObject.Find("RetryButton").GetComponent<Button>();
        quitButton = GameObject.Find("QuitButton").GetComponent<Button>();

        retryButton.onClick.AddListener(() => Retry());
        quitButton.onClick.AddListener(() => Quit());

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Retry()
    {
        SceneManager.LoadSceneAsync("newScene");
    }

    private void Quit()
    {
        Application.Quit();
    }
}

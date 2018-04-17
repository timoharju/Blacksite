using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditMenu : MonoBehaviour {

    Button exitbutton;


	// Use this for initialization
	void Start () {
        exitbutton = GameObject.Find("ExitButton").GetComponent<Button>();
        exitbutton.onClick.AddListener(() => ClickToExit());
	}
	
    private void ClickToExit ()
    {
        SceneManager.LoadSceneAsync("Startmenu");
    }

	// Update is called once per frame
	void Update () {
		
	}
}

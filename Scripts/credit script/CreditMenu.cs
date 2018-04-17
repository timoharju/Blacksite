using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditMenu : MonoBehaviour {

    Button exitbutton;
    Text clickCountText;


	// Use this for initialization
	void Start () {
        exitbutton = GameObject.Find("ExitButton").GetComponent<Button>();
        clickCountText = GameObject.Find("ClickCountText").GetComponent<Text>();

        exitbutton.onClick.AddListener(() => ClickToExit());
        clickCountText.text = "" + GotoMouse.numberOfClicks + " times.";
	}
	
    private void ClickToExit ()
    {
        SceneManager.LoadSceneAsync("Startmenu");
    }

	// Update is called once per frame
	void Update () {
		
	}
}

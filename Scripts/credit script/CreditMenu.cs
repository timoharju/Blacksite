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

        //android settings
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        exitbutton = GameObject.Find("ExitButton").GetComponent<Button>();
        clickCountText = GameObject.Find("ClickCountText").GetComponent<Text>();

        exitbutton.onClick.AddListener(() => ClickToExit());
        clickCountText.text = "" + GotoMouse.numberOfClicks + " times.";
        StartCoroutine(QuitAfterSeconds());
	}
	
    private void ClickToExit ()
    {
        
        //load the correct scene depending on platform
        if(Application.platform == RuntimePlatform.Android)
        {
            SceneManager.LoadSceneAsync("Startmenu");
        }
        else
        {
            SceneManager.LoadSceneAsync("ComputerMenu");
        }
    }

	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// credit scene timeout
    /// </summary>
    /// <returns></returns>
    private IEnumerator QuitAfterSeconds()
    {
        yield return new WaitForSeconds(30f);
        ClickToExit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// handles the start menu
/// </summary>
public class Startmenu : MonoBehaviour {

    Button startgameButton;
    Button howtoPlayButton;
    Button quitButton;
    CanvasGroup loadingCanvas;
    CanvasGroup instructionsCanvas;
    Button closeInstructions;
    
	// Use this for initialization
	void Start () {
        //android settings
        Screen.orientation = ScreenOrientation.Portrait;
        //find buttons
        startgameButton = GameObject.Find("StartGameButton").GetComponent<Button>();
        howtoPlayButton = GameObject.Find("HowtoPlayButton").GetComponent<Button>();
        quitButton = GameObject.Find("QuitButton").GetComponent<Button>();
        loadingCanvas = GameObject.Find("LoadingCanvas").GetComponent<CanvasGroup>();
        instructionsCanvas = GameObject.Find("InstructionsCanvas").GetComponent<CanvasGroup>();
        closeInstructions = GameObject.Find("CloseInstructionsButton").GetComponent<Button>();


        //add listeners
        startgameButton.onClick.AddListener(() => StartGame());
        howtoPlayButton.onClick.AddListener(() => HowToPlay());
        quitButton.onClick.AddListener(() => QuitGame());
        closeInstructions.onClick.AddListener(() => CloseInstructions());

        loadingCanvas.alpha = 0f;
        CloseInstructions();
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    /// <summary>
    /// start game button listener, load the main game scene
    /// </summary>
    private void StartGame()
    {
        loadingCanvas.alpha = 1f;
        SceneManager.LoadSceneAsync("newScene");
    }
    /// <summary>
    /// show how to play instructions
    /// </summary>
    private void HowToPlay()
    {
        //show the canvas and set it interactable
        instructionsCanvas.alpha = 1f;
        instructionsCanvas.interactable = true;
        instructionsCanvas.blocksRaycasts = true;
    }
    /// <summary>
    /// quit game
    /// </summary>
    private void QuitGame()
    {
        Application.Quit();
    }
    /// <summary>
    /// hide how to play instructions
    /// </summary>
    private void CloseInstructions()
    {
        instructionsCanvas.alpha = 0f;
        instructionsCanvas.interactable = false;
        instructionsCanvas.blocksRaycasts = false;
    }
}

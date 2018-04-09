using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// keypad "minigame"
/// </summary>
public class Keypad {


    CanvasGroup keypadCanvas;
    bool active = false; //visible or not, false by default
    string presses = "";
    Audio sound = new Audio();
    private List<Button> buttons = new List<Button>(); //list of all keypad buttons
    /// <summary>
    /// create keypad button objects and set listeners
    /// </summary>
    public Keypad()
    {

        this.keypadCanvas = GameObject.Find("KeypadCanvas").GetComponent<CanvasGroup>();

        this.keypadCanvas.alpha = 0f;
        this.keypadCanvas.interactable = false;
        this.keypadCanvas.blocksRaycasts = false;

        GetButtons(); //add buttons to list

        foreach(Button button in buttons)
        {
            button.onClick.AddListener(() => KeypadButtonClick(button));
        }
        
    }
    /// <summary>
    /// Put all the keypad buttons onto a list
    /// </summary>
    private void GetButtons()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");

        foreach(GameObject btn in objects)
        {
            buttons.Add(btn.GetComponent<Button>());
        }
    }
    /// <summary>
    /// toggle keypad visible or invisible
    /// </summary>
    public void Togglekeypad()
    {
        if(active == false)
        {
            GotoMouse.menuOpen = true;
            keypadCanvas.alpha = 1f;
            keypadCanvas.interactable = true;
            keypadCanvas.blocksRaycasts = true;
            active = true;
        }
        else if(active == true)
        {
            CloseKeypad();
        }
    }
    /// <summary>
    /// close the keypad
    /// </summary>
    public void CloseKeypad()
    {
        GotoMouse.menuOpen = false;
        keypadCanvas.alpha = 0f;
        keypadCanvas.interactable = false;
        keypadCanvas.blocksRaycasts = false;
        active = false;
    }
    /// <summary>
    /// get or set keypad visibility state
    /// </summary>
    public bool Active
    {
        get { return this.active; }
        set { this.active = value; }
    }
    
    /// <summary>
    /// Keypad button functions
    /// </summary>
    /// <param name="button"></param>
    private void KeypadButtonClick(Button button)
    {
        
        int whichButton;
        int.TryParse(button.name, out whichButton);
        presses += whichButton;
        CheckCombination();
        sound.BeepAudio();
        //Debug.Log("pressed " + whichButton);
    }
    /// <summary>
    /// check the entered combination
    /// </summary>
    private void CheckCombination()
    {
        if(presses.Length > 3)
        {
            Debug.Log("4 keys entered: " + presses);
            presses = "";
            sound.AccessDeniedAudio();
        }
        else
        {
            Debug.Log("presses: " + presses);
        }
    }
}

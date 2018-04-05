using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// options class, handle options menu functions
/// </summary>
public class Options {

    Button optionsHideButton;
    CanvasGroup optionsCanvasGroup;
    Button optionsButton;
    
    Button optionsCheatButton;
    Cheats cheats;
    CanvasGroup cheatsCanvas;
    /// <summary>
    /// set options menu hidden by default and create cheats menu object
    /// </summary>
    public Options()
    {
        optionsCanvasGroup = GameObject.Find("Options").GetComponent<CanvasGroup>();
        optionsButton = GameObject.Find("optionsButton").GetComponent<Button>();
        
        optionsCheatButton = GameObject.Find("OptionsCheatButton").GetComponent<Button>();
        optionsHideButton = GameObject.Find("OptionsHideButton").GetComponent<Button>();
        cheatsCanvas = GameObject.Find("Cheats").GetComponent<CanvasGroup>();

        cheats = new Cheats(cheatsCanvas);

        optionsHideButton.onClick.AddListener(() => HideOptions());
        optionsButton.onClick.AddListener(() => ShowOptions());
        optionsCheatButton.onClick.AddListener(() => ShowCheats());
        optionsCanvasGroup.alpha = 0f;
        optionsCanvasGroup.interactable = false;
        
        
    }
    /// <summary>
    /// hide options menu
    /// </summary>
    private void HideOptions()
    {
        optionsCanvasGroup.alpha = 0f;
        optionsCanvasGroup.interactable = false;
        Debug.Log("options closed");
    }
    /// <summary>
    /// show options menu
    /// </summary>
    private void ShowOptions()
    {
        optionsCanvasGroup.alpha = 1f;
        optionsCanvasGroup.interactable = true;
    }
    /// <summary>
    /// show cheats menu
    /// </summary>
    private void ShowCheats()
    {
        cheats.ShowCheats();
    }

}

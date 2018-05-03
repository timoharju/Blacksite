using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Menu
{

    /// <summary>
    /// options class, handle options menu functions
    /// </summary>
    public class Options
    {

        Button optionsHideButton;
        CanvasGroup optionsCanvasGroup;
        Button optionsButton;
        Button optionsQuitButton;

        Button optionsCheatButton;
        Cheats cheats;
        CanvasGroup cheatsCanvas;
        /// <summary>
        /// set options menu hidden by default and create cheats menu object
        /// </summary>
        public Options()
        {
            //create button & canvas objects
            optionsCanvasGroup = GameObject.Find("Options").GetComponent<CanvasGroup>();
            optionsButton = GameObject.Find("optionsButton").GetComponent<Button>();
            optionsQuitButton = GameObject.Find("OptionsQuitButton").GetComponent<Button>();
            optionsCheatButton = GameObject.Find("OptionsCheatButton").GetComponent<Button>();
            optionsHideButton = GameObject.Find("OptionsHideButton").GetComponent<Button>();
            cheatsCanvas = GameObject.Find("Cheats").GetComponent<CanvasGroup>();

            //create cheats canvas here for optionsCheatButton
            cheats = new Cheats(cheatsCanvas);

            //add button listeners
            optionsHideButton.onClick.AddListener(() => HideOptions());
            optionsButton.onClick.AddListener(() => ShowOptions());
            optionsCheatButton.onClick.AddListener(() => ShowCheats());
            optionsQuitButton.onClick.AddListener(() => QuitApp());

            // hide options menu by default + don't intercept clicks
            optionsCanvasGroup.alpha = 0f;
            optionsCanvasGroup.interactable = false;


        }
        /// <summary>
        /// hide options menu
        /// </summary>
        private void HideOptions()
        {
            GotoMouse.MenuOpen = false;
            GotoMouse.Move = false;
            optionsCanvasGroup.alpha = 0f;
            optionsCanvasGroup.interactable = false;
            Debug.Log("options closed");
        }
        /// <summary>
        /// show options menu
        /// </summary>
        private void ShowOptions()
        {
            GotoMouse.MenuOpen = true;
            GotoMouse.Move = false;
            optionsCanvasGroup.alpha = 1f;
            optionsCanvasGroup.interactable = true;
        }
        /// <summary>
        /// show cheats menu
        /// </summary>
        private void ShowCheats()
        {
            HideOptions();
            GotoMouse.MenuOpen = true;
            cheats.ShowCheats();
        }
        /// <summary>
        /// return to mainmenu
        /// </summary>
        private void QuitApp()
        {
            if(Application.platform == RuntimePlatform.Android)
            {
                SceneManager.LoadSceneAsync("Startmenu");
            }
            else
            {
                SceneManager.LoadSceneAsync("ComputerMenu");
            }
        }
    }
}

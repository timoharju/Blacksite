using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Minigames
{

    /// <summary>
    /// keypad "minigame"
    /// </summary>
    public class Keypad
    {


        CanvasGroup keypadCanvas;
        bool active = false; //visible or not, false by default
        string presses = "";
        Audio sound = new Audio();
        private List<Button> buttons = new List<Button>(); //list of all keypad buttons
        private int buttonPresses = 0;
        private bool solved = false;
        ScrollText scrollText;
        /// <summary>
        /// create keypad button objects and set listeners
        /// </summary>
        public Keypad(ScrollText scrollText)
        {
            this.keypadCanvas = GameObject.Find("KeypadCanvas").GetComponent<CanvasGroup>();
            this.keypadCanvas.alpha = 0f;
            this.keypadCanvas.interactable = false;
            this.keypadCanvas.blocksRaycasts = false;
            this.scrollText = scrollText;

            GetButtons(); //add buttons to list

            //add listeners to all buttons
            foreach (Button button in buttons)
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

            foreach (GameObject btn in objects)
            {
                buttons.Add(btn.GetComponent<Button>());
            }
        }
        /// <summary>
        /// toggle keypad visible or invisible
        /// </summary>
        public void Togglekeypad()
        {
            if (active == false)
            {
                GotoMouse.MenuOpen = true;
                keypadCanvas.alpha = 1f;
                keypadCanvas.interactable = true;
                keypadCanvas.blocksRaycasts = true;
                active = true;
            }
            else if (active == true)
            {
                CloseKeypad();
            }
        }
        /// <summary>
        /// close the keypad
        /// </summary>
        public void CloseKeypad()
        {
            GotoMouse.MenuOpen = false;
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
            buttonPresses++; //track how many times you have pushed the buttons
            CheckCombination();
            sound.BeepAudio();
            //Debug.Log("pressed " + whichButton);
        }
        /// <summary>
        /// check the entered combination
        /// </summary>
        private void CheckCombination()
        {
            // 2958 combination -- keys start at 0 
            if (presses == "072109")
            {
                sound.AccessGrantedAudio();
                sound.ElectricDoorOpenAudio();
                presses = "";
                buttonPresses = 0;
                Player.KeypadSolved = true;
                solved = true;

            }

            else if (buttonPresses == 5)
            {
                Debug.Log("4 keys entered: " + presses);
                presses = "";
                scrollText.Text = "Access denied.";
                scrollText.StartScrolling();
                buttonPresses = 0;
                sound.AccessDeniedAudio();
            }


        }

        public bool Solved
        {
            get { return this.solved; }
            set { this.solved = value; }
        }

        public IEnumerator WaitForSecs(float sec)
        {
            yield return new WaitForSeconds(sec);
            CloseKeypad();
        }
    }
}
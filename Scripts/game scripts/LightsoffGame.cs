using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using HelperClasses;

namespace Minigames
{

    /// <summary>
    /// handles the lightsoff minigame
    /// </summary>
    public class LightsoffGame
    {

        CanvasGroup lightsoffCanvas;
        private List<Button> buttons;
        private List<AdjacentButtons> adjacentList;
        private Sprite buttonRed;
        private Sprite buttonYellow;
        private Sprite buttonGreen;
        private Image buttonSprite;
        private Audio sound;
        private bool solved = false;
        private Button closeButton;



        /// <summary>
        /// set game hidden by default, add listeners to buttons and declare sprites
        /// </summary>
        public LightsoffGame()
        {

            sound = new Audio();
            buttons = new List<Button>();
            lightsoffCanvas = GameObject.Find("LightsoffCanvas").GetComponent<CanvasGroup>();
            closeButton = GameObject.Find("LightsoffCloseButton").GetComponent<Button>();
            buttonRed = Resources.Load<Sprite>("button_red");
            buttonYellow = Resources.Load<Sprite>("button_yellow");
            buttonGreen = Resources.Load<Sprite>("button_green");
            adjacentList = new List<AdjacentButtons>();

            closeButton.onClick.AddListener(() => HideGame());

            //put all the buttons into List called buttons
            GetButtons();

            //choose from 3 patterns
            var pattern = Random.Range(1, 4);
            GamePattern(pattern);
            //Debug.Log("pattern: " + pattern);

            //hide game by default
            HideGame();

            //set adjacent buttons, i.e button 0 should flip button 1 and 5 color
            SetAdjacent();

            //---uncomment below if you want to test the game-------------
            //ShowGame(); 

        }
        /// <summary>
        /// light button functionalities
        /// </summary>
        /// <param name="button"></param>
        private void ButtonClicked(Button button)
        {
            int buttonNumber;
            int.TryParse(button.name, out buttonNumber); // check which button you pressed
            List<Button> btns = adjacentList[buttonNumber].GetAdjecentList(); //adjacentList indexes match button list indexes, i.e button[0]'s adjacent buttons are in adjacentList[0]

            ToggleClicked(button); // flip the clicked button color

            foreach (Button btn in btns)
            {
                //Debug.Log(btn.name);
                if (btn.image.sprite.name == "button_yellow")
                {
                    //Debug.Log(btn.image.sprite.name);
                    btn.image.sprite = buttonRed;

                }
                else if (btn.image.sprite.name == "button_red")
                {
                    //Debug.Log(btn.image.sprite.name);
                    btn.image.sprite = buttonYellow;

                }
            }
            CheckWin();
        }
        /// <summary>
        /// flip the clicked button color
        /// </summary>
        /// <param name="button"></param>
        private void ToggleClicked(Button button)
        {
            if (button.image.sprite.name == "button_yellow")
            {
                button.image.sprite = buttonRed;
            }
            else if (button.image.sprite.name == "button_red")
            {
                button.image.sprite = buttonYellow;
            }

        }
        /// <summary>
        /// check if all the lights are off
        /// </summary>
        public void CheckWin()
        {
            int numOfYellows = 0;
            foreach (Button btn in buttons)
            {
                if (btn.image.sprite.name == "button_yellow")
                {
                    numOfYellows++;
                }
            }
            //Debug.Log("so close: " + numOfReds);
            if (numOfYellows == 25)
            {
                solved = true;
                sound.AccessGrantedAudio(); //placeholder
                foreach (Button btn in buttons)
                {
                    btn.image.sprite = buttonGreen; //change all buttons green
                }
                Debug.Log("you win the game");
                Player.LightsoutSolved = true;
            }
        }

        /// <summary>
        /// get all buttons created in LightsoffButtons
        /// </summary>
        private void GetButtons()
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("LightsoffButton");
            foreach (GameObject btn in objects)
            {
                buttons.Add(btn.GetComponent<Button>());
            }
        }
        /// <summary>
        /// Setup adjacent buttons.
        /// Button 0 (top left corner) should flip Button 5 (directly below it) and Button 1 (right side)
        /// Button 1 should flip Button 0 (left side), Button 2(right side) and Button 6 (below it) and so on.
        /// </summary>
        private void SetAdjacent()
        {
            int i = 0;
            foreach (Button btn in buttons)
            {
                //create object of the helper class
                //adjacentbuttons keeps a list of all the adjacent buttons you add to with SetAdjacentButton
                AdjacentButtons toList = new AdjacentButtons();



                //horizontal adjacents

                //first row, directly below buttons
                //first row doesnt have anything above it so we can only do i+5
                if (i >= 0 && i <= 4)
                {
                    toList.SetAdjacentButton(buttons[i + 5]);
                    // Debug.Log("added buttons " + i);
                }

                //second, third and fourth row, below and ontop buttons
                //these are middle rows so we can do i-5 and i+5
                if ((i >= 5 && i <= 9) || (i >= 10 && i <= 14) || (i >= 15 && i <= 19))
                {
                    toList.SetAdjacentButton(buttons[i - 5]);
                    toList.SetAdjacentButton(buttons[i + 5]);
                    // Debug.Log("added buttons " + i);
                }
                //fifth row, directly above
                //this one doesnt have anything below it so we can only do i-5
                if (i >= 20 && i <= 24)
                {
                    toList.SetAdjacentButton(buttons[i - 5]);
                    //Debug.Log("added buttons " + i);
                }

                //vertical adjecents

                //first column
                if (i == 0 || i == 5 || i == 10 || i == 15 || i == 20)
                {
                    toList.SetAdjacentButton(buttons[i + 1]);
                }

                //second column
                if (i == 1 || i == 6 || i == 11 || i == 16 || i == 21)
                {
                    toList.SetAdjacentButton(buttons[i + 1]);
                    toList.SetAdjacentButton(buttons[i - 1]);
                }

                //third column
                if (i == 2 || i == 7 || i == 12 || i == 17 || i == 22)
                {
                    toList.SetAdjacentButton(buttons[i + 1]);
                    toList.SetAdjacentButton(buttons[i - 1]);
                }

                //fourth column
                if (i == 3 || i == 8 || i == 13 || i == 18 || i == 23)
                {
                    toList.SetAdjacentButton(buttons[i + 1]);
                    toList.SetAdjacentButton(buttons[i - 1]);
                }

                //fift column
                if (i == 4 || i == 9 || i == 14 || i == 19 || i == 24)
                {
                    toList.SetAdjacentButton(buttons[i - 1]);
                }
                //Debug.Log(i);
                i++;
                adjacentList.Add(toList);
            }
        }
        /// <summary>
        /// use one of the predetermined game patterns
        /// totally random ones seemed to have impossible solutions
        /// </summary>
        /// <param name="pattern"></param>
        private void GamePattern(int pattern)
        {
            //indexer to help change some lights on
            int j = 0;
            if (pattern == 1) // solution: lightchase(x-xx-) then - - - - X and chase again
            {
                foreach (Button btn in buttons)
                {
                    btn.onClick.AddListener(() => ButtonClicked(btn));
                    if (j == 0 || j == 1 || j == 5 || j == 7 || j == 9 || j == 12 || j == 13 || j == 14 || j == 15 || j == 16 || j == 18 || j == 20 || j == 22 || j == 24)
                    {
                        btn.image.sprite = buttonRed;
                    }
                    else
                    {
                        btn.image.sprite = buttonYellow;
                    }

                    j++;
                }
            }
            else if (pattern == 2) // solution: lightchase(xxx--) then - X - - - and chase again
            {
                foreach (Button btn in buttons)
                {
                    btn.onClick.AddListener(() => ButtonClicked(btn));
                    if (j == 0 || j == 1 || j == 3 || j == 4 || j == 5 || j == 9 || j == 10 || j == 13 || j == 19 || j == 20 || j == 23)
                    {
                        btn.image.sprite = buttonRed;
                    }
                    else
                    {
                        btn.image.sprite = buttonYellow;
                    }

                    j++;
                }
            }
            else if (pattern == 3) // solution: lightchase(--xxx) then - - - X - and chase again
            {
                foreach (Button btn in buttons)
                {
                    btn.onClick.AddListener(() => ButtonClicked(btn));
                    if (j == 5 || j == 9 || j == 11 || j == 12 || j == 13 || j == 14 || j == 15 || j == 16 || j == 20 || j == 21 || j == 22 || j == 24)
                    {
                        btn.image.sprite = buttonRed;
                    }
                    else
                    {
                        btn.image.sprite = buttonYellow;
                    }

                    j++;
                }
            }
        }

        /// <summary>
        /// hides the game and sets not interactable
        /// </summary>
        public void HideGame()
        {
            GotoMouse.MenuOpen = false;
            lightsoffCanvas.alpha = 0f;
            lightsoffCanvas.interactable = false;
            lightsoffCanvas.blocksRaycasts = false;
        }
        /// <summary>
        /// show the game and sets interactable
        /// </summary>
        public void ShowGame()
        {
            GotoMouse.MenuOpen = true;
            lightsoffCanvas.alpha = 1f;
            lightsoffCanvas.interactable = true;
            lightsoffCanvas.blocksRaycasts = true;
        }

        /// <summary>
        /// return game solved status
        /// </summary>
        public bool Solved
        {
            get { return this.solved; }
            set { this.solved = value; }
        }

        /// <summary>
        /// add a delay for closing the game window
        /// </summary>
        /// <param name="sec"></param>
        /// <returns></returns>
        public IEnumerator WaitForSecs(float sec)
        {
            yield return new WaitForSeconds(sec);
            HideGame();

        }
    }
}
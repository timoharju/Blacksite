using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    /// <summary>
    /// handles the cheat menu
    /// </summary>
    public class Cheats
    {

        CanvasGroup cheats;
        Button cheatsHideButton;
        Button cheatsAddMinute;
        Button cheatsDoubleSpeed;
        Button cheatsNormalSpeed;
        /// <summary>
        /// create cheats menu buttons and add listeners, sets menu hidden by default
        /// </summary>
        /// <param name="cheats"></param>
        public Cheats(CanvasGroup cheats)
        {
            this.cheats = cheats;
            cheatsHideButton = GameObject.Find("CheatsHideButton").GetComponent<Button>();
            cheatsAddMinute = GameObject.Find("CheatsAddMinute").GetComponent<Button>();
            cheatsDoubleSpeed = GameObject.Find("CheatsDoubleSpeed").GetComponent<Button>();
            cheatsNormalSpeed = GameObject.Find("CheatsNormalSpeed").GetComponent<Button>();

            cheatsHideButton.onClick.AddListener(() => HideCheats());
            cheatsAddMinute.onClick.AddListener(() => AddMinuteOnClick());
            cheatsDoubleSpeed.onClick.AddListener(() => DoubleSpeedOnClick());
            cheatsNormalSpeed.onClick.AddListener(() => ReduceSpeed());

            cheats.alpha = 0f;
            cheats.interactable = false;
            cheats.blocksRaycasts = false;

        }
        /// <summary>
        /// show cheats menu
        /// </summary>
        public void ShowCheats()
        {
            GotoMouse.MenuOpen = true;
            cheats.alpha = 1f;
            cheats.interactable = true;
            cheats.blocksRaycasts = true;
        }
        /// <summary>
        /// cheats hide menu button functionality
        /// </summary>
        private void HideCheats()
        {
            GotoMouse.MenuOpen = false;
            cheats.alpha = 0f;
            cheats.interactable = false;
            cheats.blocksRaycasts = false;
        }
        /// <summary>
        /// adds a minute to the timer
        /// </summary>
        private void AddMinuteOnClick()
        {
            GameController.TimeLeft += 60;
        }
        /// <summary>
        /// doubles characters speed
        /// </summary>
        private void DoubleSpeedOnClick()
        {
            MainController.Speed = MainController.Speed * 2;
            GotoMouse.clickToMoveSpeed *= 2;
        }
        /// <summary>
        /// reduces character speed by half
        /// </summary>
        private void ReduceSpeed()
        {
            MainController.Speed = MainController.Speed / 2;
            GotoMouse.clickToMoveSpeed /= 2;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cheats {

    CanvasGroup cheats;
    Button cheatsHideButton;
    Button cheatsAddMinute;
    Button cheatsDoubleSpeed;
    Button cheatsNormalSpeed;

    public Cheats(CanvasGroup cheats)
    {
        this.cheats = cheats;
        cheatsHideButton = GameObject.Find("CheatsHideButton").GetComponent<Button>();
        cheatsAddMinute = GameObject.Find("CheatsAddMinute").GetComponent<Button>();
        cheatsDoubleSpeed = GameObject.Find("CheatsDoubleSpeed").GetComponent<Button>();
        cheatsNormalSpeed = GameObject.Find("CheatsNormalSpeed").GetComponent<Button>();

        cheatsHideButton.onClick.AddListener(() => HideButtonOnClick());
        cheatsAddMinute.onClick.AddListener(() => AddMinuteOnClick());
        cheatsDoubleSpeed.onClick.AddListener(() => DoubleSpeedOnClick());
        cheatsNormalSpeed.onClick.AddListener(() => NormalSpeedOnClick());

        cheats.alpha = 0f;
        cheats.interactable = false;
        cheats.blocksRaycasts = false;

    }

    public void ShowCheats()
    {
        cheats.alpha = 1f;
        cheats.interactable = true;
        cheats.blocksRaycasts = true;
    }

    private void HideButtonOnClick()
    {
        cheats.alpha = 0f;
        cheats.interactable = false;
        cheats.blocksRaycasts = false;
    }

    private void AddMinuteOnClick()
    {
        //timer = timer+60seconds
    }

    private void DoubleSpeedOnClick()
    {
        MainController.Speed = MainController.Speed * 2;
    }

    private void NormalSpeedOnClick()
    {
        MainController.Speed = MainController.Speed / 2;
    }



	
}

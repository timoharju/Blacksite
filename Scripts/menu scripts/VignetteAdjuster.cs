using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Adjust vignette alpha value so you get the intended play experience on diffirent devices
/// </summary>
public class VignetteAdjuster : MonoBehaviour {

    Image vignette;
    Slider slider;
    Button okButton;
    Slider loadingBar;

    /// <summary>
    /// returns the slider value that the user inputted
    /// </summary>
    public static float SliderValue { get; set; }

	// Use this for initialization
	void Start () {

        //android settings
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        vignette = GameObject.Find("Vignette").GetComponent<Image>();
        slider = GameObject.Find("Slider").GetComponent<Slider>();
        okButton = GameObject.Find("OkButton").GetComponent<Button>();
        loadingBar = GameObject.Find("LoadingBar").GetComponent<Slider>();

        loadingBar.value = 0f;
        loadingBar.gameObject.SetActive(false);

        slider.value = 255f;
        okButton.onClick.AddListener(() => OkButtonClicked());
	}
	
	// Update is called once per frame
	void Update () {
        //update vignettes alpha value as the user slides the slider
        vignette.color = new Color32(0, 0, 0, (byte)slider.value);
        SliderValue = slider.value;
	}

    /// <summary>
    /// executes next scene loading
    /// </summary>
    void OkButtonClicked()
    {
        loadingBar.gameObject.SetActive(true);
        StartCoroutine(LoadAsync("newScene"));
    }

    /// <summary>
    /// loads next scene and updates loading bar
    /// credits to Backeys https://www.youtube.com/watch?v=YMj2qPq9CP8
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    IEnumerator LoadAsync(string name)
    {
        //make loading an asyncoperation so you can check it's progress
        AsyncOperation operation = SceneManager.LoadSceneAsync(name);

        //while it's not done update progress bar
        while (!operation.isDone)
        {
            //clamp value between 0 and 1, since 90% of the progress is Loading and the last 10% is Activating, we want the progress bar to reflect only loading
            //we would never see the last 10% because during Activating phase Unity destroys all objects we don't need
            //but this way we "stretch" the 90% to be a full 100%, as far as the loading bar is concerned
            //so just "divide 0-1 into equal parts of 0.9"
            float progress = Mathf.Clamp01(operation.progress / .9f);

            //update loading bar progress
            loadingBar.value = progress;

            //return null if not done
            yield return null;
        }
    }
}

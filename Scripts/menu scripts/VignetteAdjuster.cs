using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VignetteAdjuster : MonoBehaviour {

    Image vignette;
    Slider slider;
    Button okButton;

    public static float SliderValue { get; set; }

	// Use this for initialization
	void Start () {
        vignette = GameObject.Find("Vignette").GetComponent<Image>();
        slider = GameObject.Find("Slider").GetComponent<Slider>();
        okButton = GameObject.Find("OkButton").GetComponent<Button>();
        slider.value = 255f;
        okButton.onClick.AddListener(() => OkButtonClicked());
	}
	
	// Update is called once per frame
	void Update () {
        vignette.color = new Color32(0, 0, 0, (byte)slider.value);
        SliderValue = slider.value;
	}

    void OkButtonClicked()
    {
        SceneManager.LoadSceneAsync("newScene");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipegameButtons : MonoBehaviour {

    //in unity configurations
    [SerializeField]
    private Transform puzzleField; //prefab field where the buttons go
    [SerializeField]
    private GameObject btn; //prefab button

    /// <summary>
    /// awake happens before start need to use awake for this one,
    /// because we add these buttons to a list on Start() in GameControllers Keypad call
    /// if we call Start here the buttons won't exist in time
    /// </summary>
    void Awake()
    {
        for (int i = 0; i < 25; i++)
        {
            GameObject button = Instantiate(btn);//create a button
            button.name = "" + i; //assign a name for the button
            button.transform.SetParent(puzzleField, false); //set buttons inside puzzleField
        }
    }

}

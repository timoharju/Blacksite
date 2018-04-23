using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// handles the pipegame
/// </summary>
public class Pipegame {

    private List<Button> buttons;
    private CanvasGroup pipeCanvasgroup;
    private Sprite pipeStraight;
    private Sprite pipeCurve;

    /// <summary>
    /// find canvas, load sprites and setup buttons
    /// </summary>
    public Pipegame()
    {
        pipeCanvasgroup = GameObject.Find("PipegameCanvas").GetComponent<CanvasGroup>();
        pipeStraight = Resources.Load<Sprite>("pipe_straight");
        pipeCurve = Resources.Load<Sprite>("pipe_90");
        buttons = new List<Button>();
        //get all the buttons we created and put them in a list
        GetButtons();
        //setup possible winning pattern
        SetupButtons();

        //set game solved to false, not sure if autoproperty is false by default
        Solved = false;

        //hide game by default
        CloseGame();
    }
    /// <summary>
    /// get all the buttons tagged PipegameButton and put them on a list
    /// </summary>
    private void GetButtons()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PipegameButton");
        foreach(GameObject obj in objects)
        {
            buttons.Add(obj.GetComponent<Button>());
        }
    }
    /// <summary>
    /// set up a winning pattern
    /// </summary>
    private void SetupButtons()
    {
        int i = 0;
        foreach (Button button in buttons)
        {
            int r = RandomInt();
            button.onClick.AddListener(() => ButtonClicked(button));

            if (i % 2 == 0)
            {
                button.image.sprite = pipeStraight;
            }
            if (i % 2 != 0)
            {
                button.image.sprite = pipeCurve;
            }

            //straight pipes for pattern
            if (i == 1 || i == 5 || i == 10 || i == 15 || i == 21 || i == 22 || i == 18 || i == 8 || i == 14 || i == 19 || i == 24)
            {
                button.image.sprite = pipeStraight;
            }
            //curve pipes for pattern
            if (i == 20 || i == 23 || i == 13 || i == 12 || i == 7 || i == 9)
            {
                button.image.sprite = pipeCurve;
            }
            int j = 0;
            //turn random amount
            while (j < r)
            {
                button.transform.Rotate(0, 0, 90);
                j++;
            }

            i++;
        }
    }
    /// <summary>
    /// check if you win or not
    /// </summary>
    private void CheckWin()
    {
        int i = 0;
        int pipesInPosition = 0; //store number of pipes in the right position
        foreach (Button button in buttons)
        {
            var zangle = button.transform.eulerAngles.z;
            //check all the pipes against winning pattern

            //first column

            if (i == 0 && (zangle == 0 || zangle == 180))
            {
                pipesInPosition++;
            }

            if (i == 5 && (zangle == 0 || zangle == 180))
            {
                pipesInPosition++;
            }

            if (i == 10 && (zangle == 0 || zangle == 180))
            {
                pipesInPosition++;
            }

            if (i == 15 && (zangle == 0 || zangle == 180))
            {
                pipesInPosition++;
            }

            if (i == 20 && (zangle == 90))
            {
                pipesInPosition++;
            }

            //second clumn
            if (i == 21 && (zangle == 90 || zangle == 270))
            {
                pipesInPosition++;
            }

            //third column
            if (i == 22 && (zangle == 90 || zangle == 270))
            {
                pipesInPosition++;
            }

            if (i == 12 && (zangle == 90))
            {
                pipesInPosition++;
            }

            if (i == 7 && (zangle == 0))
            {
                pipesInPosition++;
            }

            //fourth clumn
            if (i == 23 && (zangle == 180))
            {
                pipesInPosition++;
            }

            if (i == 18 && (zangle == 0 || zangle == 180))
            {
                pipesInPosition++;
            }

            if (i == 13 && (zangle == 270))
            {
                pipesInPosition++;
            }

            if (i == 8 && (zangle == 90 || zangle == 270))
            {
                pipesInPosition++;
            }

            //fifth column

            if (i == 9 && (zangle == 270))
            {
                pipesInPosition++;
            }

            if (i == 14 && (zangle == 0 || zangle == 180))
            {
                pipesInPosition++;
            }

            if (i == 19 && (zangle == 0 || zangle == 180))
            {
                pipesInPosition++;
            }

            if (i == 24 && (zangle == 0 || zangle == 180))
            {
                pipesInPosition++;
            }

            i++;
        }
        //if all the right pipes are in position
        if(pipesInPosition == 17)
        {
            Solved = true;
            pipeCanvasgroup.blocksRaycasts = false;
            Player.PipegameSolved = true;
        }
        //Debug.Log("pipes: " + pipesInPosition);
    }

    /// <summary>
    /// return a random int between 1 and 4
    /// </summary>
    /// <returns></returns>
    private int RandomInt()
    {
        return Random.Range(1, 5);
    }
    /// <summary>
    /// rotate button 90 degrees on click
    /// </summary>
    /// <param name="button"></param>
    private void ButtonClicked(Button button)
    {
        button.transform.Rotate(0, 0, 90);
        CheckWin();
        //Debug.Log("angles:" + button.transform.eulerAngles.z);
    }
    /// <summary>
    /// close the game and set not interactable
    /// </summary>
    public void CloseGame()
    {
        pipeCanvasgroup.alpha = 0f;
        pipeCanvasgroup.interactable = false;
        pipeCanvasgroup.blocksRaycasts = false;
        GotoMouse.MenuOpen = false;

    }
    /// <summary>
    /// show game and set interactable
    /// </summary>
    public void ShowGame()
    {
        pipeCanvasgroup.alpha = 1f;
        pipeCanvasgroup.interactable = true;
        pipeCanvasgroup.blocksRaycasts = true;
        GotoMouse.MenuOpen = true;
    }

    /// <summary>
    /// set or get bool to see if you have solved the game or not
    /// </summary>
    private bool Solved { get; set; }

}

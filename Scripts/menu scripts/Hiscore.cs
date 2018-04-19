using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Handles local highscores, stays between sessions
/// </summary>
public class Hiscore : MonoBehaviour {

    Text hiscores;
    InputField inp;
    Text yourScore;
    Button exitButton;
    int score;
    string scoreString;
    string tmp = "";
    string formatedPath;
    string path;
    string inputName;
    bool hasEntered = false;
    List<Score> scores = new List<Score>();
    List<Score> newScores = new List<Score>();
    List<string> file;
    Score p1;
    Score p2;
    Score p3;
    Score p4;
    Score p5;
    Score p6;

	// Use this for initialization
	void Start () {

        score = Random.Range(1000, 1500);

        p1 = new Score();
        p2 = new Score();
        p3 = new Score();
        p4 = new Score();
        p5 = new Score();
        p6 = new Score();

        path = Application.dataPath + "/scores.txt";
        formatedPath = Application.dataPath + "/hiscore.txt";


        hiscores = GameObject.Find("ScoreText").GetComponent<Text>();
        inp = GameObject.Find("InputField").GetComponent<InputField>();
        yourScore = GameObject.Find("YourScore").GetComponent<Text>();
        exitButton = GameObject.Find("ExitButton").GetComponent<Button>();

        exitButton.onClick.AddListener(() => ExitButtonClicked());

        inp.onEndEdit.AddListener(delegate { EditEnd(); });

        //create files if they don't exist for whatever reason
        if (!File.Exists(formatedPath))
        {
            File.Create(formatedPath);
        }

        if (!File.Exists(path))
        {
            File.Create(path);
        }

        //read highscores to a list
        file = new List<string>(File.ReadAllLines(path));
        
        //add data to score objects
        PopulateList();

        //display the old list
        DisplayOldList();

        CalculateScore();
        yourScore.text = "Your Score: " + score;

        AssetDatabase.Refresh();
	}
	
	// Update is called once per frame
	void Update () {
        CalculateScore();
        //update text field with the highscores
        hiscores.text = File.ReadAllText(formatedPath);

        //after you have entered your name disable the input field
        bool check = (!hasEntered) ? true : false;
        inp.gameObject.SetActive(check);
        

	}



    /// <summary>
    /// Called when you have entered your nickname for the highscore list
    /// </summary>
    private void EditEnd()
    {
        //save inputfield text to inputName
        inputName = inp.text;
        scoreString = score.ToString();
        p6.ScoreN = scoreString;
        p6.Name = inputName;

        //erase input field text
        inp.text = "";

        //once you've entered your name update the lists
        MakeNewLists();
        hasEntered = true;
    }

    private void ExitButtonClicked()
    {
        SceneManager.LoadSceneAsync("Credits");
    }

    /// <summary>
    /// add values to Score objects from the list
    /// make sure there is always 5 people with a score on the list (10 lines)
    /// </summary>
    private void PopulateList()
    {
        
            p1.Name = file[0];
            p1.ScoreN = file[1];

            p2.Name = file[2];
            p2.ScoreN = file[3];
       
            p3.Name = file[4];
            p3.ScoreN = file[5];
       
            p4.Name = file[6];
            p4.ScoreN = file[7];
        
            p5.Name = file[8];
            p5.ScoreN = file[9];
        

    }

    /// <summary>
    /// displays highscore history
    /// </summary>
    private void DisplayOldList()
    {
        scores.Add(p1);
        scores.Add(p2);
        scores.Add(p3);
        scores.Add(p4);
        scores.Add(p5);
        scores.Sort(delegate (Score x, Score y)
        {
           return y.ScoreN.CompareTo(x.ScoreN);
        });

        foreach(Score s in scores)
        {
            Debug.Log(s.ScoreN);
        }
        
        using (TextWriter tw = new StreamWriter(formatedPath))
        {
            foreach (Score s in scores)
            {
                tw.WriteLine(s.Name + " " + s.ScoreN);
            }
        }
        AssetDatabase.Refresh();
    }

    /// <summary>
    /// updates the lists with new data
    /// </summary>
    private void MakeNewLists()
    {
        //remove all old elements
        scores.RemoveRange(0, 5);

        //add updated elements
        scores.Add(p1);
        scores.Add(p2);
        scores.Add(p3);
        scores.Add(p4);
        scores.Add(p5);
        scores.Add(p6);
        scores.Sort(delegate (Score x, Score y)
        {
            return y.ScoreN.CompareTo(x.ScoreN);
        });

        foreach (Score s in scores)
        {
            Debug.Log(s.ScoreN);
        }
        scores.RemoveAt(5);

        //ovewrite formatted list
        using (TextWriter tw = new StreamWriter(formatedPath))
        {
            foreach (Score s in scores)
            {
                tw.WriteLine(s.Name + " " + s.ScoreN);
            }
        }

        //overwrite the list used to make the formatted list
        using (TextWriter tw = new StreamWriter(path))
        {
            foreach (Score s in scores)
            {
                tw.WriteLine(s.Name);
                tw.WriteLine(s.ScoreN);
            }
        }
        //update local Lists
        UpdateCache();
        AssetDatabase.Refresh();
    }

    /// <summary>
    /// update local lists even though you can only enter one highscore per try, this would allow you to enter multiple and have the list update properly
    /// </summary>
    private void UpdateCache()
    {
        file = new List<string>(File.ReadAllLines(path));
        PopulateList();
    }

    /// <summary>
    /// calculate players score
    /// </summary>
    private void CalculateScore()
    {
        int clicks = GotoMouse.numberOfClicks;
        float timeLeft = GameController.TimeLeft;

        int score = (int)timeLeft * 5;
        score -= clicks;
        
        if(score < 1000)
        {
            //ph for testing
            this.score = 1000;
            //this.score = 0;
        }
    }
}

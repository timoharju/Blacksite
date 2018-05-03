using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    string scoreString; //string version of score
    string formatedPath; //filepath
    string path; //filepath
    string inputName; //the string you input into InputField
    bool hasEntered = false;
    List<Score> scores = new List<Score>(); 
    List<string> file;  //file gets read into this
    Score p1;
    Score p2;
    Score p3;
    Score p4;
    Score p5;
    Score p6;

	// Use this for initialization
	void Start () {

        //android settings
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        p1 = new Score();
        p2 = new Score();
        p3 = new Score();
        p4 = new Score();
        p5 = new Score();
        p6 = new Score();

        //pc version datapaths
        /*
        path = Application.dataPath + "/scores.txt";
        formatedPath = Application.dataPath + "/hiscore.txt";
        */

        //android version datapaths (works on pc too, C:\Users\USER_NAME\AppData\LocalLow\DefaultCompany\Seikkailutime)
        path = Application.persistentDataPath + "/scores.txt";
        formatedPath = Application.persistentDataPath + "/hiscores.txt";

        Debug.Log(formatedPath);
        hiscores = GameObject.Find("ScoreText").GetComponent<Text>();
        inp = GameObject.Find("InputField").GetComponent<InputField>();
        yourScore = GameObject.Find("YourScore").GetComponent<Text>();
        exitButton = GameObject.Find("ExitButton").GetComponent<Button>();

        exitButton.onClick.AddListener(() => ExitButtonClicked());

        inp.onEndEdit.AddListener(delegate { EditEnd(); });

        //create files if they don't exist for whatever reason
        Debug.Log(formatedPath);
        // inp.text = formatedPath;
        if (!File.Exists(path))
        {
            Debug.Log("exists");
            using (StreamWriter w = File.AppendText(path))
            {
                w.WriteLine("Dummy");
                w.WriteLine("1000");
                w.WriteLine("Dummy");
                w.WriteLine("1000");
                w.WriteLine("Dummy");
                w.WriteLine("1000");
                w.WriteLine("Dummy");
                w.WriteLine("1000");
                w.WriteLine("Dummy");
                w.WriteLine("1000");
            }
        }

        if (!File.Exists(formatedPath))
        {
            Debug.Log("exists");
            using (StreamWriter w = File.AppendText(formatedPath))
            {
                w.WriteLine("Dummy 1000");
                w.WriteLine("Dummy 1000");
                w.WriteLine("Dummy 1000");
                w.WriteLine("Dummy 1000");
                w.WriteLine("Dummy 1000");
            }
        }
        
         

        //read highscores to a list
        file = new List<string>(File.ReadAllLines(path));

        
        
        //add data to score objects
        ReadFileToScores();

        //display the old list
        DisplayOldList();

        CalculateScore();
        
        
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif

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

    /// <summary>
    /// exit button functions, load the correct scene based on platform and if you progressed through the game or came from the main menus
    /// </summary>
    private void ExitButtonClicked()
    {
        if (LoadSceneOnClick.FromMenu)
        {
            LoadSceneOnClick.FromMenu = false;
            SceneManager.LoadSceneAsync("ComputerMenu");
        }
        else if (Startmenu.FromMenu)
        {
            Startmenu.FromMenu = false;
            SceneManager.LoadSceneAsync("Startmenu");
        }
        else
        {
            SceneManager.LoadSceneAsync("Credits");
        }
        
    }

    /// <summary>
    /// add values to Score objects from the list
    /// make sure there is always 5 people with a score on the list (10 lines)
    /// </summary>
    private void ReadFileToScores()
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

        //overwrite old list with sorted scores
        using (TextWriter tw = new StreamWriter(formatedPath))
        {
            foreach (Score s in scores)
            {
                tw.WriteLine(s.Name + " " + s.ScoreN);
            }
        }
#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif
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
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
    }

    /// <summary>
    /// update local lists even though you can only enter one highscore per try, this would allow you to enter multiple and have the list update properly
    /// </summary>
    private void UpdateCache()
    {
        file = new List<string>(File.ReadAllLines(path));
        ReadFileToScores();
    }

    /// <summary>
    /// calculate players score
    /// </summary>
    private void CalculateScore()
    {
        int clicks = GotoMouse.numberOfClicks;
        float timeLeft = GameController.TimeLeft;

        //score is every second left * 5 minus clicks
        score = (int)timeLeft * 5;
        score -= clicks;
        
        //completing the game is atleast 1000 points
        if(score < 1000 && score > 1 && score > 0)
        {
            this.score = 1000;
            
        }

        //if score is 0 dont let user enter anything
        //this happens if you click leaderboards in the menu
        if(score == 0)
        {
            hasEntered = true;
            yourScore.text = "";
        }
        else
        {
            yourScore.text = "Your Score: " + score;
        }
    }
}

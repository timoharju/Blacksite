using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneOnClick : MonoBehaviour
{

    Button leaderBoards;
    /// <summary>
    /// check if you came straight from the mainmenu or progressed through the game
    /// </summary>
    public static bool FromMenu { get; set; }

    void Start()
    {
        leaderBoards = GameObject.Find("Leaderboard").GetComponent<Button>();
        leaderBoards.onClick.AddListener(() => LoadByName());
        FromMenu = false;
    }

    public void LoadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    private void LoadByName()
    {
        FromMenu = true;
        SceneManager.LoadSceneAsync("HiScore");
    }
}
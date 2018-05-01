using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontStopAudio : MonoBehaviour {

    private static DontStopAudio instance = null;
    public static DontStopAudio Instance
    {
        get { return instance; }
    }
    /// <summary>
    /// Keeps the AudioSource playing for the next scene.
    /// Checks if there is an existing instance, if there already is, it destroys itself. 
    /// Otherwise it stores this instance, so later it can destroy itself again so it doesn't cause issues later.
    /// </summary>
    void Awake()
    {

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
 
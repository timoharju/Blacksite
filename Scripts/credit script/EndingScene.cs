using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingScene : MonoBehaviour {

    float speed = 1;
    private float posXhistory;
    Animator anim;
    Audio sound;
    SpriteRenderer character;
    Vector3 targetX;
    Vector3 targetY;
    Image alienRay;
    ScrollText scroll;
    CanvasGroup dialogue;
    Text dialogueText;

    bool alienSpeech = false;
    bool goal = false;
    bool suck = false;
    bool startLoadingScene = false;
    bool suckTextEvent = false;

    // Use this for initialization
    void Start () {

        //android settings
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        anim = GameObject.Find("MCharacter").GetComponent<Animator>();
        character = GameObject.Find("MCharacter").GetComponent<SpriteRenderer>();
        alienRay = GameObject.Find("AlienRay").GetComponent<Image>();
        dialogueText = GameObject.Find("DialogueText").GetComponent<Text>();
        alienRay.gameObject.SetActive(false);

        sound = new Audio();
        scroll = new ScrollText();
        StartCoroutine(WalkToMiddle());
        targetX = new Vector3(0, transform.position.y, transform.position.z);
        targetY = new Vector3(0, 500f, transform.position.z);
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //when you hit trigger box load new scene
        if(collision.gameObject.name == "Trigger")
        {
            SceneManager.LoadSceneAsync("HiScore");
        }

    }

    // Update is called once per frame
    void Update () {

        if (!scroll.ThreadStatus)
        {
            dialogueText.text = scroll.Text;
        }
        else
        {
            scroll.HideDialogue();
        }

        //move player towards the middle of the screen
        if (!goal)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetX, speed * Time.deltaTime);
        }
        

        //walking animations
        if(speed == 100)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
            {
                anim.Play("walking", -1, 0f);
            }
            sound.FootstepAudio();
        }
        else
        {
            if ((anim.GetCurrentAnimatorStateInfo(0).IsName("walking")))
            {
                anim.Play("idle", -1, 0f);
            }
        }

        //if player is in the middle stop moving, display the ray and enable floating character upwards
        if (transform.position.x <= targetX.x + 0.5 && transform.position.x >= targetX.x - 0.5)
        {
            if (!alienSpeech)
            {
                goal = true;
                //sound.AlienCreepy1Audio();
                alienSpeech = true;
                alienRay.gameObject.SetActive(true);
            }
            speed = 50f;
            suck = true;
        }

        if (suck)
        {
            if (!suckTextEvent)
            {
                scroll.Text = "ded[ph]";
                scroll.StartScrolling();
                suckTextEvent = true;
            }
            
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
            {
                anim.Play("floating", -1, 0f);
            }
            
            transform.position = Vector3.MoveTowards(transform.position, targetY, speed * Time.deltaTime);
        }

        

        

    }

    /// <summary>
    /// add a small delay before you start walking
    /// </summary>
    /// <returns></returns>
    IEnumerator WalkToMiddle()
    {
        Debug.Log("we go");
        yield return new WaitForSeconds(1);
        scroll.Text = "Freedom![ph]";
        scroll.StartScrolling();
        speed = 100;
        
        
    }
}

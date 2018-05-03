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
    RawImage fadeoutImage;

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
        fadeoutImage = GameObject.Find("FadeoutOverlay").GetComponent<RawImage>();
        alienRay.gameObject.SetActive(false);

        sound = new Audio();
        scroll = new ScrollText();
        scroll.HideDialogue();
        StartCoroutine(WalkToMiddle());
        targetX = new Vector3(0, transform.position.y, transform.position.z);
        targetY = new Vector3(0, 500f, transform.position.z);
        //make the fadeout overlay completely transparent
        fadeoutImage.color = new Color32(0, 0, 0, 0);

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
                StartCoroutine(FadeOut());
            }
            speed = 50f;
            suck = true; //enable moving up event
        }

        //check if we want to move the character upwards
        if (suck)
        {
            if (!suckTextEvent)
            {
                /*
                scroll.Text = "ded[ph]";
                scroll.StartScrolling();
                suckTextEvent = true;
                */
            }
            
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
            {
                anim.Play("floating", -1, 0f);
            }
            //move player upwards (towards targetY)
            transform.position = Vector3.MoveTowards(transform.position, targetY, speed * Time.deltaTime);
        }

        

        

    }

    /// <summary>
    /// add a small delay before you start walking
    /// </summary>
    /// <returns></returns>
    private IEnumerator WalkToMiddle()
    {
        yield return new WaitForSeconds(1);
        scroll.Text = "Freedom!";
        scroll.StartScrolling();
        speed = 100;
    }

    /// <summary>
    /// fade the screen out at the end
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadeOut()
    {
        //after two seconds start fading the screen to black
        int alpha = 0;
        yield return new WaitForSeconds(2);
        for(int i = 0; i<=5; i++)
        {
            alpha += 51;
            fadeoutImage.color = new Color32(0, 0, 0, (byte)alpha);
            yield return new WaitForSeconds(2);
        }
        
    }
}

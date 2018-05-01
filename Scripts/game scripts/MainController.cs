using UnityEngine;
using System.Collections;
using System.Threading;
/// <summary>
/// Handles character animations and the sprite, plays footstep sounds
/// </summary>
public class MainController : MonoBehaviour
{
    Rigidbody2D mCharacter;
    SpriteRenderer character;
    Audio sound;
    Animator anim;
    private static float speed = 2f; //default speed
    

    //click to move sprite flip stuff
    private float posXhistory;



    // Use this for initialization, GetComponent<PointerController>
    void Start()
    {


        mCharacter = GameObject.Find("MCharacter").GetComponent<Rigidbody2D>();
        character = GameObject.Find("MCharacter").GetComponent<SpriteRenderer>();
        sound = new Audio();
        anim = GameObject.Find("MCharacter").GetComponent<Animator>();
        posXhistory = mCharacter.position.x;


    }

    // Update is called once per frame: f means float point type
    void Update()
    {

        //click to move sprite flip
        if(mCharacter.position.x > posXhistory)
        {
            //check if prisoner is idle => not animating
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
            {
                anim.Play("walking", -1, 0f);
            }
            
            
            character.flipX = false;
            sound.FootstepAudio();
        }
        if(mCharacter.position.x < posXhistory)
        {
            //check if idle is playing
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
            {
                anim.Play("walking", -1, 0f);
            }


            character.flipX = true;
            sound.FootstepAudio();
        }

        //go back to idle if the player is not moving
        if(GotoMouse.Move == false)
        {
            //check if idle is already playing
            if (!(anim.GetCurrentAnimatorStateInfo(0).IsName("idle")))
            {
                anim.Play("idle", -1, 0f);
            }
            
        }
        
        /*
        //"detect" room change and flip the sprite facing the right way -- the version below is much more smooth, but it can break I think
        if(mCharacter.position.x >= 295) // keep the coordinates same as returnPos and nextRoomPos (from gamecontroller) and it works
        {
            character.flipX = true;
            //Debug.Log("at the edge");
        }

        if(mCharacter.position.x <= -295)
        {
            character.flipX = false;
        }
        */
        //detect room change and flip the sprite facing the right way, if this breaks use the one above
        if(mCharacter.position.x == 280 && posXhistory < 0)
        {
            character.flipX = true;
            Debug.Log("room changed left");
        }

        if(mCharacter.position.x == -280 && posXhistory > 0)
        {
            character.flipX = false;
            Debug.Log("room changed right");
        }
        
        //update history after checking if you moved or not
        posXhistory = mCharacter.position.x;
        
    }
    /// <summary>
    /// get or set the speed, used by console/cheats
    /// </summary>
    public static float Speed
    {
        get { return speed; }
        set { speed = value; }
    }


}

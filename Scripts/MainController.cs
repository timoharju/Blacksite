﻿using UnityEngine;
using System.Collections;
using System.Threading;
/// <summary>
/// Handles character movements and the sprite
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
        //anim
        
        //keyboard movement
        if (Input.GetKey("d"))
        {
            mCharacter.transform.Translate(speed, 0, 0);
            character.flipX = false;
            sound.FootstepAudio();
        }
        if (Input.GetKey("a"))
        {
            mCharacter.transform.Translate(-speed, 0, 0);
            character.flipX = true;
            sound.FootstepAudio();
        }
        // up and down axis not intended at the moment
        if (Input.GetKey("s"))
        {
            mCharacter.transform.Translate(0, -speed, 0);
        }
        if (Input.GetKey("w"))
        {
            mCharacter.transform.Translate(0, speed, 0);
        }

        //click to move sprite flip attempt
        if(mCharacter.position.x > posXhistory)
        {   
            //check if prisoner is idle => not animating
            if(character.sprite.name == "prisoner_idle")
            {
                anim.Play("run", -1, 0f);
            }
            
            character.flipX = false;
            sound.FootstepAudio();
        }
        if(mCharacter.position.x < posXhistory)
        {
            if (character.sprite.name == "prisoner_idle")
            {
                anim.Play("run", -1, 0f);
            }

            character.flipX = true;
            sound.FootstepAudio();
        }

        

        //detect roomchange and flip the character facing the right way
        if (mCharacter.position.x == 300 && posXhistory < 0)
        {
            character.flipX = true;
            //Debug.Log("room change left");
        }

        if (mCharacter.position.x == -300 && posXhistory > 0)
        {
            character.flipX = false;
            //Debug.Log("room change right");
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

using UnityEngine;
using System.Collections;

public class MainController : MonoBehaviour
{

    PointerController up;
    PointerController down;
    PointerController left;
    PointerController right;
    Rigidbody2D mCharacter;
    Vector2 movement;
    SpriteRenderer character;
    private static float speed = 2f;
    

    // Use this for initialization, GetComponent<PointerController>
    void Start()
    {
        left = GameObject.Find("Left").GetComponent<PointerController>();
        right = GameObject.Find("Right").GetComponent<PointerController>();
        mCharacter = GameObject.Find("MCharacter").GetComponent<Rigidbody2D>();
        character = GameObject.Find("MCharacter").GetComponent<SpriteRenderer>();
        

    }

    // Update is called once per frame: f means float point type
    void Update()
    {

        if (left.getPressed())
        {
            mCharacter.transform.Translate(-speed, 0, 0);
            character.flipX = true;
        }
        if (right.getPressed())
        {
            mCharacter.transform.Translate(speed, 0, 0);
            character.flipX = false;
        }
        if (Input.GetKey("w"))
        {
            mCharacter.transform.Translate(0, speed, 0);
        }
        if (Input.GetKey("d"))
        {
            mCharacter.transform.Translate(speed, 0, 0);
            character.flipX = false;
        }
        if (Input.GetKey("a"))
        {
            mCharacter.transform.Translate(-speed, 0, 0);
            character.flipX = true;
        }
        
        if (Input.GetKey("s"))
        {
            mCharacter.transform.Translate(0, -speed, 0);
        }
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

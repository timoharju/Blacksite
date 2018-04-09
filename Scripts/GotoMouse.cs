using UnityEngine;
using System.Collections;
/// <summary>
/// Click to move functionality
/// </summary>
public class GotoMouse : MonoBehaviour
{

    public static float clickToMoveSpeed = 200f; //character movement speed, about the same as WASD speed that is set in MainController
    private Vector3 target; //mouse click location
    private Vector3 targetX; //just the X component of mouse click location
    private float goal; //mouse click location in float
    public static bool move = false;  // check if we want to move(=> have clicked)
    public static bool menuOpen = false;    //are there any menus open
    Ray ray;
    RaycastHit hit;

    void Start()
    {
        target = transform.position;
    }

    void Update()
    {
        //left mouse button
        
        if (Input.GetMouseButtonDown(0))
        {

            target = Camera.main.ScreenToWorldPoint(Input.mousePosition); //get mouse location
            if (!(target.x <= -350 || target.x >= -296 || target.y <= 3))
            {
                //Debug.Log("target is within x block");
                if (!(target.y <= 3 || target.y >= 197))
                {
                    //Debug.Log("target is within Y block");
                }
            }
            else
            {
                
                targetX = new Vector3(target.x, transform.position.y, transform.position.z); //store only X component of x location
                goal = target.x;
                move = true;
            }
            
            
            //Debug.Log(target);
        }
        if (move == true && menuOpen == false)
        {
            //target.z = transform.position.z;

            transform.position = Vector3.MoveTowards(transform.position, targetX, clickToMoveSpeed * Time.deltaTime); //move towards x position of mouse location, y and z stay the same
            if (transform.position.x == goal)
            {
                move = false;
                //Debug.Log("I made it");
            }

        }
        
    }
}

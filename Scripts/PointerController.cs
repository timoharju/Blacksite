using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PointerController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    //class PointerController extends MonoBehaviour implements 
    //IPointerDownHandler, IPointerUpHandler


    private bool pressed;
    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log (eventData); 
        GotoMouse.menuOpen = false;
        GotoMouse.move = false;
        pressed = false;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log (eventData);
        //Debug.Log("pointerdown");
        GotoMouse.menuOpen = true;
        GotoMouse.move = false;
        pressed = true;
    }
    public bool getPressed()
    {
        return pressed;
    }

    

}

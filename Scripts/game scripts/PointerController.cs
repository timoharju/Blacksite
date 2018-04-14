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
        GotoMouse.MenuOpen = false;
        GotoMouse.Move = false;
        pressed = false;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log (eventData);
        //Debug.Log("pointerdown");
        GotoMouse.MenuOpen = true;
        GotoMouse.Move = false;
        pressed = true;
    }
    public bool getPressed()
    {
        return pressed;
    }

    

}

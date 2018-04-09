using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// create oldschool type scrolling text to a textfield
/// </summary>
public class ScrollText {

    private string scrolling;
    private string source;
    string defaultText = "default tester scrolling text";
    bool status = true;
    bool skipChoice = false;
    int readingTime = 2500;
    CanvasGroup dialogueCanvas;
    

    /// <summary>
    /// create the object with default text
    /// </summary>
    public ScrollText()
    {
        scrolling = "";
        source = defaultText;
        dialogueCanvas = GameObject.Find("DialogueCanvas").GetComponent<CanvasGroup>();
        

        dialogueCanvas.alpha = 0;
        dialogueCanvas.interactable = false;
        dialogueCanvas.blocksRaycasts = false;
    }
    
    /// <summary>
    /// method used by the thread to scroll the text
    /// </summary>
    /// <param name="state"></param>
    private void Scroll(object state) // thread pooli haluaa object state, ei mitään hajua mitä sillä pitäisi/kannattaisi tehdä
    {
        
        for(int i = 0; i<source.Length; i++)
        {
            scrolling += source[i];
            Thread.Sleep(50);
        }
        Thread.Sleep(readingTime);
        Finish();
        FlushStoryText();
        Debug.Log("scrolling finished!");

    }


    /// <summary>
    /// sets status to true when thread finishes
    /// </summary>
    private void Finish()
    {
        this.status = true;
    }
    /// <summary>
    /// call this to start scrolling the text
    /// </summary>
    public void StartScrolling()
    {
        if (status) // check if text is scrolling already
        {
            ShowDialogue();
            status = false; // set thread status to not available
            ThreadPool.QueueUserWorkItem(Scroll); // metodissa oltava parametri (object state)
        }
        else
        {
            Debug.Log("cant start another thread yet!");
        }
        
        
    }
    /// <summary>
    /// empty the scrolling string
    /// </summary>
    public void FlushStoryText()
    {
        scrolling = "";
    }
    /// <summary>
    /// get text depending on skip choice, and set the string you want to scroll
    /// </summary>
    public string Text {
        get {
            if(skipChoice == true)
            {
                return this.source;
            }
            else
            {
                return this.scrolling;
            }
            
        }
        set { this.source = value; }
    }

    /// <summary>
    /// set or get skip choice boolean
    /// </summary>
    public bool SkipChoice
    {
        get { return this.skipChoice; }
        set { this.skipChoice = value; }
    }
    /// <summary>
    /// return thread status, true = finished - false = still running
    /// </summary>
    public bool ThreadStatus
    {
        get { return this.status; }
    }
    /// <summary>
    /// set a custom timer for delay between texts, default 2500ms.
    /// </summary>
    public int ReadingDelay
    {
        set { readingTime = value; }
    }
    /// <summary>
    /// hide dialogue box
    /// </summary>
    public void HideDialogue()
    {
        dialogueCanvas.alpha = 0f;
    }
    /// <summary>
    /// show dialogue box
    /// </summary>
    private void ShowDialogue()
    {
        dialogueCanvas.alpha = 1f;
    }

}

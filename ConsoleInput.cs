using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Toggle a "console" type input field which takes plain text as commands,
/// console is hidden by default!
/// </summary>
public class ConsoleInput {

    InputField inputfield;  //"console"
    CanvasGroup canvasgroup;    //the canvas, inputfield should be a child
    Button consoleButton; // must be in any other canvas or it's also hidden!
    Text consoleInfoText;
    int state = 0; //console open or not, 0 = closed, 1 = open
    string consolestring = "";
    Player player;
    



    /// <summary>
    /// Takes InputField, CanvasGroup and a Button as a parameter
    /// InputField is the console type field which takes text, canvasgroup is a canvas in which the console resides so it's easy to hide
    /// and the button toggles the canvas/inputfield/console visible
    /// </summary>
    /// <param name="inputfield"></param>
    /// <param name="canvasgroup"></param>
    /// <param name="consoleButton"></param>
    public ConsoleInput(InputField inputfield, CanvasGroup canvasgroup, Button consoleButton)
    {
        this.inputfield = inputfield;
        this.canvasgroup = canvasgroup;
        this.consoleButton = consoleButton;
        inputfield.onEndEdit.AddListener(delegate { EditEnd(); });
        this.consoleButton.onClick.AddListener(() => ToggleConsole());
        canvasgroup.alpha = 0f;
        canvasgroup.interactable = false;
        consoleInfoText = GameObject.Find("ConsoleText").GetComponent<Text>();
        consoleInfoText.text = "";
    }
    /// <summary>
    /// triggered by consoleToggle onClick
    /// </summary>
    public void ToggleConsole()
    {
        
        if(state == 0)
        {
            canvasgroup.alpha = 1f;
            canvasgroup.interactable = true;
            state = 1;
        }
        else
        {
            canvasgroup.alpha = 0f;
            canvasgroup.interactable = false;
            state = 0;
        }
        
    }
    /// <summary>
    /// triggered by line 22, inputfield.onEndEdit
    /// </summary>
    public void EditEnd() 
    {
        string inputText = inputfield.text;
        inputfield.text = "";
        
        command(inputText);
        Debug.Log("command: " + inputText);
    }
    /// <summary>
    /// set the player who knows all rooms
    /// </summary>
    public Player Player
    {
        set { this.player = value; }
    }
    /// <summary>
    /// do something with the command you input in InputField
    /// </summary>
    /// <param name="command"></param>
    private void command(string command)
    {
        if(command == "exit" || command == "Exit")
        {
            if(state == 0)
            {
                state = 1;
            }
            else   //reset state to corret value so it opens up correctly next time
            {
                state = 0;
            }
            
            canvasgroup.alpha = 0f;
            canvasgroup.interactable = false;
            consolestring = "";
            Debug.Log("exited!");
        }
        if (command == "godmode" || command == "Godmode")
        {
            consolestring += "godmode activated!\n";
            consoleInfoText.text = consolestring;
            Debug.Log("godmode activated!");
        }
        if(command == "info" || command == "Info")
        {
            consolestring += command + "\n";
            consolestring += "speed is: " + MainController.Speed + "\n";
            consoleInfoText.text = consolestring;
            //consoleInfoText.text = "speed is: " + MainController.Speed;
        }
        if(command == "clear" || command == "Clear")
        {
            consolestring = "";
            consoleInfoText.text = "";
        }

        if(command == "nextroom" || command == "Nextroom")
        {
            if(player.NextLocation != null)
            {
                player.SetLocation(player.NextLocation);
            }
            else
            {
                consolestring += "no next room set\n";
                consoleInfoText.text = consolestring;
            }
            
        }
        if(command == "prevroom" || command == "Prevroom")
        {
            if(player.PreviousLocation != null)
            {
                player.SetLocation(player.PreviousLocation);
            }
            else
            {
                consolestring += "no previous room set\n";
                consoleInfoText.text = consolestring;
            }
            
            
        }
        if(command == "roomname" || command == "Roomname")
        {
            consolestring += player.LocationName+"\n";
            consoleInfoText.text = consolestring;
        }

        if(command == "position" || command == "Position")
        {
            consolestring += player.Position + "\n";
            consoleInfoText.text = consolestring;
        }

        if(command == "help" || command == "Help" || command == "?")
        {
            consolestring += "available commands:\nnextroom, prevroom, info\n";
            consoleInfoText.text = consolestring;
        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Toggle a "console" type input field which takes plain text as commands,
/// console is hidden by default!
/// </summary>
public class ConsoleInput {

    private InputField inputfield;  //"console"
    private CanvasGroup consoleCanvasGroup;    //the canvas, inputfield should be a child
    private Button consoleButton; // must be in any other canvas or it's also hidden!
    private Text consoleInfoText;
    private int state = 0; //console open or not, 0 = closed, 1 = open
    private string consolestring = "";
    private Player player;
    private LightsoffGame lightsoff;
    private Pipegame pipegame;
    

    /// <summary>
    /// Takes InputField, CanvasGroup, Button and lightsoffGame as parameters
    /// InputField is the console type field which takes text, canvasgroup is a canvas in which the console resides so it's easy to hide
    /// and the button toggles the canvas/inputfield/console visible
    /// lightsoffGame is used for the lightsout game toggle command
    /// </summary>
    /// <param name="inputfield"></param>
    /// <param name="consoleCanvasGroup"></param>
    /// <param name="consoleButton"></param>
    public ConsoleInput(LightsoffGame ligtsoffGame, Pipegame pipegame)
    {
        
        inputfield = GameObject.Find("ConsoleInputField").GetComponent<InputField>();
        consoleCanvasGroup = GameObject.Find("consoleCanvasGroup").GetComponent<CanvasGroup>();
        consoleButton = GameObject.Find("OptionsDevConsole").GetComponent<Button>();
        this.lightsoff = ligtsoffGame;
        this.pipegame = pipegame;

        //add listener for when you lose focus of inputfield or press enter
        inputfield.onEndEdit.AddListener(delegate { EditEnd(); });
        consoleButton.onClick.AddListener(() => ToggleConsole());

        consoleCanvasGroup.alpha = 0f;
        consoleCanvasGroup.interactable = false;
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
            GotoMouse.MenuOpen = true;
            FPSDisplay.Showfps = true; //show fps counter while console is open
            consoleCanvasGroup.alpha = 1f;
            consoleCanvasGroup.interactable = true;
            state = 1;
        }
        else
        {
            GotoMouse.MenuOpen = false;
            FPSDisplay.Showfps = false;
            consoleCanvasGroup.alpha = 0f;
            consoleCanvasGroup.interactable = false;
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

        //add more commands with if statements


        //close the console
        if(command == "exit" || command == "Exit")
        {
            ToggleConsole();
            consolestring = "";   //clear console text
            Debug.Log("exited!");
        }

        //print info about the character
        if(command == "info" || command == "Info")
        {
            consolestring += command + "\n";
            consolestring += "speed is: " + MainController.Speed + "\n";
            consolestring += "CtM speed is: " + GotoMouse.clickToMoveSpeed + "\n";
            consolestring += "Keypad solved: " + Player.KeypadSolved + "\n";
            consolestring += "Lightsout solved: " + Player.LightsoutSolved + "\n";
            consolestring += "Pipegame solved: " + Player.PipegameSolved + "\n";
            consoleInfoText.text = consolestring;
        }
        //clears console text
        if(command == "clear" || command == "Clear")
        {
            consolestring = "";
            consoleInfoText.text = "";
        }
        //set player to the next room if it exists
        if(command == "nextroom" || command == "Nextroom" || command == "nr")
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
        //set player to the previous room if it exists
        if(command == "prevroom" || command == "Prevroom" || command == "pr")
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
        //prints current rooms name
        if(command == "roomname" || command == "Roomname")
        {
            consolestring += player.LocationName+"\n";
            consoleInfoText.text = consolestring;
        }
        //prints player position
        if(command == "position" || command == "Position")
        {
            consolestring += player.Position + "\n";
            consoleInfoText.text = consolestring;
        }
        //lists some of the commands
        if(command == "help" || command == "Help" || command == "?")
        {
            consolestring += "available commands:\nnextroom, prevroom, info\n";
            consoleInfoText.text = consolestring;
        }
        //quits the game
        if(command == "quit" || command == "Quit")
        {
            Application.Quit();
        }
        
        //play the lights out minigame
        if(command == "lightsout" || command == "Lightsout" || command == "lo")
        {
            lightsoff.ShowGame();
        }

        //play the pipegame
        if(command == "pg" || command == "pipegame" || command == "Pipegame")
        {
            pipegame.ShowGame();
        }
        
    }

}

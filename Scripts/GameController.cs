using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
/// <summary>
/// Used to control alot of the game, create objects or rooms, player, minigames, characters, menus
/// </summary>
public class GameController : MonoBehaviour
{


    private InputField console;
    private CanvasGroup consoleCanvasGroup;
    private Rigidbody2D mCharacter;
    private Vector3 roomLeftPos;
    private Vector3 roomRightPos;
    private Rigidbody2D leftPoint;
    private Rigidbody2D rightPoint;
    private Button useButton;
    private Button inventoryButton;
    private ConsoleInput consoleInput;
    private Button optionsDevConsole;
    private Sprite background;
    private Vector3 startPos;
    private Vector3 returnPos;
    private Vector3 nextRoomPos;
    private Player player;
    private List<Rooms> rooms;
    private Keypad keypad;
    private CanvasGroup positionCanvas;
    private Text positionText;
    private Text dialogueText;
    private ScrollText scrollText;
    private Inventory inventory;
    private LightsoffGame lightsoffGame;
    private Options options;
    private Pipegame pipegame;
    private Audio sound;

    RawImage itemSlot1;
    RawImage itemSlot2;
    RawImage itemSlot3;
    Item itemButtonYellow;
    Item itemKey;
    Item itemButtonGreen;

    
    

    bool use = false;
    bool loop = false;
    bool inventoryOpen = false;

    int times;

    // Use this for initialization
    void Start()
    {

        //find objects
        console = GameObject.Find("ConsoleInputField").GetComponent<InputField>();
        consoleCanvasGroup = GameObject.Find("consoleCanvasGroup").GetComponent<CanvasGroup>();
        mCharacter = GameObject.Find("MCharacter").GetComponent<Rigidbody2D>();
        useButton = GameObject.Find("useButton").GetComponent<Button>();
        optionsDevConsole = GameObject.Find("OptionsDevConsole").GetComponent<Button>();
        positionCanvas = GameObject.Find("PositionCanvas").GetComponent<CanvasGroup>();
        positionText = GameObject.Find("PositionText").GetComponent<Text>();
        dialogueText = GameObject.Find("DialogueText").GetComponent<Text>();
        inventoryButton = GameObject.Find("InventoryButton").GetComponent<Button>();
        itemSlot1 = GameObject.Find("ItemSlot1").GetComponent<RawImage>();
        itemSlot2 = GameObject.Find("ItemSlot2").GetComponent<RawImage>();
        itemSlot3 = GameObject.Find("ItemSlot3").GetComponent<RawImage>();



        //add listeners
        useButton.onClick.AddListener(() => Use());
        inventoryButton.onClick.AddListener(() => ToggleInventory());


        //create class objects
        sound = new Audio();
        rooms = new List<Rooms>();
        keypad = new Keypad();
        scrollText = new ScrollText();
        inventory = new Inventory();
        lightsoffGame = new LightsoffGame();
        options = new Options();
        pipegame = new Pipegame();
        consoleInput = new ConsoleInput(console, consoleCanvasGroup, optionsDevConsole, lightsoffGame, pipegame);

        itemButtonYellow = new Item("buttonY", "button_yellow", itemSlot1);
        itemKey = new Item("key", "item_key", itemSlot2);
        itemButtonGreen = new Item("buttonG", "button_green", itemSlot3);


        inventory.AddItem(itemButtonYellow);
        inventory.AddItem(itemKey);
        inventory.AddItem(itemButtonGreen);

        //create rooms
        rooms.Add(new Rooms("start", "room1"));
        rooms.Add(new Rooms("cellRoom", "room2"));
        rooms.Add(new Rooms("largeCellRoom", "room3"));
        rooms.Add(new Rooms("exitRoom", "room4"));
        rooms.Add(new Rooms("securityDoor", "room5"));
        rooms.Add(new Rooms("controlRoom", "room6"));

        //setup previous and next rooms
        SetupRooms();

        //test text
        scrollText.Text = "testi tekstiä testi tekstiä testi tekstiä testi tekstiä testi tekstiä";

        sound.AmbientAudio();

   
        // set starting location to first room
        rooms[0].SetActiveRoom(rooms[0]);

        player = new Player(rooms, mCharacter);
        //player.Location = rooms[0];

        consoleInput.Player = player;

        // room positions for character
        startPos = new Vector3(-200, -54, 100);
        nextRoomPos = new Vector3(-300, -54, 100);
        returnPos = new Vector3(300, -54, 100);



        player.Position = startPos; // set player starting position
        player.SetScale(150); //set player default scale

    }
    /// <summary>
    /// use button state, true = pressed
    /// </summary>
    void Use()
    {
        use = true;
    }
    /// <summary>
    /// open or close inventory
    /// </summary>
    void ToggleInventory()
    {
        if (inventoryOpen == false)
        {
            inventory.ShowInventory();
            inventoryOpen = true;
        }
        else
        {
            inventory.HideInventory();
            inventoryOpen = false;
        }
    }
    /// <summary>
    /// set up previous and last rooms for all rooms in the list
    /// </summary>
    private void SetupRooms()
    {
        int i = 0;

        foreach (Rooms room in rooms)
        {
            if (i >= 1)
            {
                room.PreviousRoom = rooms[i - 1];
            }
            if (i < (rooms.Count - 1))
            {
                room.NextRoom = rooms[i + 1];
            }
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {

        //debugging key commands
        if (Input.GetKey("f1"))
        {
            lightsoffGame.ShowGame();
        }

        if (Input.GetKeyDown("f2"))
        {
            loop = true; // used by position textfield loop
        }

        if (Input.GetKeyDown("f3"))
        {
            
            scrollText.StartScrolling(); //test scrolling text
        }

        if (Input.GetKeyDown("f4"))
        {
            inventory.RemoveItem(itemKey); //test inventory remove

        }
        if (Input.GetKeyDown("f5"))
        {
            inventory.AddItem(itemButtonYellow);//test inventory add
            inventory.AddItem(itemKey);
            inventory.AddItem(itemButtonGreen);
        }

        dialogueText.text = scrollText.Text; //update story text all the time

        //hide dialogue if thread is finished scrolling
        if (scrollText.ThreadStatus == true)
        {
            scrollText.HideDialogue();
        }

        if (loop)
        {
            positionCanvas.alpha = 1f; //set position textfield visible and print players coordinates
            positionText.text = "" + player.Position + GotoMouse.move + GotoMouse.menuOpen;
        }

        //enter previous room if you are at the left side of the screen
        if (mCharacter.position.x <= -320)
        {
            GotoMouse.move = false;//set click to move loop to false;

            if (player.PreviousLocation != null)//check if previous room exists
            {
                player.Position = returnPos;
                player.SetLocation(player.PreviousLocation);
            }
        }

        //enter next room if you are at the right side of the screen
        if (mCharacter.position.x >= 320)
        {
            GotoMouse.move = false;//set click to move loop to false

            if (player.NextLocation != null) //check if next room exists
            {
                player.Position = nextRoomPos;
                player.SetLocation(player.NextLocation);
            }
        }

        //artificial boundary for the first and last rooms, so the character can't leave the screen
        if (player.Location.NextRoom == null && mCharacter.position.x > 300)
        {
            player.Position = new Vector3(299, -54, 100);
            scrollText.Text = "I can't go that way";
            scrollText.StartScrolling();
        }

        if (player.Location.PreviousRoom == null && mCharacter.position.x < -300)
        {
            player.Position = new Vector3(-299, -54, 100);
            scrollText.Text = "I can't go that way";
            scrollText.StartScrolling();
        }

        //keypad minigame toggling  -- check if character is in the right position, and which room you are in
        if (player.Location.RoomName == "securityDoor" && use == true && mCharacter.position.x >= -80 && mCharacter.position.x <= -40)
        {
            keypad.Togglekeypad();
            use = false;
        }

        //play room sounds
        if (player.Location.RoomName != "start")
        {
            sound.WaterdropAudio();
        }
        if (player.Location.RoomName == "securityDoor")
        {

            sound.ControlRoomAudio();
            
        }
        if (player.Location.RoomName == "largeCellRoom" && times == 0)
        {
                      
         sound.PrisoncellDoorAudio();
            times++;
                    
         }
     


        /* probably obsolete, leave just in case
        if(keypad.Active == true && mCharacter.position.x < -104 || mCharacter.position.x > -8)
        {
            keypad.Togglekeypad();
        }
        */
        use = false; //leave as last entry, toggle use off
    }
}

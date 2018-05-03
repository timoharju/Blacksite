using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using UnityEngine.SceneManagement;
using Minigames;
using Menu;
/// <summary>
/// Used to control alot of the game, create objects for rooms, player, minigames, characters, menus
/// </summary>
public class GameController : MonoBehaviour
{


    private Rigidbody2D mCharacter;
    private Button useButton;
    private Button inventoryButton;
    private ConsoleInput consoleInput;
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
    private CanvasGroup key1Start;
    private CanvasGroup key1Sewer;
    private Button specialRoomExit;
    private Renderer specialRoomExitRend;
    private Button clogCleaner;
    private Button yellowkey;
    private Text timerText;
    private GameObject paperFold;
    private GameObject sleepOverlay;
    private GameObject pipePile;
    private GameObject pipesFixed;
    private Image vignette;

    private bool use = false; //use key
    private bool loop = false;//f2 menu loop
    private bool inventoryOpen = false;

    //event flags
    private bool sewersFirstEntry = true; //used to move the character to the right spot when entering sewers
    private bool usedClog = false; //check if the player has used clog remover on the toilet
    private bool pipeGameTextEvent1 = true; //text event flags for sewers & pipegame, so they won't loop forever
    private bool pipeGameTextEvent2 = true; 
    private bool firstFlush = true; //used in start room events
    private bool hasClogcleaner = false; //check if player has clog cleaner item in inventory
    private bool hasYellowkey = false; //check if player has this key in inventory
    private bool hasBluekey = false;
    private int dragMarkEvent; //random int, chooses where the alien is in room 2
    private bool dragEvent = false;
    private bool sleepEventRunning = false;
    

    //timer, if it reaches zero, game over
    public static float TimeLeft { get; set; }

    List<GameObject> colliderList;
    GameObject room1;
    GameObject room2;
    GameObject room3;
    GameObject room4;
    GameObject room5;
    GameObject room6;
    GameObject room7;
    GameObject room8;
    GameObject room9;

    //inventory & item stuff

    Item itemKeyYellow;
    Item itemKeyBlue;
    Item itemClogcleaner;
    Item itemPaperFold;

    string colliderName;

    // Use this for initialization
    void Start()
    {
        //set timer
        TimeLeft = 480f;

        //roll a random int for dragMarkEvent in room 2
        dragMarkEvent = Random.Range(1, 4);

        //android settings
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        //find all of the GameObjects we need
        FindGameObjects();

        //create a list of room specific collider holders
        CreateColliderList();

        //hide special room exit button by default, only show it in the special rooms
        specialRoomExit.gameObject.SetActive(true);

        //add listeners to buttons
        AddonClickListeners();

        //create class objects
        CreateClassObjects();

        //create items we can pickup ingame
        CreateItems();

        //create rooms and add to list
        CreateRooms();

        //setup room relations => previous/next and specialrooms
        SetupRooms();


        //send the games to ConsoleInput for the toggle commands
        consoleInput = new ConsoleInput(lightsoffGame, pipegame);
        //create player, give it a populated rooms list and the character
        player = new Player(rooms, mCharacter);
        //create audio object, pass the player to it because it uses some room locations as conditions
        sound = new Audio(player);
        //set a player for the consoleInput class, for some commands
        consoleInput.Player = player;


        //setup player specific stuff, like starting location and scale
        SetupPlayer();

        //start waking up event
        StartCoroutine(WakingUpScreen());

        //adjust vignette colors
        vignette = GameObject.Find("Vignette").GetComponent<Image>();
        vignette.color = new Color32(0, 0, 0, (byte)VignetteAdjuster.SliderValue);

    }
    /// <summary>
    /// use button state, true = pressed
    /// </summary>
    void Use()
    {
        use = true;
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        //update collider name for trigger methods
        colliderName = collider.name;

        //check if player is in the start room
        if (player.LocationName == "start")
        {
            StartRoomTriggers();
        }

        //check if player is in the cell room
        else if(player.LocationName == "cellRoom")
        {
            CellRoomTriggers();
        }

        //third room
        else if(player.LocationName == "largeCellRoom")
        {
            LargeCellRoomTriggers();
        }

        //check if player is in the exit room (fourth room)
        else if(player.LocationName == "exitRoom")
        {
            ExitRoomTriggers();
        }
        
        //fifth room securityDoor
        else if(player.LocationName == "securityDoor")
        {
            SecurityDoorTriggers();
        }

        //sixth room controlRoom
        else if(player.LocationName == "controlRoom")
        {
            ControlRoomTriggers();
        }

        //seventh room maintenanceRoom
        else if(player.LocationName == "maintenanceRoom")
        {
            MaintenanceRoomTriggers();
        }

        //ninth room sewers
        else if(player.LocationName == "sewers")
        {
            SewerTriggers();
        }
        
        
    }

    /// <summary>
    /// set using to false when you leave any trigger(just incase)
    /// more detailed explanation in OnTriggerEnter2D docs
    /// </summary>
    void OnTriggerExit2D()
    {
        use = false;
        //important line, used in ArtificialBoundaries, if there is no next/previous room, don't let the player walk off the screen
        colliderName = "";
    }

    /// <summary>
    /// set using to false when you enter any trigger(just in case)
    /// if you click use where there isn't any interactable objects use will stay true, but right as you enter an object you want to interact with, it will set use to false
    /// so you don't accidentally pick up anything or start a game
    /// </summary>
    void OnTriggerEnter2D()
    {
        use = false;
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
    /// add clog cleaner to players inventory and hide the button
    /// </summary>
    void CollectCleaner()
    {
        inventory.AddItem(itemClogcleaner);
        clogCleaner.gameObject.SetActive(false);

    }

    /// <summary>
    /// add yellow key to players inventory and hide the button
    /// </summary>
    void CollectYellowKey()
    {
        inventory.AddItem(itemKeyYellow);
        yellowkey.gameObject.SetActive(false);
    }

    /// <summary>
    /// exit out of special rooms, like the cleaning closet or control room
    /// </summary>
    void SpecialRoomExit()
    {
        GotoMouse.Move = false;
        if(player.LocationName == "controlRoom")
        {
            player.Position = new Vector3(-145, mCharacter.position.y, 100);
        }

        if (player.LocationName == "cleaningCloset")
        {
            mCharacter.gameObject.SetActive(true);
            player.Position = new Vector3(3, mCharacter.position.y, 100);
        }

        player.SetLocation(player.Location.SpecialRoom);
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

        //config special case rooms
        //room index is always room N - 1

        //room 5, next room is room 7 "maintenace room" and vice versa
        rooms[4].NextRoom = rooms[6];
        rooms[6].PreviousRoom = rooms[4];

        //room 6 "control room" doesn't have a next or previous room, only special room, "security door"
        rooms[5].NextRoom = null;
        rooms[5].PreviousRoom = null;
        rooms[5].SpecialRoom = rooms[4];

        //room 7 "maintenance room" has 2 next rooms, sort of, it has two enterances
        rooms[6].NextRoom = null;

        //cleaning closet and sewers don't have next or previous room, only special rooms
        rooms[7].NextRoom = null;
        rooms[7].PreviousRoom = null;
        rooms[8].NextRoom = null;
        rooms[8].PreviousRoom = null;


        //set special room for cleaning closet and sewer
        rooms[7].SpecialRoom = rooms[6];
        rooms[8].SpecialRoom = rooms[6];
    }

    /// <summary>
    /// create and add GameObjects which hold room specific colliders to a list
    /// </summary>
    private void CreateColliderList()
    {
        colliderList = new List<GameObject>();

        colliderList.Add(room1 = GameObject.Find("Room1"));
        colliderList.Add(room2 = GameObject.Find("Room2"));
        colliderList.Add(room3 = GameObject.Find("Room3"));
        colliderList.Add(room4 = GameObject.Find("Room4"));
        colliderList.Add(room5 = GameObject.Find("Room5"));
        colliderList.Add(room6 = GameObject.Find("Room6"));
        colliderList.Add(room7 = GameObject.Find("Room7"));
        colliderList.Add(room8 = GameObject.Find("Room8"));
        colliderList.Add(room9 = GameObject.Find("Room9"));

        //turn all room colliders off by default, activate when inside a given room with EnableColliders() method
        foreach (GameObject obj in colliderList)
        {
            obj.SetActive(false);
        }
    }

    /// <summary>
    /// find and get components of all the GameObjects we need
    /// </summary>
    private void FindGameObjects()
    {
        mCharacter = GameObject.Find("MCharacter").GetComponent<Rigidbody2D>();
        useButton = GameObject.Find("useButton").GetComponent<Button>();
        positionCanvas = GameObject.Find("PositionCanvas").GetComponent<CanvasGroup>();
        positionText = GameObject.Find("PositionText").GetComponent<Text>();
        dialogueText = GameObject.Find("DialogueText").GetComponent<Text>();
        inventoryButton = GameObject.Find("InventoryButton").GetComponent<Button>();

        paperFold = GameObject.Find("PaperFold");
        sleepOverlay = GameObject.Find("SleepOverlay");
        pipePile = GameObject.Find("Pipepile");
        pipesFixed = GameObject.Find("PipesFixed");

        key1Start = GameObject.Find("Key1StartCanvas").GetComponent<CanvasGroup>();
        key1Sewer = GameObject.Find("Key1SewerCanvas").GetComponent<CanvasGroup>();
        specialRoomExit = GameObject.Find("SpecialRoomExitButton").GetComponent<Button>();
        clogCleaner = GameObject.Find("ClogCleaner").GetComponent<Button>();
        yellowkey = GameObject.Find("YellowKey").GetComponent<Button>();
        timerText = GameObject.Find("TimerText").GetComponent<Text>();
    }

    /// <summary>
    /// add all the onClick listeners we need
    /// </summary>
    private void AddonClickListeners()
    {
        useButton.onClick.AddListener(() => Use());
        inventoryButton.onClick.AddListener(() => ToggleInventory());
        specialRoomExit.onClick.AddListener(() => SpecialRoomExit());
        clogCleaner.onClick.AddListener(() => CollectCleaner());
        yellowkey.onClick.AddListener(() => CollectYellowKey());
    }

    /// <summary>
    /// create objects of all the classes we need
    /// </summary>
    private void CreateClassObjects()
    {
        scrollText = new ScrollText();
        keypad = new Keypad(scrollText); //keypad uses some dialogue, pass scrollText to it
        inventory = new Inventory();
        lightsoffGame = new LightsoffGame();
        options = new Options();
        pipegame = new Pipegame();
        
    }

    /// <summary>
    /// create all the items we can pickup in the game
    /// </summary>
    private void CreateItems()
    {
        itemKeyYellow = new Item("keyYellow", "item_key");
        itemClogcleaner = new Item("clogcleaner", "clog_cleaner");
        itemKeyBlue = new Item("keyBlue", "item_key2");
        itemPaperFold = new Item("paperFold", "paper_fold");
    }

    /// <summary>
    /// set up all coordinates for room change positions
    /// set player scale and starting location
    /// </summary>
    private void SetupPlayer()
    {
        // room positions for character, used when you change rooms
        startPos = new Vector3(-150, -54, 100);
        nextRoomPos = new Vector3(-280, -54, 100);
        returnPos = new Vector3(280, -54, 100);

        player.SetScale(75); //set player default scale
        player.SetLocation(rooms[0]); //set player starting location
    }


    // Update is called once per frame
    void Update()
    {
        
        //timer stuff
        timerText.text = "" + (int)TimeLeft;
        TimeLeft -= Time.deltaTime;
        if(TimeLeft <= 0)
        {
            SceneManager.LoadSceneAsync("GameOver");
        }


        //debugging key commands
        CheckDebuggingKeyCommands();

        //check if we want to update dialogue text (is thread running)
        if(scrollText.ThreadStatus == false)
        {
            dialogueText.text = scrollText.Text;
        }
        
        //hide dialogue box if thread is finished scrolling
        if (scrollText.ThreadStatus == true)
        {
            scrollText.HideDialogue();
        }

        //debugging tool, loop is toggled true if you press F2
        //toggled in CheckDebuggingKeyCommands()
        if (loop)
        {
            positionCanvas.alpha = 1f; //set info textfield visible and print players coordinates
            //display player position, click to move loop statuses and if you have solved minigames or not
            positionText.text = "" + player.Position + GotoMouse.Move + GotoMouse.MenuOpen + "\nKeypad: " + Player.KeypadSolved + "\nLightsout: " + Player.LightsoutSolved + "\nPipegame: " + Player.PipegameSolved + "\nclicks: " + GotoMouse.numberOfClicks + "\nUse: " + use + "\nColl: " + colliderName + "\nLoc: " + player.LocationName;
        }
        else
        {
            positionCanvas.alpha = 0f;
        }

        //check if we need to change the room, based on character coordinates
        CheckRoomSwitch();

        //artificial boundary for the first and last rooms, so the character can't leave the screen
        ArtificialBoundaries();

        //play or stop room based sounds
        RoomSounds();
        
        //check if you have solved minigames and show dialogues based on that
        SolvedGames();

        //first room events
        StartRoomEvents();

        //cleaning closet events
        CleaningClosetEvents();

        //events for the sewer room
        SewerEvents();

        //enable special exit button for special case rooms
        EnableSpecialExitButton();

        //enable room specific colliders
        EnableColliders();

        //dont let the character go out of bounds in special rooms
        SpecialRoomBoundaries();

        //events for the largeCellRoom
        LargeCellRoomEvents();

        //adjust vignette in the sewers, otherwise it's too dark
        AdjustVignette();

    }
    /// <summary>
    /// check if you are pressing any of the function keys
    /// </summary>
    private void CheckDebuggingKeyCommands()
    {
        if (Input.GetKey("f1"))
        {
            lightsoffGame.ShowGame();
        }

        if (Input.GetKeyDown("f2"))
        {
            //same as if loop == true, loop = false - else loop = true
            bool change = (loop) ? false : true;
            loop = change; // used by position textfield loop
        }

        if (Input.GetKeyDown("f3"))
        {
            usedClog = true; //debug pipegame events
            Player.LightsoutSolved = true;
            Player.KeypadSolved = true;
        }

        if (Input.GetKeyDown("f4"))
        {
            inventory.AddItem(itemClogcleaner);
            inventory.AddItem(itemKeyBlue);
            inventory.AddItem(itemKeyYellow);
            inventory.AddItem(itemPaperFold);
        }

        if (Input.GetKeyDown("f5"))
        {
            usedClog = true;
        }

        if (Input.GetKeyDown("f6"))
        {
            inventory.RemoveItem(itemClogcleaner);
            inventory.RemoveItem(itemKeyBlue);
            inventory.RemoveItem(itemKeyYellow);
            inventory.RemoveItem(itemPaperFold);
        }

        if (Input.GetKeyDown("f7"))
        {
            inventory.AddItem(itemClogcleaner);
            inventory.AddItem(itemKeyBlue);
        }
        
    }

    /// <summary>
    /// check if character wants to change rooms (=> is at the edge of the screen)
    /// </summary>
    private void CheckRoomSwitch()
    {
        //enter previous room if you are at the left side of the screen
        if (colliderName == "RoomSwitchLeft")
        {
            GotoMouse.Move = false;//set click to move loop to false;

            if (player.PreviousLocation != null)//check if previous room exists
            {
                player.Position = returnPos; //sprite position in the room
                player.SetLocation(player.PreviousLocation); //actual room location
                colliderName = ""; //clear colliderName so you don't warp multiple rooms, Update and OnCollide functions run at diffirent times, so this is very important
            }
        }

        //enter next room if you are at the right side of the screen
        if (colliderName == "RoomSwitchRight")
        {
            GotoMouse.Move = false;//set click to move loop to false

            if (player.NextLocation != null) //check if next room exists
            {
                player.Position = nextRoomPos;//sprite position in the room
                player.SetLocation(player.NextLocation);//actual room location
                colliderName = ""; //clear colliderName so you don't warp multiple rooms, Update and OnCollide functions run at diffirent times, so this is very important
            }
        }
    }

    /// <summary>
    /// create artificial boundaries, i.e if nextroom is null dont let the character go any further
    /// </summary>
    private void ArtificialBoundaries()
    {
        //exclude sewers, special case room - see SpecialRoomBoundaries()
        if (!(player.LocationName == "sewers"))
        {
            if (player.Location.NextRoom == null && colliderName == "RoomSwitchRight")
            {
                GotoMouse.Move = false;
                player.Position = new Vector3(280, -54, 100);
                scrollText.Text = "I can't go that way.";
                scrollText.StartScrolling();
                sound.Mumble5Audio();
            }

            if (player.Location.PreviousRoom == null && colliderName == "RoomSwitchLeft")
            {
                GotoMouse.Move = false;
                player.Position = new Vector3(-280, -54, 100);
                scrollText.Text = "I can't go that way.";
                scrollText.StartScrolling();
                sound.Mumble5Audio();
            }
        }
    }

    /// <summary>
    /// Events for securityDoor room, room 5
    /// </summary>
    private void SecurityDoorTriggers()
    {
        //keypad minigame toggling  -- check if character is in the right position, and which room you are in
        if (colliderName == "Keypad" && use)
        {
            keypad.Togglekeypad();
        }

        //check if keypad is solved and you want to enter "control room"
        if (colliderName == "Room5Door" && use)
        {
            if (Player.KeypadSolved == true)
            {
                player.SetLocation(rooms[5]);
                
            }
            else if (Player.KeypadSolved == false)
            {
                scrollText.Text = "It's locked.";
                scrollText.StartScrolling();
                sound.Mumble5Audio();
            }

        }

        //check if player is at the window
        if(colliderName == "Room5Window" && use)
        {
            scrollText.Text = "I think I can control some locks from there.";
            scrollText.StartScrolling();
            sound.Mumble4shortAudio();
        }

        use = false;
    }

    /// <summary>
    /// play or stop room based sounds
    /// </summary>
    private void RoomSounds()
    {
        //play room sounds
        if (player.Location.RoomName != "controlRoom" || player.Location.RoomName != "securityDoor")
        {
            sound.AmbientAudio();
        }

        if (player.Location.RoomName == "start")
        {
            sound.WaterdropAudio();
        }
        if (player.Location.RoomName == "cellRoom")
        {
            sound.WaterdropLowAudio();
        }
        if (player.Location.RoomName == "securityDoor")
        {
            sound.ControlRoomAudio();
        }
      

        sound.stopAudio();

    }
    /// <summary>
    /// 
    /// check if you have solved minigames and show a dialogue based on that
    /// </summary>
    private void SolvedGames()
    {
        //check if keypad has been solved and show a dialogue
        if (keypad.Solved)
        {
            keypad.Solved = false; // set it to false so it won't run again, Player keeps track of solved also
            StartCoroutine(keypad.WaitForSecs(1.5f));
            scrollText.Text = "Access granted.";
            scrollText.StartScrolling();
            //refresh room image
            rooms[4].RoomBackground = "room5_open";
            player.SetLocation(player.Location);
        }

        //check if lightsoffGame has been solved, show a dialogue
        if (lightsoffGame.Solved)
        {
            lightsoffGame.Solved = false;
            StartCoroutine(lightsoffGame.WaitForSecs(1.5f));
            scrollText.Text = "Something unlocked.";
            scrollText.StartScrolling();
            sound.Mumble5Audio();
        }


    }

    /// <summary>
    /// create rooms and add them to the room list
    /// </summary>
    private void CreateRooms()
    {
        rooms = new List<Rooms>();
        //create rooms and add to list
        rooms.Add(new Rooms("start", "room1"));
        rooms.Add(new Rooms("cellRoom", "room2"));
        rooms.Add(new Rooms("largeCellRoom", "room3"));
        rooms.Add(new Rooms("exitRoom", "room4"));
        rooms.Add(new Rooms("securityDoor", "room5"));
        rooms.Add(new Rooms("controlRoom", "room6"));
        rooms.Add(new Rooms("maintenanceRoom", "room7"));
        rooms.Add(new Rooms("cleaningCloset", "room8"));
        rooms.Add(new Rooms("sewers", "room9"));
    }

    /// <summary>
    /// handle maintenance room events
    /// </summary>
    private void MaintenanceRoomTriggers()
    {
        //check if you are at the cleaning room door in maintenanceroom
        if (colliderName == "Room7CleaningDoor" && use)
        {
            //check if you have completed the lightsout game
            if(Player.LightsoutSolved == true)
            {
                player.SetLocation(rooms[7]);
                sound.NormalDoorOpenAudio();
            }
            else//if not display a dialogue
            {
                scrollText.Text = "It's locked.";
                scrollText.StartScrolling();
                sound.Mumble5Audio();
            }
            use = false;
        }

        //check if you are at the sewer door
        if(colliderName == "Room7SewerDoor" && use)
        {
            player.SetLocation(rooms[8]);
            use = false;
            sound.NormalDoorOpenAudio();
        }
    }

    /// <summary>
    /// handle sewer room events
    /// </summary>
    private void SewerTriggers()
    {

        //see if you want to leave (use stairs)
        if(colliderName == "Room9Stairs" && use)
        {
            player.SetLocation(player.Location.SpecialRoom);
            use = false;
            player.Position = new Vector3(200, -54, 100);
        }

        //pickup the key
        if(colliderName == "Room9Key" && use)
        {
            //add key to inventory if it's shown and hide it in the world
            if(!(key1Sewer.alpha == 0f))
            {
                inventory.AddItem(itemKeyBlue); //already in the list check is in inventory addItem method
                key1Sewer.alpha = 0f;
            }
            
            use = false;
        }

        //see if player wants to play the pipe game
        if(colliderName == "Room9Pipes" && use)
        {
            pipegame.ShowGame();
            use = false;
        }
    }

    /// <summary>
    /// sewer events, character position and pipegame 
    /// </summary>
    private void SewerEvents()
    {
        if (player.LocationName == "sewers")
        {
            List<Item> items = inventory.GetItems();
            foreach(Item item in items)
            {
                if(item.Name == "keyBlue")
                {
                    hasBluekey = true;
                }
            }

            //position the character slightly lower if you just entered
            if (sewersFirstEntry)
            {
                player.Position = new Vector3(-195, -102, 100);
                sewersFirstEntry = false;
            }

            //if player has used clog cleaner but they dont have the key in their inventory, show the key
            if (usedClog && !hasBluekey && Player.PipegameSolved)
            {
                key1Sewer.alpha = 1f;
            }
            
            
            //if player has the key in their inventory already, hide the key
            if (hasBluekey)
            {
                key1Sewer.alpha = 0f;
            }


            //show/hide pile of pipes and fixed pipes if player hasn't/has solved pipegame
            bool showPipepile = (!Player.PipegameSolved) ? true : false;
            pipePile.SetActive(showPipepile);
            pipesFixed.SetActive(!showPipepile);
            

            //dont interact with the pipes if you already won the game
            if (Player.PipegameSolved == true)
            {
                pipegame.CloseGame();
            }
        }
        else //if player is not in the sewers
        {
            pipePile.SetActive(false);
            pipesFixed.SetActive(false);
            sewersFirstEntry = true; //set first entry back to true when you leave
            key1Sewer.alpha = 0f;
        }

        //dialogue stuff after you complete the game
        if (Player.PipegameSolved == true)
        {
            if (player.LocationName == "sewers")
            {

                //has player used clog remover?
                if (usedClog)
                {
                    // see if the text event has triggered already
                    if (pipeGameTextEvent1)
                    {
                        scrollText.Text = "That's the key from my cell!";
                        scrollText.StartScrolling();
                        pipeGameTextEvent1 = false;
                        sound.Mumble1Audio();

                    }
                }
                else //if player hasnt used clog remover
                {
                    //see if the text event has triggered already
                    if (pipeGameTextEvent2)
                    {
                        scrollText.Text = "I think the pipes are clogged, nothing is coming out.";
                        scrollText.StartScrolling();
                        pipeGameTextEvent2 = false;
                        sound.Mumble4Audio();
                    }
                }
            }
        }
    }

    /// <summary>
    /// alien kidnapping for the second room
    /// </summary>
    private void CellRoomTriggers()
    {
        
        //check if you are near door 1
        if (colliderName == "Room2Door1" && use)
        {
            //check random roll
            if(dragMarkEvent == 1)
            {
                //check if the alien has dragged you already
                if (!dragEvent)
                {
                    scrollText.Text = "What is that?";
                    scrollText.StartScrolling();
                    dragEvent = true;
                    StartCoroutine(MonsterEvent(1));
                    sound.Mumble1shortAudio();
                    sound.AlienScreechAudio();
                }
                else
                {
                    //prevent overlapping dialogues
                    if (scrollText.ThreadStatus)
                    {
                        scrollText.Text = "I better keep moving.";
                        scrollText.StartScrolling();
                        sound.Mumble1shortAudio();
                    }
                }
                
            }
            else
            {
                scrollText.Text = "It's empty.";
                scrollText.StartScrolling();
                sound.Mumble5Audio();
            }
            
        }
        //check if you are near door 2
        else if (colliderName == "Room2Door2" && use)
        {
            if(dragMarkEvent == 2)
            {
                if (!dragEvent)
                {
                    scrollText.Text = "What is that?";
                    scrollText.StartScrolling();
                    dragEvent = true;
                    StartCoroutine(MonsterEvent(2));
                    sound.Mumble1shortAudio();
                    sound.AlienScreechAudio();
                }
                else
                {
                    //prevent overlapping dialogues
                    if (scrollText.ThreadStatus)
                    {
                        scrollText.Text = "I better keep moving.";
                        scrollText.StartScrolling();
                        sound.Mumble1shortAudio();
                    }
                }
            }
            else
            {
                scrollText.Text = "I think I saw something...                                      \n...nevermind.";
                scrollText.StartScrolling();
                sound.Mumble1Audio();
                sound.Mumblegreat2Audio();
            }
            
        }
        //check if you are near door 3
        else if (colliderName == "Room2Door3" && use)
        {
            if(dragMarkEvent == 3)
            {
                if (!dragEvent)
                {
                    scrollText.Text = "What is that?";
                    scrollText.StartScrolling();
                    dragEvent = true;
                    StartCoroutine(MonsterEvent(3));
                    sound.Mumble1shortAudio();
                    sound.AlienScreechAudio();
                }
                else
                {
                    //prevent overlapping dialogues
                    if (scrollText.ThreadStatus)
                    {
                        scrollText.Text = "I better keep moving.";
                        scrollText.StartScrolling();
                        sound.Mumble1shortAudio();
                    }
                }
            }
            else
            {
                scrollText.Text = "Nothing of value.";
                scrollText.StartScrolling();
                sound.Mumble5Audio();
            }

        }
        use = false; //toggle use last
    }

    /// <summary>
    /// show or hide key number 1 based on room location and toilet events
    /// </summary>
    private void StartRoomEvents()
    {
        //show or hide key depending on location
        if (player.LocationName == "start")
        {
            if (firstFlush)
            {
                key1Start.alpha = 1f;
            }

        }
        else
        {
            key1Start.alpha = 0f;
        }
    }

    /// <summary>
    /// Trigger events for the first room based on collision and use
    /// </summary>
    private void StartRoomTriggers()
    {

        //search players inventory for clog cleaner
        List<Item> items = inventory.GetItems();
        bool hasCleaner = false;
        foreach (Item item in items)
        {
            if (item.Name == "clogcleaner")
            {
                hasCleaner = true;
            }
        }

        //check if you are near the toilet and presssed use
        if (colliderName == "ToiletCollider" && use)
        {
            //check if you have triggered first use and that you don't have the cleaner yet
            //also check ThreadStatus to see if the other text event is still going on
            if (!firstFlush && !hasCleaner && scrollText.ThreadStatus) 
            {
                if (usedClog)
                {
                    scrollText.Text = "The pipes should be unclogged now.";
                    sound.ToiletFlushAudio();
                }
                else
                {
                    scrollText.Text = "I think the pipes might be clogged.";
                }
                scrollText.StartScrolling();
                sound.Mumble3shortAudio();
            }

            //check if the player has clogcleaner and that first use has been triggered
            if (hasCleaner && !firstFlush)
            {
                scrollText.Text = "Used the clog cleaner.";
                scrollText.StartScrolling();
                usedClog = true;
                sound.Mumble5Audio();
                //remove clogCleaner from the player
                inventory.RemoveItem(itemClogcleaner);
            }

            //check if first use has been triggered
            if (firstFlush)
            {
                scrollText.Text = "The key fell down into the toilet.             \nGreat.";
                scrollText.StartScrolling();
                firstFlush = false;
                key1Start.alpha = 0f;
                sound.ObjectDropWaterAudio();
                sound.Mumble2Audio();
                sound.MumblegreatAudio();
            }

            
        }
        //check if player is at the bed and pressed use
        if(colliderName == "BedCollider" && use)
        {
            scrollText.Text = "I guess I could take a nap.";
            scrollText.StartScrolling();
            //prevent overlapping events
            if (!sleepEventRunning)
            {
                sleepEventRunning = true;
                StartCoroutine(SleepEvent(2.5f));

            }
            
            sound.Mumble5Audio();
        }

        use = false; //toggle use last
    }

    /// <summary>
    /// exit rooms events & dialogue
    /// </summary>
    private void ExitRoomTriggers()
    {
        if(player.LocationName == "exitRoom")
        {
            //check how many keys you have
            int keyCount = 0;
            List<Item> items = inventory.GetItems();
            foreach(Item item in items)
            {
                if(item.Name == "keyYellow" || item.Name == "keyBlue")
                {
                    keyCount++;
                }
            }
            //check if you are at the door
            if(colliderName == "Room4LargeDoor" && use)
            {
                if (keyCount == 0)
                {
                    scrollText.Text = "The door is locked, there are two keyholes.        \nIt's my way to freedom.";
                    scrollText.StartScrolling();
                    sound.Mumble4shortAudio();
                    sound.MumblegreatAudio();
                }
                else if(keyCount == 1)
                {
                    scrollText.Text = "It needs a second key.";
                    scrollText.StartScrolling();
                    sound.Mumble5Audio();
                }
                else if(keyCount == 2)
                {
                    SceneManager.LoadSceneAsync("EndingScene");
                }
            }
            use = false;
        }
    }

    /// <summary>
    /// handle control room events
    /// </summary>
    private void ControlRoomTriggers()
    {
        
        //check if you are near the table and want to play lightsout
        if(colliderName == "LightsoutGame" && use)
        {
            lightsoffGame.ShowGame();
        }

        if(colliderName == "ButtonWall" && use)
        {
            scrollText.Text = "I have no idea what I'm doing.";
            scrollText.StartScrolling();
            sound.Mumble4shortAudio();
        }
        use = false;
    }

    /// <summary>
    /// handle cleaning closet events
    /// </summary>
    private void CleaningClosetEvents()
    {
        //check if player is in the cleaning closet
        if (player.LocationName == "cleaningCloset")
        {
            //check if player has clogcleaner in his inventory, then hide or show it in the world
            
            List<Item> items = inventory.GetItems();
            foreach(Item item in items)
            {
                if(item.Name == "clogcleaner")
                {
                    hasClogcleaner = true;
                }

                if(item.Name == "keyYellow")
                {
                    hasYellowkey = true;
                }
            }
            //check if player has the clog cleaner and hide or show the button
            if (hasClogcleaner)
            {
                clogCleaner.gameObject.SetActive(false);
            }
            else
            {
                clogCleaner.gameObject.SetActive(true);
            }

            //check if player has the yellow key and hide or show the button
            if (hasYellowkey)
            {
                yellowkey.gameObject.SetActive(false);
            }
            else
            {
                yellowkey.gameObject.SetActive(true);
            }
            
            //special case room, disable character here
            mCharacter.gameObject.SetActive(false);
        }
        else //if the player is not in the cleaning closet
        {
            clogCleaner.gameObject.SetActive(false);
            yellowkey.gameObject.SetActive(false);
            mCharacter.gameObject.SetActive(true);
        }
        
    }
    
    /// <summary>
    /// enable specialexitbutton for special case rooms
    /// </summary>
    private void EnableSpecialExitButton()
    {
        if(player.LocationName == "controlRoom" || player.LocationName == "cleaningCloset")
        {
            specialRoomExit.gameObject.SetActive(true);
        }
        else
        {
            specialRoomExit.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// enable/disable room specific colliders based on your location
    /// </summary>
    private void EnableColliders()
    {
        bool check;
        //check if you are in room 1
        check = (player.LocationName == "start") ? true : false;
        colliderList[0].SetActive(check);

        //room 2
        check = (player.LocationName == "cellRoom") ? true : false;
        colliderList[1].SetActive(check);

        //room 3
        check = (player.LocationName == "largeCellRoom") ? true : false;
        colliderList[2].SetActive(check);

        //room 4
        check = (player.LocationName == "exitRoom") ? true : false;
        colliderList[3].SetActive(check);

        //room 5
        check = (player.LocationName == "securityDoor") ? true : false;
        colliderList[4].SetActive(check);

        //room 6
        check = (player.LocationName == "controlRoom") ? true : false;
        colliderList[5].SetActive(check);

        //room 7
        check = (player.LocationName == "maintenanceRoom") ? true : false;
        colliderList[6].SetActive(check);

        //room 8
        check = (player.LocationName == "cleaningCloset") ? true : false;
        colliderList[7].SetActive(check);

        //room 9
        check = (player.LocationName == "sewers") ? true : false;
        colliderList[8].SetActive(check);
    }
    
    /// <summary>
    /// create boundaries for some special rooms
    /// </summary>
    private void SpecialRoomBoundaries()
    {
        //create artificial boundaries for sewer room, the character is placed lower on the screen here, that's why it's excluded from the normal ArtificialBoundaries() method
        if(player.LocationName == "sewers")
        {
            //don't let the character go out of bounds
            if (colliderName == "RoomSwitchRight")
            {
                GotoMouse.Move = false;
                player.Position = new Vector3(280, -102, 100); //normal y value -54
            }
            //don't let the character go out of bounds
            if (colliderName == "RoomSwitchLeft")
            {
                GotoMouse.Move = false;
                player.Position = new Vector3(-280, -102, 100);
            }
        }
    }
    

    /// <summary>
    /// events & dialogue for large cell room
    /// </summary>
    private void LargeCellRoomTriggers()
    {
        bool hasPaper = false;
        if (colliderName == "AlienSkeleton" && use)
        {
            List<Item> items = inventory.GetItems();
            foreach(Item item in items)
            {
                if(item.Name == "paperFold")
                {
                    hasPaper = true;
                }
            }

            //check if you have the paper in your inventory, same as using for the second time
            if (hasPaper)
            {
                scrollText.Text = "The flesh has melted to a green puddle of goo.";
                scrollText.StartScrolling();
                sound.Mumble3Audio();
            }
            else//first use
            {
                inventory.AddItem(itemPaperFold);
                scrollText.Text = "There was a piece of paper in the aliens hand.";
                scrollText.StartScrolling();
                sound.Mumble1Audio();
            }

            use = false;
        }
    }

    /// <summary>
    /// Handle largeCellRoom events, right now just enable/disable paper in aliens hand
    /// </summary>
    private void LargeCellRoomEvents()
    {
        //check if player is in largeCellRoom
        bool checkLocation = (player.LocationName == "largeCellRoom") ? true : false;
        bool hasPaper = false;
        if (checkLocation)
        {
            List<Item> items = inventory.GetItems();

            //check the inventory for paperFold
            foreach(Item item in items)
            {
                if(item.Name == "paperFold")
                {
                    hasPaper = true;
                }
            }
            //hide paper if you've already picked it up
            paperFold.SetActive(!hasPaper);
        }
        else//hide paper if you are not in largeCellRoom
        {
            paperFold.SetActive(false);
        }
        
    }

    /// <summary>
    /// Add delay between sleep overlay activations
    /// </summary>
    /// <param name="sec">time in seconds</param>
    /// <returns></returns>
    private IEnumerator SleepEvent(float sec)
    {
        //dont let the player move while sleeping
        GotoMouse.MenuOpen = true;
        yield return new WaitForSeconds(sec);
        sleepOverlay.SetActive(true);
        yield return new WaitForSeconds(3);
        StartCoroutine(WakingUpScreen());
        scrollText.Text = "Refreshing! Although I lost some valuable time.";
        scrollText.StartScrolling();
        sound.Mumble4Audio();

        //add the delays here, totals to about 1 minute
        TimeLeft -= 54.5f;

        //let the player move again
        GotoMouse.MenuOpen = false;
        //mark event over
        sleepEventRunning = false;

    }
    
    /// <summary>
    /// monster kidnapping you in the second room, takes door number as a parameter
    /// </summary>
    /// <param name="sec"></param>
    /// <returns></returns>
    private IEnumerator MonsterEvent(int roomNumber)
    {
        Debug.Log("monster event");
        //don't let the player move
        GotoMouse.MenuOpen = true;
        yield return new WaitForSeconds(2f);
        //make the screen black
        sleepOverlay.SetActive(true);

        //decrease timer by 55 seconds, include waits and it totals to about 1 minute
        TimeLeft -= 55;

        //set dragmarks to room 1
        rooms[0].RoomBackground = "room1_dragmarks";

        //set the right dragmarks to room 2
        if(roomNumber == 1)
        {
            rooms[1].RoomBackground = "room2_dragmarks_left";
        }
        else if(roomNumber == 2)
        {
            rooms[1].RoomBackground = "room2_dragmarks_middle";
        }
        else if(roomNumber == 3)
        {
            rooms[1].RoomBackground = "room2_dragmarks_right";
        }
        
        //set player location and pos
        player.SetLocation(player.PreviousLocation);
        player.Position = startPos;
        yield return new WaitForSeconds(3);
        StartCoroutine(WakingUpScreen());
        scrollText.Text = "What was that?";
        scrollText.StartScrolling();
        sound.Mumble4shortAudio();
        //let player move again
        GotoMouse.MenuOpen = false;
        
    }

    /// <summary>
    /// fade in black screen at the beginning of the game
    /// </summary>
    /// <returns></returns>
    private IEnumerator WakingUpScreen()
    {
        //max alpha value
        float alpha = 255;

        //decrease alpha by a little rapidly so it looks like a smooth fade in
        for(int i = 0; i < 100; i++)
        {
            //if this line gives you errors make sure to enable SleepOverlay before you start the game!!
            sleepOverlay.GetComponent<Image>().color = new Color32(0, 0, 0, (byte)alpha);
            alpha -= 2.55f;
            yield return new WaitForSeconds(0.02f);
        }
        //reset colors to normal
        sleepOverlay.GetComponent<Image>().color = new Color32(0, 0, 0, 255);
        sleepOverlay.SetActive(false);
    }

    /// <summary>
    /// adjust vignette color in the sewers, it's too dark otherwise
    /// </summary>
    private void AdjustVignette()
    {
        //if player is in the sewers reduce alpha by 50
        float n = (player.LocationName == "sewers") ? (VignetteAdjuster.SliderValue - 50) : VignetteAdjuster.SliderValue;
        vignette.color = new Color32(0, 0, 0, (byte)n);
    }
    
   
}


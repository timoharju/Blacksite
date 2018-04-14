using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
/// <summary>
/// Used to control alot of the game, create objects for rooms, player, minigames, characters, menus
/// </summary>
public class GameController : MonoBehaviour
{


    private Rigidbody2D mCharacter;
    private Vector3 roomLeftPos;
    private Vector3 roomRightPos;
    private Rigidbody2D leftPoint;
    private Rigidbody2D rightPoint;
    private Button useButton;
    private Button inventoryButton;
    private ConsoleInput consoleInput;
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
    private CanvasGroup key1Start;
    private CanvasGroup key1Sewer;
    private Button specialRoomExit;
    private Renderer specialRoomExitRend;
    private Button clogCleaner;

    private bool use = false;
    private bool loop = false;//f2 menu loop
    private bool inventoryOpen = false;
    private bool sewersFirstEntry = true; //used to move the character to the right spot when entering sewers
    private bool usedClog = false; //check if the player has used clog remover on the toilet
    private bool pipeGameTextEvent1 = true; //text event flags for sewers & pipegame, so they won't loop forever
    private bool pipeGameTextEvent2 = true;
    private bool firstFlush = true;
    private bool hasClogcleaner = false;

    //place holders
    RawImage itemSlot1;
    RawImage itemSlot2;
    RawImage itemSlot3;
    Item itemKeyYellow;
    Item itemKeyBlue;
    Item itemClogcleaner;

    

    // Use this for initialization
    void Start()
    {
        //android settings
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        //find game objects
        mCharacter = GameObject.Find("MCharacter").GetComponent<Rigidbody2D>();
        useButton = GameObject.Find("useButton").GetComponent<Button>();
        positionCanvas = GameObject.Find("PositionCanvas").GetComponent<CanvasGroup>();
        positionText = GameObject.Find("PositionText").GetComponent<Text>();
        dialogueText = GameObject.Find("DialogueText").GetComponent<Text>();
        inventoryButton = GameObject.Find("InventoryButton").GetComponent<Button>();
        itemSlot1 = GameObject.Find("ItemSlot1").GetComponent<RawImage>();
        itemSlot2 = GameObject.Find("ItemSlot2").GetComponent<RawImage>();
        itemSlot3 = GameObject.Find("ItemSlot3").GetComponent<RawImage>();
        key1Start = GameObject.Find("Key1StartCanvas").GetComponent<CanvasGroup>();
        key1Sewer = GameObject.Find("Key1SewerCanvas").GetComponent<CanvasGroup>();
        specialRoomExit = GameObject.Find("SpecialRoomExitButton").GetComponent<Button>();
        clogCleaner = GameObject.Find("ClogCleaner").GetComponent<Button>();
        


        //hide key in the sewers 
        key1Sewer.alpha = 0f;

        //hide special room exit button by default, only show it in the special rooms
        specialRoomExit.gameObject.SetActive(true);
        



        //add listeners
        useButton.onClick.AddListener(() => Use());
        inventoryButton.onClick.AddListener(() => ToggleInventory());
        specialRoomExit.onClick.AddListener(() => SpecialRoomExit());
        clogCleaner.onClick.AddListener(() => CollectCleaner());


        //create class objects
        rooms = new List<Rooms>();
        scrollText = new ScrollText();
        keypad = new Keypad(scrollText);
        inventory = new Inventory();
        lightsoffGame = new LightsoffGame();
        options = new Options();
        pipegame = new Pipegame();

        //send the games to ConsoleInput for the toggle commands
        consoleInput = new ConsoleInput(lightsoffGame, pipegame);
        
        itemKeyYellow = new Item("keyYellow", "item_key", itemSlot1);
        itemClogcleaner = new Item("clogcleaner", "clog_cleaner", itemSlot2);
        itemKeyBlue = new Item("keyBlue", "item_key2", itemSlot3);
        /*
        //inventory placeholder stuff
        inventory.AddItem(itemKeyYellow);
        inventory.AddItem(itemKeyBlue);
        inventory.AddItem(itemClogcleaner);
        */
        //create rooms and add to list
        CreateRooms();

        //setup previous and next rooms
        SetupRooms();


        //create player, give it a populated rooms list and the character
        player = new Player(rooms, mCharacter);
        //create audio object, pass the player to it because it uses some room locations as conditions
        sound = new Audio(player);

        //set a player for the consoleInput class, for some commands
        consoleInput.Player = player;

        // room positions for character
        startPos = new Vector3(-150, -54, 100);     
        nextRoomPos = new Vector3(-295, -54, 100);  
        returnPos = new Vector3(295, -54, 100);     



        //player.Position = startPos; // set player starting position
        player.SetScale(150); //set player default scale
        player.SetLocation(rooms[0]); //set player starting location

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
    /// add clog cleaner to players inventory and hide the button
    /// </summary>
    void CollectCleaner()
    {
        inventory.AddItem(itemClogcleaner);
        
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
            player.Position = new Vector3(3, mCharacter.position.y, 100);
        }

        player.SetLocation(player.PreviousLocation);
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

        //room 5, next room is room 7 "maintenace room" and vice versa
        rooms[4].NextRoom = rooms[6];
        rooms[6].PreviousRoom = rooms[4];

        //room 6 "control room" doesn't have a next room, only the previous one, "security door"
        rooms[5].NextRoom = null;

        //room 7 "maintenance room" has 2 next rooms, sort of, it has two enterances
        rooms[6].NextRoom = null;
        //cleaning closet only has a previous room, "maintenance room"
        rooms[7].NextRoom = null;
        //set previous room for cleaning closet and sewer
        rooms[7].PreviousRoom = rooms[6];
        rooms[8].PreviousRoom = rooms[6];
    }

    // Update is called once per frame
    void Update()
    {

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
            positionText.text = "" + player.Position + GotoMouse.Move + GotoMouse.MenuOpen + "\nKeypad: " + Player.KeypadSolved + "\nLightsout: " + Player.LightsoutSolved + "\nPipegame: " + Player.PipegameSolved + "\nclicks: " + GotoMouse.numberOfClicks;
        }

        //check if we need to change the room, based on character coordinates
        CheckRoomSwitch();

        //artificial boundary for the first and last rooms, so the character can't leave the screen
        ArtificialBoundaries();

        //play or stop room based sounds
        RoomSounds();
        


        //toggle keypad, check if you can enter "control room" (solved keypad)
        KeypadFunctions();

        //check if you have solved minigames and show dialogues based on that
        SolvedGames();

        //handles what happens in the maintenance room, atleast two door positions
        MaintenanceRoomEvents();

        //handles the sewers, pipegame and exiting
        SewerEvents();

        //shows time wasting dialogues if you use the doors in the second room
        CellroomDialogue();

        //first room events
        StartRoomEvents();

        //events for the exit room
        ExitRoomEvents();

        //control room events
        ControlRoomEvents();

        //cleaning closet events
        CleaningClosetEvents();

        //enable special exit button for special case rooms
        EnableSpecialExitButton();
        
        use = false; //leave as last entry, toggle use off if you didn't actually use anything
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
            loop = true; // used by position textfield loop
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
        }
        
    }
    /// <summary>
    /// check if character wants to change rooms (=> is at the edge of the screen)
    /// </summary>
    private void CheckRoomSwitch()
    {
        //enter previous room if you are at the left side of the screen
        if (mCharacter.position.x <= -305)
        {
            GotoMouse.Move = false;//set click to move loop to false;

            if (player.PreviousLocation != null)//check if previous room exists
            {
                player.Position = returnPos; //sprite position in the room
                player.SetLocation(player.PreviousLocation); //actual room location
            }
        }

        //enter next room if you are at the right side of the screen
        if (mCharacter.position.x >= 305)
        {
            GotoMouse.Move = false;//set click to move loop to false

            if (player.NextLocation != null) //check if next room exists
            {
                player.Position = nextRoomPos;//sprite position in the room
                player.SetLocation(player.NextLocation);//actual room location
            }
        }
    }
    /// <summary>
    /// create artificial boundaries, i.e if nextroom is null dont let the character go any further
    /// </summary>
    private void ArtificialBoundaries()
    {
        //exclude sewers, special case room - see SewerFunctions()
        if (!(player.LocationName == "sewers"))
        {
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
        }
    }
    /// <summary>
    /// toggle keypad if you USE near it, also check if you can enter "control room"
    /// </summary>
    private void KeypadFunctions()
    {
        //keypad minigame toggling  -- check if character is in the right position, and which room you are in
        if (player.Location.RoomName == "securityDoor" && use == true && mCharacter.position.x >= -80 && mCharacter.position.x <= -40)
        {
            keypad.Togglekeypad();
            use = false;
        }

        //check if keypad is solved and you want to enter "control room"
        if (player.Location.RoomName == "securityDoor" && mCharacter.position.x <= -108 && mCharacter.position.x >= -190 && use == true)
        {
            if (Player.KeypadSolved == true)
            {
                player.SetLocation(rooms[5]);
                use = false;
                sound.ElectricDoorOpenAudio();
            }
            else if (Player.KeypadSolved == false)
            {
                scrollText.Text = "It's locked.";
                scrollText.StartScrolling();
            }

        }
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
    
        
        
        if (player.Location.RoomName == "largeCellRoom")
        {

            sound.PrisoncellDoorAudio();
            
        }
        sound.stopAudio();

    }
    /// <summary>
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
        }

        //check if lightsoffGame has been solved, show a dialogue
        if (lightsoffGame.Solved)
        {
            lightsoffGame.Solved = false;
            StartCoroutine(lightsoffGame.WaitForSecs(1.5f));
            scrollText.Text = "Solved lights out game(PH).";
            scrollText.StartScrolling();
        }


    }
    /// <summary>
    /// create rooms and add them to the room list
    /// </summary>
    private void CreateRooms()
    {
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
    private void MaintenanceRoomEvents()
    {
        //check if you are at the cleaning room door in maintenanceroom
        if (player.LocationName == "maintenanceRoom" && mCharacter.position.x > -34 && mCharacter.position.x < 60 && use == true)
        {

            if(Player.LightsoutSolved == true)
            {
                player.SetLocation(rooms[7]);
            }
            else
            {
                scrollText.Text = "It's locked.";
                scrollText.StartScrolling();
            }
            use = false;
        }

        if(player.LocationName == "maintenanceRoom" && mCharacter.position.x > 153 && mCharacter.position.x < 249 && use == true)
        {
            player.SetLocation(rooms[8]);
            use = false;
        }
    }
    /// <summary>
    /// handle sewer room events
    /// </summary>
    private void SewerEvents()
    {
        if(player.LocationName == "sewers")
        {
            //check if you just entered the room and position the character slightly lower
            if (sewersFirstEntry)
            {
                player.Position = new Vector3(-195, -102, 100);
                sewersFirstEntry = false;
            }
            
            //show the game if you are in the dewers
            pipegame.ShowGame();

            if (Player.PipegameSolved == true)
            {
                pipegame.SetUninteractable();
            }
            //don't let the character go out of bounds
            if(mCharacter.position.x > 300)
            {
                player.Position = new Vector3(299, -102, 100);
            }
            //don't let the character go out of bounds
            if (mCharacter.position.x < -300)
            {
                player.Position = new Vector3(-299, -102, 100);
            }
            //see if you want to leave (use stairs)
            if(mCharacter.position.x < -200 && use)
            {
                player.SetLocation(player.PreviousLocation);
                use = false;
                player.Position = new Vector3(200, -54, 100);
            }

            //pickup the key
            if(mCharacter.position.x > 276 && mCharacter.position.x <= 299 && use)
            {
                //add key to inventory and hide it in the world
                inventory.AddItem(itemKeyBlue); //already in the list check is in inventory addItem method
                key1Sewer.alpha = 0f;
                use = false;
            }


            //dialogue stuff
            if(Player.PipegameSolved == true)
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
                        key1Sewer.alpha = 1f;
                    }
                }
                else
                {
                    //see if the text event has triggered already
                    if (pipeGameTextEvent2)
                    {
                        scrollText.Text = "I think the pipes are clogged, nothing is coming out.";
                        scrollText.StartScrolling();
                        pipeGameTextEvent2 = false;
                    }
                }
            }

        }
        else //hide game if you are not in the sewers
        {
            pipegame.CloseGame();
            sewersFirstEntry = true;
        }
    }
    /// <summary>
    /// time wasting dialogue for the second room
    /// </summary>
    private void CellroomDialogue()
    {
        //check the room
        if (player.LocationName == "cellRoom")
        {
            //door nr. 1
            if (mCharacter.position.x > -287 && mCharacter.position.x < -200 && use)
            {
                scrollText.Text = "Nothing of value.";
                scrollText.StartScrolling();
                use = false;
            }

            //dooor nr. 2
            if (mCharacter.position.x > -28 && mCharacter.position.x < 60 && use)
            {
                scrollText.Text = "I think I saw something...                                     \n ...nevermind.";
                scrollText.StartScrolling();
                use = false;
            }

            if(mCharacter.position.x > 210 && mCharacter.position.x < 280 && use)
            {
                scrollText.Text = "It's empty.";
                scrollText.StartScrolling();
                use = false;
            }

        }
    }
    /// <summary>
    /// first room events
    /// </summary>
    private void StartRoomEvents()
    {
        //check that you are in the first room
        if(player.LocationName == "start")
        {
            //search players inventory for clog cleaner and the first key
            List<Item> items = inventory.GetItems();
            bool hasCleaner = false;
            foreach (Item item in items)
            {
                if (item.Name == "clogcleaner")
                {
                    hasCleaner = true;
                }
            }
            //show the first key if the first use on toilet hasn't been made
            if (firstFlush)
            {
                key1Start.alpha = 1f;
            }
            
            //check if you are near the toilet
            if (mCharacter.position.x > -24 && mCharacter.position.x < 24 && use)
            {
                use = false;
                
                
                if (firstFlush)//check if this is the first use
                {
                    scrollText.Text = "The key fell down into the toilet.             \nGreat.";
                    scrollText.StartScrolling();
                    firstFlush = false;
                    key1Start.alpha = 0f;
                }
                else
                {
                    scrollText.Text = "I think the pipes might be clogged.";
                    scrollText.StartScrolling();
                }

                if (hasCleaner)//check if the player has clogcleaner
                {
                    scrollText.Text = "Used the clog cleaner.";
                    scrollText.StartScrolling();
                    usedClog = true;
                }
            }
        }
        else
        {
            //hide key if you leave the room
            key1Start.alpha = 0f;
        }
        
    }
    /// <summary>
    /// exit rooms events & dialogue
    /// </summary>
    private void ExitRoomEvents()
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
            if(mCharacter.position.x > -114 && mCharacter.position.x < 136 && use)
            {
                if (keyCount == 0)
                {
                    scrollText.Text = "The door is locked, there is a keyhole on both sides of the door.";
                    scrollText.StartScrolling();
                }
                else if(keyCount == 1)
                {
                    scrollText.Text = "It needs another key";
                    scrollText.StartScrolling();
                }
                else if(keyCount == 2)
                {
                    //win the game, show click count, some ending scene and load credits
                }
            }
        }
    }

    /// <summary>
    /// handle control room events
    /// </summary>
    private void ControlRoomEvents()
    {
        if(player.LocationName == "controlRoom")
        {

            //dont let character out the normal way
            if(mCharacter.position.x < -300)
            {
                player.Position = new Vector3(-299, -54, 100);
            }
            //check if you are near the table and want to play lightsout
            if(mCharacter.position.x > 204 && mCharacter.position.x < 301 && use)
            {
                lightsoffGame.ShowGame();
            }
        }
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
            }
            if (hasClogcleaner)
            {
                clogCleaner.gameObject.SetActive(false);
            }
            else
            {
                clogCleaner.gameObject.SetActive(true);
            }
            
            //special case room, disable character here
            mCharacter.gameObject.SetActive(false);
        }
        else
        {
            clogCleaner.gameObject.SetActive(false);
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
}

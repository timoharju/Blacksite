using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    
    InputField console;
    CanvasGroup consoleCanvasGroup;
    Rigidbody2D mCharacter;
    Vector3 roomLeftPos;
    Vector3 roomRightPos;
    Rigidbody2D leftPoint;
    Rigidbody2D rightPoint;
    Button useButton;
    ConsoleInput consoleInput;
    Button optionsDevConsole;
    Options options;
    Sprite background;
    Rooms room1;
    Rooms room2;
    Rooms room3;
    Rooms room4;
    Rooms room5;
    Rooms room6;
    Vector3 startPos;
    Vector3 returnPos;
    Vector3 nextRoomPos;
    Player player;
    List<Rooms> rooms;


    bool use = false;

    // Use this for initialization
    void Start () {
        
        console = GameObject.Find("ConsoleInputField").GetComponent<InputField>();
        consoleCanvasGroup = GameObject.Find("consoleCanvasGroup").GetComponent<CanvasGroup>();
        mCharacter = GameObject.Find("MCharacter").GetComponent<Rigidbody2D>();
        useButton = GameObject.Find("useButton").GetComponent<Button>();
        useButton.onClick.AddListener(() => Use());
        optionsDevConsole = GameObject.Find("OptionsDevConsole").GetComponent<Button>();
        consoleInput = new ConsoleInput(console, consoleCanvasGroup, optionsDevConsole);

        options = new Options();
        rooms = new List<Rooms>();

        room1 = new Rooms("start", "room1");
        room2 = new Rooms("cellRoom", "room2");
        room3 = new Rooms("largeCellRoom", "room3");
        room4 = new Rooms("exitRoom", "room4");
        room5 = new Rooms("securityDoor", "room5");
        room6 = new Rooms("controlRoom", "room6");

        
        rooms.Add(room1);
        rooms.Add(room2);
        rooms.Add(room3);
        rooms.Add(room4);
        rooms.Add(room5);
        rooms.Add(room6);



        //setup previous and next rooms
        int i = 0;

        foreach(Rooms room in rooms)
        {
            if(i >= 1)
            {
                room.PreviousRoom = rooms[i - 1];
            }
            if(i < (rooms.Count-1))
            {
                room.NextRoom = rooms[i + 1];
            }
            i++;
        }
        
        

        room1.SetActiveRoom(room1); // set starting location

        player = new Player(rooms, mCharacter);
        consoleInput.Player = player;

        startPos = new Vector3(-200, -54, 100);
        nextRoomPos = new Vector3(-360, -54, 100);
        returnPos = new Vector3(360, -54, 100);

        player.Position = startPos;
        player.SetScale(150);


        //mCharacter.position = returnPos;
        
    }

    void Use()
    {
        use = true;
    }


	// Update is called once per frame
	void Update () {
        
        if (Input.GetKey("f1"))
        {
            consoleInput.ToggleConsole();
        }

        if (Input.GetKey("f2"))
        {

            Debug.Log("pos: " + player.Position);
        }
        if (Input.GetKey("f3"))
        {
            player.Position = startPos;
        }
        //previous room
        if(mCharacter.position.x <= -364)
        {
            
            if(player.PreviousLocation != null)
            {
                player.Position = returnPos;
                player.SetLocation(player.PreviousLocation);
            }
            
            use = false;
            
        }
        //next room
        if(mCharacter.position.x >= 364)
        {
            
            if(player.NextLocation != null)
            {
                player.Position = nextRoomPos;
                player.SetLocation(player.NextLocation);
            }
            
            use = false;
        }

        //artificial boundary for the first and last rooms
        if(player.Location.NextRoom == null && mCharacter.position.x > 300)
        {
            player.Position = new Vector3(299, -54, 100);
        }
        if(player.Location.PreviousRoom == null && mCharacter.position.x < -300)
        {
            player.Position = new Vector3(-299, -54, 100);
        }
    }

    
}

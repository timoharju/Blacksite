using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// class for handling rooms, knows next and previous
/// </summary>
public class Rooms {

    private string roomName = "";
    private string roomBackground = "";
    private Rooms nextRoom;
    private Rooms previousRoom;
    private RawImage backgroundPicture;
    private Rooms specialRoom;
    /// <summary>
    /// roomName just a generic name, roomBackground has to be a picture from Assets folder!
    /// </summary>
    /// <param name="roomName"></param>
    /// <param name="roomBackground"></param>
    public Rooms(string roomName, string roomBackground)
    {
        this.roomName = roomName;
        this.roomBackground = roomBackground;
        backgroundPicture = GameObject.Find("BackgroundPicture").GetComponent<RawImage>();
        backgroundPicture.texture = (Texture)Resources.Load(roomBackground, typeof(Texture));
    }
    /// <summary>
    /// return/set next room(if any)
    /// </summary>
    public Rooms NextRoom
    {
        get { return this.nextRoom; }
        set { this.nextRoom = value; }
    }
    /// <summary>
    /// return/set previous room(if any)
    /// </summary>
    public Rooms PreviousRoom
    {
        get { return this.previousRoom; }
        set { this.previousRoom = value; }
    }

    /// <summary>
    /// get/set a special room
    /// </summary>
    public Rooms SpecialRoom
    {
        get { return this.specialRoom; }
        set { this.specialRoom = value; }
    }

    /// <summary>
    /// return generic roomName
    /// </summary>
    public string RoomName
    {
        get { return this.roomName; }
        set { this.roomName = value; }
    }
    /// <summary>
    /// return/set background from assets folder
    /// </summary>
    public string RoomBackground
    {
        get { return this.roomBackground; }
        set { this.roomBackground = value; }
    }
    /// <summary>
    /// set active room
    /// </summary>
    /// <param name="room"></param>
    public void SetActiveRoom(Rooms room)
    {
        string bg = room.RoomBackground;
        backgroundPicture.texture = (Texture)Resources.Load(bg, typeof(Texture));
    }
}

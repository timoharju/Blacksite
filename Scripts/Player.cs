using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// knows players location
/// </summary>
public class Player  {

    private Rooms location;
    private List<Rooms> roomList;
    private Rigidbody2D mCharacter;

    /// <summary>
    /// list of all rooms
    /// </summary>
    /// <param name="roomList"></param>
    public Player(List<Rooms> roomList, Rigidbody2D mCharacter)
    {
        this.roomList = roomList;
        this.location = roomList[0];
        this.mCharacter = mCharacter;
    }
    /// <summary>
    /// current location
    /// </summary>
    public Rooms Location
    {
        get { return this.location; }
        set { this.location = value; }
    }
    /// <summary>
    /// set first room as active one
    /// </summary>
    public Rooms StartingLocation
    {
        get { return roomList[0]; }
    }
    /// <summary>
    /// set player to another room
    /// </summary>
    /// <param name="room"></param>
    public void SetLocation(Rooms room)
    {
        if(room.RoomName != "start")
        {
            SetScale(100);
        }
        else
        {
            SetScale(150);
        }
        room.SetActiveRoom(room);
        Location = room;
    }
    /// <summary>
    /// set player to next room
    /// </summary>
    public Rooms NextLocation
    {
        get { return Location.NextRoom; }
    }
    /// <summary>
    /// set player to previous room
    /// </summary>
    public Rooms PreviousLocation
    {
        get { return Location.PreviousRoom; }
    }
    /// <summary>
    /// get the location name
    /// </summary>
    public string LocationName
    {
        get { return Location.RoomName; }
    }
    /// <summary>
    /// get or set player sprite position
    /// </summary>
    public Vector3 Position
    {
        get { return this.mCharacter.position; }
        set { mCharacter.transform.position = value; }
    }
    /// <summary>
    /// set player scale, for diffirent sized rooms
    /// </summary>
    /// <param name="scale"></param>
    public void SetScale(int scale)
    {
        mCharacter.transform.localScale = new Vector3(scale, scale, scale);
    }

}

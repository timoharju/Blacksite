using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
/// <summary>
/// create items
/// </summary>
public class Item {

    private string name;
    private string imageLocation;
    private string imageLocationHistory;
    private RawImage itemSlot;
    
    /// <summary>
    /// give items a name and sprite to use
    /// </summary>
    /// <param name="name"></param>
    /// <param name="imageLocation"></param>
    public Item(string name, string imageLocation, RawImage itemSlot)
    {
        this.name = name;
        this.imageLocation = imageLocation;
        imageLocationHistory = imageLocation;
        this.itemSlot = itemSlot;
        SetImage();
    }
    /// <summary>
    /// return item name
    /// </summary>
    public string Name
    {
        get { return this.name; }
    }
    /// <summary>
    /// return item image
    /// </summary>
    public string Image
    {
        get { return this.imageLocation; }
        set { this.imageLocation = value; }
    }
    /// <summary>
    /// sets items image visible again, used if you set the image to transparent to update inventory screen
    /// </summary>
    public void RestoreImage()
    {
        Image = imageLocationHistory;
    }
    /// <summary>
    /// call this to update item image in inventory screen
    /// </summary>
    public void SetImage()
    {
        itemSlot.texture = (Texture)Resources.Load(imageLocation, typeof(Texture));

    }
}

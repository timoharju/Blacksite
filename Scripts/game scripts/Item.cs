using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
/// <summary>
/// Create items for your inventory
/// </summary>
public class Item {

    /// <summary>
    /// Get/Set the item name
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Get/Set items image location, path to the image in the Resources folder
    /// </summary>
    public string Image { get; set; }
    
    /// <summary>
    /// give items a name and sprite to use
    /// </summary>
    /// <param name="name">Name of the item.</param>
    /// <param name="imageLocation">Path to the image in resources folder</param>
    public Item(string name, string imageLocation)
    {
        Name = name;
        Image = imageLocation;
    }

}

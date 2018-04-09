using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Hold all your items!
/// </summary>
public class Inventory {

    private List<Item> itemList;
    CanvasGroup inventoryCanvas;
    Audio sound = new Audio();
    
    /// <summary>
    /// create empty item list, set inventory hidden by default
    /// </summary>
    public Inventory()
    {

        itemList = new List<Item>();
        inventoryCanvas = GameObject.Find("InventoryCanvas").GetComponent<CanvasGroup>();

        inventoryCanvas.alpha = 0f;
        inventoryCanvas.interactable = false;
        inventoryCanvas.blocksRaycasts = false;

       
    }
    /// <summary>
    /// Add items to the inventory list
    /// </summary>
    /// <param name="item"></param>
    public void AddItem(Item item)
    {
        if (!itemList.Contains(item))
        {
            itemList.Add(item);
            item.RestoreImage();
            item.SetImage();
        }
    }
    /// <summary>
    /// remove items from list
    /// </summary>
    /// <param name="item"></param>
    public void RemoveItem(Item item)
    {
        if (itemList.Contains(item))
        {
            itemList.Remove(item);
            item.Image = "transparent";
            item.SetImage();
        }
    }
    /// <summary>
    /// show inventory canvas
    /// </summary>
    public void ShowInventory()
    {
        GotoMouse.menuOpen = true;
        GotoMouse.move = false;
        sound.InventoryOpenAudio();
        inventoryCanvas.alpha = 1f;
        inventoryCanvas.interactable = true;
        inventoryCanvas.blocksRaycasts = true;
        
    }
    /// <summary>
    /// hide inventory canvas
    /// </summary>
    public void HideInventory()
    {
        GotoMouse.menuOpen = false;
        GotoMouse.move = false;
        inventoryCanvas.alpha = 0f;
        inventoryCanvas.interactable = false;
        inventoryCanvas.blocksRaycasts = false;
    }
}

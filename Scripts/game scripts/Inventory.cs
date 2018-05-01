using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Hold all your items!
/// </summary>
public class Inventory {

    private List<Item> itemList;
    private CanvasGroup inventoryCanvas;
    private Audio sound = new Audio();
    private RawImage itemSlot1;
    private RawImage itemSlot2;
    private RawImage itemSlot3;
    private RawImage itemSlot4;
    private List<RawImage> itemSlots;
    private GameObject paperUnfold;
    private GameObject clogCleanerItem;

    
    /// <summary>
    /// create empty item list, item slot list, set inventory window hidden by default
    /// </summary>
    public Inventory()
    {

        itemList = new List<Item>();
        inventoryCanvas = GameObject.Find("InventoryCanvas").GetComponent<CanvasGroup>();
        paperUnfold = GameObject.Find("PaperUnfold");
        clogCleanerItem = GameObject.Find("ClogCleanerInspect");
        //hide unfolded paper screen
        //if this line gives you errors make sure the PaperUnfold -is active- when you start the game!! (otherwise it can't find the object)
        paperUnfold.SetActive(false);
        clogCleanerItem.SetActive(false);

        itemSlots = new List<RawImage>();
        itemSlots.Add(itemSlot1 = GameObject.Find("ItemSlot1").GetComponent<RawImage>());
        itemSlots.Add(itemSlot2 = GameObject.Find("ItemSlot2").GetComponent<RawImage>());
        itemSlots.Add(itemSlot3 = GameObject.Find("ItemSlot3").GetComponent<RawImage>());
        itemSlots.Add(itemSlot4 = GameObject.Find("ItemSlot4").GetComponent<RawImage>());

        inventoryCanvas.alpha = 0f;
        inventoryCanvas.interactable = false;
        inventoryCanvas.blocksRaycasts = false;

        //add button listeners to all itemslots
        foreach(RawImage slot in itemSlots)
        {
            slot.GetComponent<Button>().onClick.AddListener(() => ItemClick(slot.GetComponent<Button>()));
        }
       
    }
    /// <summary>
    /// Add items to the inventory list, if it isn't on it already
    /// </summary>
    /// <param name="item"></param>
    public void AddItem(Item item)
    {
        if (!itemList.Contains(item))
        {
            itemList.Add(item);
        }
        UpdateItemSlots();
    }
    /// <summary>
    /// Remove specified item from the list (if it is there)
    /// </summary>
    /// <param name="item"></param>
    public void RemoveItem(Item item)
    {
        if (itemList.Contains(item))
        {
            itemList.Remove(item);
        }
        UpdateItemSlots();
    }
    /// <summary>
    /// show inventory window
    /// </summary>
    public void ShowInventory()
    {
        GotoMouse.MenuOpen = true;
        GotoMouse.Move = false;
        sound.InventoryOpenAudio();
        inventoryCanvas.alpha = 1f;
        inventoryCanvas.interactable = true;
        inventoryCanvas.blocksRaycasts = true;
        
    }
    /// <summary>
    /// hide inventory window
    /// </summary>
    public void HideInventory()
    {
        GotoMouse.MenuOpen = false;
        GotoMouse.Move = false;
        inventoryCanvas.alpha = 0f;
        inventoryCanvas.interactable = false;
        inventoryCanvas.blocksRaycasts = false;
        //hide the paper when you close the inventory
        paperUnfold.SetActive(false);
        clogCleanerItem.SetActive(false);
    }

    /// <summary>
    /// returns all items in the inventory as List<Item>
    /// </summary>
    /// <returns>List<Item></returns>
    public List<Item> GetItems()
    {
        return this.itemList;
    }

    /// <summary>
    /// update item slots in the inventory, called after every add or remove
    /// </summary>
    private void UpdateItemSlots()
    {
        int i = 0;
        foreach(RawImage slot in itemSlots)
        {
            if(i >= itemList.Count)
            {
                slot.texture = (Texture)Resources.Load("transparent", typeof(Texture));
            }
            else
            {
                slot.texture = (Texture)Resources.Load(itemList[i].Image, typeof(Texture));
            }
            i++;
        }
    }

    /// <summary>
    /// every item slot has a button, but we only care about the paper_fold and clog_cleaner buttons right now
    /// but since they could be anywhere add listeners to all item slots, and let's find one of them by texture name
    /// </summary>
    /// <param name="button"></param>
    private void ItemClick(Button button)
    {
        //get the buttons RawImage texture name... pretty much the only way to tell them apart since the items can be in any order
        if(button.gameObject.GetComponent<RawImage>().texture.name == "paper_fold")
        {
            paperUnfold.SetActive(true);
        }

        if(button.gameObject.GetComponent<RawImage>().texture.name == "clog_cleaner")
        {
            clogCleanerItem.SetActive(true);
        }
    }
}

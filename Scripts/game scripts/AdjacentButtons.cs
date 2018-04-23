using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// lightsoffGame helper, keep track of adjacent buttons
/// </summary>
public class AdjacentButtons {

    private List<Button> adjacentList;
	/// <summary>
    /// create new list of adjacent buttons
    /// </summary>
    public AdjacentButtons()
    {
        adjacentList = new List<Button>();
    }
    /// <summary>
    /// add buttons to the list
    /// </summary>
    /// <param name="button"></param>
    public void SetAdjacentButton(Button button)
    {
        adjacentList.Add(button);
    }
    /// <summary>
    /// get the list of adjecent buttons
    /// </summary>
    /// <returns></returns>
    public List<Button> GetAdjecentList()
    {
        return this.adjacentList;
    }
}

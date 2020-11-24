using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> characterItems = new List<Item>();
    public ItemDatabase itemDatabase;
    UIInventory uinv;


    private void Start()
    {
        uinv = FindObjectOfType<UIInventory>();
        GiveItem(0);
    }

    public void GiveItem(int id)
    {
        Item itemToAdd = itemDatabase.GetItem(id);
        characterItems.Add(itemToAdd);
        uinv.AddNewItem(itemToAdd);
        Debug.Log("Added: " + itemToAdd.title);
    }

    public Item CheckForItem(int id)
    {
        return characterItems.Find(item => item.id == id);
    }

    public void RemoveItem(int id)
    {
        Item item = CheckForItem(id);
        if (item != null)
        {
            characterItems.Remove(item);
            uinv.RemoveItem(item);
            Debug.Log("ItemRemoved: " + item.title);
        }
    }   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIInventory : MonoBehaviour
{
    public List <UIItem> uIItemslots = new List<UIItem>();
    public GameObject slotPrefab;
    public Transform slotPanel;
    public int numberOfSlots = 2;

    private void Awake()
    {
        for(int i = 0; i < numberOfSlots; i++)
        {

        }
    }

    public void UpdateSlot(int slot, Item item)
    {
        uIItemslots[slot].UpdateItem(item);
    }

    public void AddNewItem(Item item)
    {
        UpdateSlot(uIItemslots.FindIndex(i => i.item == null), item);
    }

    public void RemoveItem(Item item)
    {
        UpdateSlot(uIItemslots.FindIndex(i => i.item == item), null);
    }
}

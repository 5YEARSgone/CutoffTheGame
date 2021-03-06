﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> items = new List<Item>();


    private void Awake()
    {
        BuildDatabase();
    }

    public Item GetItem(int id)
    {
        return items.Find(item => item.id == id);
    }

    public Item GetItem(string itemName)
    {
        return items.Find(item => item.title == itemName);
    }

    void BuildDatabase()
    {
        items = new List<Item>()
        {
            new Item(0, "Flashlight", "Description",
            new Dictionary<string, int>
            {
                {"Stat1", 15 },
                {"Stat2", 100 }

            }),
            new Item(1, "Key1", "Desc1ription",
            new Dictionary<string, int>
            {
                {"Stat1", 434 },
                {"Stat2", 1030 }

            }),
            new Item(2, "Key2", "Desc1ription",
            new Dictionary<string, int>
            {
                {"Stat1", 434 },
                {"Stat2", 1030 }

            })
        };
    }
}

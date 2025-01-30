using System;
using System.Collections.Generic;

[Serializable]
public class Inventory
{
    public Dictionary<ItemType, long> resources = new Dictionary<ItemType, long>();

    public void AddItem(ItemType itemType)
    {
        if (!resources.TryAdd(itemType, 1))
            resources[itemType]++;
    }

    public void AddItem(ItemType itemType, long amount)
    {
        if (!resources.TryAdd(itemType, amount))
            resources[itemType] += amount;
    }
}
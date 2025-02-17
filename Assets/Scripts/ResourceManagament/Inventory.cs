using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class Inventory
{
    public const long MAX_RESOURCE_VALUE = 1000;

    /* Ideally, all objects should have the resources they would use initialized,
     * even if they do not possess it initially */
    public Dictionary<ItemType, long> resources = new Dictionary<ItemType, long>();

    public void AddItem(ItemType itemType, long amount = 1)
    {
        if (!resources.TryAdd(itemType, amount))
            resources[itemType] += amount;

        if (resources[itemType] > MAX_RESOURCE_VALUE)
            resources[itemType] = MAX_RESOURCE_VALUE;
    }

    public void AddItem(Dictionary<ItemType, long> dictionaryToAdd, bool emptyOtherDic = false)
    {
        Dictionary<ItemType, long> dicToAddCopy = new Dictionary<ItemType, long>(dictionaryToAdd);

        foreach (var kvp in dicToAddCopy)
        {
            if (!resources.TryAdd(kvp.Key, kvp.Value))
                resources[kvp.Key] += kvp.Value;

            if (resources[kvp.Key] > MAX_RESOURCE_VALUE)
                resources[kvp.Key] = MAX_RESOURCE_VALUE;
        }

        if (emptyOtherDic)
        {
            foreach (var key in dictionaryToAdd.Keys.ToList())
            {
                dictionaryToAdd[key] = 0;
            }
        }
    }

    public bool ReduceItem(ItemType itemType, long amount = 1)
    {
        if (resources.TryGetValue(itemType, out long item) && item >= amount)
        {
            resources[itemType] -= amount;
            return true;
        }

        return false;
    }

    public bool ReduceAll(long amount = 1)
    {
        foreach (var key in resources.Keys.ToList())
        {
            if (!ReduceItem(key, amount))
            {
                return false;
            }
        }

        return true;
    }

    public long GetValue(ItemType itemType)
    {
        return resources[itemType];
    }
}
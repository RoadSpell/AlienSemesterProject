using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class Engine : SerializedMonoBehaviour, IInteractable, IInventoryHolder
{
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private long consumptionPerSec;
    //[OdinSerialize] private Inventory inventory;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating(nameof(ConsumeResources), 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void ConsumeResources()
    {
        if (!OwnInventory.ReduceAll(consumptionPerSec))
        {
            NotifyGameOver();
        }
    }

    private void NotifyGameOver()
    {
    }

    [field: SerializeField] public GameObject WorldSpaceUI { get; set; }

    public void Interact()
    {
        Dictionary<ItemType, long> playerInventoryDic = new Dictionary<ItemType, long>(playerInventory.resources);

        foreach (var kvp in playerInventoryDic)
        {
            OwnInventory.AddItem(kvp.Key, kvp.Value);
        }
    }

    [field: OdinSerialize] public Inventory OwnInventory { get; set; }
}
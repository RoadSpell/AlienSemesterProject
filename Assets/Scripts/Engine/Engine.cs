using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class Engine : SerializedMonoBehaviour, IInteractable, IInventoryHolder
{
    [OdinSerialize] private IInventoryHolder playerInventory;
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
        if (Input.GetKeyDown(KeyCode.E) && WorldSpaceUI.activeSelf)
            GetInteracted();
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

    public void GetInteracted()
    {
        Debug.Log("Interacting with the engine");
        Dictionary<ItemType, long> playerInventoryDicCopy =
            new Dictionary<ItemType, long>(playerInventory.OwnInventory.resources);

        foreach (var kvp in playerInventoryDicCopy)
        {
            OwnInventory.AddItem(kvp.Key, kvp.Value);
        }

        playerInventory.OwnInventory.resources = new Dictionary<ItemType, long>()
        {
            { ItemType.Carbon, 0 }, { ItemType.Hydrogen, 0 },
            { ItemType.Oxygen, 0 }
        };
    }

    [field: OdinSerialize] public Inventory OwnInventory { get; set; }
}
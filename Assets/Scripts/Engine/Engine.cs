using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Serialization;

public class Engine : SerializedMonoBehaviour, IInteractable, IInventoryHolder
{
    [OdinSerialize] private IInventoryHolder playerInventory;
    [SerializeField] private long baseConsumptionPerSec;
    [SerializeField] private bool inOverDriveMode = false;
    public long OverDriveConsumptionPerSec => baseConsumptionPerSec * 2;
    private long _currentConsumptionPerSec;
    [SerializeField] private float overdriveRequiredRng;
    //[OdinSerialize] private Inventory inventory;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentConsumptionPerSec = baseConsumptionPerSec;
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
        if (!OwnInventory.ReduceAll(_currentConsumptionPerSec))
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

    private void RngForOverdriveMode()
    {
        float rngResult = Random.Range(0f, 100f);

        if (rngResult <= overdriveRequiredRng)
        {
            GoIntoOverDriveMode();
        }
    }

    private void GoIntoOverDriveMode()
    {
        inOverDriveMode = true;
        _currentConsumptionPerSec = OverDriveConsumptionPerSec;
    }

    private void StopOverDriveMode()
    {
        inOverDriveMode = false;
    }

    [field: OdinSerialize] public Inventory OwnInventory { get; set; }
}
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class Engine : SerializedMonoBehaviour, IInteractable, IInventoryHolder
{
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
    [field: OdinSerialize] public Inventory OwnInventory { get; set; }
}
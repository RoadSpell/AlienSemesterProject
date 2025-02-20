using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class StardustVacuum : SerializedMonoBehaviour, IInventoryHolder, IInteractable
{
    [OdinSerialize] public Inventory OwnInventory { get; set; }
    [OdinSerialize] private IInventoryHolder _playerInventory;
    [SerializeField] private long generatedStardustPerSec;
    [SerializeField] private MeshRenderer vacuumGlassMeshRenderer;

    [field: SerializeField] public GameObject WorldSpaceUI { get; set; }

    private void Start()
    {
        InvokeRepeating(nameof(GenerateStardust), 0f, 1f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && WorldSpaceUI.activeSelf)
        {
            GetInteracted();
        }
    }

    public void GetInteracted()
    {
        _playerInventory.OwnInventory.AddItem(OwnInventory.resources, true);
    }

    private void GenerateStardust()
    {
        OwnInventory.AddItem(ItemType.Stardust, generatedStardustPerSec);
        vacuumGlassMeshRenderer.material.color =
            Color.magenta *
            Mathf.Clamp01(OwnInventory.resources[ItemType.Stardust] / (Inventory.MAX_RESOURCE_VALUE * 0.1f));
    }
}
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class InventoryResourceShower : SerializedMonoBehaviour
{
    [SerializeField] private ItemType itemType;
    [SerializeField] private Image fillImage;
    [OdinSerialize] private IInventoryHolder inventoryHolder;

    void Update()
    {
        fillImage.fillAmount =
            Mathf.Clamp01((float)inventoryHolder.OwnInventory.GetValue(itemType) / Inventory.MAX_RESOURCE_VALUE);
    }
}
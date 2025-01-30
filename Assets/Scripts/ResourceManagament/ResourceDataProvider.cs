using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ResourceDataProvider : SerializedMonoBehaviour
{
    public Dictionary<ItemType, ResourceSO> resourceData = new Dictionary<ItemType, ResourceSO>();
}
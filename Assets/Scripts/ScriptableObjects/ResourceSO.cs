using UnityEngine;

[CreateAssetMenu(fileName = "ResourceSO", menuName = "Scriptable Objects/ResourceSO")]
public class ResourceSO : ScriptableObject
{
    public string Name;
    public string Description;
    public Sprite Icon;
    public ItemType ResourceType;

    public bool IsSameResource(ItemType targetType)
    {
        return ResourceType == targetType;
    }

    public bool IsSameResource(ResourceSO targetType)
    {
        return ResourceType == targetType.ResourceType;
    }
}


public enum ItemType
{
    Carbon,
    Hydrogen,
    Oxygen,
    Stardust
}
using UnityEngine;

public interface IInteractable
{
    public GameObject WorldSpaceUI { get; set; }

    public void GetInteracted();
}
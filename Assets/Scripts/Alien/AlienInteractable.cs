using UnityEngine;

public class AlienInteractable : MonoBehaviour, IInteractable
{
    [field: SerializeField] public GameObject WorldSpaceUI { get; set; }

    public void Interact()
    {
        throw new System.NotImplementedException();
    }
}
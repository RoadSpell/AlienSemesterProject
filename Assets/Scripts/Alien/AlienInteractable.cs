using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AlienInteractable : MonoBehaviour, IInteractable
{
    [field: SerializeField] public GameObject WorldSpaceUI { get; set; }
    [SerializeField] private Image happinessBar;
    [FormerlySerializedAs("onFedEvent")] [SerializeField] private OnInteractedEvent onInteractedEvent;
    public float Happiness;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && WorldSpaceUI.activeSelf)
            GetInteracted();
    }

    public void GetInteracted()
    {
        //Debug.Log($"{transform.parent.name} got fed!");
        onInteractedEvent?.SendEventMessage(transform.parent.gameObject);
    }

    public void AdjustHappinessUIBar()
    {
        happinessBar.fillAmount = Mathf.Clamp01(Happiness / 100f);
    }
}
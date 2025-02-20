using System;
using System.Collections.Generic;
using System.Linq;
using AssetInventory;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class PlayerLegacy : SerializedMonoBehaviour, IInventoryHolder
{
    private Vector3 _moveDir = Vector3.zero;

    //[SerializeField] private OnFedEvent onFedEvent;
    [SerializeField] private OnPlayerStumbled onPlayerStumbled;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private CharacterController controller;
    [SerializeField] private float playerSpeed = 3f;
    [SerializeField] private float getUpTime = 1f;

    [SerializeField] private AudioSource playerAudioSource;
    [SerializeField] private AudioClip fellDownSfx;
    private bool _fallenDown = false;

    public bool FallenDown
    {
        get => _fallenDown;
        set
        {
            _fallenDown = value;
            playerAudioSource.PlayOneShot(fellDownSfx);
        }
    }


    [SerializeField, Unity.Collections.ReadOnly]
    private List<Transform> closestInteractables = new List<Transform>();

    [SerializeField, Unity.Collections.ReadOnly]
    private Transform closestInteractable;

    [OdinSerialize] public Inventory OwnInventory { get; set; }

    /*public Dictionary<ItemType, int> Inventory = new Dictionary<ItemType, int>
    {
        { ItemType.Carbon, 0 },
        { ItemType.Hydrogen, 0 },
        { ItemType.Oxygen, 0 }
    };*/

    public Transform ClosestInteractable
    {
        get => closestInteractable;
        set
        {
            closestInteractable = value;
            //Debug.Log($"Closest interactable: {closestInteractable.name}");
            ShowClosestInteractable();
        }
    }

    private void Start()
    {
        onPlayerStumbled.Event += DoFallDown;
    }

    private void OnDisable()
    {
        onPlayerStumbled.Event -= DoFallDown;
    }

    private void OnDestroy()
    {
        onPlayerStumbled.Event -= DoFallDown;
    }

    void Update()
    {
        /* HADES STYLE CAMERA MOVEMENT
         if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            transform.forward = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            _moveDir = transform.forward;
        }
        else
        {
            _moveDir = Vector3.zero;
        }*/

        // RESIDENT EVIL STYLE CAMERA DEPENDENT MOVEMENT
        if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && !_fallenDown)
        {
            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;

            forward.y = 0f;
            right.y = 0f;

            // Normalize the vectors here to make speed independent of the distance to the camera
            forward.Normalize();
            right.Normalize();

            _moveDir = forward * Input.GetAxis("Vertical") + right * Input.GetAxis("Horizontal");
            // Face the direction of movement
            transform.forward = _moveDir;
        }
        else
        {
            _moveDir = Vector3.zero;
        }

        DetermineClosestInteractable();
    }

    private void FixedUpdate()
    {
        controller.SimpleMove(_moveDir * playerSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"{other.name} entered trigger");
        if (other.CompareTag("Interactable") && other.TryGetComponent(out IInteractable interactable))
        {
            closestInteractables.Add(other.gameObject.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log($"{other.name} exited trigger");
        if (other.CompareTag("Interactable") && other.TryGetComponent(out IInteractable interactable))
        {
            interactable.WorldSpaceUI.SetActive(false);
            closestInteractables.Remove(other.gameObject.transform);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        //Debug.Log($"{other.gameObject.name} collided with player");
    }

    private void DetermineClosestInteractable()
    {
        if (closestInteractables.Count == 0)
        {
            closestInteractable = null;
            return;
        }

        float closestDistance = Mathf.Infinity;
        Transform closest = null;

        foreach (var interactable in closestInteractables)
        {
            float distance = Vector3.Distance(transform.position, interactable.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = interactable;
            }
        }

        ClosestInteractable = closest;
    }

    private void ShowClosestInteractable()
    {
        if (ClosestInteractable == null)
            return;

        ClosestInteractable.GetComponent<IInteractable>().WorldSpaceUI.SetActive(true);
        closestInteractables.Where(interactable => interactable != closestInteractable)
            .ForEach(interactable => interactable.GetComponent<IInteractable>().WorldSpaceUI.SetActive(false));
    }

    public void PickUpItem(ItemType itemType, long amount = 1)
    {
        OwnInventory.AddItem(itemType, amount);
    }

    private void DoFallDown(GameObject agent, GameObject target)
    {
        if (target != gameObject || _fallenDown)
        {
            Debug.Log($"Something stumbled: {target.name}, but not me: {gameObject.name}!");
            return;
        }


        Debug.Log("I fell down!");
        _fallenDown = true;
        if (fellDownSfx != null)
            playerAudioSource.PlayOneShot(fellDownSfx);
        Invoke(nameof(GetBackUp), getUpTime);
    }

    private void GetBackUp()
    {
        _fallenDown = false;
        Debug.Log("I got up!");
    }
}
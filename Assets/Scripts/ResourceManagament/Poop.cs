using System;
using UnityEngine;

public class Poop : MonoBehaviour
{
    public ItemType itemType = ItemType.Carbon;
    private float _sinDegree = 0f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3 spawnInitialForce;
    [SerializeField] private Vector3 spawnInitialTorque;

    private void Start()
    {
        rb.AddForce(spawnInitialForce, ForceMode.Impulse);
        rb.angularVelocity = spawnInitialTorque;
    }

    private void Update()
    {
        _sinDegree = (_sinDegree + Time.deltaTime) % 360;
    }

    private void FixedUpdate()
    {
        Vector3 oldPos = transform.position;
        rb.MovePosition(new Vector3(oldPos.x, (Mathf.Sin(_sinDegree) / 8) + 0.5f, oldPos.z));
        //transform.position = new Vector3(oldPos.x, (Mathf.Sin(_sinDegree) / 4) + 0.5f, oldPos.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerLegacy playerLegacy = other.GetComponent<PlayerLegacy>();
            playerLegacy.PickUpItem(itemType);
            Destroy(gameObject);
        }
    }
}
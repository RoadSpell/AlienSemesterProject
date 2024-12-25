using System;
using UnityEngine;

public class Poop : MonoBehaviour
{
    public ItemType itemType = ItemType.Carbon;
    private float _sinDegree = 0f;

    private void Update()
    {
        _sinDegree = (_sinDegree + Time.deltaTime) % 360;
    }

    private void FixedUpdate()
    {
        Vector3 oldPos = transform.position;
        transform.position = new Vector3(oldPos.x, (Mathf.Sin(_sinDegree) / 4) + 0.5f, oldPos.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            player.PickUpItem(itemType);
            Destroy(gameObject);
        }
    }
}
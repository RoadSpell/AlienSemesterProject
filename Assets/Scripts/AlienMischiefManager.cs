using System;
using System.Collections.Generic;
using UnityEngine;

public class AlienMischiefManager : MonoBehaviour
{
    public static AlienMischiefManager Instance { get; private set; }
    public LinkedList<Alien> aliens = new LinkedList<Alien>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        Mischief();
    }

    private void Mischief()
    {
        LinkedListNode<Alien> targetAlienNode = aliens.First;
        Alien targetAlien = targetAlienNode.Value;
        aliens.RemoveFirst();
        targetAlien.Mischief();
        Debug.Log($"Target alien is null: {targetAlien == null}");
        aliens.AddLast(targetAlien);
        Debug.Log($"Target alien is now last: {aliens.Last.Value == targetAlien}");
    }
}
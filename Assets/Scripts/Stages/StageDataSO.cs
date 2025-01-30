using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageDataSO", menuName = "Scriptable Objects/Stage Data")]
public class StageDataSO : ScriptableObject
{
    //public Transform alienSpawnPoint;
    public List<GameObject> aliens = new List<GameObject>();
    [Min(0)] public float alienSpawnInterval;

    // Ran after the previous stage
    [Min(0)] public float stageInitiationDelay;
}
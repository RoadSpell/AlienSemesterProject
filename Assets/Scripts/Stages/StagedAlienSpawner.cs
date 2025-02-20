using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagedAlienSpawner : MonoBehaviour
{
    [SerializeField] private List<StageDataSO> stages = new List<StageDataSO>();
    [SerializeField] private Transform alienSpawnPoint;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private Transform toiletDoorTransform;
    [SerializeField] private AudioClip toiletFlushSfx;
    [SerializeField] private AudioClip doorSlamSfx;
    private int _stageIndex = 0;
    //private Coroutine _stageCoroutine = null;


    private void Start()
    {
        StartCoroutine(IterateOnStages());
    }

    private IEnumerator IterateOnStages()
    {
        while (_stageIndex < stages.Count)
        {
            // Waits for the current stage to end before calling for the activation of the next one
            yield return StartCoroutine(InitiateTargetStage(_stageIndex));
            _stageIndex++;
        }
    }

    private IEnumerator InitiateTargetStage(int stageIndex)
    {
        StageDataSO targetStage = stages[stageIndex];
        yield return new WaitForSeconds(targetStage.stageInitiationDelay);
        sfxSource.PlayOneShot(doorSlamSfx);
        sfxSource.PlayOneShot(toiletFlushSfx);
        toiletDoorTransform.Rotate(new Vector3(0f, 90f, 0f), Space.World);
        // Wait for all aliens to be spawned before resolving
        yield return StartCoroutine(SpawnAliens(targetStage));
        yield return new WaitForSeconds(0.75f);
        toiletDoorTransform.Rotate(new Vector3(0f, -90f, 0f), Space.World);
        sfxSource.PlayOneShot(doorSlamSfx);
    }

    private IEnumerator SpawnAliens(StageDataSO stage)
    {
        foreach (var alienGameObject in stage.aliens)
        {
            GameObject spawnedAlien = Instantiate(alienGameObject, alienSpawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(stage.alienSpawnInterval);
        }
    }
}
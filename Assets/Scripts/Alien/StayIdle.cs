using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class StayIdle : MonoBehaviour
{
    [SerializeField] private Alien alien;
    [SerializeField] private Wanderer wandererComponent;
    [SerializeField, Min(0)] private float standingIdleRandomMin;
    [SerializeField, Min(0)] private float standingIdleRandomMax;
    private float _randomWaitTime;
    private float _timer = 0f;

    private void OnEnable()
    {
        alien.currentState = Alien.State.Idle;
        _randomWaitTime = Random.Range(standingIdleRandomMin, standingIdleRandomMax);
    }

    void Update()
    {
        if (_timer >= _randomWaitTime)
        {
            _timer = 0f;
            wandererComponent.enabled = true;
            this.enabled = false;
            return;
        }

        _timer += Time.deltaTime;
    }
}
using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

// Walks around randomly
public class Wanderer : MonoBehaviour
{
    [SerializeField] private Alien alien;
    private Vector3 _targetWanderPoint;
    [SerializeField, Min(0)] private float wanderPointRandomizationAmountMin;
    [SerializeField, Min(2)] private float wanderPointRandomizationAmountMax;
    [Min(0f)] public float wanderPointErrorMargin;
    public Rigidbody rb;
    public float speed;
    [SerializeField] private StayIdle stayIdleComponent;
    private RaycastHit _hit;

    private void OnEnable()
    {
        alien.currentState = Alien.State.Wandering;
        SetRandomWanderPoint();
    }

    private void FixedUpdate()
    {
        WanderAround();
    }

    protected void WanderAround()
    {
        // Do NOT forget to set a new random wander point when initially spawned!
        Vector3 currentPos = transform.position;
        if (Vector3.Distance(transform.position, _targetWanderPoint) <= wanderPointErrorMargin)
        {
            stayIdleComponent.enabled = true;
            this.enabled = false;
            return;
        }

        Vector3 dir = (_targetWanderPoint - currentPos).normalized;
        rb.linearVelocity = dir * speed;
    }

    protected void SetRandomWanderPoint()
    {
        Vector3 currentPos = transform.position;
        _targetWanderPoint = currentPos;
        float xRandomized = (Random.value < 0.5f ? -1f : 1f) *
                            Random.Range(wanderPointRandomizationAmountMin, wanderPointRandomizationAmountMax);
        float zRandomized = (Random.value < 0.5f ? -1f : 1f) *
                            Random.Range(wanderPointRandomizationAmountMin, wanderPointRandomizationAmountMax);
        Vector3 pointToAdd = new Vector3(xRandomized,
            0f, zRandomized);


        Vector3 dir = (_targetWanderPoint - currentPos).normalized;
        Ray ray = new Ray(transform.position, dir);

        if (Physics.Raycast(ray, out _hit, pointToAdd.magnitude))
        {
            //Debug.LogError("Wander point is not acceptable!");
            SetRandomWanderPoint();
        }
        else
        {
            //Debug.Log("Found acceptable wander point");
            _targetWanderPoint += pointToAdd;
        }
    }
}
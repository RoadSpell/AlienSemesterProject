using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class Alien : MonoBehaviour, IInteractable
{
    protected const float HAPPINESS_LIMIT = 100f;
    protected const float HAPPY_THRESHOLD = 80f;
    protected const float SAD_THRESHOLD = 30f;

    [SerializeField] protected float happiness = 100f;
    [SerializeField] protected float happinessDecreaseRate = 0.55f;
    [SerializeField] protected float poopInterval = 10f;
    [SerializeField, ReadOnly] protected float poopTimer = 0f;
    [SerializeField] GameObject poopPrefab;
    [SerializeField, ReadOnly] protected Mood mood = Mood.Happy;
    [SerializeField] protected bool justSpawned = true;

    public List<Action> methods = new List<Action>() { };

    /*private Vector3 _targetWanderPoint;
    [SerializeField, Min(0)] private float wanderPointRandomizationAmountMin;
    [SerializeField, Min(2)] private float wanderPointRandomizationAmountMax;
    [SerializeField, Min(0f)] private float wanderPointErrorMargin;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    private RaycastHit _hit;
    [SerializeField, Min(0)] private float standingIdleRandomMin;
    [SerializeField, Min(0)] private float standingIdleRandomMax;
    protected Coroutine idleRoutine = null;*/
    public State currentState = State.Idle;
    public Wanderer wandererComponent;
    public StayIdle stayIdleComponent;
    public BehaviourSOBase currentBehaviour = null;


    public float Happiness
    {
        get => happiness;
        protected set
        {
            // Clamp happiness between 0 and 100
            happiness = (value >= 0) ? value : 0;
            happiness = (value <= 100) ? value : 100;

            DetermineHappinessState();
            //happinessBar.fillAmount = happiness / 100f;
        }
    }

    /*private void OnEnable()
    {
        AlienMischiefManager.Instance.aliens.AddLast(this);
    }*/

    private void OnDisable()
    {
        AlienMischiefManager.Instance.aliens.Remove(this);
    }

    private void OnDestroy()
    {
        AlienMischiefManager.Instance.aliens.Remove(this);
    }

    private void Start()
    {
        //SetRandomWanderPoint();
        wandererComponent.enabled = true;
        AlienMischiefManager.Instance.aliens.AddLast(this);
        InvokeRepeating(nameof(DecreaseHappiness), 0f, 1f);
    }


    void Update()
    {
        // Change to state machine
        if (mood != Mood.Sad)
            HandlePoopingAndTimer();

        /*if (currentBehaviour != null)
        {
            currentBehaviour.Execute(this);
        }*/
    }

    /*private void FixedUpdate()
    {
        Wander();
    }*/

    public void Mischief()
    {
        Debug.Log($"I, {transform.parent.gameObject.name} am going to misbehave!");
    }

    [field: SerializeField] public GameObject WorldSpaceUI { get; set; }

    protected void HandlePoopingAndTimer()
    {
        if (poopTimer >= poopInterval)
        {
            poopTimer = 0f;
            Instantiate(poopPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            poopTimer += Time.deltaTime;
        }
    }


    protected void DecreaseHappiness()
    {
        Happiness -= happinessDecreaseRate;
    }

    public void GetFed()
    {
        Happiness = HAPPINESS_LIMIT;
    }

    /*protected void Wander()
    {
        // Do NOT forget to set a new random wander point when initially spawned!
        Vector3 currentPos = transform.position;
        if (Vector3.Distance(transform.position, _targetWanderPoint) <= wanderPointErrorMargin && idleRoutine == null)
        {
            idleRoutine = StartCoroutine(StayIdle());
            return;
        }


        currentState = State.Wandering;
        Vector3 dir = (_targetWanderPoint - currentPos).normalized;
        rb.linearVelocity = dir * speed;
    }

    protected IEnumerator StayIdle()
    {
        Debug.Log("Staying idle!");
        rb.linearVelocity = Vector3.zero;
        currentState = State.Idle;
        yield return new WaitForSeconds(Random.Range(standingIdleRandomMin, standingIdleRandomMax));
        SetRandomWanderPoint();
        idleRoutine = null;
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
            Debug.LogError("Wander point is not acceptable!");
            SetRandomWanderPoint();
        }
        else
        {
            Debug.Log("Found acceptable wander point");
            _targetWanderPoint += pointToAdd;
        }
    }*/

    private void DetermineHappinessState()
    {
        if (Happiness >= HAPPY_THRESHOLD)
            mood = Mood.Happy;
        else if (Happiness <= SAD_THRESHOLD)
            mood = Mood.Sad;
        else
            mood = Mood.Bored;
    }

    protected enum Mood
    {
        Bored,
        Happy,
        Sad
    }

    public enum State
    {
        Idle,
        Wandering,
        Misbehaving
    }
}
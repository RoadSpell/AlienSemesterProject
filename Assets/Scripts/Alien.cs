using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class Alien : SerializedMonoBehaviour, IInteractable
{
    protected const float HAPPINESS_LIMIT = 100f;
    protected const float HAPPY_THRESHOLD = 80f;
    protected const float SAD_THRESHOLD = 30f;

    //[SerializeField] protected float happinessDecreaseInterval = 10f;
    [SerializeField] protected float happiness = 100f;
    [SerializeField] protected float happinessDecreaseRate = 0.55f;
    [SerializeField] protected float poopInterval = 10f;
    [SerializeField, ReadOnly] protected float poopTimer = 0f;
    [SerializeField] GameObject poopPrefab;
    [SerializeField, ReadOnly] protected bool boosted = false;
    [SerializeField, ReadOnly] protected Mood mood = Mood.Happy;
    [field: SerializeField] public GameObject WorldSpaceUI { get; set; }


    private void Start()
    {
        InvokeRepeating(nameof(DecreaseHappiness), 0f, 1f);
    }

    void Update()
    {
        // Change to state machine
        if (mood != Mood.Sad)
            HandlePoopingAndTimer();
    }

    // Make handling pooping the responsibility of another class
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
        if (happiness == 0)
            return;

        happiness -= happinessDecreaseRate;
        if (happiness <= 0)
            happiness = 0;

        DetermineHappinessState();
    }

    private void DetermineHappinessState()
    {
        if (happiness >= HAPPY_THRESHOLD)
            mood = Mood.Happy;
        else if (happiness <= SAD_THRESHOLD)
            mood = Mood.Sad;
        else
            mood = Mood.Bored;
    }

    public void GetFed()
    {
        happiness = HAPPINESS_LIMIT;
    }

    protected enum Mood
    {
        Bored,
        Happy,
        Sad
    }
}
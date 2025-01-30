using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

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
    [field: SerializeField] public GameObject WorldSpaceUI { get; set; }
    public List<Action> methods = new List<Action>() { };

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
}
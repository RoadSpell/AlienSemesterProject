using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Set Happiness in Alien Script", story: "Set [Happiness] in [AlienScript]", category: "Action",
    id: "95a327888fa0d94c802cd1b5d3d956e5")]
public partial class SetHappinessInAlienScriptAction : Action
{
    [SerializeReference] public BlackboardVariable<float> Happiness;
    [SerializeReference] public BlackboardVariable<AlienInteractable> AlienScript;

    protected override Status OnStart()
    {
        AlienScript.Value.Happiness = Happiness.Value;
        AlienScript.Value.AdjustHappinessUIBar();
        return Status.Success;
    }
}
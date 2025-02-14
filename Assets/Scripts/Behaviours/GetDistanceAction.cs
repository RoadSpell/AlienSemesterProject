using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "GetDistance", story: "Set [Value] to distance between [Agent] and [Target]", category: "Action",
    id: "350251ac19ccef8cece5755f8e2c5625")]
public partial class GetDistanceAction : Action
{
    [SerializeReference] public BlackboardVariable<float> Value;
    [SerializeReference] public BlackboardVariable<Transform> Agent;
    [SerializeReference] public BlackboardVariable<Transform> Target;

    protected override Status OnStart()
    {
        Value.Value = Vector3.Distance(Agent.Value.position, Target.Value.position);
        return Status.Success;
    }
}
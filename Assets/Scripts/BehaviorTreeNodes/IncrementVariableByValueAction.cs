using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Increment Variable By Value", story: "Increment [Variable] by [Value]", category: "Action",
    id: "c8c64f96d814e79c81a8ebba6040be8e")]
public partial class IncrementVariableByValueAction : Action
{
    [SerializeReference] public BlackboardVariable<float> Variable;
    [SerializeReference] public BlackboardVariable<float> Value;

    protected override Status OnStart()
    {
        Variable.Value += Value.Value;
        return Status.Success;
    }
}
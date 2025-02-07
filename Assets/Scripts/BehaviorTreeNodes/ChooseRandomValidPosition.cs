using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Random = UnityEngine.Random;


namespace BehaviorTreeNodes
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "ChooseRandomValidPosition", story: "[Agent] Chooses A Random Valid [TargetPosition]",
        category: "Action")]
    public partial class ChooseRandomValidPosition : Action
    {
        [SerializeReference] public BlackboardVariable<GameObject> Agent;
        [SerializeReference] public BlackboardVariable<Vector3> TargetPosition;
        [SerializeReference] public BlackboardVariable<float> Min;
        [SerializeReference] public BlackboardVariable<float> Max;

        protected override Status OnStart()
        {
            Vector3 currentPos = Agent.Value.transform.position;

            float xRandomized = (Random.value < 0.5f ? -1f : 1f) * Random.Range(Min, Max);
            float zRandomized = (Random.value < 0.5f ? -1f : 1f) * Random.Range(Min, Max);

            Vector3 positionToAddToCurrentPos = new Vector3(xRandomized, 0f, zRandomized);

            TargetPosition.Value = currentPos + positionToAddToCurrentPos;
            return Status.Success;
        }
    }
}
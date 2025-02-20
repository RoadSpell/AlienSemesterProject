using System;
using System.Linq;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Find Target Closest To Agent That Is Not Target To Be Excluded With Tag",
    story: "Find [Target] closest to [Agent] that is not [Excluded] with tag: [Tag]", category: "Action",
    id: "5fd516e8b0606ea98c032314895aa284")]
public partial class FindClosestThatIsNotTargetWithTagAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<GameObject> Excluded;
    [SerializeReference] public BlackboardVariable<string> Tag;

    protected override Status OnStart()
    {
        if (Agent.Value == null)
        {
            LogFailure("No agent provided.");
            return Status.Failure;
        }

        Vector3 agentPosition = Agent.Value.transform.position;

        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(Tag.Value)
            .Where(t => t.gameObject != Excluded.Value).ToArray();
        float closestDistanceSq = Mathf.Infinity;
        GameObject closestGameObject = null;
        foreach (GameObject gameObject in gameObjects)
        {
            float distanceSq = Vector3.SqrMagnitude(agentPosition - gameObject.transform.position);
            if (closestGameObject == null || distanceSq < closestDistanceSq)
            {
                closestDistanceSq = distanceSq;
                closestGameObject = gameObject;
            }
        }

        Target.Value = closestGameObject;

        if (closestGameObject == Agent.Value)
        {
            Debug.Log("BRUHHHH it is targeting itself again!");
        }

        return Target.Value == null ? Status.Failure : Status.Success;
    }
}
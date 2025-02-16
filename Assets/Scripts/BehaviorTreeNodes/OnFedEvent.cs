using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/OnFed Event")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "OnFed Event", message: "[Agent] got fed", category: "Events", id: "ac7636d59309962b6cb685b07b6703b7")]
public partial class OnFedEvent : EventChannelBase
{
    public delegate void OnFedEventEventHandler(GameObject Agent);
    public event OnFedEventEventHandler Event; 

    public void SendEventMessage(GameObject Agent)
    {
        Event?.Invoke(Agent);
    }

    public override void SendEventMessage(BlackboardVariable[] messageData)
    {
        BlackboardVariable<GameObject> AgentBlackboardVariable = messageData[0] as BlackboardVariable<GameObject>;
        var Agent = AgentBlackboardVariable != null ? AgentBlackboardVariable.Value : default(GameObject);

        Event?.Invoke(Agent);
    }

    public override Delegate CreateEventHandler(BlackboardVariable[] vars, System.Action callback)
    {
        OnFedEventEventHandler del = (Agent) =>
        {
            BlackboardVariable<GameObject> var0 = vars[0] as BlackboardVariable<GameObject>;
            if(var0 != null)
                var0.Value = Agent;

            callback();
        };
        return del;
    }

    public override void RegisterListener(Delegate del)
    {
        Event += del as OnFedEventEventHandler;
    }

    public override void UnregisterListener(Delegate del)
    {
        Event -= del as OnFedEventEventHandler;
    }
}


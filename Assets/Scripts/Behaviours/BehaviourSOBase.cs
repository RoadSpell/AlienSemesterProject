using UnityEngine;

[CreateAssetMenu(fileName = "AlienMischiefSOBase", menuName = "Scriptable Objects/AlienMischiefSOBase")]
public abstract class BehaviourSOBase : ScriptableObject
{
    public abstract void Execute(Alien behavingAlien);
}

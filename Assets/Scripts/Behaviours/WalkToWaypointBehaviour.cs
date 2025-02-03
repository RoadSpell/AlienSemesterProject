using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WalkToWaypointBehaviour", menuName = "Scriptable Objects/WalkToWaypointBehaviour")]
public class WalkToWaypointBehaviour : BehaviourSOBase
{
    public List<Transform> waypoints = new List<Transform>();


    public override void Execute(Alien behavingAlien)
    {
        if (waypoints.Count == 0)
            return;

        if (waypoints.Count == waypoints.Count -1)
        {

        }
        int waypointIndex = 0;
        Transform targetWaypoint = waypoints[waypointIndex];
        Wanderer alienWanderer = behavingAlien.wandererComponent;
        Rigidbody alienRb = alienWanderer.rb;
        //Debug.Log($"Wanderer is null: {alienWanderer == null}");
        Debug.Log($"targetWaypoint is null: {targetWaypoint == null}");


        if (Vector3.Distance(behavingAlien.transform.parent.position, targetWaypoint.position) <=
            alienWanderer.wanderPointErrorMargin)
        {
            waypointIndex++;
        }

        Vector3 dir = (targetWaypoint.position - behavingAlien.transform.parent.position).normalized;
        alienRb.angularVelocity = alienWanderer.speed * dir;
    }
}
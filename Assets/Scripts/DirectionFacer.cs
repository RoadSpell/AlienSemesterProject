using UnityEngine;

public class DirectionFacer : MonoBehaviour
{
    [SerializeField] private Vector3 targetDir = Vector3.zero;

    void Update()
    {
        transform.forward = targetDir;
    }
}
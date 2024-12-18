using UnityEngine;

public class TargetFacer : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void Start()
    {
        if (target == null)
            if (Camera.main != null)
                target = Camera.main.transform;
    }

    void Update()
    {
        transform.LookAt(target);
    }
}
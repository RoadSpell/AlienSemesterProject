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
        //Vector3 lookedTransform = transform.rotation.eulerAngles;
        transform.Rotate(new Vector3(0f, 180f, 0f));
    }
}
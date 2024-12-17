using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 _moveDir = Vector3.zero;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private CharacterController controller;
    [SerializeField] private float playerSpeed = 3f;

    void Update()
    {
        /* HADES STYLE CAMERA MOVEMENT
         if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            transform.forward = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            _moveDir = transform.forward;
        }
        else
        {
            _moveDir = Vector3.zero;
        }*/

        // RESIDENT EVIL STYLE CAMERA DEPENDENT MOVEMENT
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;

            forward.y = 0f;
            right.y = 0f;

            // Normalize the vectors here to make speed independent of the distance to the camera
            forward.Normalize();
            right.Normalize();

            _moveDir = forward * Input.GetAxis("Vertical") + right * Input.GetAxis("Horizontal");
            // Face the direction of movement
            transform.forward = _moveDir;
        }
        else
        {
            _moveDir = Vector3.zero;
        }
    }

    private void FixedUpdate()
    {
        controller.Move(_moveDir * playerSpeed * Time.deltaTime);
    }
}

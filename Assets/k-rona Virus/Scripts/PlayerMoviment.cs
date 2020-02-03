using UnityEngine;

[RequireComponent(typeof(CharacterController))]
class PlayerMoviment : MonoBehaviour {

    private bool isControlEnabled = true;
    private CharacterController characterController;
    private float gravity = 20.0f;
    private Vector3 _moveDirection = Vector3.zero;
    public float speed = 5.0f;
    public float rotationSpeed = 240.0f;


    void Start() {
        characterController = GetComponent<CharacterController>();
    }

    public void SetControlActivity(bool state) {
        isControlEnabled = state;
    }

    void Update() {
        if (isControlEnabled) {

            // Get Input for axis
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            // Calculate the forward vector
            Vector3 camForward_Dir = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 move = v * camForward_Dir + h * Camera.main.transform.right;

            if (move.magnitude > 1f) move.Normalize();

            // Calculate the rotation for the player
            move = transform.InverseTransformDirection(move);

            // Get Euler angles
            float turnAmount = Mathf.Atan2(move.x, move.z);

            transform.Rotate(0, turnAmount * rotationSpeed * Time.deltaTime, 0);

            if (characterController.isGrounded) {
                _moveDirection = transform.forward * move.magnitude;

                _moveDirection *= speed;
            }

            _moveDirection.y -= gravity * Time.deltaTime;

            characterController.Move(_moveDirection * Time.deltaTime);
        }
    }

    /*CharacterController characterController;

    public float speed = 12f;
    public float gravity = -9.81f;
    Vector3 velocity;


    void Start() {
        characterController = GetComponent<CharacterController>();
    }

    void Update() {

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        characterController.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);

    }
} */
}
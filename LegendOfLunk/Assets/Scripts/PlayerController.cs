using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Public variables for tweaking in the Inspector
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;

    // Components (no need to make these public)
    private Animator animator;
    private Rigidbody rb;

    private void Awake()
    {
        // Get references to components once, in Awake (more efficient than Start)
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Input & Movement
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized;

        if (moveDirection.magnitude > 0.1f) 
        {
            // Smoother movement with Rigidbody's MovePosition
            rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.deltaTime);

            // Smooth rotation towards movement direction
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Animation
        animator.SetFloat("Speed", moveDirection.magnitude);  
    }
}
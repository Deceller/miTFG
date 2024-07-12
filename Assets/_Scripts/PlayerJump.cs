using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpForce = 5f;  
    public LayerMask groundLayer; 
    public Transform groundCheck; 
    public float groundCheckRadius = 0.2f;  

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (groundCheck == null)
        {
            Debug.LogError("groundCheck no está asignado en el script PlayerJump.");
        }
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
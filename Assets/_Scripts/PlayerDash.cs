using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public float dashForce = 10f;  // Fuerza del dash
    public float dashDuration = 0.2f;  // Duración del dash
    public float dashCooldown = 1f;  // Tiempo de espera entre dashes

    private Rigidbody rb;
    private bool isDashing = false;
    private float dashCooldownTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody no encontrado en el objeto " + gameObject.name);
        }
    }

    void Update()
    {
        dashCooldownTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && dashCooldownTimer <= 0f)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        Vector3 dashDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

        if (dashDirection.magnitude > 0)
        {
            rb.AddForce(dashDirection * dashForce, ForceMode.Impulse);
        }

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;
        dashCooldownTimer = dashCooldown;
    }
}
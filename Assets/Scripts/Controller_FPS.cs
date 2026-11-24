using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class QuickFPS : MonoBehaviour
{
    public Transform playerCamera;
    public Transform firePoint;
    public float moveSpeed = 6f;
    public float lookSensitivity = 0.15f;
    public float shootForce = 35f;

    private CharacterController controller;
    private float xRotation = 0f;
    private float verticalVelocity = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        var kb = Keyboard.current;
        var mouse = Mouse.current;
        if (kb == null || mouse == null) return;

        // Mouse Look
        Vector2 look = mouse.delta.ReadValue() * lookSensitivity;
        xRotation = Mathf.Clamp(xRotation - look.y, -85f, 85f);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * look.x);

        // WASD Movement + Jump
        float moveX = (kb.dKey.isPressed ? 1 : 0) - (kb.aKey.isPressed ? 1 : 0);
        float moveZ = (kb.wKey.isPressed ? 1 : 0) - (kb.sKey.isPressed ? 1 : 0);
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        if (controller.isGrounded && verticalVelocity < 0) verticalVelocity = -2f;
        if (kb.spaceKey.wasPressedThisFrame && controller.isGrounded) verticalVelocity = 7f;
        verticalVelocity += Physics.gravity.y * 2f * Time.deltaTime;

        controller.Move((move.normalized * moveSpeed + Vector3.up * verticalVelocity) * Time.deltaTime);

        // Left-Click to Shoot
        if (mouse.leftButton.wasPressedThisFrame && firePoint != null)
        {
            GameObject bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            bullet.transform.position = firePoint.position;
            bullet.transform.localScale = Vector3.one * 0.2f;
            Rigidbody rb = bullet.AddComponent<Rigidbody>();
            rb.useGravity = false;
            rb.AddForce(firePoint.forward * shootForce, ForceMode.Impulse);
            Destroy(bullet, 3f);
        }
    }
}

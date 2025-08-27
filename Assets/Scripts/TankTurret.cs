using UnityEngine;
using UnityEngine.InputSystem;

public class TankTurret : MonoBehaviour
{
    public Transform turretTransform;
    public float rotationSpeed = 100f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 20f;

    private Vector2 aimInput;

    public void OnAim(InputAction.CallbackContext context)
    {
        aimInput = context.ReadValue<Vector2>();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Shoot();
        }
    }

    public void Update()
    {
        if (aimInput.sqrMagnitude > 0.1f)
        {
            float rotationAmount = aimInput.x * rotationSpeed * Time.deltaTime;

            turretTransform.Rotate(0, rotationAmount, 0, Space.World);
        }
    }

    public void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(firePoint.forward * bulletSpeed , ForceMode.Impulse);
            }
            Debug.Log("Shoot");
        }
    }
}

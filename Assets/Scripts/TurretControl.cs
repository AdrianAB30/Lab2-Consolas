using UnityEngine;
using UnityEngine.InputSystem;

public class TurretControl : MonoBehaviour
{
    public Transform turretTransform;
    public float rotationSpeed = 100f;

    public string bulletTag = "Bullet"; 
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

    void Update()
    {
        if (aimInput.sqrMagnitude > 0.1f)
        {
            float rotationAmount = aimInput.x * rotationSpeed * Time.deltaTime;
            turretTransform.Rotate(0, rotationAmount, 0, Space.World);
        }
    }

    void Shoot()
    {

        if (firePoint == null) return;

        GameObject bullet = PoolManager.Instance.SpawnFromPool(bulletTag, firePoint.position, firePoint.rotation);
        if (bullet == null)
        {
            return;
        }

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;      
            rb.angularVelocity = Vector3.zero;
            rb.AddForce(firePoint.forward * bulletSpeed, ForceMode.Impulse);
        }
    }
}

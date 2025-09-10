using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class TurretControl : MonoBehaviour
{
    [Header("Referencias")]
    public Transform turretBase; 
    public Transform cannon; 
    public LineRenderer trajectoryLine;
    public ParticleSystem shootPartycle;

    [Header("Configuración")]
    public float horizontalRotationSpeed = 100f;
    public float verticalRotationSpeed = 50f;
    public float minVerticalAngle = -10f;
    public float maxVerticalAngle = 45f;

    [Header("Disparo")]
    public string bulletTag = "Bullet";
    public Transform firePoint;
    public float bulletSpeed = 20f;
    public float gravity = 9.81f;

    [Header("Trayectoria")]
    public int trajectoryPoints = 20;
    public float trajectoryTimeStep = 0.1f;

    private Vector2 aimInput;
    private float currentVerticalAngle = 0f;

    public void OnAim(InputAction.CallbackContext context)
    {
        aimInput = context.ReadValue<Vector2>();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Shoot();
            print("Disparo realizado");
        }
    }
    void Update()
    {
        HandleRotation();
        UpdateTrajectory();
    }

    private void HandleRotation()
    {
        if (Mathf.Abs(aimInput.x) > 0.1f)
        {
            float horizontalRotation = aimInput.x * horizontalRotationSpeed * Time.deltaTime;
            turretBase.Rotate(0, horizontalRotation, 0, Space.World);
        }

        if (Mathf.Abs(aimInput.y) > 0.1f)
        {
            float verticalRotation = -aimInput.y * verticalRotationSpeed * Time.deltaTime;
            currentVerticalAngle += verticalRotation;
            currentVerticalAngle = Mathf.Clamp(currentVerticalAngle, minVerticalAngle, maxVerticalAngle);

            cannon.localRotation = Quaternion.Euler(currentVerticalAngle, 0f, 0f);
        }
    }

    private void UpdateTrajectory()
    {
        if (trajectoryLine == null) return;

        Vector3[] points = CalculateTrajectory();
        trajectoryLine.positionCount = points.Length;
        trajectoryLine.SetPositions(points);
    }

    private Vector3[] CalculateTrajectory()
    {
        Vector3[] points = new Vector3[trajectoryPoints];
        Vector3 startVelocity = firePoint.forward * bulletSpeed;
        Vector3 currentPosition = firePoint.position;

        for (int i = 0; i < trajectoryPoints; i++)
        {
            float time = i * trajectoryTimeStep;

            Vector3 displacement = startVelocity * time +
                                 0.5f * Physics.gravity * time * time;

            points[i] = currentPosition + displacement;

            if (i > 0 && Physics.Linecast(points[i - 1], points[i], out RaycastHit hit))
            {
                points[i] = hit.point;
                trajectoryLine.positionCount = i + 1;
                break;
            }
        }

        return points;
    }

    void Shoot()
    {
        if (firePoint == null) return;

        GameObject bullet = PoolManager.Instance.SpawnFromPool(bulletTag, cannon.position, firePoint.rotation);
        if (bullet == null)
        {
            return;
        }

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            Vector3 initialVelocity = firePoint.forward * bulletSpeed;
            rb.AddForce(initialVelocity, ForceMode.Impulse);
            shootPartycle.Play();
            GameFeelManager.Instance.OnShootFeedback();
            GameFeelManager.Instance.Rumble();

            cannon.DOLocalMoveY(-0.006f, 0.1f).SetLoops(2, LoopType.Yoyo);

        }
    }

    public void SetTrajectoryVisible(bool visible)
    {
        if (trajectoryLine != null)
        {
            trajectoryLine.enabled = visible;
        }
    }
}
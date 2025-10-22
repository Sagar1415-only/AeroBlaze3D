using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [Header("References")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("Shooting Settings")]
    public float fireRate = 2.5f;  // seconds between shots
    public float bulletSpeed = 20f;
    public float aimError = 3f;    // adds some inaccuracy

    private Transform player;
    private float nextFireTime = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null)
            Debug.LogError("❌ Player not found! Add tag 'Player' to your player jet.");
    }

    void Update()
    {
        if (player == null || bulletPrefab == null || firePoint == null)
            return;

        // shoot periodically
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            ShootAtPlayer();
        }
    }

    void ShootAtPlayer()
    {
        // calculate direction
        Vector3 dir = (player.position - firePoint.position).normalized;
        dir = Quaternion.Euler(Random.Range(-aimError, aimError), Random.Range(-aimError, aimError), 0) * dir;

        // spawn bullet
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(dir));

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
            rb.velocity = dir * bulletSpeed;

        Destroy(bullet, 5f);
    }
}

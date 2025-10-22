using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;    // empty GameObject at front of jet
    public float bulletSpeed = 30f;
    public float fireRate = 0.3f;

    private float nextFireTime = 0f;

    void Update()
    {
        if (Time.time >= nextFireTime && (Input.GetKey(KeyCode.Space)))
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null || firePoint == null) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * bulletSpeed;
        Destroy(bullet, 3f); // auto destroy after 3 seconds
    }
}

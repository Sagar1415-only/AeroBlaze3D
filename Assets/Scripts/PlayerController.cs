using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 15f;
    public float tiltAmount = 30f;
    public float boundaryX = 15f;
    public float boundaryZ = 20f;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 25f;
    public float fireRate = 0.25f;
    private float nextFireTime = 0f;

    void Update()
    {
        HandleMovement();
        HandleShooting();
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(moveX, 0, moveZ) * moveSpeed * Time.deltaTime;
        transform.Translate(move, Space.World);

        float tilt = moveX * -tiltAmount;
        transform.rotation = Quaternion.Euler(0, 0, tilt);

        float clampedX = Mathf.Clamp(transform.position.x, -boundaryX, boundaryX);
        float clampedZ = Mathf.Clamp(transform.position.z, -boundaryZ, boundaryZ);
        transform.position = new Vector3(clampedX, transform.position.y, clampedZ);
    }

    void HandleShooting()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
                rb.velocity = firePoint.forward * bulletSpeed;
            Destroy(bullet, 5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            if (GameManager.instance != null)
                GameManager.instance.TakeDamage(1);
            Destroy(other.gameObject);
        }
    }
}

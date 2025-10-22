using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 30f;          // Bullet movement speed
    public float lifeTime = 5f;        // Auto-destroy after 5 seconds
    private Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;

        // Destroy after lifetime
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        if (player == null) return;

        // Move toward player dynamically
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // Optional: make bullet visually face the player
        transform.LookAt(player);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Player takes damage
            if (GameManager.instance != null)
                GameManager.instance.TakeDamage(1);

            Destroy(gameObject);
        }
        else if (other.CompareTag("Bullet"))
        {
            Destroy(gameObject); // destroy bullet if hit by player bullet
        }
    }
}

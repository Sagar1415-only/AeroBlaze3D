using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public int health = 3;
    private Transform player;

    [Header("Orbit Settings")]
    public float orbitRadius = 10f;
    public float orbitSpeed = 20f; // degrees per second
    private Vector3 orbitAxis;
    private float randomYOffset;

    
    
public System.Action OnDestroyEvent;

void OnDestroy()
{
    OnDestroyEvent?.Invoke();
}
    
    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        if (player == null)
        {
            Debug.LogWarning($"{name}: Player not found!");
            return;
        }

        // Random axis tilt and Y offset for variation
        orbitAxis = Vector3.up;
        randomYOffset = Random.Range(-2f, 2f);
    }

    void Update()
    {
        if (player == null) return;

        // Orbit around player
        float speed = Random.Range(15f, 25f);
        transform.RotateAround(player.position, Vector3.up, speed * Time.deltaTime);
        // Face the player
        transform.LookAt(player);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            health--;
            Destroy(other.gameObject);

            if (health <= 0)
            {
                if (GameManager.instance != null)
                    GameManager.instance.AddScore(10);
                Destroy(gameObject);
            }
        }
    }
}

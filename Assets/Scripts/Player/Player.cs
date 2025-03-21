using UnityEngine;

public class Player : MonoBehaviour
{

    public GameObject bulletPrefab;
    private Vector3 enemyPosition;
    private float nextFireTime = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void fire()
    {
        var enemyDirection = (enemyPosition - transform.position).normalized;
        transform.up = enemyDirection;
        var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.transform.up = enemyDirection;

    }

    void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Collision detected");
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Quaternion.LookRotation(direction);
            if (Time.time >= nextFireTime)
            {
                enemyPosition = collision.gameObject.transform.position;
                nextFireTime = Time.time + 5f;
                fire();
            }
            // Destroy(gameObject);
        }

    }
}

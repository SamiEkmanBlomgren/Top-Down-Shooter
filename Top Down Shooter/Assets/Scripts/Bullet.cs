using UnityEngine;
public class Bullet : MonoBehaviour
{
    float bigEnemyHealth = 3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemies"))
        {
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
        if (collision.gameObject.CompareTag("BigEnemies"))
        {
            bigEnemyHealth -= 1;
            if (bigEnemyHealth <= 0)
            {
                Destroy(collision.gameObject);
            }
        }
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

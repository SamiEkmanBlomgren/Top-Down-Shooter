using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 moveInput;
    Vector2 screenBoundary;
    [SerializeField] float moveSpeed = 3;
    [SerializeField] float bulletSpeed = 7f;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject gun;
    [SerializeField] float rotationSpeed = 700f;
    int playerHealth = 3;
    float targetAngle;
    SpriteRenderer playerSprite;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        screenBoundary = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnAttack()
    {
        Rigidbody2D playerBullet = Instantiate(bullet, gun.transform.position, transform.rotation).GetComponent<Rigidbody2D>();
        playerBullet.AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = moveInput * moveSpeed;
        if (moveInput != Vector2.zero)
        {
            targetAngle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg;
        }
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -screenBoundary.x, screenBoundary.x)
                                        , Mathf.Clamp(transform.position.y, -screenBoundary.y, screenBoundary.y));
    }
    private void FixedUpdate()
    {
        float rotation = Mathf.MoveTowardsAngle(rb.rotation, targetAngle - 90, rotationSpeed * Time.fixedDeltaTime);
        rb.MoveRotation(rotation);
    }
    IEnumerator IFrame()
    {
        playerSprite.color = Color.darkRed;
        yield return new WaitForSeconds(2);
        playerSprite.color = Color.red;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemies"))
        {
            StartCoroutine("IFrame");
            playerHealth -= 1;
        }
        if (playerHealth == 0)
        {
            Destroy(gameObject);
        }
    }
}
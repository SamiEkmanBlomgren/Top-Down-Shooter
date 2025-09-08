using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 moveInput;
    [SerializeField] float moveSpeed = 3;
    [SerializeField] float bulletSpeed = 7f;
    [SerializeField] GameObject bullet; 



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnAttack()
    {
        Rigidbody2D playerBullet = Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
        playerBullet.AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }
}

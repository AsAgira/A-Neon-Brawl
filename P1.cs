using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1 : MonoBehaviour {

    #region Variables

    [Range(.1f, 25f)]
    public float speed;

    [Range(.1f, 25f)]
    public float jumpForce;

    public float checkRadius;

    private float jumpTimeCounter;

    public float jumpTime;

    private float timeBtwShoot;

    public float startTimeBtwShoot;

    public int health;

    public static bool isP1Alive = true;

    private bool isGrounded;

    private bool isJumping;

    [HideInInspector]
    public bool facingRight;

    public Transform feetPos;

    public LayerMask whatIsGround;

    public Rigidbody2D rb;

    private GameObject bullet;

    public Transform firePos;

    [HideInInspector]
    public BulletTypes selectedBulletType;

    public GameObject bulletPrefab;
   
    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bulletPrefab = selectedBulletType.bulletPrefab;
    }

    void Update()
    {
        #region Updaters

        if (health <= 0 || isP1Alive == false)
            Die();

        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        
        #endregion

        #region Movement

        if (Input.GetKey(KeyCode.D))
            HorizontalMove(true, false);

        if (Input.GetKey(KeyCode.A))
            HorizontalMove(false, true);

        if (Input.GetKeyDown(KeyCode.E))
        {
            Shoot();
        }
        if (timeBtwShoot > 0)
        {
            timeBtwShoot -= Time.deltaTime;
        }
        Jump();
        
        #endregion

    }

    void HorizontalMove(bool right, bool left)
    {
        if (right && left == false)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            Flip(true, false);
            facingRight = true;
        }
        if (left && right == false)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            Flip(false, true);
            facingRight = false;
        }
    }
    void Die()
    {
        isP1Alive = false;
        Destroy(gameObject, .1f);
    }

    void Shoot()
    {
        if (timeBtwShoot < 0)
        {
            selectedBulletType.ShootBullet(selectedBulletType, firePos, true, false);
            timeBtwShoot = startTimeBtwShoot;
        }
    }
    void Jump()
    {
        if (isGrounded == true && Input.GetKeyDown(KeyCode.W))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }
        if (Input.GetKey(KeyCode.W) && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
                isJumping = false;

        }
        if (Input.GetKeyUp(KeyCode.W))
            isJumping = false;
    }
    void Flip(bool right, bool left)
    {
        if (right && left == false)
        {
            transform.Rotate(0f, 0f, 0f);
            facingRight = true;
        }
        if (left & right == false)
        {
            transform.Rotate(0f, 180f, 0f);
            facingRight = false;
        }
    }
    public void InstantiateBullet()
    {
        Instantiate(bulletPrefab, firePos);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}

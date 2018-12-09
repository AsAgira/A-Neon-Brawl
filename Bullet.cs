using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    #region Variables

    private Rigidbody2D rb;

    public float timeBeforeDestruction;

    private SpriteRenderer sr;

    private bool active = true;

    private bool canMoveHorizontal;

    private bool movingRight;

    private bool player1;

    private P1 pl1;

    private bool player2;

    private GameObject bullet;

    private float Speed;

    private bool homing;

    private int power;

    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        pl1 = FindObjectOfType<P1>();
        if (player1)
        {
            sr.sprite = pl1.selectedBulletType.sprite;
            power = pl1.selectedBulletType.power;
            Speed = pl1.selectedBulletType.speed;
            homing = pl1.selectedBulletType.isHoming;
        }
        Destroy(gameObject, timeBeforeDestruction);  
    }

    void Update()
    {
        sr.color = pl1.selectedBulletType.color;

        if (active)
        {
            if (homing)
            {


                
            }
            else if (canMoveHorizontal)
            {
                if (movingRight)
                {
                    transform.Rotate(0f, 0f, 0f);
                    rb.velocity = Vector2.right * Speed * Time.deltaTime;
                }
                else
                {
                    transform.Rotate(0f, 180f, 0);
                    rb.velocity = Vector2.right * Speed * Time.deltaTime;
                }
            }
        }
    }

    public void RegShoot(bool p1, bool p2, bool goingRight)
    {
        active = true;
        player1 = p1;
        player2 = p2;
        movingRight = goingRight;
        canMoveHorizontal = true;
    }
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log(hitInfo);
        active = false;
        if (hitInfo.CompareTag("p1") && player1 == false)
        {
            pl1.TakeDamage(power);
        }
        if (hitInfo.CompareTag("p2") && player2 == false)
        {
            
        }
        Destroy(gameObject);
    }
}

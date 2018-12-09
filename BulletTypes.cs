
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Bullet/bulletType", order = 1)]
public class BulletTypes : ScriptableObject {

    #region Variables

    [Header("Data")]
    public string bulletName;

    private Bullet bullet;

    public GameObject bulletPrefab;

    public Sprite sprite;

    public Color color;

    private P1 player1;

    [Header("Stats")]
    [Tooltip("The Amount of damage this does...")]
    public int power;

    [Tooltip("The speed in which the bullet travels")]
    public float speed;

    [Header("BulletAttributes")]
    [Tooltip("The bullet chases after another player")]
    public bool isHoming;

    [Tooltip("The Player has to hold down the shoot button and wait a set amount of time before firing")]
    public bool isCharger;

    private float chargeTime;

    [Header("For Chargers")] [Tooltip("The time the player has to hold down to shoot")]
    public float endChargeTime;




    #endregion

    void Awake()
    {
        bullet = FindObjectOfType<Bullet>();
        player1 = FindObjectOfType<P1>();
    }

    public void ShootBullet(BulletTypes bulletType, Transform firePos, bool p1, bool p2)
    {

        if (bulletType.isHoming)
        {
            
        }
        else if (bulletType.isCharger)
        {
            
        }
        else
        {
            if (p1)
            {
                player1.InstantiateBullet();
                bullet.RegShoot(p1, p2, player1.facingRight);
            }
        }
    }
    IEnumerator Charge()
    {
        chargeTime = 0f;

        while (chargeTime < endChargeTime)
        {
            yield return new WaitForSeconds(0.3f);

            chargeTime++;
        }
          
    }

}

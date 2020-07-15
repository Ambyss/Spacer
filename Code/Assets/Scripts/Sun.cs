using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    float HP;
    GameController GC;
    public GameObject puff;

    private void Start()
    {
        HP = 5000;
        GC = GameObject.Find("GameControl").GetComponent<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet") Hit(GC.bulletDamage);
        else if (collision.tag == "Laser") Hit(GC.laserDamage);
    }

    void Hit(float damage)
    {
        HP -= damage;
        GC.SunHP(HP);
        if (HP <= 0) Death();
    }

    void Death()
    {
        Instantiate(puff, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    
}

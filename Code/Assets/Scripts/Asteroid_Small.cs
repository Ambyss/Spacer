using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid_Small : MonoBehaviour
{
    float HP;
    Rigidbody2D rb;
    float bulletDamage;
    float laserDamage;
    Vector2 dirVel;
    Transform sun;
    public GameObject puff;
    GameController GC;

    private void Awake()
    {
        GC = GameObject.Find("GameControl").GetComponent<GameController>();
        sun = GameObject.Find("Sun").GetComponent<Transform>();
        dirVel = (sun.position - transform.position).normalized;
        dirVel = new Vector2(dirVel.x + Random.Range(-1, 1), dirVel.y + Random.Range(-1, 1));
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = dirVel * UnityEngine.Random.Range(0, 10);
    }

    void Start()
    {
        laserDamage = 5f;
        bulletDamage = 1f;
        HP = 5;
    }

    void Hit(string tag)
    {
        if (tag == "Bullet") HP -= GC.bulletDamage;
        else if (tag == "Laser")
        {
            HP -= GC.laserDamage;
        }
        if (HP <= 0) Death();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Laser")
        {
            Hit(collision.gameObject.tag);
        }
    }

    void Death()
    {
        GameObject.Find("GameControl").GetComponent<GameController>().AddScore(5);
        Instantiate(puff, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

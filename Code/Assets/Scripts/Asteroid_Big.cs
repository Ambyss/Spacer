using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Asteroid_Big : MonoBehaviour
{
    float HP;
    Rigidbody2D rb;
    public GameObject[] sAsteroid;
    Vector3 position;
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
        dirVel = new Vector2(dirVel.x + Random.Range(-3, 3), dirVel.y + Random.Range(-3, 3));
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = dirVel * UnityEngine.Random.Range(0, 10);
    }

    void Start()
    {
        bulletDamage = GameObject.Find("GameControl").GetComponent<GameController>().bulletDamage;
        laserDamage = GameObject.Find("GameControl").GetComponent<GameController>().laserDamage;
        HP = 15;
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
        GameObject.Find("GameControl").GetComponent<GameController>().AddScore(15);
        position = transform.position;
        Instantiate(sAsteroid[0], position, Quaternion.identity);
        Instantiate(sAsteroid[1], position, Quaternion.identity);
        Instantiate(sAsteroid[2], position, Quaternion.identity);
        Instantiate(puff, transform.position, Quaternion.identity);
        Destroy(gameObject);
        
    }
}

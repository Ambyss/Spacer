using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    Player shuttle;
    float speed;

    private void Start()
    {
        speed = 35;
        rb = GetComponent<Rigidbody2D>();
        shuttle = GameObject.Find("Shuttle").GetComponent<Player>();
        //transform.position = shuttle.transform.position;
        rb.velocity = shuttle.speed * speed;
        StartCoroutine("Shot");
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Gravity" && collision.gameObject.tag != "Borders") Death();
    }

    void Death()
    {
        Destroy(gameObject);
        // TODO: Puff
    }

    IEnumerator Shot()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}

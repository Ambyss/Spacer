using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Borders : MonoBehaviour
{
    Player player;

    private void Start()
    {
        player = GameObject.Find("Shuttle").GetComponent<Player>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") StopCoroutine("Damage");
    }

    IEnumerator Damage()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);
            player.Hit(2);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "AstBelt") Destroy(collision.gameObject);
        else if (collision.tag == "Player")
        {
            StartCoroutine("Damage");
        } 
    }

    
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunKiller : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") GameObject.Find("Shuttle").GetComponent<Player>().Hit(50);
        else Destroy(collision.gameObject);
    }
}

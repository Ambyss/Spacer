using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    Rigidbody2D rbObj;
    Vector2 pos;
    float dist;
    Vector2 vec;
    float force;

    private void Start()
    {
        pos = transform.position;
        force = 0.04f;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        { 
            rbObj = collision.gameObject.GetComponent<Rigidbody2D>();
            vec = (transform.position - rbObj.transform.position).normalized;
            dist = (rbObj.transform.position - transform.position).magnitude;
            rbObj.velocity += vec * force / (dist * 0.08f);
        }
    }
}

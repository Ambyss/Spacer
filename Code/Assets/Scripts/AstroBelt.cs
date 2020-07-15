using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class AstroBelt : MonoBehaviour
{
    Vector3 sun;
    Rigidbody2D rb;
    Vector3 dirSun;
    Vector2 dirVel;
    
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        sun = GameObject.Find("Sun").GetComponent<Transform>().position;
        dirSun = (sun - transform.position).normalized;
        dirVel = new Vector2(-dirSun.y / dirSun.x, 1).normalized;
        if (transform.position.x < sun.x) dirVel = -dirVel;
        
        rb.velocity = dirVel * 4.39f;
       
    }
}

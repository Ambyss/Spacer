using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AstGravity : MonoBehaviour
{

    public GameObject[] asteroids;
    HashSet<Rigidbody2D> belt = new HashSet<Rigidbody2D>();

    // Update is called once per frame
    void FixedUpdate()
    {
        belt.Clear();
        asteroids = GameObject.FindGameObjectsWithTag("AstBelt");
        foreach (GameObject i in asteroids)
        {
            belt.Add(i.GetComponent<Rigidbody2D>());
        }
        foreach (Rigidbody2D body in belt)
        {
            Vector3 dir = new Vector3(transform.position.x - body.position.x, transform.position.y - body.position.y, 0);
            float distance = dir.magnitude;
            dir = dir.normalized;
            body.AddForce(dir/(distance*0.5f) * 20);
        }
    }
}

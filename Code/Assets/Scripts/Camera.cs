using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Camera : MonoBehaviour
{
    public GameObject target;
    Rigidbody2D pos;
    Vector2 vel;
    float k;
    Transform sun;
    float s;

    void Start()
    {
        pos = target.GetComponent<Rigidbody2D>();
        sun = GameObject.Find("Sun").GetComponent<Transform>();
        k = 0.3f;
        s = 0.04f;
    }

    public void Target(bool anim)
    {
        if (anim == false)
            gameObject.transform.position = new Vector3(target.transform.position.x + pos.velocity.x * k, target.transform.position.y + pos.velocity.y * k, -10);
        else
            gameObject.transform.position = new Vector3(target.transform.position.x + pos.velocity.x * k + Random.Range(-s, s), target.transform.position.y + pos.velocity.y * k + Random.Range(-s, s), -10);
    }

    private void FixedUpdate()
    {
        Target(false);
    }
}

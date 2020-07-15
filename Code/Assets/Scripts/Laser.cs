using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    SpriteRenderer sprite;
    float a;
    BoxCollider2D collider;

    private void Start()
    {
        StartCoroutine("Death");
        sprite = gameObject.GetComponent<SpriteRenderer>();
        a = 1f;
        collider = gameObject.GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        if (a > 0) a -= 0.1f;
        sprite.color = new Color(1, 1, 1, a);
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}

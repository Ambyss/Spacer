using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlanets : MonoBehaviour
{
    public float angle;
    private void FixedUpdate()
    {
        transform.RotateAround(transform.position, Vector3.forward, 0.01f * angle);
    }
}

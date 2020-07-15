using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject enemy;
    public GameObject asteroidBig;
    public GameObject asteroidSmall;

    void Start()
    {

        StartCoroutine("SpawnS");
        StartCoroutine("SpawnBig");
    }



    IEnumerator SpawnBig()
    {
        Instantiate(asteroidBig, transform.position, transform.rotation);
        yield return new WaitForSeconds(10);
        StartCoroutine("SpawnBig");
    }

    IEnumerator SpawnS()
    {
        Instantiate(asteroidSmall, transform.position, transform.rotation);
        yield return new WaitForSeconds(4);
        StartCoroutine("SpawnS");
    }
}

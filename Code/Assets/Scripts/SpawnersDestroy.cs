using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnersDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Death");
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }
}

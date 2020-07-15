using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    float capacity;
    int interval;
    int reloadTime;
    bool fire;
    public GameObject bullet;
    public GameObject laser;
    GameController GC;
    int maxCP;
    float CapRegen;

    private void Start()
    {
        CapRegen = 1;
        capacity = 40;
        fire = true;
        interval = 30;
        reloadTime = 10;
        GC = GameObject.Find("GameControl").GetComponent<GameController>();
        maxCP = 30;
        GC.GetCP(maxCP);
    }

    public void AddCap(int points)
    {
        maxCP += points;
        CapRegen += 0.2f;
        
    }

    IEnumerator GunReload()
    {
        yield return new WaitForSeconds(0.15f);
        fire = true;
    }

    IEnumerator LaserReload()
    {
        Instantiate(laser, transform.position, transform.rotation);
        yield return new WaitForSeconds(0.5f);
        fire = true;
    }

    private void FixedUpdate()
    {
        interval += 1; 
        if (capacity < maxCP && interval % reloadTime == 0)
        {
            capacity += CapRegen;
            GC.Capacity(capacity);
        }
        // BULLET
        if (Input.GetKey(KeyCode.Space))
        {
            if (fire && capacity >= 2)
            {
                Debug.Log(transform.position);
                Instantiate(bullet, transform.position, Quaternion.identity);
                GC.Capacity(capacity);
                fire = false;
                capacity -= 2;
                StartCoroutine("GunReload");
                
            }
        }
        // LASER
        if (Input.GetKey(KeyCode.Q))
        {
            if (fire && capacity >= 15)
            {
                GC.Capacity(capacity);
                fire = false;
                capacity -= 15;
                StartCoroutine("LaserReload");
            }
        }
    }
    
}

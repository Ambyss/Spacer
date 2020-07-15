using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngineInternal;

public class Player : MonoBehaviour
{
    float h;
    Quaternion rot;
    float rotSpeed = 7;
    public Vector2 speed;
    float acc;
    private Rigidbody2D rb;
    float sp;
    public GameObject[] engine;
    Camera cam;
    Transform BG;
    Vector2 obj;
    float bum;
    float hp;
    GameController GC;
    float energy;
    float MaxHP;
    float RegenSpeed;
    int MaxEN;
    float ENRegen;
    public GameObject puff;
     
    void Start()
    {
        MaxEN = 100;
        ENRegen = 0.3f;
        GC = GameObject.Find("GameControl").GetComponent<GameController>();
        BG = GameObject.Find("BG").GetComponent<Transform>();
        rot = transform.rotation;
        rotSpeed = 5;
        speed = Vector2.zero;
        rb = GetComponent<Rigidbody2D>();
        acc = 0.07f;
        rb.velocity = new Vector2(0.0001f,0);
        hp = 100;
        MaxHP = 100;
        RegenSpeed = 0.01f;
        GameObject.Find("GameControl").GetComponent<GameController>().GetHP(hp);
        cam = GameObject.Find("Camera").GetComponent<Camera>();
        energy = 100f;
        engine[0].SetActive(false);
        engine[1].SetActive(false);
    }

    public void AddHP(int points)
    {
        MaxHP += points;
        RegenSpeed += 0.01f;
    }

    public void AddEN(int points)
    {
        MaxEN += points;
        ENRegen += 0.002f;
        acc += 0.005f;
    }

    public void Hit(float damage)
    {
        hp -= damage;
        if (hp <= 0) Death();
        GC.Health(hp);
    }

    void Death()
    {
        Debug.Log("DEATH!!!");
        Destroy(gameObject);
        GC.GameEnd();
        Instantiate(puff, transform.position, quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Hit(bum);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        obj = collision.GetComponent<Rigidbody2D>().velocity;
        bum = new Vector2(rb.velocity.x - obj.x, rb.velocity.y - obj.y).magnitude;
    }

    void FixedUpdate()
    {
        if (hp < MaxHP)
        {
            hp += RegenSpeed;
            GC.Health(hp);
        }
        if (energy < MaxEN)
        {
            energy += ENRegen;
            GC.Energy(energy);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            Hit(30);
        }

        BG.position = transform.position * 0.7f;
        engine[2].SetActive(false);
        engine[3].SetActive(false);
        // ROTATION
        h = Input.GetAxis("Horizontal");
        rot = Quaternion.AngleAxis(-h * rotSpeed, Vector3.forward);
        transform.rotation *= rot;

        // Moving
        speed.x = -math.sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad);
        speed.y = math.cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad);
        sp = 3 * Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y);
        //rb.velocity -= rb.velocity * drag;
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity += speed * acc;
            engine[0].SetActive(true);
            engine[1].SetActive(true);
            if (Input.GetKey(KeyCode.LeftShift) && energy > 0)
            {
                rb.velocity += speed * acc;
                engine[2].SetActive(true);
                engine[3].SetActive(true);
                cam.Target(true);
                energy -= 1;
                GC.Energy(energy);
            }
        }
        else 
        {
            engine[0].SetActive(false);
            engine[1].SetActive(false);
        }
        
    }
}

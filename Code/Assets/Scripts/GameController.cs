using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public float bulletDamage;
    public float laserDamage;
    public Image[] HP;
    public Image[] CP;
    public Image[] EN;
    int CPFull;
    float HPFull;
    float ENFull;
    float HPLen;
    float ENLen;
    float CPLen;
    public static float fps;
    public float _HP;
    public Image SunHealth;
    public Image time;
    Vector3 TimeScale;
    float MaxSunHP;
    int score;
    Player player;
    Gun gun;
    public Image ENButton;
    public Image HPButton;
    public Image CPButton;
    int scoreRequiryEN;
    int scoreRequiryCP;
    int scoreRequiryHP;
    Color alpha;
    Color nonAlpha;
    public Text scoreText;
    public Text gameOver;
    public Text win;
    public Text HPScore;
    public Text CPScore;
    public Text ENScore;
    int energyScore;
    int healthScore;
    int capacityScore;
    public Image guide;
    public Button exitButton;
    public Button resumeButton;
    public SaveGame PP;

    void Awake()
    {
        exitButton.gameObject.SetActive(false);
        resumeButton.gameObject.SetActive(false);
        gameOver.gameObject.SetActive(false);
        player = GameObject.Find("Shuttle").GetComponent<Player>();
        gun = GameObject.Find("Gun").GetComponent<Gun>();
        score = 0;
        laserDamage = 4f;
        bulletDamage = 1f;
        CPLen = CP.Length;
        ENLen = EN.Length;
        HPLen = HP.Length;
        ENFull = 100;
        TimeScale = new Vector3(-0.0005f, 0, 0);
        StartCoroutine("TimeScroll");
        MaxSunHP = 5000;
        scoreRequiryCP = 10;
        scoreRequiryHP = 10;
        scoreRequiryEN = 10;
        alpha = new Color(1, 1, 1, 0.3f);
        nonAlpha = new Color(1, 1, 1, 1);
        HPButton.color = alpha;
        ENButton.color = alpha;
        CPButton.color = alpha;
        energyScore = 0;
        healthScore = 0;
        capacityScore = 0;
        HPScore.text = healthScore.ToString();
        CPScore.text = capacityScore.ToString();
        ENScore.text = energyScore.ToString();
        StartCoroutine("Guide");
        win.gameObject.SetActive(false);
        PP = GetComponent<SaveGame>();
        score = PP.GetScore();
    }

    IEnumerator Guide()
    {
        yield return new WaitForSeconds(17);
        Destroy(guide);
    }

    public void GameEnd()
    {
        StartCoroutine("GameReload");
        gameOver.gameObject.SetActive(true);
    }

    public void Win()
    {
        StartCoroutine("GameReload");
        win.gameObject.SetActive(true);
    }

    IEnumerator GameReload()
    {
       
        yield return new WaitForSeconds(3);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void AddScore(int points)
    {
        score += points;
        DrawScore();
    }

    void AddHP(int hp)
    {
        GetHP(HPFull + hp);
        player.AddHP(hp);
        score -= scoreRequiryHP;
        scoreRequiryHP += 10;
        DrawScore();
        ++healthScore;
        HPScore.text = healthScore.ToString();
    }

    void AddCP(int cp)
    {
        GetCP(CPFull + cp);
        gun.AddCap(2);
        laserDamage += 2f;
        bulletDamage += 0.3f;
        score -= scoreRequiryCP;
        scoreRequiryCP += 10;
        DrawScore();
        ++capacityScore;
        CPScore.text = capacityScore.ToString();
    }

    void AddEN(int en)
    {
        ENFull += en;
        player.AddEN(en);
        score -= scoreRequiryEN;
        scoreRequiryEN += 10;
        DrawScore();
        ++energyScore;
        ENScore.text = energyScore.ToString();
    }

    IEnumerator TimeScroll()
    {
        time.transform.localScale += TimeScale;
        if (time.transform.localScale.x < 0) GameEnd();
        yield return new WaitForSeconds(0.5f);
        StartCoroutine("TimeScroll");
        
    }

    public void SunHP(float health)
    {
        SunHealth.transform.localScale = new Vector3(health/MaxSunHP, 1, 1);
    }

    public void Capacity(float capacity)
    {
        for(int i = 0; i < CPLen; i++)
        {
            if (i / CPLen > capacity / CPFull)
            {
                CP[i].enabled = false;
            }
            else CP[i].enabled = true;
        }
    }

    public void Health(float health)
    {
        for (int i = 0; i < HPLen; i++)
        {
            if (i / HPLen > health / HPFull)
            {
                HP[i].enabled = false;
            }
            else HP[i].enabled = true;
        }
    }

    public void Energy(float energy)
    {
        for (int i = 0; i < ENLen; i++)
        {
            if (i / ENLen > energy / ENFull)
            {
                EN[i].enabled = false;
            }
            else EN[i].enabled = true;
        }
    }

    public void GetHP(float hp)
    {
        HPFull = hp;
    }

    public void GetCP(int cap)
    {
        CPFull = cap;
    }

    void DrawScore()
    {
        scoreText.text = score.ToString();
    }

    void Pause()
    {
        exitButton.gameObject.SetActive(true);
        resumeButton.gameObject.SetActive(true);
        Time.timeScale = 0;

    }

    public void Resume()
    {
        exitButton.gameObject.SetActive(false);
        resumeButton.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void Exit()
    {
        PP.SaveScore(score);
        Application.Quit();
    }

    private void Update()
    {
        HPButton.color = alpha;
        ENButton.color = alpha;
        CPButton.color = alpha;
        if (score >= scoreRequiryHP)
        {
            HPButton.color = nonAlpha;
            if (Input.GetKeyDown(KeyCode.Alpha1)) AddHP(10);
        }

        if (score >= scoreRequiryCP)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2)) AddCP(3);
            CPButton.color = nonAlpha;
        }

        if (score >= scoreRequiryEN)
        {
            if (Input.GetKeyDown(KeyCode.Alpha3)) AddEN(10);
            ENButton.color = nonAlpha;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) Pause();



    }
}

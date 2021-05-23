using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_ : MonoBehaviour
{
    public static GameManager_ instance;

    public GameObject weapon;
    public GameObject CinematineCam;
    public GameObject MenuPanel;
    public GameObject homesceneobjecs;
    public GameObject sequence;
    public GameObject enemyManager;
    public GameObject loadingpanel;

    [Space]
    public TMPro.TMP_Text lifesText;

    int numberofhumanformsTaken;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        Invoke("EnableThings", 40);
        numberofhumanformsTaken = Numberofhumanlifes;
        lifesText.text = numberofhumanformsTaken.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void EnableThings()
    {
        //weapon.SetActive(true);
        //CinematineCam.SetActive(true);
        sequence.SetActive(false);
    }

    public void StartGame()
    {
        MenuPanel.GetComponent<Animator>().SetTrigger("close");
        enemyManager.SetActive(true);
        enemyManager.GetComponent<EnemyManager>().StartManager();
        weapon.SetActive(true);
        CinematineCam.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    void asyncoperationloadcomplete(AsyncOperation ao)
    {
        homesceneobjecs.SetActive(false);
        loadingpanel.SetActive(false);
    }

    void asyncoperationUnloadcomplete(AsyncOperation ao)
    {
        homesceneobjecs.SetActive(true);
        MenuPanel.SetActive(true);
       // MenuPanel.GetComponent<Animator>().SetTrigger("open");
       // homesceneobjecs.SetActive(true);
        loadsceneStarted = false;
        loadingpanel.SetActive(false);
        weapon.SetActive(true);
        CinematineCam.SetActive(true);
        Cursor.visible = true;
    }

    bool loadsceneStarted;
    public void LoadScene()
    {
        loadingpanel.SetActive(true);
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (var item in enemies)
        {
            Destroy(item.gameObject);
        }

        Enemy2[] enemy2 = FindObjectsOfType<Enemy2>();
        foreach (var item in enemy2)
        {
            Destroy(item.gameObject);
        }

         AsyncOperation ao =  SceneManager.LoadSceneAsync("level1", LoadSceneMode.Additive);
         ao.completed += asyncoperationloadcomplete;
        
    }

    public void Unloadscene()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        numberofhumanformsTaken++;
        lifesText.text = numberofhumanformsTaken.ToString();
        Numberofhumanlifes = numberofhumanformsTaken;

        loadingpanel.SetActive(true);
        AsyncOperation ao = SceneManager.UnloadSceneAsync("level1");
        ao.completed += asyncoperationUnloadcomplete;


    }

    internal void StartHumanLife()
    {
        if (!loadsceneStarted)
        {
            loadsceneStarted = true;

            StopAllCoroutines();
            CinematineCam.SetActive(false);
            weapon.SetActive(false);
            Camera.main.transform.GetComponent<Animator>().SetTrigger("humanform");
            Invoke("LoadScene",2.5f);
        }
    }


    public int Numberofhumanlifes
    {
        get
        {
            if (PlayerPrefs.HasKey("Humanlifes"))
            {
                return PlayerPrefs.GetInt("Humanlifes"); 
            }
            else
            {
                return 0;
            }
        }
        set
        {
            PlayerPrefs.SetInt("Humanlifes", value);
        }
    }
}

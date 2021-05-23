using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class LevelOneManager : MonoBehaviour
{
    public Transform birthpos;
    public Transform Growthpos;
    public Transform Reproducepos;
    public Transform Deathpos;
    public Transform player;

    int distancetoTrigger = 3;
    int CircleNumber;

    [Space]
    public GameObject steps;
    public GameObject flyingobject;
    public GameObject Spiritenemy;

    public GameObject birthtrigger;
    public GameObject growthtrigger;
    public GameObject reproducetrigger;
    public GameObject deathtrigger;

    [Space]
    public GameObject flora;

    [Space]
    public Transform[] enemypositions;
   [Space]
    public PostProcessVolume PostProcessVolume;
    
    LifeCycle currentstate;
    LifeCycle previousState;

    // Start is called before the first frame update
    void Start()
    {
        CircleNumber = 0;
    }

    bool hueshiftactive;
    bool gameover;
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.position, birthpos.position) < distancetoTrigger)
        {
            if (currentstate == LifeCycle.Death || currentstate == LifeCycle.Birth)
            {
                if (currentstate == LifeCycle.Death)
                    previousState = LifeCycle.Death;
             
                currentstate = LifeCycle.Birth;
            }
        }
        
        if (Vector3.Distance(player.position, Growthpos.position) < distancetoTrigger)
        {
            if (currentstate == LifeCycle.Birth)
            {
                previousState = currentstate;
                currentstate = LifeCycle.Growth;
            }
        }
        
        if (Vector3.Distance(player.position, Reproducepos.position) < distancetoTrigger)
        {
            if (currentstate == LifeCycle.Growth)
            {
                previousState = currentstate;
                currentstate = LifeCycle.reproduce;
            }
        }

        if (Vector3.Distance(player.position, Deathpos.position) < distancetoTrigger)
        {
            if (currentstate == LifeCycle.reproduce)
            {
                previousState = currentstate;
                currentstate = LifeCycle.Death;
                CircleNumber++;
            }
            //else
            //{
            //    currentstate = LifeCycle.Death;
            //    //instantiate spirit enemy
            //    Instantiate(Spiritenemy, enemypositions[ Random.Range(0, enemypositions.Length)].position, Quaternion.identity);
            //}
        }

        switch (CircleNumber)
        {
            case 1:
                break;
            case 2:
                steps.SetActive(true);
                break;
            case 3:
                flyingobject.SetActive(true);
                steps.SetActive(false);
                break;
            case 7:
                if(!hueshiftactive)
                    InvokeRepeating("huechange", 0, .5f);hueshiftactive = true;
                birthtrigger.SetActive(false);
                growthtrigger.SetActive(false);
                reproducetrigger.SetActive(false);
                deathtrigger.SetActive(false);
                break;
            case 4:
                //CancelInvoke("huechange");
                hueshiftactive = false;
                //PostProcessVolume.profile.GetSetting<ColorGrading>().hueShift.value = 0;
                birthtrigger.SetActive(true);
                growthtrigger.SetActive(true);
                reproducetrigger.SetActive(true);
                deathtrigger.SetActive(true);
                break;
            case 5:
                //
                flora.SetActive(true);

                break;
            case 6:
                steps.SetActive(true);

                break;
            case 8:

                if (!gameover)
                {
                    Debug.LogError("GameOver");
                    StartCoroutine("increasebloom");
                    gameover = true;
                }
                break;

            //case 9:
            //    CancelInvoke("huechange");
            //    PostProcessVolume.profile.GetSetting<ColorGrading>().hueShift.value = 0;
                
            //    break;
            default:
                break;
        }
    }

    IEnumerator increasebloom()
    {
        Bloom b;
        PostProcessVolume.profile.TryGetSettings<Bloom>(out b);

        while(b.intensity.value < 80)
        {
            b.intensity.value++;
            yield return new WaitForEndOfFrame();
        }

        //unloadscene
        GameManager_.instance.Unloadscene();

    }
    void huechange()
    {
        ColorGrading c;
        PostProcessVolume.profile.TryGetSettings<ColorGrading>(out c);

        c.hueShift.value = Random.Range(-180, 180);
    }

}

public enum LifeCycle
{
    Birth,
    Growth,
    reproduce,
    Death
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public EnemyWave[] waves;
    public Transform[] enemypositions;
    public Transform Player;
    
    // Start is called before the first frame update
    void Start()
    {
     //   StartCoroutine("StartEnemyWave",0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StartManager()
    {
        StartCoroutine("StartEnemyWave", 0);
    }
    IEnumerator StartEnemyWave(int waveNumber)
    {
        
        for (int i = 0; i < waves[waveNumber].numberofenemies; i++)
        {
            GameObject enemy = Instantiate(waves[waveNumber].enemies[Random.Range(0, waves[waveNumber].enemies.Length)], enemypositions[Random.Range(0, enemypositions.Length)].position, Quaternion.identity);
           if(enemy.GetComponent<Enemy>())
                enemy.GetComponent<Enemy>().target = Player;
            Destroy(enemy, 10);
            yield return new WaitForSeconds(waves[waveNumber].gapbetweenenemies);
        }

        yield return new WaitForSeconds(5f);

        //show wave complete
        int num = waveNumber + 1;
        Debug.Log(num);
        StartCoroutine("StartEnemyWave", num);
    }

}

[System.Serializable]
public class EnemyWave
{
    public GameObject[] enemies;
    public int numberofenemies;
    public float gapbetweenenemies;

}
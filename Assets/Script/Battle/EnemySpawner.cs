using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnSquare;
    public GameObject[] enemies;
    public GameObject []vacios;
    int randomSpawnPoint, randomEnemy;
    public static bool spawnAllowed;
    public GameObject SceneManager;
    public GameObject enemiesDestroyed;
   
    // Start is called before the first frame update
    void Start()
    {
        spawnAllowed = true;
        SpawnEnemy();
       
        
           
       

    }

    // Update is called once per frame
    void Update()
    {
        CheckEnemy();
        Destroy(GameObject.Find("Nada" + 0 + "(Clone)"));
    }
    void SpawnEnemy()
    {
        if (spawnAllowed)
        {
            randomSpawnPoint = Random.Range(0, spawnSquare.Length);
            randomEnemy = Random.Range(0, enemies.Length);
            Instantiate(enemies[randomEnemy], spawnSquare[randomSpawnPoint].position, Quaternion.identity);
        }
    }
    void CheckEnemy()
    {
        if (GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            print("nadie");
            StartCoroutine(VictoryEpilogue(2f));
            enemiesDestroyed.SetActive(true);
            Destroy(GameObject.Find("Blast(Clone)"));
            Destroy(GameObject.Find("TankBlast(Clone)"));
        }
        
    }
    private void FixedUpdate()
    {
        CheckEnemy();
    }

    IEnumerator VictoryEpilogue(float wait)
    {
        yield return new WaitForSeconds(wait);
        SceneManager.GetComponent<ChangeSceneManager>().ToVictory();

        //print("Basado y rojopileado");
    }
}

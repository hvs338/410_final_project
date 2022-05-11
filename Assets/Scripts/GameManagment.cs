using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagment : MonoBehaviour
{
    public int round = 1;
    int zombieInRound = 10;
    int zombieSpawnTimer = 0;

    int zombieSpawnInRounds = 0;

    public Transform[] zombieSpawnPoints;


    public GameObject zombieEnemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(zombieSpawnInRounds < zombieInRound){
            if(zombieSpawnTimer > 30){
                SpawnZombie();
                zombieSpawnTimer = 0;
            }
            else{
                zombieSpawnTimer++;
            }
        }
    }

    void SpawnZombie(){
        Vector3 randomSpawnPoint = zombieSpawnPoints[Random.Range(0, zombieSpawnPoints.Length)].position;
        Instantiate(zombieEnemy, randomSpawnPoint, Quaternion.identity);
        zombieSpawnInRounds++;
    }
}

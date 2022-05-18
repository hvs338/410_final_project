using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagment : MonoBehaviour
{
    public Transform[] zombieSpawnPoints;

    public GameObject zombieEnemy;

    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    
    [SerializeField] 
    public GameObject player;
   
    public int round = 0;
    
        
    public int startCount = 2;
    
    
    [SerializeField]
    public ZombieCounter aliveCount;

    
    private int nextWave = 1;

    public float timeBetweenWaves = 20f;
    public float waveCountDown;
    private float searchCountDown = 1f;

    private SpawnState state = SpawnState.COUNTING;

    bool EnemyIsAlive(){
        searchCountDown -= Time.deltaTime;
        
        if(aliveCount.counter == 0)return false;
        if(searchCountDown <= 0f){
            searchCountDown = 1f;
            if(GameObject.FindGameObjectsWithTag("Enemy") == null)
                return false;
        }
        else{
            return true;
        }
            
        return false;


    }
/*
    void BeginNewRound(){
        Debug.Log("Wave Completed!");
        state = SpawnState.COUNTING;

        waveCountDown = timeBetweenWaves;

        if(aliveCount.counter == 0){
            Debug.Log("All waves complete!");
        }

        
    }

*/
    // Start is called before the first frame update
    void Start()
    {
        waveCountDown = timeBetweenWaves;
        aliveCount.counter = 0;

    }

    // Update is called once per frame
    void Update()
    {   
       

        if(waveCountDown <= 0){
            if(state != SpawnState.SPAWNING){
                // START spawning wave
                if(aliveCount.counter == 0){
                    round++;
                    //new WaitForSeconds(10);
                    StartCoroutine(SpawnWave());
                }
            }
        }
        else{
            waveCountDown -= Time.deltaTime;
        }
    }

    
    IEnumerator SpawnWave(){
        //Debug.Log("Spawning Wave: " + _wave.name);
        state = SpawnState.SPAWNING;

        yield return new WaitForSeconds(15);

        // SPAWN
        startCount += 5;
        for(int i = 0; i < startCount; ++i){
            aliveCount.counter+= 1;
            SpawnZombie(zombieEnemy);
            // Delay for zombie spawning 
            yield return new WaitForSeconds(3);
        }

        

        state = SpawnState.WAITING;



        yield break;
    }
    


    void SpawnZombie(GameObject _enemy){

        Vector3 randomSpawnPoint = zombieSpawnPoints[Random.Range(0, zombieSpawnPoints.Length)].position;
        Debug.Log("TEST" + _enemy);
        GameObject temp = Instantiate(_enemy, randomSpawnPoint, Quaternion.identity);
        temp.GetComponent<Enemy>().player = player.transform;
        
        
        Debug.Log("Spawning Enemy: " + _enemy.name);
    }
}

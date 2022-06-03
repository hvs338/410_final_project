using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
// Written by Elias
public class GameManagment : MonoBehaviour
{   


    [SerializeField]
    private Button factoryv2;

    [SerializeField]
    private Button dock;

    public Transform[] zombieSpawnPoints;

    public GameObject zombieEnemy;

    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    
    [SerializeField] 
    public GameObject player;
   
    public int round = 1;
    
        
    public int startCount = 2;
    
    [SerializeField]
    public MapInfo map;

    
    [SerializeField]
    public ZombieCounter aliveCount;

    public GameOverScreen GameOverScreen;

    public void GameOver(){
        GameOverScreen.Setup(round);
    }
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
    // Start is called before the first frame update
    void Start()
    {   
        map.round = 0;
        waveCountDown = timeBetweenWaves;
        aliveCount.counter = 0;
        map.name = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {   
       
        map.round = round;
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


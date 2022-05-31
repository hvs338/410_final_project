using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSelect : MonoBehaviour
{   
    [SerializeField]
    public GameObject factoryv2;

    [SerializeField]
    public GameObject dock;

    [SerializeField]
    public MapInfo map;

    void Start(){

        Debug.Log(map);
        if(map != null && map.name.Equals("Factory") && map.round >= 2){
            factoryv2.SetActive(true);
        }
        if(map != null && map.name.Equals("Factoryv2") && map.round >= 3){
            factoryv2.SetActive(true);
            dock.SetActive(true);
        }
        
    }

    public void selectScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }
}

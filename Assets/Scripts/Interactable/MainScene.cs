using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScene : MonoBehaviour
{   
    [SerializeField]
    public MapInfo map;

    void Start(){
        Debug.Log(map);
        Cursor.lockState = CursorLockMode.None;
    }
    public void selectScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }
}

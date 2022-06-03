using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameScript : MonoBehaviour
{
    public GunController health;

    void Start(){
        health = GameObject.Find("Player").GetComponent<GunController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health.currentHealth <= 0){
            SceneManager.LoadScene("EndScreen");
            Cursor.lockState = CursorLockMode.None;
        }

    }
}

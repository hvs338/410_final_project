using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSelect : MonoBehaviour
{
   public void selectScene(string sceneName){
       SceneManager.LoadScene(sceneName);
   }
}

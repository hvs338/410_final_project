using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{   
    public Button[] button_list;

    public int test = 3;

    // Start is called before the first frame update
    void Start()
    {
        int levelAt = PlayerPrefs.GetInt("levelAt", 1);

        for(int i = 0; i < button_list.Length; i++){

            if(i + 2 > levelAt){
                test++;
            }
        }
    }

   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    // Start is called before the first frame update

    public Gun[] Guns;
    public Gun test;
    public int prior;
    
    void Start()
    {
        InitVariables();
        
    }

    public void Switch(){
        addItem(test);
    }
    public void addItem(Gun new_gun){
        prior = (int)new_gun.priority;
        
        if(Guns[prior] != null){
            RemoveItem(prior);

        }
        Guns[prior] = new_gun;
        
    }

    public void RemoveItem(int index){
        Guns[index] = null;

    }

    // Update is called once per frame
    void InitVariables()

    {
        Guns = new Gun[2];
        
    }
}


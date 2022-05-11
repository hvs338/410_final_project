using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    // Start is called before the first frame update

    public Item[] weapons;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void InitVariables()

    {
        weapons = new Item[3];
        
    }
}

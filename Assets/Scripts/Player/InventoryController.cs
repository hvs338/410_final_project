using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    // Start is called before the first frame update

    public Gun[] Guns;
    public Gun starting_gun;
    public int prior;

    public int currentlyEquipped  = 0;

    public GameObject current_gun;
    
    [SerializeField] Transform weaponHolder = null;

    private GunController GC;
    void Awake()
    {   
        current_gun = GameObject.Find("AK-47");

        
        Guns = new Gun[2];
        Guns[0] = starting_gun;
        GC = GetComponent<GunController>();
        
        
    }

    public void Switch(){


        
        Debug.Log(currentlyEquipped);


        int weapon_to_Switch_to = 1 - currentlyEquipped;
        Gun switching_gun = Guns[weapon_to_Switch_to];

        if(switching_gun != null){
            Gun currently_equipped_gun = Guns[currentlyEquipped];
            GameObject gun_prefab = switching_gun.prefab;
            GameObject currently_equipped_prefab = currently_equipped_gun.prefab;
            UnEquip();
            EquipWeapon(gun_prefab,(int)switching_gun.priority);
            
            currentlyEquipped = weapon_to_Switch_to;
        }






    }
    public void addItem(Gun new_gun){

        
        prior = (int)new_gun.priority;
        Debug.Log(Guns[prior]);
        
        
        if(Guns[prior] != null){
            Debug.Log("HERE");
            
            UnEquip();
            RemoveItem(prior);
            current_gun = Instantiate(new_gun.prefab,weaponHolder);
            Guns[prior] = new_gun;

            GC.AmmoCapacity = new_gun.Capacity;
            GC.Magazine = new_gun.Magazine;
            GC.currentAmmo = new_gun.Magazine;
            GC.reserveAmmo = new_gun.Capacity;
            GC.damage = new_gun.damage;
            GC.range = new_gun.range;
            

        }
        else{
        Guns[prior] = new_gun;
        }
        
        

        

        //EquipWeapon(new_gun.prefab,(int)new_gun.priority);
        
    }
    public void EquipWeapon(GameObject weapon, int priority){


        
        Gun gun = GetItem(priority);
        if(gun != null){
            Debug.Log("Eqipping");
            currentlyEquipped = priority;
            current_gun = Instantiate(weapon,weaponHolder);
            GC.AmmoCapacity = gun.Capacity;
            GC.Magazine = gun.Magazine;
            GC.currentAmmo = gun.Magazine;
            GC.reserveAmmo = gun.Capacity;
            GC.damage = gun.damage;
            GC.range = gun.range;
        }

        //currentlyEquipped = weaponStyle;

 


    }

    private void UnEquip(){
        Debug.Log("Un-Equipping");
        Destroy(current_gun);
    }

    public Gun GetItem(int index){

        return Guns[index];
    }


    public void RemoveItem(int index){
        //Debug.Log("YES");
        //Destroy(weaponHolder.GetChild(index).gameObject);
        Guns[index] = null;

    }

    // Update is called once per frame

}

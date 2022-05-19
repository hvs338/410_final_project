using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    // Start is called before the first frame update

    public Gun[] Guns;
    public Gun starting_gun;
    public int prior;

    private int currentlyEquipped  = 0;
    
    [SerializeField] Transform weaponHolder = null;

    private GunController GC;
    void Awake()
    {
        Guns = new Gun[2];
        Guns[0] = starting_gun;
        GC = GetComponent<GunController>();
        
        
    }

    public void Switch(){


        
        Debug.Log(currentlyEquipped);


        int weapon_to_Switch_to = 1 - currentlyEquipped;
        currentlyEquipped = weapon_to_Switch_to;
        

        Gun currently_equipped_gun = Guns[currentlyEquipped];
        Gun switching_gun = Guns[weapon_to_Switch_to];
        GameObject gun_prefab = switching_gun.prefab;

        Destroy(weaponHolder.GetChild(currentlyEquipped).gameObject);
        Instantiate(gun_prefab,weaponHolder,false);







    }
    public void addItem(Gun new_gun){
        prior = (int)new_gun.priority;
        //Debug.Log(Guns[prior]);
        
        if(Guns[prior] != null){
            //Debug.Log("HERE");
            RemoveItem(prior);

        }
        Guns[prior] = new_gun;

    
        EquipWeapon(new_gun.prefab,(int)new_gun.priority);
        
    }
    public void EquipWeapon(GameObject weapon, int weaponStyle){

        Gun gun = GetItem(weaponStyle);
        if(gun != null){
            Instantiate(weapon,weaponHolder);
        }

        //currentlyEquipped = weaponStyle;

        GC.AmmoCapacity = gun.Capacity;
        GC.Magazine = gun.Magazine;
        GC.currentAmmo = gun.Magazine;
        GC.reserveAmmo = gun.Capacity;
        GC.damage = gun.damage;
        GC.range = gun.range;


    }

    public Gun GetItem(int index){

        return Guns[index];
    }


    public void RemoveItem(int index){
        //Debug.Log("YES");
        Destroy(weaponHolder.GetChild(index).gameObject);
        Guns[index] = null;

    }

    // Update is called once per frame

}


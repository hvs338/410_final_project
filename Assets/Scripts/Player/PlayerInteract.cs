using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerInteract : MonoBehaviour
{
    // Start is called before the first frame update

    private Camera cam;


    [SerializeField]
    private float distance = 3f;
    [SerializeField]
    private LayerMask PickUp_layer;
    private PlayerUI playerUI;

    private InputManager inputManager;

    InventoryController inventory;

    GunController gun_controller;
    public TextMeshProUGUI itemDescription;
    //public  text_info; 

    public bool bought_before;
    public bool bought_before2;
    private void Start()
    {
        cam = Camera.main;
        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
        inventory = GetComponent<InventoryController>();
        gun_controller= GetComponent<GunController>();


    }

    private void Update(){
        bought_before2 = false;

        itemDescription.SetText(string.Empty);
        Ray ray = new Ray(cam.transform.position,cam.transform.forward);
        RaycastHit info;
        Debug.DrawRay(ray.origin,ray.direction*distance);
        if(Physics.Raycast(ray,out info,distance,PickUp_layer)){

            
            Gun new_gun = info.transform.GetComponent<ItemObject>().item as Gun;

            bought_before2 = check_in_inventory(new_gun);

            Debug.Log(bought_before2);

            if(bought_before2 != true){
            itemDescription.SetText(new_gun.information);
            }
            else{
                itemDescription.SetText("Buy more anumition for gun, $350");
            }

        }

        

    }

    // Update is called once per frame
    public void PickUp()


    { // USES Q KEY

        
        Ray ray = new Ray(cam.transform.position,cam.transform.forward);
        RaycastHit info;
        Debug.DrawRay(ray.origin,ray.direction*distance);
        if(Physics.Raycast(ray,out info,distance,PickUp_layer)){
           
            

            Gun new_item = info.transform.GetComponent<ItemObject>().item as Gun;

            Debug.Log(new_item.name);


            bought_before2 = check_in_inventory(new_item);
       

            if( new_item.price <= gun_controller.playerPoints && bought_before2 != true ){
                
                inventory.addItem(new_item);
                //new_item.bought_before = true;
                gun_controller.playerPoints = gun_controller.playerPoints - new_item.price;
                //Destroy(info.transform.gameObject);
            }

            else{

                int to_add = new_item.Capacity - gun_controller.reserveAmmo;
                gun_controller.reserveAmmo += to_add;
                gun_controller.playerPoints =gun_controller.playerPoints -350;

            }

           

            itemDescription.SetText("Not Enough Money, go kill some Zombies Loser");
            
        }
    }
    public bool check_in_inventory(Gun new_item){


            bool in_inventory = false;

            int inventory_length = inventory.Guns.Length;
            //Debug.Log(inventory_length);
            for (int i = 0; i < inventory_length; i ++){
                
                
                if(inventory.Guns[i] != null){
                    //Debug.Log(inventory.Guns[i].name);
                if(new_item.name_compare == inventory.Guns[i].name_compare){
                   // Debug.Log(inventory.Guns[i].name_compare==new_item.name_compare);
                    
                    print("ITS TRUE");
                    in_inventory = true;
                    return in_inventory;
                    }
                }
                
            }

        return in_inventory;
        
    
        }


    }



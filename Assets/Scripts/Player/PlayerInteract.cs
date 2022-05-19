using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        cam = Camera.main;
        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
        inventory = GetComponent<InventoryController>();
        gun_controller= GetComponent<GunController>();


    }

    // Update is called once per frame
    public void PickUp()


    { // USES Q KEY

        
        Ray ray = new Ray(cam.transform.position,cam.transform.forward);
        RaycastHit info;
        Debug.DrawRay(ray.origin,ray.direction*distance);
        if(Physics.Raycast(ray,out info,distance,PickUp_layer)){
            Debug.Log(info.transform.name);

            Gun new_item = info.transform.GetComponent<ItemObject>().item as Gun; 

            if( new_item.price <= gun_controller.playerPoints){
            inventory.addItem(new_item);
            Destroy(info.transform.gameObject);
            }
            
        }
    }

}
//if(info.collider.GetComponent<Interactable>()!= null){
                /*
                Interactable interactable = info.collider.GetComponent<Interactable>();
                playerUI.UpdateText(interactable.promptMessage);
                if(inputManager.onFoot.Interact.triggered){

                    

                }
            }
            */
        //}
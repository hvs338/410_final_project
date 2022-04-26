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
    private LayerMask mask;
    private PlayerUI playerUI;

    private InputManager inputManager;


    void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();

    }

    // Update is called once per frame
    void Update()
    {
        playerUI.UpdateText(string.Empty);
        Ray ray = new Ray(cam.transform.position,cam.transform.forward);
        RaycastHit info;
        Debug.DrawRay(ray.origin,ray.direction*distance);
        if(Physics.Raycast(ray,out info,distance,mask)){
            if(info.collider.GetComponent<Interactable>()!= null){
                Interactable interactable = info.collider.GetComponent<Interactable>();
                playerUI.UpdateText(interactable.promptMessage);
                if(inputManager.onFoot.Interact.triggered){

                    interactable.BaseIneract();

                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class AnimationScript : MonoBehaviour
{
    // Start is called before the first frame update
    UnityEngine.Animator anim;
    PlayerInput input;
    bool Firing;
    public AudioSource Audio;
    
    private GunController player;

    void Awake(){

        input = new PlayerInput();
        input.OnFoot.Shoot.performed += Animate_and_Sound ;
        input.OnFoot.Reload.performed += Reload_Animate;
        
    }
    void Start()
    {
        anim = GetComponent<UnityEngine.Animator>();
        Audio = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<GunController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);

        if(info.IsName("Fire")) anim.SetBool("Firing",false);

    }
    void Animate_and_Sound(InputAction.CallbackContext obj){
        if (player.canshoot == true){ 
        anim.SetBool("Firing",true);  
        
        Audio.Play();
        }
    }

    void Reload_Animate(InputAction.CallbackContext obj){

        if(player.canReload == true){
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);

        if(info.IsName("Reload")) return;
        anim.CrossFadeInFixedTime("Reload",0.01f);
    }

    }

    void stop_idle(){

    }
    void OnEnable(){
        input.OnFoot.Enable();

    }
    void OnDisable(){
        input.OnFoot.Disable();

    }
}

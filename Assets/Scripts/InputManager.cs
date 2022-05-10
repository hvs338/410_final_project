using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{   
    private PlayerInput playerInput;
    public PlayerInput.OnFootActions onFoot;

    public PlayerMotor motor;

    public PlayerLook look;

    [SerializeField]
    GunController GC;


    



    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        GC = GetComponent<GunController>();
        

        onFoot.Jump.performed += ctx => motor.Jump();
        onFoot.Crouch.performed += ctx => motor.Crouch();
        onFoot.Shoot.performed += ctx => GC.Shoot();

      
    

        // Need to do value based input
        

        //
        onFoot.Reload.performed += _ => GC.Reload();
        

    
        
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
        
    }
    void Update(){
        GC.processAim(onFoot.Aim.ReadValue<float>());
        //GC.Shoot(onFoot.Shoot.ReadValue<float>());
        motor.Sprint(onFoot.Sprint.ReadValue<float>());
    }

    private void LateUpdate(){
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable(){
        onFoot.Enable();
    }
    private void OnDisable(){
        onFoot.Disable();
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using TMPro;
using UnityEngine.UI;






public class GunController : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Gun Settings")]
    public float fireRate = 1f;
    public int Magazine;
    public int AmmoCapacity;
    public float reloadTime;

    public float Firing;
    UnityEngine.Animator anim;

    public int player_health = 100;
    public int currentHealth;

   

    //variables that change throughoyut code

    public bool canshoot;
    public int currentAmmo;
    public int reserveAmmo;

    Transform cam;
    
    public LayerMask whatIsEnemy;
    public ParticleSystem muzzleFlash, bullet;
    public GameObject bulletHoleGraphic,fleshHit;  
    
    //public float camShakeMagnitude, camShakeDuration;
    public TextMeshProUGUI AmmoCount;
    public TextMeshProUGUI Points;

    //public Animator animation;
    public GameObject ak;

    public GameObject original_pos;
    public Vector3 original_pos_vec;
    public GameObject aim_pos;
    public Vector3 aim_pos_vec;
    public GameObject gun;
    public bool canReload;

    public int playerPoints;
    public float range = 50f;
    public int damage = 10;
    GameObject player;
    public float aimSpeed = 8f;
    public bool isReloading;
    public IEnumerator coroutine;
    private Animator fps_animator;
    public AudioSource Audio;

    public healthBar Health_bar;

    private InventoryController IC;
    private Gun Weapon;
    private float time_start;

    

    public void Start(){

        canshoot = true;        
        playerPoints = 2000;
        cam = Camera.main.transform;
        player = GameObject.Find("Player");
        gun = GameObject.Find("FPS");

        fps_animator = GameObject.Find("FPS").GetComponent<Animator>();
        aim_pos_vec = aim_pos.transform.localPosition;
        original_pos_vec = original_pos.transform.localPosition;

        //anim = GetComponentInChildren<UnityEngine.Animator>();
        
       
        canReload = false;
  
        
        Audio = GetComponentInChildren<AudioSource>();
        Health_bar.SetMaxHealth(player_health);
        currentHealth = player_health;

        IC = GetComponent<InventoryController>();
        Weapon = IC.Guns[0];

        currentAmmo = Weapon.Magazine;

        reserveAmmo = Weapon.Capacity;
        Magazine = Weapon.Magazine;



         
    


    }
       private void Update()
    {
        // Player UI
        string clipSize_string = Magazine.ToString();
        string currentAmmo_string = currentAmmo.ToString();
        string reserve = reserveAmmo.ToString();
        string fullDisplay = currentAmmo_string +" / "+ reserve;
        AmmoCount.SetText(fullDisplay);
        Points.SetText("$"+playerPoints.ToString());

        //if( time_start > 0){
         //       fps_animator.SetBool("Firing",false);
        //}

        if(time_start <= 0){
            canshoot = true;
            
        }
        else{
            time_start -= Time.deltaTime;
            
            fps_animator.SetBool("Firing",false);
        
        }


     
    }
    private void FixedUpdate(){
        AnimatorStateInfo info = fps_animator.GetCurrentAnimatorStateInfo(0);

        if(info.IsName("Fire")) fps_animator.SetBool("Firing",false);

        
         //Shooting Logic

        

    }
    
    public void Shoot(float input){
        //float input){
            
            //bool pressed;
            //pressed = input.isPressed;
        
        if(input == 1){
   
            if(canshoot == true){
                
                time_start = IC.Guns[IC.currentlyEquipped].fireRate;
                
                if(currentAmmo > 0){
                    fps_animator.SetBool("Firing",true);
                    muzzleFlash.Play();
                    bullet.Play();
                    Audio.Play();

                    Debug.Log("HERE");
                    //fps_animator.SetBool("Firing",true);
                    Debug.Log("should be working");
                    canshoot = false;
                    currentAmmo = currentAmmo-1;
                    
                    //Ray Cast AKA Shooting
                    Ray ray = new Ray(cam.transform.position,cam.transform.forward);
                    if(Physics.Raycast(cam.position,cam.forward,out RaycastHit hit, range)){
                            Debug.Log(hit.collider.name);
                        if(hit.transform.TryGetComponent<Enemy>(out Enemy EN)){
                            Debug.Log("hit");
                            playerPoints += 10;

                            Instantiate(fleshHit, hit.point, Quaternion.Euler(0, 180, 0));
                            EN.TakeDamage(damage);
                        }
                        else{
                            Instantiate(bulletHoleGraphic, hit.point, Quaternion.Euler(0, 180, 0));
                        }

                    }
                }
            }
        }
        
        
            //}
        else{
            //Debug.Log("hey");
            //StartCoroutine(CooldownFinished(IC.Guns[IC.currentlyEquipped].fireRate));
            //canshoot = true;

        }
        
    }
    

    public void Reload(){
         
        Debug.Log("Reloading");
        canshoot = false;

 
        if(currentAmmo < Magazine && reserveAmmo>0){
        AnimatorStateInfo info = fps_animator.GetCurrentAnimatorStateInfo(0);

        
        fps_animator.CrossFadeInFixedTime("Reload",0.01f);

            int amountNeeded = Magazine - currentAmmo;
            if(amountNeeded >= reserveAmmo){
                currentAmmo += reserveAmmo;
                reserveAmmo -= amountNeeded;
            }

            else{

                currentAmmo = Magazine;
                reserveAmmo-= amountNeeded;

            }

        }
        canReload = false;
        StartCoroutine(finishReload());
        
    }

    public void processAim(float input){
        
        
        if(!isReloading){


        
            if(input == 1){
                //anim.SetBool("Aiming",true);

                //gun.transform.localPosition = Vector3.Lerp(gun.transform.localPosition,aim_pos_vec,Time.deltaTime*aimSpeed);
            }
            else{
                fps_animator.SetBool("Aiming",false);

                gun.transform.localPosition = Vector3.Lerp(gun.transform.localPosition,original_pos_vec,Time.deltaTime*aimSpeed);

            }


        //gun.transform.Localosition = Vector3.Lerp(gun.transform.localPosition,aim_pos_vec,Time.deltaTime*aimSpeed);
        }
    
    }
        public IEnumerator CooldownFinished(float fire_rate)
        {
        
        yield return new WaitForSeconds(fire_rate);
        canshoot = true;

        }
        
        
        void SetToTrue(bool b) {
     b = true;
 }

 public IEnumerator finishReload(){
     yield return new WaitForSeconds(1f);
     canshoot = true;
 }

}
   





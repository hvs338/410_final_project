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

    

    public void Start(){

        canshoot = true;        
        playerPoints = 500;
        cam = Camera.main.transform;
        player = GameObject.Find("Player");
        gun = GameObject.Find("FPS");
        aim_pos_vec = aim_pos.transform.localPosition;
        original_pos_vec = original_pos.transform.localPosition;

        anim = GetComponentInChildren<UnityEngine.Animator>();
        
        coroutine = CooldownFinished();
        canReload = false;
        fps_animator = GetComponentInChildren<UnityEngine.Animator>();
        Audio = GetComponentInChildren<AudioSource>();
        Health_bar.SetMaxHealth(player_health);
        currentHealth = player_health;

        IC = GetComponent<InventoryController>();
        Weapon = IC.Guns[0];

        currentAmmo = Weapon.Magazine;
        reserveAmmo = Weapon.Capacity;



         
    


    }
       private void Update()
    {
    
        string clipSize_string = Magazine.ToString();

        string currentAmmo_string = currentAmmo.ToString();
        string reserve = reserveAmmo.ToString();

        string fullDisplay = currentAmmo_string +" / "+ reserve;
        
        AmmoCount.SetText(fullDisplay);
        Points.SetText("$"+playerPoints.ToString());
     
    }
    private void FixedUpdate(){
        AnimatorStateInfo info = fps_animator.GetCurrentAnimatorStateInfo(0);

        if(info.IsName("Fire")) anim.SetBool("Firing",false);
    }
    
    public void Shoot(){
        
        //if(input == 1){
            
            if(canshoot == true){
                fps_animator.SetBool("Firing",true);
                Audio.Play();
                if(currentAmmo > 0){
                    muzzleFlash.Play();
                    bullet.Play();
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
            anim.SetBool("Firing",false);
           // }
            /*else{
            //Debug.Log("hey");
            StartCoroutine(coroutine);
            canshoot = true;
                }
                */
            
        

            //}
            canshoot = true;
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
                anim.SetBool("Aiming",true);

                gun.transform.localPosition = Vector3.Lerp(gun.transform.localPosition,aim_pos_vec,Time.deltaTime*aimSpeed);
            }
            else{
                anim.SetBool("Aiming",false);

                gun.transform.localPosition = Vector3.Lerp(gun.transform.localPosition,original_pos_vec,Time.deltaTime*aimSpeed);

            }


        //gun.transform.Localosition = Vector3.Lerp(gun.transform.localPosition,aim_pos_vec,Time.deltaTime*aimSpeed);
        }

    }
        IEnumerator CooldownFinished()
    {
        
        yield return new WaitForSeconds(1f);

        }
        
        
        void SetToTrue(bool b) {
     b = true;
 }

 public IEnumerator finishReload(){
     yield return new WaitForSeconds(1f);
     canshoot = true;
 }

}
   





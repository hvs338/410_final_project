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
    public float fireRate = 0.1f;
    public int clipSize = 30;
    public int AmmoCapacity = 270;
    public float reloadTime;

    public float Firing;


   

    //variables that change throughoyut code

    public bool canshoot;
    public int currentAmmo;
    public int reserveAmmo;

    Transform cam;
    public Transform attackPoint;
    public LayerMask whatIsEnemy;
    public ParticleSystem muzzleFlash, bullet;
    public GameObject bulletHoleGraphic,fleshHit;  
    
    //public float camShakeMagnitude, camShakeDuration;
    public TextMeshProUGUI AmmoCount;
    public TextMeshProUGUI Points;

    //public Animator animation;
    public GameObject ak;

    public int playerPoints;
    float range = 50f;
    int damage = 10;
    GameObject player;

    public void Start(){
        currentAmmo = clipSize;
        reserveAmmo = AmmoCapacity;
        canshoot = true;        
        playerPoints = 500;
        cam = Camera.main.transform;
        player = GameObject.Find("Player");


    }
       private void Update()
    {
    
        string clipSize_string = clipSize.ToString();

        string currentAmmo_string = currentAmmo.ToString();
        string reserve = reserveAmmo.ToString();

        string fullDisplay = currentAmmo_string +" / "+ reserve;
        
        AmmoCount.SetText(fullDisplay);
        Points.SetText(playerPoints.ToString());
     
    }
    
    public void Shoot(){

        if(canshoot && currentAmmo>0){
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
        
        
        
  
        Invoke("CooldownFinished",fireRate);
        }


    }

    public void Reload(){
        Debug.Log("Reloading");

        if(currentAmmo < clipSize && reserveAmmo>0){

            int amountNeeded = clipSize - currentAmmo;
            if(amountNeeded >= reserveAmmo){
                currentAmmo += reserveAmmo;
                reserveAmmo -= amountNeeded;
            }

            else{

                currentAmmo = clipSize;
                reserveAmmo-= amountNeeded;

            }

        }
    }
        private void CooldownFinished()
    {
        canshoot = true;
    }

}
   





using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using TMPro;







public class GunController : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Gun Settings")]
    public float fireRate = 0.1f;
    public int clipSize = 30;
    public int AmmoCapacity = 270;
    public float reloadTime;


   

    //variables that change throughoyut code

    public bool canshoot;
    public int currentAmmo;
    public int reserveAmmo;

    Transform cam;
    public Transform attackPoint;
    public LayerMask whatIsEnemy;
    public ParticleSystem muzzleFlash, bullet;
    public GameObject bulletHoleGraphic;  
    
    //public float camShakeMagnitude, camShakeDuration;
    public TextMeshProUGUI AmmoCount;

    float range = 50f;
    int damage = 10;

    public void Start(){
        currentAmmo = clipSize;
        reserveAmmo = AmmoCapacity;
        canshoot = true;

        cam = Camera.main.transform;

 
        //AmmoCount.SetText(clipSize.ToString());
        //text = GetComponent<Canvas>().ammo.text;


    }
       private void Update()
    {
        // Cant get this fucking working
        /*
        string clipSize_string = clipSize.ToString();

        string currentAmmo_string = currentAmmo.ToString();

        string fullDisplay = currentAmmo_string +" / "+ clipSize_string;
        
        AmmoCount.SetText(fullDisplay);
        */

        //SetText
        //Debug.Log(currentAmmo);
        //Debug.Log(clipSize);
        //AmmoCount.text = currentAmmo.ToString() + " / " + clipSize.ToString();
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
                    EN.TakeDamage(damage);
                }
            }
        
        
        Instantiate(bulletHoleGraphic, hit.point, Quaternion.Euler(0, 180, 0));
        //Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

        //bulletsLeft--;
        //bulletsShot--;
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
   





using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;




public class GunController : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Gun Settings")]
    public float fireRate = 0.1f;
    public int clipSize = 30;
    public int AmmoCapacity = 270;

   

    //variables that change throughoyut code

    public bool canshoot;
    public int currentAmmo;
    public int reserveAmmo;

    Transform cam;

    float range = 50f;
    float damage = 10f;

    public void Start(){
        currentAmmo = clipSize;
        reserveAmmo = AmmoCapacity;
        canshoot = true;

        cam = Camera.main.transform;

    }
    //InputAction.CallbackContext obj
    public void Shoot(){

        if(canshoot && currentAmmo>0){
            
            canshoot = false;
            currentAmmo = currentAmmo-1;
            //Debug.Log(currentAmmo);
            //StartCoroutine(ShootGun());
            Ray ray = new Ray(cam.transform.position,cam.transform.forward);
    
            if(Physics.Raycast(cam.position,cam.forward,out RaycastHit hit, range)){

            
            }
        }
        else{
            Debug.Log("Hey");
            Invoke(nameof(CooldownFinished),fireRate);

        }

            

    }

    public void Reload(){
        if(currentAmmo< reserveAmmo && reserveAmmo>0){

        }
    }
    /*IEnumerator ShootGun(){
        yield return new WaitForSeconds(fireRate);
        canshoot = true;
    }
    */
        private void CooldownFinished()
    {
        canshoot = true;
    }

}
   





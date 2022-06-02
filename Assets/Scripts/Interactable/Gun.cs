using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Gun",menuName ="Player/Gun")]
public class Gun : Item
{
        public GameObject prefab;
        public string name_compare;
        public int Magazine;
        public int Capacity;
        public float range;
        public int damage;
        public string information;
        public float playerspeed; 
        public enum GunType {Shotgun, Automatic, SemiAction, SingleShot};
        public enum Priority{Primary, Secondary};

        public int price;

        public GunType gun_type;
        public Priority priority;

        public bool bought_before;
        
        private bool bought ;
        
        public AudioClip audio;

        public float fireRate;



    private void OnEnable(){
        bought = bought_before;
    }
    // Start is called before the first frame update
    
}





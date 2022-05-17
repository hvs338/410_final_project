using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Gun",menuName ="Player/Gun")]
public class Gun : Item
{
        public GameObject prefab;
        public int Magazine;
        public int Capacity;
        public float range; 
        public enum GunType {Shotgun, Automatic, SemiAction, SingleShot};
        public enum Priority{Primary, Secondary};

        public GunType gun_type;
        public Priority priority;



    // Start is called before the first frame update
    
}





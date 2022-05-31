using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class PowerUp : MonoBehaviour


{


    public virtual void Power(){


    }

    public virtual string Usage(){


        return string.Empty;
    }

    // Start is called before the first frame update

}

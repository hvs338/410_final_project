using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHealth : PowerUp
{
    private GunController player;
    public AudioSource Audio;
    // Start is called before the first frame update
    void Start()


    {
        player = GameObject.Find("Player").GetComponent<GunController>();
        Audio = GetComponent<AudioSource>();
        
    }

    public override void Power(){

        if (player.currentHealth < 100){

        int to_add = player.player_health - player.currentHealth;
        player.currentHealth += to_add;
        Audio.Play();


        }
    }

    public override string Usage(){


        string return_message = "Regenerates To Maximum Health, Press Q to buy, Price: $750";
        return return_message;
    }
}

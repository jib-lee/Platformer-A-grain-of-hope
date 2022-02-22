using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible_sake : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("DRUNK!!!");

            //do some drunk stuff
            
            Destroy(this.gameObject);
            other.gameObject.SendMessage("HitSake");
            
        }
    }
}

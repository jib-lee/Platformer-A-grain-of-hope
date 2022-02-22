using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible_mochi : MonoBehaviour
{

    public AudioSource pickUpSound;
   
    void Start()
    {
        
    }

  
    void Update()
    {
        
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            pickUpSound.Play();
            //Debug.Log("collect me!");
            this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            //Destroy(this.gameObject);
        }
    }
}

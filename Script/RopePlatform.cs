using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopePlatform : MonoBehaviour
{
    public GameObject platform;
    public bool ropeBroken;

    private Animator anim;

    void Start()
    {
        ropeBroken = false;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        

        if (ropeBroken)
        {
            platform.GetComponent<Rigidbody2D>().isKinematic = false;
            platform.GetComponent<Rigidbody2D>().mass = 4.5f;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            anim.Play("ropeBreak");
            anim.SetBool("ropeBreak", true);
            ropeBroken = true;
           
            //
            Destroy(other.gameObject);
            //falling animation?
            //destroy 2d sprite ---> each shoot 

            Invoke("makeDisappear", 1f);
            
        }
    }

    void makeDisappear()
    {
        //Destroy(this.gameObject);
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

}

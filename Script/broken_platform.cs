using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class broken_platform : MonoBehaviour
{
    private float delay;

    void Start()
    {
        delay = .5f;
    }

    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Invoke("Fall", delay);
        }
    }

    void Fall()
    {
        GetComponent<Rigidbody2D>().isKinematic = false;
    }
}

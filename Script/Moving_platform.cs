using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_platform : MonoBehaviour
{
    public float start_pos;
    public float end_pos;

    void Start()
    {
     
    }

   
    void Update()
    {
        //transform.position = new Vector3(Mathf.PingPong(Time.time, 3), transform.position.y, transform.position.z);

        
       Vector3 start = new Vector3(start_pos, transform.position.y, 0f);
       Vector3 end = new Vector3(end_pos, transform.position.y, 0f);

        Vector3 moving = Vector3.Lerp(start, end, Mathf.PingPong(Time.time/1.3f, 1f));
        transform.position = moving;
    
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.SetParent(transform);
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.SetParent(null);
        }
    }
    /*
        if (other.transform.tag == "Player")
        {
            other.transform.parent = null;
        }
        */

}

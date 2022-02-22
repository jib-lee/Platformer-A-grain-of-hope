using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownPlatform : MonoBehaviour
{

    public float start_pos;
    public float end_pos;

    void Start()
    {
        
    }

    void Update()
    {
     
        Vector3 start = new Vector3(transform.position.x, start_pos, 0f);
        Vector3 end = new Vector3(transform.position.x, end_pos, 0f);

        Vector3 moving = Vector3.Lerp(start, end, Mathf.PingPong(Time.time / 1.8f, 1f));
        transform.position = moving;

    }

    //make player stay on platform
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

}

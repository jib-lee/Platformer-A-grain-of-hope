using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class twigScript : MonoBehaviour
{
    public LayerMask playerLayer;

    private float delay;

    public string sceneName;

    public AudioSource playerDie;

    public AudioSource crackSound;
   
    void Start()
    {
        delay = 0.18f;
    }

    void Update()
    {
        bool playerIsClose = Physics2D.OverlapCircle(transform.position, 12f, playerLayer);

        if (playerIsClose)
        {
            //play crack sound is delayed?
            crackSound.Play();
            Invoke("twigFall", delay);
        }

    }

    void twigFall()
    {

        GetComponent<Rigidbody2D>().isKinematic = false;
    }

    //kill player
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerDie.Play();
            SceneManager.LoadScene(sceneName);
        }
    }
}

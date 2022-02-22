using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyOneHit : MonoBehaviour
{
    public LayerMask playerLayer;
    
    public GameObject player;
    int direction;
    public float speed;

    public Transform target;
    public Transform[] moveSpots;
    int targetSpot;
    float distToSpot;

    float waitTime;
    float startWait;

    private SpriteRenderer sr;

    public string sceneName;

    bool wait;

    public GameObject sake;

    public AudioSource quack;
    public AudioSource dieSound;
    public AudioSource playerDieSound;

    private Animator anim;

    void Start()
    {
        speed = 2f;
        waitTime = startWait;
        startWait = 0.5f;
        targetSpot = 0;
        distToSpot = 0.02f;
        sr = GetComponent<SpriteRenderer>();

        anim = GetComponent<Animator>();

    }

    
    void Update()
    {

        bool playerIsClose = Physics2D.OverlapCircle(transform.position, 9f, playerLayer);

        if (playerIsClose && !wait)
        {
            //follow

            Vector3 localPosition = target.position - transform.position;
            localPosition = localPosition.normalized;
            transform.Translate(localPosition.x * Time.deltaTime * speed, 0, localPosition.z * Time.deltaTime * speed);

            //flip
            //make sprite flip code!
            if (player.transform.position.x - transform.position.x > 0)
            {
                direction = 1;
            } else if (player.transform.position.x - transform.position.x < 0)
            {
                direction = -1;
            }
            
            if (direction == 1)
            {
               sr.flipX = true;

            } else if (direction == -1)
            {
                sr.flipX = false;
            }

            if (!threwSake)
            {
                StartCoroutine(throwSake());
            }

        }
        else
        {
            quack.PlayDelayed(0.8f);
            patrol();
        }
    }

    bool threwSake = false;

    IEnumerator throwSake()
    {

        //throw sake
        //play throw sound
        Vector2 spawnPosition = new Vector2(transform.position.x + direction, transform.position.y + 3.5f);
        GameObject sakePrefab = Instantiate(sake, spawnPosition, Quaternion.identity) as GameObject;
        sakePrefab.GetComponent<Rigidbody2D>().AddForce(transform.right * 450 * direction);
      
        // throwing anim?
        
        threwSake = true;
        yield return new WaitForSeconds(7f);
        threwSake = false;
    }

    void waitSomeTime()
    {
        wait = false;
    }

    void die()
    {
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
          //  enemyAnim.SetBool("isDead", true);
            Destroy(other.gameObject);
            //dying animation
            anim.SetBool("isDead", true);
            dieSound.Play();

            //turn off collider first so it doesnt hurt player
            this.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;

            Invoke("die", 0.5f);
        } 
      

        if (other.CompareTag("Edge"))
        {
            wait = true;
            Invoke("waitSomeTime", 2f);
        }

        print(other.tag);

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Player")
        {
            playerDieSound.Play();
            SceneManager.LoadScene(sceneName);

        }
    }

    void patrol()
    {
       
        //patrol 
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[targetSpot].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, moveSpots[targetSpot].position) < distToSpot)
        {

            if (waitTime <= 0)
            {
                targetSpot++;

                if (targetSpot > moveSpots.Length - 1)
                {
                    targetSpot = 0;
                }
                waitTime = startWait;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }

            //flip??
            if (targetSpot == 0)
            {
                sr.flipX = false;
            }
            else if (targetSpot == 1)
            {
                sr.flipX = true;
            }
        }
    }
}

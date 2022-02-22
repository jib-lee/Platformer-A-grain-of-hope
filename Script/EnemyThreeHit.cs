using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyThreeHit : MonoBehaviour
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

    public int counter;
    public string sceneName;

    bool wait;

    public AudioSource grunt;
    public AudioSource playerDieSound;
    public AudioSource hitSound;

    public GameObject sake;
    private Animator anim;
    void Start()
    {
        speed = 2f; //moves faster when player is close
        waitTime = startWait;
        startWait = 0.2f;
        targetSpot = 0;
        distToSpot = 0.02f;
        sr = GetComponent<SpriteRenderer>();
        counter = 3;

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        bool playerIsClose = Physics2D.OverlapCircle(transform.position, 13f, playerLayer);

        if (playerIsClose && !wait)
        {
            //follow

            Vector3 localPosition = target.position - transform.position;
            localPosition = localPosition.normalized;
            transform.Translate(localPosition.x * Time.deltaTime * speed, 0, localPosition.z * Time.deltaTime * speed);
            speed = 3f;

            //flip
            //make sprite flip code!
            if (player.transform.position.x - transform.position.x > 0)
            {
                direction = 1;
            }
            else if (player.transform.position.x - transform.position.x < 0)
            {
                direction = -1;
            }

            if (direction == 1)
            {
                sr.flipX = true;

            }
            else if (direction == -1)
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
            grunt.PlayDelayed(0.8f);
            patrol();
        }

        if (counter <= 0)
        {
            anim.SetBool("isDeadThree", true);
            //turn off collider first so it doesnt hurt player
            this.gameObject.GetComponent<CircleCollider2D>().enabled = false;

            Invoke("die", 2.1f);
            //enemy die sound
        }

    }

    void die()
    {
        Destroy(this.gameObject);
    }
    

    bool threwSake = false;

    IEnumerator throwSake()
    {

        //throw sake
        //play throw sound?
        Vector2 spawnPosition = new Vector2(transform.position.x + direction, transform.position.y + 3f);
        GameObject sakePrefab = Instantiate(sake, spawnPosition, Quaternion.identity) as GameObject;
        sakePrefab.GetComponent<Rigidbody2D>().AddForce(transform.right * 550 * direction);

        // throwing anim?

        threwSake = true;
        yield return new WaitForSeconds(5.5f);
        threwSake = false;
    }

    void waitSomeTime()
    {
        wait = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            
            hitSound.Play();

            Destroy(other.gameObject);
           
            counter -= 1;
            //anim.SetBool("isDeadThree", true);
        }

        if (other.CompareTag("Edge"))
        {
            wait = true;
            Invoke("waitSomeTime", 2f);
        }
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

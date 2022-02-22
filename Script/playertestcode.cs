using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playertestcode : MonoBehaviour
{

    public int speed = 10;
    public int jumpForce = 600;
    private Rigidbody2D rb;

    public LayerMask groundLayer;
    public Transform feet;
    public bool canJump;
    int jumpCount;

    private SpriteRenderer sr;
    int direction = 1;
    public GameObject bullet;

    public Animator animator;

    public AudioSource shootSound;
    public AudioSource jumpSound;
    public AudioSource SakeSound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
     
    }

    void Update()
    {
        canJump = Physics2D.OverlapCircle(feet.position, .1f, groundLayer);
        animator.SetBool("isJumping", !canJump);
        //if feet on ground then can Jump
        if (canJump)
        {
            jumpCount = 2;

        }

        //jump
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            jumpSound.Play();
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, jumpForce));   //addforce = adding a value, so 0 is ok!
            jumpCount--;
          
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void FixedUpdate()
    {
        //move

        float xSpeed = Input.GetAxis("Horizontal") * speed;
        rb.velocity = new Vector2(xSpeed, rb.velocity.y);  //velocity = setting a value
        animator.SetFloat("Speed",Mathf.Abs( xSpeed));


        //flip
        if (xSpeed < 0)
        {
            sr.flipX = true;

            direction = -1;
        }
        else if (xSpeed > 0)
        {
            sr.flipX = false;

            direction = 1;
        }

        //shoot
        if (Input.GetButtonDown("Fire1"))
        {
            shootSound.Play();
            Vector2 spawnPosition = new Vector2(transform.position.x + direction, transform.position.y + 0.5f);
            GameObject bulletPrefab = Instantiate(bullet, spawnPosition, Quaternion.identity) as GameObject;
            bulletPrefab.GetComponent<Rigidbody2D>().AddForce(transform.right * 600 * direction);
            animator.SetTrigger("isShooting");
        }
    }
    void HitSake()
    {
        StartCoroutine(SakeWasHit());
      
    }
    IEnumerator SakeWasHit()
    {
        SakeSound.Play();
        speed -= 8;
        yield return new WaitForSeconds(4f);
        speed = 10;
      
        
    }
    void HitEma()
    {
        StartCoroutine(EmaWasHit());
    }
    IEnumerator EmaWasHit()
    {
        //emasound
        speed +=8;
        yield return new WaitForSeconds(4f);
        speed = 10;
    }
    public void onLanding()
    {
        animator.SetBool("IsJumping", false);
    }
}

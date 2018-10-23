using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public Text countText;
    public Text winText;
    private AudioSource source;
    public AudioClip jumpClip;
    public AudioClip coinClip;
    public AudioClip coinPickUpClip;
    public AudioClip winClip;
    public AudioClip NewEnemyDeathClip;
    public AudioClip deathClip;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;

    private Rigidbody2D rb2d;
    private int count;
    private bool facingRight = true;
    public float speed;
    public float jumpForce;

    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;

    // Use this for initialization
    void Start () {

        winText.text = "";
        rb2d = GetComponent<Rigidbody2D>();
        count = 0;
        SetCountText();
	}

    void Awake()
    { source = GetComponent<AudioSource>(); }

    // Update is called once per frame
    void Update () {

        if (Input.GetKey("escape"))
            Application.Quit();



    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag ("CoinPickUp"))
        {
            source.PlayOneShot(coinPickUpClip);
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }

        if (other.gameObject.CompareTag("Mushroom"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }

        if (other.gameObject.CompareTag("Flagpole"))
            winText.text = "You Win!";

    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >=12)
        {
            winText.text = "You Win!";
            source.PlayOneShot(winClip);
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(moveHorizontal, 0);

        rb2d.AddForce(movement * speed);

        rb2d.velocity = new Vector2(moveHorizontal * speed, rb2d.velocity.y);

        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);

        Debug.Log(isOnGround);

        if (facingRight == false && moveHorizontal > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveHorizontal < 0)
        {
            Flip();
        }


}

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.collider.tag == "Ground") {

            if (Input.GetKey(KeyCode.UpArrow)) {

                rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                rb2d.velocity = Vector2.up * jumpForce;
                float vol = Random.Range(volLowRange, volHighRange);
                source.PlayOneShot(jumpClip);
            }

        }

        if (collision.collider.tag == "Blocks")
        {

            if (Input.GetKey(KeyCode.UpArrow))
            {
                source.PlayOneShot(jumpClip);
                rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                rb2d.velocity = Vector2.up * jumpForce;
            }

        }

        if (collision.collider.tag == "Pipes")
        {

            if (Input.GetKey(KeyCode.UpArrow))
            {
                source.PlayOneShot(jumpClip);
                rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                rb2d.velocity = Vector2.up * jumpForce;
            }

        }

        if (collision.collider.tag == ("Flagpole"))
        {
            source.PlayOneShot(jumpClip);
            winText.text = "You Win! & You reached the end of the level!";
            source.PlayOneShot(winClip);
        }

        if (collision.collider.tag == ("CoinBox"))
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                source.PlayOneShot(jumpClip);
                source.PlayOneShot(coinClip);
                count = count + 1;
                SetCountText();
            }
        }

        if (collision.collider.tag == ("MushroomBox"))
        {
 
        }

        if (collision.collider.tag == "NewEnemy")
        {
            source.PlayOneShot(NewEnemyDeathClip);
        }

        if (collision.collider.tag == ("Death"))
        {
            source.PlayOneShot(deathClip);
            count = count - 1000;
            SetCountText();
        }
    }
}

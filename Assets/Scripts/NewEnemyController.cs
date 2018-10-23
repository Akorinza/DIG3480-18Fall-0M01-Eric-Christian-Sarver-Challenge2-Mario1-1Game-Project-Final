using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemyController : MonoBehaviour {

    private Animator Anim;

    public float speed;

    public LayerMask isGround;

    public Transform wallHitBox;
    public Transform playerHitBox;

    private bool wallHit;
    private bool playerHit;

    public float wallHitHeight;
    public float wallHitWidth;
    public float playerHitHeight;
    public float playerHitWidth;

    private AudioSource source;
    public AudioClip deathClip;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;

    private GameObject playerDeath;

    // Use this for initialization
    void Start () {
        playerDeath = GameObject.FindWithTag("Player");
    }

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.Translate(speed * Time.deltaTime, 0, 0);

        wallHit = Physics2D.OverlapBox(wallHitBox.position, new Vector2(wallHitWidth, wallHitHeight), 0, isGround);
        if (wallHit == true)
        {
            speed = speed * -1;
        }

        playerHit = Physics2D.OverlapBox(playerHitBox.position, new Vector2(playerHitWidth, playerHitHeight), 0, isGround);
        if (playerHit == true)
        {
            speed = speed * -1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            float vol = Random.Range(volLowRange, volHighRange);
            source.PlayOneShot(deathClip);
            Debug.Log("I loved living");
            Destroy(gameObject, 0.25f);
        }

        if (collision.collider.tag == "Player" && playerHit == true)
        {
            Debug.Log("I loved living");
            Destroy(playerDeath);
        }

        if (collision.collider.tag == "Mushroom")
        {

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(wallHitBox.position, new Vector3(wallHitWidth, wallHitHeight, 1));

        Gizmos.color = Color.blue;
        Gizmos.DrawCube(playerHitBox.position, new Vector3(playerHitWidth, playerHitHeight, 1));
    }
}

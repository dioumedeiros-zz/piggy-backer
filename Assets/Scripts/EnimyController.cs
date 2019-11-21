using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnimyController : MonoBehaviour
{

    private PersonagemController Player;

    public Rigidbody2D body;

    private Animator animator;

    public float speed;

    private int score;
    public int forceAttack;

    private AudioSource audioSource;

    public AudioClip deadScream;
    public AudioClip punchSound;

    public float jumpForce = 300.1f;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        Player = GameObject.Find("hero").GetComponent<PersonagemController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("IN"+Player.transform.position.x);
        if (transform.position.x > Player.transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        animator.SetBool("walking", true);
        // transform.position = new Vector3(x, transform.position.y, transform.position.z);
        transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("TESTE - " + other.gameObject.name);
        float calcDist = Player.transform.position.y - transform.position.y;

        if (other.gameObject.name == "hero" && calcDist > 1)
        {
            animator.SetBool("walking", false);
            AudioSource.PlayClipAtPoint(deadScream, transform.position);
            score = PlayerPrefs.GetInt("deads");
            score = score + 1;
            PlayerPrefs.SetInt("deads", score);
            Player.heroJump();
            Destroy(gameObject);
        }
        else if (other.gameObject.name == "hero")
        {
            audioSource.PlayOneShot(punchSound);
            animator.SetBool("atack", true);
            animator.SetBool("walking", false);
            int life = PlayerPrefs.GetInt("heroLife");
            life -= forceAttack;
            if (life < 0)
            {
                life = 0;
                PlayerPrefs.SetInt("gameOver", 1);
            }
            PlayerPrefs.SetInt("heroLife", life);
            transform.position = Vector2.MoveTowards(transform.position, (transform.position += Vector3.back * Time.deltaTime), speed * Time.deltaTime);
        }
        else if (other.gameObject.name == "box" || other.gameObject.name == "bau")
        {
            Vector2 force = new Vector2(0f, jumpForce);
            body.AddForce(force);
        }
        PlayerPrefs.Save();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "hero")
        {
            animator.SetBool("atack", false);
        }
    }

    private void OnBecameInvisible()
    {


    }
}

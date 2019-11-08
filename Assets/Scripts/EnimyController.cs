using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnimyController : MonoBehaviour
{

    private GameObject Player;

    public Rigidbody2D body;

    private Animator animator;

    public float speed;

    private int score;

    private AudioSource audioSource;

    public AudioClip deadScream;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        Player = GameObject.Find("hero");
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
        animator.SetBool("walking", false);
        if (other.gameObject.name == "hero")
        {
            animator.SetBool("walking", false);
            AudioSource.PlayClipAtPoint(deadScream, transform.position);
            score = PlayerPrefs.GetInt("deads");
            score = score + 1;
            PlayerPrefs.SetInt("deads", score);
            PlayerPrefs.Save();

            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {


    }
}

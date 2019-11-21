using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestController : MonoBehaviour
{

    private Animator animator;
    private int score, limitOrda, tesouros;
    private AudioSource audioSource;

    public AudioClip openSound;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("open", false);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OTHER:" + other.gameObject.name);
        if (other.gameObject.name == "hero")
        {
            Debug.Log("OPEN");
            // AudioSource.PlayClipAtPoint(deadScream, transform.position);
            score = PlayerPrefs.GetInt("deads");
            limitOrda = PlayerPrefs.GetInt("limitOrda");
            if (score >= limitOrda)
            {
                audioSource.PlayOneShot(openSound);
                animator.SetBool("open", true);
                tesouros = PlayerPrefs.GetInt("tesouros");
                tesouros++;
                PlayerPrefs.SetInt("tesouros", tesouros);
                PlayerPrefs.Save();
            }
        }

    }
}

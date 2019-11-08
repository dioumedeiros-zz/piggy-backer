using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestController : MonoBehaviour
{

    private Animator animator;
    private int score;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("open", false);
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
            animator.SetBool("open", true);
            Debug.Log("OPEN");
            // AudioSource.PlayClipAtPoint(deadScream, transform.position);
            score = PlayerPrefs.GetInt("deads");
            score = 0;
            PlayerPrefs.SetInt("deads", score);
            PlayerPrefs.Save();
        }

    }
}

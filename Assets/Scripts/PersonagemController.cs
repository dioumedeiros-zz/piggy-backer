using UnityEngine;
using UnityEngine.UI;

public class PersonagemController : MonoBehaviour
{

    public Rigidbody2D body;

    public float jumpForce = 500.1f;

    public float speed = 7;

    public bool onFloor;

    public float scaleX;

    private int directionCharacter = 1;

    private Animator animator;

    public Transform refChao;

    public LayerMask layerChao;

    private Vector3 camSpeed = Vector3.zero;

    private float timeCam = 0.5f;

    private AudioSource audioSource;

    public AudioClip jumpSound;

    public Text scoreText;
    public Text treasureText;
    public Text lifeText;

    private int gameOver = 0;
    private int score = 0;
    private int limitOrda = 12;
    private int tesouros = 0;
    private int heroLife = 25;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start");
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        scaleX = transform.localScale.x;
        PlayerPrefs.SetInt("gameOver", 0);
        PlayerPrefs.SetInt("deads", 0);
        PlayerPrefs.SetInt("nivelAtual", 1);
        PlayerPrefs.SetInt("limitOrda", limitOrda);
        PlayerPrefs.SetInt("tesouros", 0);
        PlayerPrefs.SetInt("heroLife", 25);
        this.updateScores();
    }


    void updateScores()
    {
        score = PlayerPrefs.GetInt("deads");
        limitOrda = PlayerPrefs.GetInt("limitOrda");
        tesouros = PlayerPrefs.GetInt("tesouros");
        heroLife = PlayerPrefs.GetInt("heroLife");
        treasureText.text = "Tesouros: " + tesouros.ToString();
        scoreText.text = "Inimigos Pisados: " + score.ToString() + "/" + limitOrda.ToString();
        lifeText.text = heroLife.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        gameOver = PlayerPrefs.GetInt("gameOver");
        this.updateScores();
        float axisRaw = Input.GetAxisRaw("Horizontal");
        // if (Input.GetButtonDown("Fire1"))
        // {
        //     audioSource.PlayOneShot(shootSound);

        //     GameObject objNovo = Instantiate(objetoTiro);
        //     objNovo.GetComponent<controleTiro>().direction = directionCharacter;
        //     objNovo.transform.position = refTiro.position;

        //     score++;
        //     scoreText.text = score.ToString();
        //     PlayerPrefs.SetInt("score", score);
        //     PlayerPrefs.Save();
        // }
        if (gameOver == 1)
        {
            audioSource.Stop();
            animator.SetBool("running", false);
            animator.SetBool("jumping", false);
            animator.SetBool("busted", true);
        }
        else
        {
            if (Input.GetButtonDown("Jump") && onFloor)
            {
                this.heroJump();
            }


            if (Input.GetButton("Horizontal"))
            {
                if (axisRaw != 0)
                {
                    directionCharacter = (int)axisRaw;
                    transform.localScale = new Vector3(scaleX * axisRaw, 1, 1);
                    // Running Animation     
                    if (onFloor)
                    {
                        animator.SetBool("running", true);
                    }
                }

                float x = transform.position.x + (speed * Input.GetAxis("Horizontal") * Time.deltaTime);
                transform.position = new Vector3(x, transform.position.y, transform.position.z);
            }
            else
            {
                // not Running Animation
                animator.SetBool("running", false);
            }
        }

        Vector3 newPositionCam = Vector3.SmoothDamp(Camera.main.transform.position, transform.position, ref camSpeed, timeCam);

        newPositionCam.x = (float)(newPositionCam.x + 0.1);
        newPositionCam.y = Camera.main.transform.position.y;
        newPositionCam.z = Camera.main.transform.position.z;
        Camera.main.transform.position = newPositionCam;

    }

    public void heroJump()
    {
        animator.SetBool("jumping", true);
        audioSource.PlayOneShot(jumpSound);
        Vector2 force = new Vector2(0f, jumpForce);
        body.AddForce(force);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OTHER:" + other.gameObject.name);
        if (other.gameObject.name == "floor" || other.gameObject.name == "box"
        || other.gameObject.name == "bau")
        {
            animator.SetBool("jumping", false);
            onFloor = true;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("OTHER:" + other.gameObject.name);
        if (other.gameObject.name == "floor")
        {
            onFloor = false;
        }
    }
}

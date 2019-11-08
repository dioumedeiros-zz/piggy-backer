using UnityEngine;
using UnityEngine.UI;

public class personagemControle : MonoBehaviour
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

    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start");
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        scaleX = transform.localScale.x;
        PlayerPrefs.SetInt("deads", 0);
        score = PlayerPrefs.GetInt("deads");
        scoreText.text = "Inimigos Pisados:"+score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        float axisRaw = Input.GetAxisRaw("Horizontal");
        score = PlayerPrefs.GetInt("deads");
        scoreText.text = "Inimigos Pisados:"+score.ToString();
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
        if (Input.GetButtonDown("Jump") && onFloor)
        {
            animator.SetBool("jumping", true);
            audioSource.PlayOneShot(jumpSound);
            Vector2 force = new Vector2(0f, jumpForce);
            body.AddForce(force);
            
        }


        if (Input.GetButton("Horizontal"))
        {

            // if (Input.GetAxisRaw("Horizontal") > 0) {
            //     //GetComponent<SpriteRenderer>().flipX = false;
            //     transform.localScale = new Vector3(2, 2, 2);
            // } else if (Input.GetAxisRaw("Horizontal") < 0) {
            //     //GetComponent<SpriteRenderer>().flipX = true;
            //     transform.localScale = new Vector3(-2, 2, 2);
            // }

            if (axisRaw != 0)
            {
                directionCharacter = (int)axisRaw;
                transform.localScale = new Vector3(scaleX * axisRaw, 1, 1);

                // Running Animation     
                if(onFloor){
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

        //onFloor = Physics2D.Linecast(transform.position, refChao.position, layerChao);
        onFloor = true;
        Vector3 newPositionCam = Vector3.SmoothDamp(Camera.main.transform.position, transform.position, ref camSpeed, timeCam);
        
        newPositionCam.x = (float) (newPositionCam.x + 0.1);
        newPositionCam.y = Camera.main.transform.position.y;
        newPositionCam.z = Camera.main.transform.position.z;
        Camera.main.transform.position = newPositionCam;

    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("OTHER:"+other.gameObject.name);
        if(other.gameObject.name == "floor"){
            animator.SetBool("jumping", false);
            onFloor = true;    
        }
        
    }

    private void OnTriggerExit2D(Collider2D other) {
        onFloor = false;
    }
}

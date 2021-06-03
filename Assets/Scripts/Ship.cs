using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    private Camera _camera;
    private Vector3 mousePosition;
    public float speed = 100;
    public Rigidbody2D rb2D;
    [SerializeField] private float thrust = 1f;
    //[SerializeField] private GameObject ball;
    [SerializeField] private GameObject fireball;
    private GameObject ship;
    private GameController gameController;
    private bool shipNonTarget;
    private Animator animator;
    [SerializeField] private Animator fireAnimator;

    private float t = 0.0f;
    private float ship_timer = 0.0f;
    private Collider2D m_Collider;
    private AudioController audioController;


    private void Awake()
    {
        gameController = GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<GameController>();
        audioController = GameObject.FindGameObjectsWithTag("AudioController")[0].GetComponent<AudioController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        _camera = FindObjectOfType<Camera>();
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        m_Collider = gameObject.GetComponent<Collider2D>();
        shipNonTarget = false;
        SetNonTarget();
    }

    // Update is called once per frame
    void Update()
    {
       

        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioController.PlaySoundFromSounds("ship_shot");
            GameObject ball = Instantiate(fireball) as GameObject;
            ball.transform.position = transform.TransformPoint(Vector3.up * 2.5f);
            ball.transform.rotation = transform.rotation;
        }

        if (shipNonTarget)
        {
            if (ship_timer > 0)
            {
                ship_timer -= Time.deltaTime;
                animator.SetBool("NonTarget", true);
                m_Collider.enabled = false;
            }
            else
            {
                shipNonTarget = false;
                animator.SetBool("NonTarget", false);
                m_Collider.enabled = true;
            }
        }

    }


    void FixedUpdate()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        Vector3 tempVect = new Vector3(0, h, 0);


        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            transform.rotation = transform.rotation * Quaternion.Euler(transform.rotation.x, transform.rotation.y, -h * speed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            fireAnimator.SetBool("Left", true);
        }
        else
        {
            fireAnimator.SetBool("Left", false);
        }

        if (Input.GetKey(KeyCode.D))
        {
            fireAnimator.SetBool("Right", true);
        }
        else
        {
            fireAnimator.SetBool("Right", false);
        }

        if (Input.GetKey(KeyCode.W))
        {
            rb2D.AddForce(transform.up * thrust);
            fireAnimator.SetBool("FireOn", true);
            audioController.PlayShipSound(true);
        }
        else if (rb2D.velocity.magnitude > 0.01)
        {
            rb2D.velocity = rb2D.velocity * 0.99f;
            fireAnimator.SetBool("FireOn", false);
            audioController.PlayShipSound(false);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Asteroid" || other.tag == "Alien" || other.tag == "AlienBullet")
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (gameController != null)
        {
            gameController.MinusLife(1);

        }
        if(audioController!= null)
        {
            audioController.PlaySoundFromSounds("explosion_ship");
            audioController.PlayShipSound(false);
        }
        for (int i = 0; i < 3; i++)
        {
            GameObject part = Instantiate(Resources.Load<GameObject>("Prefabs/ship_part_" + i), gameObject.transform.position, gameObject.transform.rotation);
            part.transform.localScale *= Vector2.one * Random.Range(1.5f, 2.5f);
        }
    }

    public void SetNonTarget()
    {
        shipNonTarget = true;
        ship_timer = 1.5f;

    }
}


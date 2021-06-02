using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    public Rigidbody2D rb2D;
    [SerializeField] private float thrust = 1f;
    [SerializeField] private float h;
    private Vector2 velocity;
    List<Vector2> Movment = new List<Vector2> { new Vector2(1f, 1f), new Vector2(1f, -1f), new Vector2(-1f, 1f), new Vector2(-1f, -1f), new Vector2(-1f, 0), new Vector2(1f, 0) };
    [SerializeField] private float timer_1;
    [SerializeField] private float little_timer;
    [SerializeField] private float fire_timer;
    private GameController gameController;
    private AudioController audio;
    private GameObject ship;
    private Vector2 firstMove;
    private float first_timer;


    private void Awake()
    {
        gameController = GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<GameController>();
        audio = GameObject.FindGameObjectsWithTag("AudioController")[0].GetComponent<AudioController>();

    }

    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        //velocity = new Vector2(0, 1.1f);
        velocity = Movment[0];
        //Debug.LogError(Movment[0]);

        timer_1 = 2f;
        little_timer = 1;
        fire_timer = 1f;
        first_timer = 5f;

        if (gameObject.transform.position.x > 0)
        {
            firstMove = new Vector2(-1f,0);
        }
        else
        {
            firstMove = new Vector2(1f, 0);

        }
    }

    // Update is called once per frame
    /*void Update()
    {
        gameObject.GetComponent<Rigidbody2D>().MovePosition(gameObject.GetComponent<Rigidbody2D>().position + new Vector2(gameObject.transform.up.x, gameObject.transform.up.y) * Time.fixedDeltaTime * thrust);

    }*/

    private void Update()
    {
        if (gameController.GetGameCon() == GameController.GameCondition.Restart)
        {
            Destroy(gameObject);
        }

        if (fire_timer > 0)
        {


            fire_timer -= Time.deltaTime;
        }
        else
        {
            Fire();
            fire_timer = 1f;

        }

    }

    void FixedUpdate()
    {
        if (first_timer > 0)
        {

            rb2D.MovePosition(rb2D.position + firstMove * Time.fixedDeltaTime);
            first_timer -= Time.deltaTime;
        }
        else
        {
            if (little_timer > 0)
            {
                rb2D.MovePosition(rb2D.position + velocity * Time.fixedDeltaTime);
                little_timer -= Time.deltaTime;

            }
            else
            {
                velocity = Movment[Random.Range(0, Movment.Count)];
                little_timer = 1f;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            Destroy(other.gameObject);

            Destroy(gameObject);
            gameController.AddScore(5);

        }
        else if (other.tag == "Player")
        {
            Destroy(other.gameObject);

            Destroy(gameObject);
        }
        else if (other.tag == "Asteroid")
        {
            Destroy(other.gameObject);

            Destroy(gameObject);
        }

    }

    public void Fire()
    {
        audio.PlaySoundFromSounds("alien_shot");
        GameObject ball = Instantiate(Resources.Load<GameObject>("Prefabs/Alien_Fire")) as GameObject;
        ball.transform.position =
            transform.TransformPoint(ball.transform.position);
        ball.GetComponent<AlienFire>().SetTargetPosition(gameController.GetShipPosition());
        //Debug.LogError(gameController.GetComponent<GameController>().GetShipPosition());
    }

    private void OnDestroy()
    {
        audio.PlaySoundFromSounds("explosion_alien");

        for (int i = 0; i < 4; i++)
        {

            GameObject part = Instantiate(Resources.Load<GameObject>("Prefabs/alien_part_" + i), gameObject.transform.position, gameObject.transform.rotation);
            part.transform.localScale *= Vector2.one * Random.Range(1.5f, 2.5f);
        }
    }
}

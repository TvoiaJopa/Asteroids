using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private float angel;
    private float angel2;
    public float thrust = 2f;
    private enum Asteroids { Big, Midle, Little }
    private enum Trigger { Bullet, Others }
    [SerializeField] private Asteroids asteroid;
    [SerializeField] private GameController gameController;
    private AudioController audioController;
    private SpriteRenderer spritRenderer;


    private void Awake()
    {
        gameController = GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<GameController>();
        audioController = GameObject.FindGameObjectsWithTag("AudioController")[0].GetComponent<AudioController>();
        spritRenderer = gameObject.GetComponent<SpriteRenderer>();

        gameController.AddAsteroid();
    }

    void Start()
    {
        spritRenderer.sprite = Resources.Load<Sprite>("Images/asteroid_" + Random.Range(1,4));


        gameObject.transform.localScale *= Vector2.one * Random.Range(0.8f, 1.3f);

        if (asteroid == Asteroids.Big)
        {
            angel = Random.Range(-1.5f, 1.5f);
            angel2 = Random.Range(-1.5f, 1.5f);

        }
        else if (asteroid == Asteroids.Midle)
        {
            angel = Random.Range(-2.2f, 2.2f);
            angel2 = Random.Range(-2.2f, 2.2f);
        }
        else if (asteroid == Asteroids.Little)
        {
            angel = Random.Range(-2.5f, 2.5f);
            angel2 = Random.Range(-2.5f, 2.5f);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.GetGameCon() == GameController.GameCondition.Restart)
        {
            Destroy(gameObject);
        }
    }
    private void FixedUpdate()
    {
        gameObject.GetComponent<Rigidbody2D>().MovePosition(gameObject.GetComponent<Rigidbody2D>().position + new Vector2(angel, angel2) * Time.fixedDeltaTime);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet" || other.tag == "AlienBullet" || other.tag == "Alien" || other.tag == "Player")
        {
            Destroy(other.gameObject);
            Vector3 spawnLoc = gameObject.transform.position;

            if (asteroid == Asteroids.Big)
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/Asteroid_1"), spawnLoc, gameObject.transform.rotation);
                Instantiate(Resources.Load<GameObject>("Prefabs/Asteroid_1"), spawnLoc, gameObject.transform.rotation);

            }
            else if(asteroid == Asteroids.Midle)
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/Asteroid_2"), spawnLoc, gameObject.transform.rotation);
                Instantiate(Resources.Load<GameObject>("Prefabs/Asteroid_2"), spawnLoc, gameObject.transform.rotation);
            }
            else if (asteroid == Asteroids.Little)
            {

            }

            AsteroidEnter(other.tag);
        }

    }

    private void AsteroidEnter(string trigger)
    {

        if (gameController != null)
        {
            if (trigger == "Bullet")
            {
                gameController.AddScore(1);

            }
            gameController.DeleteAsteroid();


        }



        Vector3 spawnLoc = gameObject.transform.position;

        for (int i = 0; i < Random.Range(3, 7); i++)
        {
            if (asteroid == Asteroids.Big)
            {
                GameObject part = Instantiate(Resources.Load<GameObject>("Prefabs/AsteroidsPart"), gameObject.transform.position, gameObject.transform.rotation);
                part.transform.localScale *= Vector2.one * Random.Range(1.5f, 2.5f);
            }
            else if (asteroid == Asteroids.Midle)
            {
                GameObject part = Instantiate(Resources.Load<GameObject>("Prefabs/AsteroidsPart"), gameObject.transform.position, gameObject.transform.rotation);
                part.transform.localScale *= Vector2.one * Random.Range(1.2f, 1.7f);
            }
            else if (asteroid == Asteroids.Little)
            {
                GameObject part = Instantiate(Resources.Load<GameObject>("Prefabs/AsteroidsPart"), gameObject.transform.position, gameObject.transform.rotation);
                part.transform.localScale *= Vector2.one * Random.Range(0.8f, 1.2f);
            }

        }

        Destroy(gameObject);

    }



    private void OnDestroy()
    {
        if (audioController != null)
        {
            audioController.PlaySoundFromSounds("explosion_asteroid");

        }
    }
}

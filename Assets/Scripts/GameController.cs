using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private int life;
    [SerializeField] private int score;
    [SerializeField] private int asteroids;
    public GameCondition gameCon;
    private GameObject ship;
    BoxCollider2D boxCol;
    private Vector2 cubeSize;
    private Vector2 cubeCenter;
    private float asteroidRecoilTimer = 0f;
    private float recoilShipSpawn = 0f;


    public List<GameObject> colAliens;



    private float alienSpawnTimer;
    private GameObject alien;
    private AudioController audioController;




    public enum GameCondition { Game, Pause, MainMenu, Options, Restart }

    private void Awake()
    {
        audioController = GameObject.FindGameObjectsWithTag("AudioController")[0].GetComponent<AudioController>();
        boxCol = gameObject.GetComponent<BoxCollider2D>();
        Transform cubeTrans = boxCol.GetComponent<Transform>();

        //Get helpers for asteroid's spawn position
        cubeCenter = cubeTrans.position;
        // Multiply by scale because it does affect the size of the collider
        cubeSize.x = cubeTrans.localScale.x * boxCol.size.x;
        cubeSize.y = cubeTrans.localScale.y * boxCol.size.y;
    }

    void Start()
    {
        gameCon = GameCondition.MainMenu;
        life = 3;
        score = 0;
        alienSpawnTimer = 5.0f;
    }

    void Update()
    {
        if (gameCon == GameCondition.Game)
        {
            if (asteroids == 0)
            {
                if (asteroidRecoilTimer > 0)
                {
                    asteroidRecoilTimer -= Time.deltaTime;
                }
                else
                {
                    for (int i = 0; i < 4; i++)
                    {
                        AsteroidSpawn();
                        asteroidRecoilTimer = 2.0f;
                    }
                }
            }
            

            if (ship == null)
            {
                if (recoilShipSpawn > 0)
                {
                    recoilShipSpawn -= Time.deltaTime;
                }
                else
                {
                    SpawnShip();
                    recoilShipSpawn = 2f;
                }
            }


            if (alien == null)
            {
                if (alienSpawnTimer > 0)
                {
                    alienSpawnTimer -= Time.deltaTime;
                }
                else
                {
                    float randomNumber = UnityEngine.Random.Range(-1, 1);
                    if (randomNumber >= 0)
                    {
                        AlienSpawn();
                    }
                    alienSpawnTimer = 5.0f;
                }
            }
        }
    }

    public void GameOver()
    {
        gameCon = GameCondition.Restart;
    }

    public void GameRestart()
    {
        for (int i = 0; i < 4; i++)
        {
            AsteroidSpawn();
            asteroidRecoilTimer = 2.0f;
        }
        life = 3;
        score = 0;
    }

    private void AlienSpawn()
    {
        audioController.PlaySoundFromSounds("alert_alarm");
        var obj = Instantiate(Resources.Load<GameObject>("Prefabs/Alien"), GetRandomPositionAlien(), transform.rotation);
        obj.transform.localScale *= Vector2.one * UnityEngine.Random.Range(0.6f, 0.8f);
        alien = obj;
    }

    private void AsteroidSpawn()
    {
        var obj = Instantiate(Resources.Load<GameObject>("Prefabs/Asteroid_0"), GetRandomPosition(), transform.rotation);
    }

    public void SpawnShip()
    {
        var obj = Instantiate(Resources.Load<GameObject>("Prefabs/Ship"), new Vector3(0, 0, 0), transform.rotation);
        obj.GetComponent<Ship>().SetNonTarget();
        ship = obj;
    }

    public void AddAsteroid()
    {
        asteroids++;
    }

    public void DeleteAsteroid()
    {
        asteroids--;

    }

    public void AddScore(int sc)
    {
        score += sc;

    }

    public void MinusLife(int li)
    {
        life -= li;
        if(life <= 0)
        {
            GameOver();
        }

    }

    public int GetLife()
    {
        return life;
    }

    public int GetScore()
    {
        return score;
    }

    private Vector2 GetRandomPosition()
    {
        Vector2 randomPosition;
        randomPosition = new Vector2(UnityEngine.Random.Range(-cubeSize.x / 2, cubeSize.x / 2), UnityEngine.Random.Range(-cubeSize.y / 2, cubeSize.y / 2));
        return cubeCenter + randomPosition;
    }

    private Vector2 GetRandomPositionAlien()
    {
        GameObject zone = colAliens[UnityEngine.Random.Range(0, colAliens.Count)];
        BoxCollider2D boxCol = zone.GetComponent<BoxCollider2D>();
        Transform cubeTrans = boxCol.GetComponent<Transform>();
        Vector2 cubeCenter = cubeTrans.position;
        Vector2 cubeSize;
        // Multiply by scale because it does affect the size of the collider
        cubeSize.x = cubeTrans.localScale.x * boxCol.size.x;
        cubeSize.y = cubeTrans.localScale.y * boxCol.size.y;

        Vector2 randomPosition;
        randomPosition = new Vector2(UnityEngine.Random.Range(-cubeSize.x / 2, cubeSize.x / 2), UnityEngine.Random.Range(-cubeSize.y / 2, cubeSize.y / 2));
        return cubeCenter + randomPosition;
    }



    public Vector2 GetShipPosition()
    {
        Vector2 shipPos = new Vector2(0.0f, 0.0f);

        if (ship!= null)
        {
            shipPos = new Vector2(ship.transform.position.x, ship.transform.position.y);
        }
        
        return shipPos;
    }

    public GameCondition GetGameCon()
    {
        return gameCon;
    }

    public void SetGameCon(GameCondition game)
    {
        gameCon = game;
    }

}

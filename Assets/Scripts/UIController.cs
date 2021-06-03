using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text lifeText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text mainScore;
    [SerializeField] private GameController gameController;

    [SerializeField] private GameObject Game;
    [SerializeField] private GameObject Restart;
    [SerializeField] private GameObject Pause;
    [SerializeField] private GameObject MainMenu;

    // Start is called before the first frame update
    private void Awake()
    {
        gameController = GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<GameController>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifeText.text = "life:" + gameController.GetLife();
        scoreText.text = "score:" + gameController.GetScore();
        mainScore.text = "score:" + gameController.GetScore();

        //Set condition canvases
        if(gameController.GetGameCon() == GameController.GameCondition.Game)
        {
            Game.SetActive(true);
            Restart.SetActive(false);
            Pause.SetActive(false);
            MainMenu.SetActive(false);
            Time.timeScale = 1f;
        }
        else if (gameController.GetGameCon() == GameController.GameCondition.Restart)
        {
            Game.SetActive(false);
            Restart.SetActive(true);
            Pause.SetActive(false);
            MainMenu.SetActive(false);
            Time.timeScale = 1f;
        }
        else if (gameController.GetGameCon() == GameController.GameCondition.Pause)
        {
            Game.SetActive(false);
            Restart.SetActive(false);
            Pause.SetActive(true);
            MainMenu.SetActive(false);
            Time.timeScale = 0.0f;

        }
        else if (gameController.GetGameCon() == GameController.GameCondition.MainMenu)
        {
            Game.SetActive(false);
            Restart.SetActive(false);
            Pause.SetActive(false);
            MainMenu.SetActive(true);
            Time.timeScale = 1f;
        }
    }

    public void SetPause()
    {
        gameController.SetGameCon(GameController.GameCondition.Pause);
    }


    public void SetPauseOff()
    {
        gameController.SetGameCon(GameController.GameCondition.Game);
    }

    public void SetGame()
    {
        gameController.SetGameCon(GameController.GameCondition.Game);
    }

    public void SetRestart()
    {
        SceneManager.LoadScene(0);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(0);
    }
}

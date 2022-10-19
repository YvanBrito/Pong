using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject menuUI;
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject leftWinUI;
    [SerializeField] private GameObject rightWinUI;
    [SerializeField] private GameObject optionsUI;
    [SerializeField] private GameObject pausedUI;
    [SerializeField] private GameObject chooseSecondPlayerUI;
    [SerializeField] private GameObject gameObjects;

    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new UIManager();
            }

            return instance;
        }
    }
    void Awake()
    {
        instance = this;
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state)
    {
        if (GameManager.Instance.State == GameState.EndGame)
        {
            if (ScoreManager.Instance.IsFirstPlayerWinner()) ShowLeftWin();
            else ShowRightWin();
        }
    }

    public void ChooseSecondPlayer()
    {
        menuUI.SetActive(false);
        chooseSecondPlayerUI.SetActive(true);
    }

    public void StartGame(string secondPlayerMode)
    {
        GameManager.Instance.SecondPlayer = secondPlayerMode switch
        {
            "AI" => SecondPlayerMode.AI,
            "Manual" => SecondPlayerMode.Manual,
            _ => GameManager.Instance.SecondPlayer,
        };
        GameManager.Instance.ResetGame();
        GameManager.Instance.UpdateGameState(GameState.PlayState);
        inGameUI.SetActive(true);
        gameObjects.SetActive(true);
        menuUI.SetActive(false);
        pausedUI.SetActive(false);
        leftWinUI.SetActive(false);
        rightWinUI.SetActive(false);
        chooseSecondPlayerUI.SetActive(false);
    }

    void ShowLeftWin()
    {
        leftWinUI.SetActive(true);
    }

    void ShowRightWin()
    {
        rightWinUI.SetActive(true);
    }

    public void ToggleOptionsMenu(bool b)
    {
        menuUI.SetActive(!b);
        optionsUI.SetActive(b);
    }

    public void SetMaxScore(int newMaxScore)
    {
        ScoreManager.Instance.MaxScore = newMaxScore;
    }

    public void GoToMenu()
    {
        GameManager.Instance.EndGame();
        GameManager.Instance.UpdateGameState(GameState.MenuState);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseToggle();
        }
    }

    public void PauseToggle()
    {
        if (GameManager.Instance.State == GameState.PlayState)
        {
            GameManager.Instance.UpdateGameState(GameState.PauseState);
            pausedUI.SetActive(true);
        }
        else if(GameManager.Instance.State == GameState.PauseState)
        {
            GameManager.Instance.UpdateGameState(GameState.PlayState);
            pausedUI.SetActive(false);
        }
    }

    public void Reset()
    {
        menuUI.SetActive(true);
        pausedUI.SetActive(false);
        optionsUI.SetActive(false);
        inGameUI.SetActive(false);
        gameObjects.SetActive(false);
        rightWinUI.SetActive(false);
        leftWinUI.SetActive(false);
    }

    public void HowToStartRound(int initMethod)
    {
        if (initMethod == 0)
            GameManager.Instance.InitMethod = InitMethod.WhoScored;
        else if(initMethod == 1)
            GameManager.Instance.InitMethod = InitMethod.Alternate;
        else
            GameManager.Instance.InitMethod = InitMethod.WhoLost;
    }

    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }
}

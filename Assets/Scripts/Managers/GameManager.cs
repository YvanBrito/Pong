using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Ball ballPrefab;
    private Ball inGameBall;
    [SerializeField] private GameObject _gameObjects;
    public int timeToStart;
    [SerializeField] private Player[] players;
    private int _lastVictory = 1;
    public int LastVictory
    {
        get { return _lastVictory; }
        set { _lastVictory = value; }
    }
    private InitMethod _initMethod;
    public InitMethod InitMethod 
    { 
        get { return _initMethod; } 
        set { _initMethod = value; }
    }
    private GameState _state;
    public GameState State { get { return _state; } }
    public static event Action<GameState> OnGameStateChanged;

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManager();
            }

            return instance;
        }
    }

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateGameState(GameState.MenuState);
        _initMethod = InitMethod.Alternate;
    }

    // Update is called once per frame
    public void UpdateGameState(GameState newState)
    {
        _state = newState;
        switch (newState)
        {
            case GameState.MenuState:
                ResetGame();
                break;
            case GameState.PlayState:
                InitGame();
                Time.timeScale = 1;
                break;
            case GameState.PauseState:
                Time.timeScale = 0;
                break;
            case GameState.EndRound:
                if (ScoreManager.Instance.isThereSomeWinner())
                {
                    UpdateGameState(GameState.EndGame);
                }
                else
                {
                    inGameBall.Restart();
                    UpdateGameState(GameState.PlayState);
                }
                break;
            case GameState.EndGame:
                Destroy(inGameBall.gameObject);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }

    public void ResetGame()
    {
        ScoreManager.Instance.Reset();
        UIManager.Instance.Reset();
        for (int i = 0; i < players.Length; i++)
        {
            players[i].transform.position = new Vector2(players[i].transform.position.x, 0);
        }
    }

    public void InitGame()
    {
        if (inGameBall == null)
            inGameBall = Instantiate(ballPrefab, _gameObjects.transform);

        switch(_initMethod)
        {
            case InitMethod.WhoScored:
                inGameBall.SideToInit = _lastVictory;
                break;
            case InitMethod.WhoLost:
                inGameBall.SideToInit = _lastVictory * -1;
                break;
            case InitMethod.Alternate:
                inGameBall.SideToInit *= -1;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(_initMethod), _initMethod, null);
        }
    }
}

public enum InitMethod
{
    WhoScored,
    WhoLost,
    Alternate
}

public enum GameState
{
    MenuState,
    PlayState,
    PauseState,
    EndRound,
    EndGame
}

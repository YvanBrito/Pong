using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text firstPlayerText;
    [SerializeField] private TMP_Text secondPlayerText;
    [SerializeField] private int _maxScore;
    public int MaxScore
    {
        get { return _maxScore; }
        set
        {
            if (value > 1) _maxScore = value;
            else _maxScore = 1;
        }
    }
    private int firstPlayerScore;
    public int FirstPlayerScore
    {
        get
        { return firstPlayerScore; }
    }
    private int secondPlayerScore;

    public int SecondPlayerScore
    {
        get
        { return secondPlayerScore; }
    }
    private static ScoreManager instance;
    public static ScoreManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ScoreManager();
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
        // Debug.Log("ScoreManager: " + state);
    }

    void Start()
    {
        firstPlayerScore = 0;
        secondPlayerScore = 0;
        _maxScore = 3;
        firstPlayerText.text = firstPlayerScore.ToString();
        secondPlayerText.text = secondPlayerScore.ToString();
    }

    public bool IsFirstPlayerWinner()
    {
        return firstPlayerScore > secondPlayerScore;
    }

    public void AddFirstPlayerPoint()
    {
        ++firstPlayerScore;
        firstPlayerText.text = firstPlayerScore.ToString();
        GameManager.Instance.LastVictory = -1;
        GameManager.Instance.UpdateGameState(GameState.EndRound);
    }

    public void AddSecondPlayerPoint()
    {
        ++secondPlayerScore;
        secondPlayerText.text = secondPlayerScore.ToString();
        GameManager.Instance.LastVictory = 1;
        GameManager.Instance.UpdateGameState(GameState.EndRound);
    }

    public bool isThereSomeWinner()
    {
        return FirstPlayerScore == MaxScore ||
                SecondPlayerScore == MaxScore;
    }

    public void Reset()
    {
        firstPlayerScore = 0;
        secondPlayerScore = 0;
        firstPlayerText.text = firstPlayerScore.ToString();
        secondPlayerText.text = secondPlayerScore.ToString();
    }

    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }
}

using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    [HideInInspector]
    public bool gameStartedFromMainMenu, gameRestartedAfterPlayerDied;

    [HideInInspector]
    public int score, coinScore, lifeScore;

    void Awake()
    {
        MakeSingleton();

    }

    void Start()
    {
        InitializeVariables();
    }

    void OnLevelWasLoaded()
    {
        if(Application.loadedLevelName == "Gameplay")
        {
            if(gameRestartedAfterPlayerDied)
            {
                GamePlayController.instance.SetScore(score);
                GamePlayController.instance.SetCoinScore(coinScore);
                GamePlayController.instance.SetLifeScore(lifeScore);

                PlayerScore.scoreCount = score;
                PlayerScore.coinScore = coinScore;
                PlayerScore.lifeScore = lifeScore;
                
            }
            else if(gameStartedFromMainMenu)
            {
                PlayerScore.scoreCount = 0;
                PlayerScore.coinScore = 0;
                PlayerScore.lifeScore = 2;

                GamePlayController.instance.SetScore(0);
                GamePlayController.instance.SetCoinScore(0);
                GamePlayController.instance.SetLifeScore(2);
            }
        }
    }

    public void CheckGameStatus(int score, int coinScore, int lifeScore)
    {
        if(lifeScore < 0)
        {
            if (GamePreferences.GetEasyDifficultyState() == 0)
            {
                int highScore = GamePreferences.GetEasyDifficultyHighScore();
                int coin = GamePreferences.GetEasyDifficultyCoinScore();

                if (highScore < score)
                {
                    GamePreferences.SetEasyDifficultyHighScore(score);
                }
                if (coin < coinScore)
                {
                    GamePreferences.SetEasyDifficultyCoinScore(coinScore);
                }
            }

            if (GamePreferences.GetMediumDifficultyState() == 0)
            {
                int highScore = GamePreferences.GetMediumDifficultyHighScore();
                int coin = GamePreferences.GetMediumDifficultyCoinScore();

                if (highScore < score)
                {
                    GamePreferences.SetMediumDifficultyHighScore(score);
                }
                if (coin < coinScore)
                {
                    GamePreferences.SetMediumDifficultyCoinScore(coinScore);
                }
            }

            if (GamePreferences.GetHardDifficultyState() == 0)
            {
                int highScore = GamePreferences.GetHardDifficultyHighScore();
                int coin = GamePreferences.GetHardDifficultyCoinScore();

                if (highScore < score)
                {
                    GamePreferences.SetHardDifficultyHighScore(score);
                }
                if (coin < coinScore)
                {
                    GamePreferences.SetHardDifficultyCoinScore(coinScore);
                }
            }

            gameStartedFromMainMenu = false;
            gameRestartedAfterPlayerDied = false;

            AdsController.instance.ShowInterstitial();

            GamePlayController.instance.GameOverShowPanel( score, coinScore);
        } else
        {
            this.score = score;
            this.coinScore = coinScore;
            this.lifeScore = lifeScore;

            gameStartedFromMainMenu = false;
            gameRestartedAfterPlayerDied = true;

            GamePlayController.instance.PlayerDiedRestartTheGame();
        }
    }

    void InitializeVariables()
    {
        if(!PlayerPrefs.HasKey("Game Initialized") ) {

            GamePreferences.SetEasyDifficultyState(0);
            GamePreferences.SetEasyDifficultyHighScore(0);
            GamePreferences.SetEasyDifficultyCoinScore(0);

            GamePreferences.SetMediumDifficultyState(1);
            GamePreferences.SetMediumDifficultyHighScore(0);
            GamePreferences.SetMediumDifficultyCoinScore(0);

            GamePreferences.SetHardDifficultyState(0);
            GamePreferences.SetHardDifficultyHighScore(0);
            GamePreferences.SetHardDifficultyCoinScore(0);

            GamePreferences.SetMusicState(0);

            PlayerPrefs.SetInt("Game Initialized", 1);
        }
    }

	void MakeSingleton()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }
}

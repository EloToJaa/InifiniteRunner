using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Text scoreText;
    public Text coinScoreText;
    public Text highScoreText;
    public float worldScrollingSpeed = 0.2f;
    public bool inGame;
    public GameObject resetButton;
    public Immortality immortality;
    public Magnet magnet;

    private float score;
    private float highScore;
    private int coins;

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }

        InitializeGame();
    }

    public void MagnetCollected()
    {
        if(magnet.isActive)
        {
            CancelInvoke("CancelMagnet");
            CancelMagnet();
        }
        magnet.isActive = true;
        Invoke("CancelMagnet", magnet.GetDuration());
    }

    private void CancelMagnet()
    {
        magnet.isActive = false;
    }

    public void ImmortalityCollected()
    {
        if(immortality.isActive)
        {
            CancelInvoke("CancelImmortality");
            CancelImmortality();
        }
        immortality.isActive = true;
        worldScrollingSpeed += immortality.GetSpeedBoost();
        Invoke("CancelImmortality", immortality.GetDuration());
    }

    private void CancelImmortality()
    {
        worldScrollingSpeed -= immortality.GetSpeedBoost();
        immortality.isActive = false;
    }

    public void CoinCollected(int value = 1)
    {
        SoundManager.instance.PlayCoinGrab();
        coins += value;
        PlayerPrefs.SetInt("Coins", coins);
    }

    public void InitializeGame()
    {
        inGame = true;

        if(PlayerPrefs.HasKey("Coins"))
        {
            coins = PlayerPrefs.GetInt("Coins");
        }
        else
        {
            coins = 0;
            PlayerPrefs.SetInt("Coins", 0);
        }

        if (PlayerPrefs.HasKey("Highscore"))
        {
            highScore = PlayerPrefs.GetFloat("Highscore");
        }
        else
        {
            highScore = 0;
            PlayerPrefs.SetFloat("Highscore", 0);
        }

        immortality.isActive = false;
        magnet.isActive = false;
    }

    public void GameOver()
    {
        inGame = false;

        resetButton.SetActive(true);
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.inGame) return;

        score += worldScrollingSpeed;

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat("Highscore", highScore);
        }
    }

    private void Update()
    {
        UpdateOnScreenScore();
    }

    private void UpdateOnScreenScore()
    {
        scoreText.text = score.ToString("0");
        highScoreText.text = highScore.ToString("0");
        coinScoreText.text = coins.ToString();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }
}

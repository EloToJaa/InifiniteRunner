using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Text coinsValue;
    public Text soundBtnText;
    public Text highScoreText;

    public Text magnetLevelText;
    public Text magnetButtonText;

    public Text immortalityLevelText;
    public Text immortalityButtonText;

    public Powerup magnet;
    public Powerup immortality;

    public GameObject mainMenuPanel;
    public GameObject upgradeStorePanel;

    int coins = 0;
    float highScore = 0;

    public void OpenUpgradeStore()
    {
        mainMenuPanel.SetActive(false);
        upgradeStorePanel.SetActive(true);
    }

    public void CloseUpgradeStore()
    {
        mainMenuPanel.SetActive(true);
        upgradeStorePanel.SetActive(false);
    }

    private void Start()
    {
        Screen.SetResolution(1600, 1000, false);

        if(PlayerPrefs.HasKey("Coins"))
        {
            coins = PlayerPrefs.GetInt("Coins");
        }
        if(PlayerPrefs.HasKey("Highscore"))
        {
            highScore = PlayerPrefs.GetFloat("Highscore");
        }

        mainMenuPanel.SetActive(true);
        upgradeStorePanel.SetActive(false);

        UpdateUI();
    }

    public void UpdateUI()
    {
        coinsValue.text = coins.ToString();
        highScoreText.text = highScore.ToString("0");

        if(SoundManager.instance.GetMuted())
        {
            soundBtnText.text = "TURN ON SOUND";
        }
        else
        {
            soundBtnText.text = "TURN OFF SOUND";
        }

        immortalityLevelText.text = immortality.ToString();
        immortalityButtonText.text = immortality.UpgradeCostString();

        magnetLevelText.text = magnet.ToString();
        magnetButtonText.text = magnet.UpgradeCostString();
    }

    public void UpgradePowerup(Powerup powerup)
    {
        if(coins >= powerup.GetNextUpgradeCost() && !powerup.IsMaxedOut())
        {
            ReduceCoinsAmount(powerup.GetNextUpgradeCost());
            powerup.Upgrade();
            UpdateUI();
        }
    }

    public void UpgradeMagnetButton()
    {
        UpgradePowerup(magnet);
    }

    public void UpgradeImmortalityButton()
    {
        UpgradePowerup(immortality);
    }

    public void ReduceCoinsAmount(int amount)
    {
        coins -= amount;
        PlayerPrefs.SetInt("Coins", coins);
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void SoundButton()
    {
        SoundManager.instance.ToggleMuted();
        UpdateUI();
    }
}

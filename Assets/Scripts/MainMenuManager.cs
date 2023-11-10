using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Text levelSelectText;
    public Text playerCountText;

    private int currentLevelIndex;
    public int maxNumberofLevels = 3;
    public int minNumberofLevels = 1;
    public Button levelSelectLeftButton;
    public Button levelSelectRightButton;

    public static int currentPlayerCount;
    public int maxNumberofPlayers = 2;
    public int minNumberofPlayers = 1;
    public Button playersSelectLeftButton;
    public Button playersSelectRightButton;

    private void Start()
    {
        Time.timeScale = 1;
        currentLevelIndex = 1;
        currentPlayerCount = 1;
        UpdateLevelDisplay();
        UpdatePlayersDisplay();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    public void IncreaseScreenIndex()
    {
        currentLevelIndex++;
        UpdateLevelDisplay();
    }

    public void DecreaseScreenIndex()
    {
        currentLevelIndex--;
        UpdateLevelDisplay();
    }

    private void UpdateLevelDisplay()
    {
        UpdateLevelSelectText();
        UpdateLevelSelectButtons();
    }

    private void UpdateLevelSelectText()
    {
        levelSelectText.text = $"Level {currentLevelIndex}";
    }

    private void UpdateLevelSelectButtons()
    {
        if (currentLevelIndex >= maxNumberofLevels)
        {
            levelSelectRightButton.gameObject.SetActive(false);
            levelSelectLeftButton.gameObject.SetActive(true);
        }
        else if (currentLevelIndex <= minNumberofLevels)
        {
            levelSelectLeftButton.gameObject.SetActive(false);
            levelSelectRightButton.gameObject.SetActive(true);
        }
        else
        {
            levelSelectLeftButton.gameObject.SetActive(true);
            levelSelectRightButton.gameObject.SetActive(true);
        }
    }

    public void IncreasePlayerCount()
    {
        currentPlayerCount++;
        UpdatePlayersDisplay();
    }

    public void DecreasePlayerCount()
    {
        currentPlayerCount--;
        UpdatePlayersDisplay();
    }

    private void UpdatePlayersDisplay()
    {
        UpdatePlayerSelectText();
        UpdatePlayerSelectButtons();
    }

    private void UpdatePlayerSelectText()
    {
        playerCountText.text = $"{currentPlayerCount}";
    }

    private void UpdatePlayerSelectButtons()
    {
        if (currentPlayerCount >= maxNumberofPlayers)
        {
            playersSelectRightButton.gameObject.SetActive(false);
            playersSelectLeftButton.gameObject.SetActive(true);
        }
        else if (currentPlayerCount <= minNumberofPlayers)
        {
            playersSelectLeftButton.gameObject.SetActive(false);
            playersSelectRightButton.gameObject.SetActive(true);
        }
        else
        {
            playersSelectLeftButton.gameObject.SetActive(true);
            playersSelectRightButton.gameObject.SetActive(true);
        }
    }

    public void LoadSelectedScene()
    {
        SceneManager.LoadScene(currentLevelIndex);
    }

}

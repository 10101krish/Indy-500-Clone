using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Text modeSelectText;
    public Button modeSelectLeftButton;
    public Button modeSelectRightButton;
    public string[] modeNames = { "Max Laps", "Crash Obstacles" };
    public int[] modeScenes = { 3, 3 };
    private int currentModeIndex;

    public Text levelSelectText;
    public Button levelSelectLeftButton;
    public Button levelSelectRightButton;
    private int currentLevelPrefix = 0;
    private int currentLevelIndex;

    public Text playerCountText;
    public Button playersSelectLeftButton;
    public Button playersSelectRightButton;
    public static int currentPlayerCount = 1;
    public int maxNumberofPlayers = 2;
    public int minNumberofPlayers = 1;

    private void Start()
    {
        currentModeIndex = 1;
        currentLevelIndex = 1;
        currentPlayerCount = 1;
        currentLevelPrefix = 0;
        UpdateModeDisplay();
        UpdateLevelDisplay();
        UpdatePlayersDisplay();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    public void IncreaseModeIndex()
    {
        currentLevelPrefix += modeScenes[currentModeIndex - 1];
        currentModeIndex++;
        UpdateModeDisplay();
        ResetLevelDisplay();
    }

    public void DecreaseModeIndex()
    {
        currentLevelPrefix -= modeScenes[currentModeIndex - 2];
        currentModeIndex--;
        UpdateModeDisplay();
        ResetLevelDisplay();
    }

    private void ResetLevelDisplay()
    {
        currentLevelIndex = 1;
        UpdateLevelDisplay();
    }

    private void UpdateModeDisplay()
    {
        UpdateModeSelectText();
        UpdateModeSelectButtons();
    }

    private void UpdateModeSelectText()
    {
        modeSelectText.text = $"{modeNames[currentModeIndex - 1]}";
    }

    private void UpdateModeSelectButtons()
    {
        if (currentModeIndex >= modeScenes.Length)
        {
            modeSelectLeftButton.gameObject.SetActive(true);
            modeSelectRightButton.gameObject.SetActive(false);
        }
        else if (currentModeIndex <= 1)
        {
            modeSelectLeftButton.gameObject.SetActive(false);
            modeSelectRightButton.gameObject.SetActive(true);
        }
        else
        {
            modeSelectLeftButton.gameObject.SetActive(true);
            modeSelectRightButton.gameObject.SetActive(true);
        }
    }

    public void IncreaseLevelIndex()
    {
        currentLevelIndex++;
        UpdateLevelDisplay();
    }

    public void DecreaseLevelIndex()
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
        levelSelectText.text = $"{currentLevelIndex}";
    }

    private void UpdateLevelSelectButtons()
    {
        if (currentLevelIndex >= modeScenes[currentModeIndex - 1])
        {
            levelSelectLeftButton.gameObject.SetActive(true);
            levelSelectRightButton.gameObject.SetActive(false);
        }
        else if (currentLevelIndex <= 1)
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
            playersSelectLeftButton.gameObject.SetActive(true);
            playersSelectRightButton.gameObject.SetActive(false);
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
        SceneManager.LoadScene(currentLevelPrefix + currentLevelIndex);
    }

}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject carPrefab;
    protected int numberOfCars;

    public Transform[] carSpawnTransforms;
    public GameObject carTransformParent;

    public Text[] carScoreTexts;
    protected int[] carScores;

    public Text timeRemainingText;
    protected float timeRemaining;
    public float levelTime = 60f;

    protected bool gamePaused;
    public Canvas gamePausedCanvas;
    public Text gameWinText;

    protected virtual void Start()
    {
        Time.timeScale = 1;
        numberOfCars = MainMenuManager.currentPlayerCount;
        carScores = new int[numberOfCars];

        gamePaused = false;
        gamePausedCanvas.gameObject.SetActive(false);
    }

    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(0);
        else if (gamePaused && Input.GetKeyDown(KeyCode.Space))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    protected void UpdateCarScoreText(int carIndex)
    {
        carScoreTexts[carIndex].text = carScores[carIndex].ToString();
    }

    protected void BeginTimer()
    {
        timeRemaining = levelTime;
        UpdateTimeRemainingText();
    }

    protected void UpdateTimeRemainingText()
    {
        timeRemainingText.text = Mathf.FloorToInt(Mathf.Clamp(timeRemaining, 0, levelTime)).ToString();
    }

    protected GameObject InstantiateCar(int carIndex)
    {
        GameObject carGameObject = Instantiate(carPrefab, carSpawnTransforms[carIndex].position, carSpawnTransforms[carIndex].rotation);
        carGameObject.transform.parent = carTransformParent.transform;
        carScoreTexts[carIndex].text = $"Player {carIndex + 1}";
        CarMovement carMovement = carGameObject.GetComponent<CarMovement>();
        carMovement.SetCarNumber(carIndex + 1);

        return carGameObject;
    }

    protected void DisableOtherCarScoreTexts(int carIndex)
    {
        for (; carIndex < carScoreTexts.Length; carIndex++)
            carScoreTexts[carIndex].gameObject.SetActive(false);
    }
}

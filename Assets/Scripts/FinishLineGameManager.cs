using UnityEngine;

public class FinishLineGameManager : GameManager
{
    private bool firstLapStarted = false;
    public AudioClip finishLineCrossingAudioClip;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        firstLapStarted = false;
        InstantiateCars();
    }

    protected override void Update()
    {
        base.Update();
        if (firstLapStarted && !gamePaused)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimeRemainingText();
            if (timeRemaining <= 0)
                PauseGame();
        }
    }

    protected void InstantiateCars()
    {
        int carIndex = 0;
        for (; carIndex < numberOfCars; carIndex++)
        {
            GameObject carGameObject = InstantiateCar(carIndex);
            FinishLineCar finishLineCar = carGameObject.AddComponent<FinishLineCar>();
            finishLineCar.SetCarNumber(carIndex + 1);
            finishLineCar.SetFinishLineGameManager(this);
            finishLineCar.SetFinishLineCrossingAudioClip(finishLineCrossingAudioClip);
        }
        DisableOtherCarScoreTexts(carIndex);
    }

    private void BeginLapCount()
    {
        int carIndex = 0;

        for (; carIndex < numberOfCars; carIndex++)
        {
            carScores[carIndex] = 0;
            UpdateCarScoreText(carIndex);
        }
    }

    public void LapFinished(int playerNumber)
    {
        int carIndex = playerNumber - 1;
        if (!firstLapStarted)
        {
            firstLapStarted = true;
            BeginTimer();
            BeginLapCount();
        }
        else
        {
            carScores[carIndex]++;
            UpdateCarScoreText(carIndex);
        }
    }

    protected void PauseGame()
    {
        PlayGameOverSound();
        gamePaused = true;
        Time.timeScale = 0;

        FinishLineCar[] cars = carTransformParent.GetComponentsInChildren<FinishLineCar>();
        for (int i = 0; i < cars.Length; i++)
            Destroy(cars[i].gameObject);

        gamePausedCanvas.gameObject.SetActive(true);
        if (numberOfCars >= 2)
        {
            if (carScores[0] == carScores[1])
                gameWinText.text = "Draw";
            else if (carScores[0] > carScores[1])
                gameWinText.text = $"Player 1 Won";
            else
                gameWinText.text = $"Player 2 Won";
        }
        else
        {
            int player1Score = carScores[0];
            gameWinText.text = $"Game Score {player1Score}";
        }
    }
}

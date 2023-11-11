using UnityEngine;
using UnityEngine.Tilemaps;

public class CrashObstacleGameManager : GameManager
{
    public GameObject obstaclePrefab;
    public Tilemap raceTrackTileMap;

    protected bool firstObstacleCrashed;
    protected bool obstacleSpawned;

    Vector3Int raceTrackTileMapSize;

    protected override void Start()
    {
        base.Start();
        firstObstacleCrashed = false;
        obstacleSpawned = false;
        InstantiateCars();
        GatherTileMapAttributes();
        SpawnObstacle();
    }

    protected override void Update()
    {
        base.Update();
        if (firstObstacleCrashed)
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
            CrashObstacleCar crashObstacleCar = carGameObject.AddComponent<CrashObstacleCar>();
            crashObstacleCar.SetCarNumber(carIndex + 1);
            crashObstacleCar.SetCrashObstacleGameManager(this);
        }
        DisableOtherCarScoreTexts(carIndex);
    }

    protected void GatherTileMapAttributes()
    {
        raceTrackTileMapSize = raceTrackTileMap.size;
    }

    protected void SpawnObstacle()
    {
        int randomX = Random.Range(-raceTrackTileMapSize.x, raceTrackTileMapSize.x);
        int randomY = Random.Range(-raceTrackTileMapSize.y, raceTrackTileMapSize.y);
        Vector3Int raceTrackTilePosition = new Vector3Int(randomX, randomY, 0);
        TileBase raceTrackTile = raceTrackTileMap.GetTile(raceTrackTilePosition);

        if (raceTrackTile != null)
        {
            obstacleSpawned = true;
            Vector3 worldPoint = raceTrackTileMap.GetCellCenterWorld(raceTrackTilePosition);
            Instantiate(obstaclePrefab, worldPoint, Quaternion.identity);
        }
        else
            SpawnObstacle();
    }

    public void ObstacleHit(int carNumber)
    {
        int carIndex = carNumber - 1;
        if (!firstObstacleCrashed)
        {
            firstObstacleCrashed = true;
            BeginTimer();
            BeginScoreCount();
        }
        carScores[carIndex]++;
        UpdateCarScoreText(carIndex);

        obstacleSpawned = false;
        SpawnObstacle();
    }

    private void BeginScoreCount()
    {
        int carIndex = 0;
        for (; carIndex < numberOfCars; carIndex++)
        {
            carScores[carIndex] = 0;
            UpdateCarScoreText(carIndex);
        }
    }

    protected void PauseGame()
    {
        gamePaused = true;
        Time.timeScale = 0;

        CrashObstacleCar[] cars = carTransformParent.GetComponentsInChildren<CrashObstacleCar>();
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

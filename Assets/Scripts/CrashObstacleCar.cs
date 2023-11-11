using UnityEngine;

public class CrashObstacleCar : MonoBehaviour
{
    private CrashObstacleGameManager crashObstacleGameManager;

    private int carNumber;

    public void SetCrashObstacleGameManager(CrashObstacleGameManager crashObstacleGameManager)
    {
        this.crashObstacleGameManager = crashObstacleGameManager;
    }

    public void SetCarNumber(int carNumber)
    {
        this.carNumber = carNumber;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Obstacle")))
        {
            Destroy(other.gameObject);
            crashObstacleGameManager.ObstacleHit(carNumber);
        }
    }
}

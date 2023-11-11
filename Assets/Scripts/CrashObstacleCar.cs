using UnityEngine;

public class CrashObstacleCar : MonoBehaviour
{
    private CrashObstacleGameManager crashObstacleGameManager;

    private AudioSource audioSource;

    private int carNumber;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SetCrashObstacleGameManager(CrashObstacleGameManager crashObstacleGameManager)
    {
        this.crashObstacleGameManager = crashObstacleGameManager;
    }

    public void SetObstacleCrashedAudioClip(AudioClip obstacleCrashedAudioClip)
    {
        audioSource.clip = obstacleCrashedAudioClip;
    }

    public void SetCarNumber(int carNumber)
    {
        this.carNumber = carNumber;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Obstacle")))
        {
            audioSource.Play();
            Destroy(other.gameObject);
            crashObstacleGameManager.ObstacleHit(carNumber);
        }
    }
}

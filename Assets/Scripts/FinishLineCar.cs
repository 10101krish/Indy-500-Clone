using UnityEngine;

public class FinishLineCar : MonoBehaviour
{
    private FinishLineGameManager finishLineGameManager;
    private float previousFinishLineEnteryDirection;
    private float newFinishLineEnteryDirection;
    private bool firstLapStarted = false;

    private AudioSource audioSource;

    private int carNumber;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        firstLapStarted = false;
        ResetCar();
    }

    public void SetFinishLineGameManager(FinishLineGameManager finishLineGameManager)
    {
        this.finishLineGameManager = finishLineGameManager;
    }

    public void SetFinishLineCrossingAudioClip(AudioClip finishLineCrossingAudioClip)
    {
        audioSource.clip = finishLineCrossingAudioClip;
    }

    public void SetCarNumber(int carNumber)
    {
        this.carNumber = carNumber;
    }

    private void ResetCar()
    {
        previousFinishLineEnteryDirection = -1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("FinishLine")))
        {
            if (CheckEnteryContactWithFinishLine(other))
            {
                if (!firstLapStarted)
                {
                    finishLineGameManager.BeginGame();
                    firstLapStarted = true;
                }
                else
                {
                    finishLineGameManager.LapFinished(carNumber);
                    audioSource.Play();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("FinishLine")))
            CheckExitContactWithFinishLine(other);
    }

    private bool CheckEnteryContactWithFinishLine(Collider2D other)
    {
        bool result;

        if (transform.position.x >= other.transform.position.x)
            newFinishLineEnteryDirection = -1;
        else
            newFinishLineEnteryDirection = 1;

        if (previousFinishLineEnteryDirection == newFinishLineEnteryDirection)
            result = true;
        else
            result = false;

        return result;
    }

    private void CheckExitContactWithFinishLine(Collider2D other)
    {
        if (transform.position.x >= other.transform.position.x)
            previousFinishLineEnteryDirection = 1;
        else
            previousFinishLineEnteryDirection = -1;
    }

}

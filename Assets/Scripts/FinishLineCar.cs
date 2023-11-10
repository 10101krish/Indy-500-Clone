using UnityEngine;

public class FinishLineCar : MonoBehaviour
{
    private FinishLineGameManager finishLineGameManager;

    private float previousFinishLineEnteryDirection;
    private float newFinishLineEnteryDirection;

    private int carNumber;

    private void Start()
    {
        ResetCar();
    }

    public void SetFinishLineGameManager(FinishLineGameManager finishLineGameManager)
    {
        this.finishLineGameManager = finishLineGameManager;
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
                finishLineGameManager.LapFinished(carNumber);
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

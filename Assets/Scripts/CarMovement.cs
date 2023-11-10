using UnityEngine;

public class CarMovement : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;

    private Vector3 startingPosition;
    private Vector3 startingRotation;

    private float verticalDirection;
    private float horizontalDirection;
    public float rotationSpeed = 45f;
    public float reverseCliplingFactor = 0.5f;

    public bool onSand = false;
    public float sandCriplingFactor = 0.25f;

    private int carNumber;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        GatherStartingValues();
        ResetCar();
    }

    private void Update()
    {
        CheckVerticalMovement();
        CheckHorizontalMovement();
    }

    private void FixedUpdate()
    {
        if (onSand)
            rigidbody2D.AddRelativeForce(sandCriplingFactor * verticalDirection * Vector3.up, ForceMode2D.Impulse);
        else
            rigidbody2D.AddRelativeForce(verticalDirection * Vector3.up, ForceMode2D.Impulse);
    }

    public void SetCarNumber(int carNumber)
    {
        this.carNumber = carNumber;
    }

    private void GatherStartingValues()
    {
        startingPosition = transform.position;
        startingRotation = transform.eulerAngles;
    }

    private void ResetCar()
    {
        onSand = false;
        transform.position = startingPosition;
        transform.eulerAngles = startingRotation;
        rigidbody2D.totalForce = Vector2.zero;
        rigidbody2D.totalTorque = 0;
        rigidbody2D.angularVelocity = 0;
    }

    private void CheckVerticalMovement()
    {
        if (carNumber == 1)
        {
            if (Input.GetKey(KeyCode.UpArrow))
                verticalDirection = 1;
            else if (Input.GetKey(KeyCode.DownArrow))
                verticalDirection = -1;
            else
                verticalDirection = 0;
        }

        if (carNumber == 2)
        {
            if (Input.GetKey(KeyCode.W))
                verticalDirection = 1;
            else if (Input.GetKey(KeyCode.S))
                verticalDirection = -1;
            else
                verticalDirection = 0;
        }
        if (verticalDirection < 0)
            verticalDirection *= reverseCliplingFactor;
    }

    private void CheckHorizontalMovement()
    {

        if (carNumber == 1)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
                horizontalDirection = -1;
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
                horizontalDirection = 1;
            else
                horizontalDirection = 0;
        }

        if (carNumber == 2)
        {
            if (Input.GetKeyDown(KeyCode.D))
                horizontalDirection = -1;
            else if (Input.GetKeyDown(KeyCode.A))
                horizontalDirection = 1;
            else
                horizontalDirection = 0;
        }

        transform.eulerAngles += horizontalDirection * rotationSpeed * Vector3.forward;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Sand")))
            onSand = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Sand")))
            onSand = false;
    }
}

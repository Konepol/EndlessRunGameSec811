using UnityEngine;

public class ScriptPlayerController : MonoBehaviour
{
    public float forwardSpeed = 8f;
    public float laneChangeSpeed = 12f;
    
    public float jumpHeight = 1.5f;
    public float gravity = -25f;
    private float verticalVelocity;

    private CharacterController controller;
    private int currentLane;
    private float targetX;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentLane = GameConstrant.Centerlane;
        targetX = GameConstrant.LaneToX(currentLane);
    }

    // Update is called once per frame
    void Update()
    {
        HandleLaneInput();
        Move();
    }

    void HandleLaneInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            currentLane = Mathf.Max(currentLane - 1, 0);
            targetX = GameConstrant.LaneToX(currentLane);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            currentLane = Mathf.Min(currentLane + 1, GameConstrant.LaneCount - 1);
            targetX = GameConstrant.LaneToX(currentLane);
        }

        targetX = GameConstrant.LaneToX(currentLane);
    }

    void Move()
    {
        Vector3 move = Vector3.zero;
        move.z = forwardSpeed;

        float newX = Mathf.MoveTowards(transform.position.x, targetX, laneChangeSpeed * Time.deltaTime);

        move.x = (newX - transform.position.x) / Time.deltaTime;

        if (controller.isGrounded)
        {
            verticalVelocity = -1f;

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
            {
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }
        else verticalVelocity += gravity * Time.deltaTime;
        move.y = verticalVelocity;

        controller.Move(move * Time.deltaTime);
    }
}

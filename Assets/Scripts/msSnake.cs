using System.Collections.Generic;
using UnityEngine;

public class msSnake : MonoBehaviour
{
    public Transform snakeHead; // Snake head transform
    public GameObject[] pathPoints; // Array of path points (GameObjects) in clockwise order
    public float speed = 1f; // Movement speed of the snake
    public Transform snakeSegmentPrefab; // Prefab for snake tail segments
    public int initialTailLength = 3; // Number of tail segments at the start

    private List<Transform> tailSegments = new List<Transform>(); // List to store tail segments
    private int currentPathIndex = 0; // Current index in the pathPoints array

    private void Start()
    {
        // Initialize the snake head's position at the first path point (top-left)
        if (pathPoints.Length > 0)
        {
            snakeHead.position = pathPoints[0].transform.position;
        }

        // Initialize tail segments
        InitializeTailSegments();
    }

    private void Update()
    {
        MoveSnake();
    }

    private void MoveSnake()
    {
        // Move the snake head towards the current path point
        Transform target = pathPoints[currentPathIndex].transform;
        snakeHead.position = Vector3.MoveTowards(snakeHead.position, target.position, speed * Time.deltaTime);

        // Rotate the snake head based on the direction
        UpdateRotation(target.position);

        // Move tail segments to follow the head
        for (int i = tailSegments.Count - 1; i > 0; i--)
        {
            tailSegments[i].position = tailSegments[i - 1].position;
        }
        if (tailSegments.Count > 0)
        {
            tailSegments[0].position = snakeHead.position;
        }

        // Check if the snake head has reached the current path point
        if (Vector3.Distance(snakeHead.position, target.position) < 0.01f)
        {
            currentPathIndex = (currentPathIndex + 1) % pathPoints.Length; // Cycle to the next point
        }
    }

    private void UpdateRotation(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - snakeHead.position).normalized;

        // Determine rotation based on movement direction
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) // Horizontal movement
        {
            if (direction.x > 0) // Moving right
            {
                snakeHead.rotation = Quaternion.Euler(0f, 0f, 90f);
            }
            else if (direction.x < 0) // Moving left
            {
                snakeHead.rotation = Quaternion.Euler(0f, 0f, 270f);
            }
        }
        else // Vertical movement
        {
            if (direction.y > 0) // Moving up
            {
                snakeHead.rotation = Quaternion.Euler(0f, 0f, 180f);
            }
            else if (direction.y < 0) // Moving down
            {
                snakeHead.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }
    }

    private void InitializeTailSegments()
    {
        for (int i = 0; i < initialTailLength; i++)
        {
            // Instantiate tail segments and position them behind the snake head
            Transform segment = Instantiate(snakeSegmentPrefab);
            segment.position = snakeHead.position - Vector3.right * (i + 1); // Adjust spacing as needed
            tailSegments.Add(segment);
        }
    }
}


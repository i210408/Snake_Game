using System.Collections.Generic;
using UnityEngine;

public class msSnake : MonoBehaviour
{
    public Transform snakeHead;
    public GameObject[] pathPoints;
    public float speed = 1f;
    public Transform snakeSegmentPrefab;
    public int initialTailLength = 3;

    private List<Transform> tailSegments = new List<Transform>();
    private int currentPathIndex = 0;

    private void Start()
    {
        if (pathPoints.Length > 0)
        {
            snakeHead.position = pathPoints[0].transform.position;
        }

        InitializeTailSegments();
    }

    private void Update()
    {
        MoveSnake();
    }

    private void MoveSnake()
    {
        Transform target = pathPoints[currentPathIndex].transform;
        snakeHead.position = Vector3.MoveTowards(snakeHead.position, target.position, speed * Time.deltaTime);

        UpdateRotation(target.position);

        for (int i = tailSegments.Count - 1; i > 0; i--)
        {
            tailSegments[i].position = tailSegments[i - 1].position;
        }
        if (tailSegments.Count > 0)
        {
            tailSegments[0].position = snakeHead.position;
        }

        if (Vector3.Distance(snakeHead.position, target.position) < 0.01f)
        {
            currentPathIndex = (currentPathIndex + 1) % pathPoints.Length;
        }
    }

    private void UpdateRotation(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - snakeHead.position).normalized;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                snakeHead.rotation = Quaternion.Euler(0f, 0f, 90f);
            }
            else if (direction.x < 0)
            {
                snakeHead.rotation = Quaternion.Euler(0f, 0f, 270f);
            }
        }
        else
        {
            if (direction.y > 0)
            {
                snakeHead.rotation = Quaternion.Euler(0f, 0f, 180f);
            }
            else if (direction.y < 0)
            {
                snakeHead.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }
    }

    private void InitializeTailSegments()
    {
        for (int i = 0; i < initialTailLength; i++)
        {
            Transform segment = Instantiate(snakeSegmentPrefab);
            segment.position = snakeHead.position - Vector3.right * (i + 1);
            tailSegments.Add(segment);
        }
    }
}

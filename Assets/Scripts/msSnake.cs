// 21i-0408
// 21i-0425

using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class msSnake : MonoBehaviour
{
    public Transform snakeHead;
    public GameObject[] pathPoints;
    public Transform snakeSegmentPrefab;
    public int initialTailLength = 3;
    public Vector2 headDirection = Vector2.right;
    private List<Transform> tailSegments = new List<Transform>();
    private int currentPathIndex = 0;

    private void Start()
    {
        tailSegments.Add(snakeHead);
        if (pathPoints.Length > 0)
        {
            snakeHead.position = pathPoints[0].transform.position;
        }
        snakeHead.rotation = Quaternion.Euler(0, 0, 90f);

        InitializeTailSegments();
    }

    private void FixedUpdate()
    {
        MoveSnake();
        UpdateRotation();
    }

    private void MoveSnake()
    {
        Transform target = pathPoints[currentPathIndex].transform;
        for (int i = tailSegments.Count - 1; i > 0; i--)
        {
            tailSegments[i].position = tailSegments[i - 1].position;
        }
        snakeHead.position = new Vector2(snakeHead.position.x + headDirection.x, snakeHead.position.y + headDirection.y);
    }

    private void UpdateRotation()
    {
        if (snakeHead.transform.position == pathPoints[0].transform.position)
        {
            headDirection = Vector2.right;
            snakeHead.rotation = Quaternion.Euler(0, 0, 90f);
            ChangeTransparency(0, 1);
        }
        else if (snakeHead.transform.position == pathPoints[1].transform.position)
        {
            headDirection = Vector2.down;
            snakeHead.rotation = Quaternion.Euler(0, 0, 0f);
            ChangeTransparency(1, 2);
        }
        else if (snakeHead.transform.position == pathPoints[2].transform.position)
        {
            headDirection = Vector2.left;
            snakeHead.rotation = Quaternion.Euler(0, 0, 270f);
            ChangeTransparency(2, 3);
        }
        else if (snakeHead.transform.position == pathPoints[3].transform.position)
        {
            headDirection = Vector2.up;
            snakeHead.rotation = Quaternion.Euler(0, 0, 180f);
            ChangeTransparency(3, 0);
        }
    }

    private void InitializeTailSegments()
    {
        for (int i = 0; i < initialTailLength; i++)
        {
            Transform segment = Instantiate(snakeSegmentPrefab);
            segment.position = tailSegments[tailSegments.Count - 1].position;
            tailSegments.Add(segment);
        }
    }

    private void ChangeTransparency(int point1, int point2)
    {
        SpriteRenderer sprite1 = pathPoints[point1].GetComponent<SpriteRenderer>();
        SpriteRenderer sprite2 = pathPoints[point2].GetComponent<SpriteRenderer>();
        Color color1 = sprite1.color;
        Color color2 = sprite2.color;
        color1.a = 0f;
        color2.a = 255f;
        sprite1.color = color1;
        sprite2.color = color2;
    }
}
